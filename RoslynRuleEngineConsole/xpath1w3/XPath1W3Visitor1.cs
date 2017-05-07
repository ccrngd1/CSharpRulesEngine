using System;
using System.Collections.Generic;
using System.Linq; 
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace RoslynRuleEngineConsole.xpath1w3
{
    internal class XPath1W3Visitor : XPath1W3BaseVisitor<string>
    {
        private static readonly List<string> PathTraverseNodes = new List<string> { nameof(XPath1W3Parser.PathExprContext), nameof(XPath1W3Parser.RelativePathExprContext), nameof(XPath1W3Parser.AxisStepContext) };
        private static readonly List<string> PredicateNodes = new List<string> { nameof(XPath1W3Parser.PredicateContext) };
        private static readonly List<string> ParenthesizedNodes = new List<string> { nameof(XPath1W3Parser.ParenthesizedExprContext) };
        private static readonly List<string> LogicNodes = new List<string> {nameof(XPath1Parser.AndExprContext), nameof(XPath1Parser.OrExprContext)};

        public override string VisitStart(XPath1W3Parser.StartContext context)
        {
            LevelInformationStack stack = new LevelInformationStack();

            WalkSyntaxTree(context, stack);
            base.VisitStart(context);

            return stack.LevelInfo[stack.LevelInfo.Keys.Min()].ConvertedCode;
        } 

        private void WalkSyntaxTree(RuleContext tree, LevelInformationStack stackInfo)
        {
            LevelInformationStack savedLevelContext = null;

            //if this branch has  1 children, don't add it to the dictionary because there is nothing to do except walk down
            //0 Children mean a terminal node and we will add this to the current depth and let the parent merge upward 
            //if there are no entries in the stack, this is the first node of the tree and we need to add it...if not we won't be able to roll everything up corectly.
            if (stackInfo.LevelInfo.Keys.Count == 0 || tree.ChildCount > 1 || tree.ChildCount == 0)
                stackInfo.LevelInfo.Add(tree.Depth(), new LevelInformation());

            if (tree.ChildCount > 0)
            {
                //predicate specifies a filtering of the current path, meaning we need to isolate the stack
                if (PredicateNodes.Contains(tree.GetType().Name))
                {
                    savedLevelContext = stackInfo;
                    stackInfo = new LevelInformationStack(tree.Depth(), new LevelInformation()) {CallingParentFullPath = savedLevelContext.RollUpPartialPath()};
                }
                //parenthesized nodes are like preciate nodes but mo' special
                //they get a new stack, but the first entry for the partialPath rolled up full path of the parent
                //this path can never be rolled up into the calling parent, but unlike a filter, we need it to know what our current scope is
                else if (ParenthesizedNodes.Contains(tree.GetType().Name))
                {
                    savedLevelContext = stackInfo;
                    stackInfo = new LevelInformationStack(tree.Depth(), new LevelInformation { CurrentPartialPath = savedLevelContext.RollUpPartialPath() });
                }

                for (int i = 0; i < tree.ChildCount; i++)
                {
                    //if this is a terminal node, it is not the same interface as a normal parse node and has to be handled seperately
                    var termTest = tree.GetChild(i) as TerminalNodeImpl;

                    if (termTest != null) HandleTerminalNode(termTest, stackInfo);
                    else WalkSyntaxTree((RuleContext)tree.GetChild(i), stackInfo);

                    int maxLevelInfo = stackInfo.LevelInfo.Max(c => c.Key);

                    int instCount = 0;

                    int depthToRollUpStack = tree.Depth();
                    
                    //if this tree depth isn't in the stack, get the next highest one
                    //a parent that is on the stack will take care of rolling up the information to the next level
                    while (!stackInfo.LevelInfo.ContainsKey(depthToRollUpStack))
                    { depthToRollUpStack--; }

                    if (maxLevelInfo == depthToRollUpStack)
                        continue;

                    //roll up relevant code and scoped path
                    //this can only be done by a parent that has registered on the stack because we have to have somewhere to store this data.
                    do
                    {
                        if (!stackInfo.LevelInfo.ContainsKey(maxLevelInfo)) continue;

                        instCount++;

                        if (instCount > 1)
                            Console.WriteLine("uh, what?!");//debug 

                        //if there is code in the child, bring it up
                        if (!string.IsNullOrWhiteSpace(stackInfo.LevelInfo[maxLevelInfo].CurrentPartialPath))
                        {
                            //if there is converted code already at this depth, add a '.' to begin combining them together
                            if (!string.IsNullOrWhiteSpace(stackInfo.LevelInfo[depthToRollUpStack].CurrentPartialPath))
                                stackInfo.LevelInfo[depthToRollUpStack].CurrentPartialPath += ".";

                            stackInfo.LevelInfo[depthToRollUpStack].CurrentPartialPath += stackInfo.LevelInfo[maxLevelInfo].CurrentPartialPath;
                        } 

                        //if there is code in the child, bring it up
                        if (!string.IsNullOrWhiteSpace(stackInfo.LevelInfo[maxLevelInfo].ConvertedCode))
                        {
                            ////debug
                            ////hopefully the '.'s are already in place correctly, if not, this is doing more harm than good and needs to be re-thought 
                            ////if there is converted code already at this depth, add a '.' to begin combining them together
                            //if (!string.IsNullOrWhiteSpace(levelInfo[depthToRollUpStack].convertedCode))
                            //    levelInfo[depthToRollUpStack].convertedCode += ".";
                             
                            stackInfo.LevelInfo[depthToRollUpStack].ConvertedCode += stackInfo.LevelInfo[maxLevelInfo].ConvertedCode;
                        }

                        stackInfo.LevelInfo.Remove(maxLevelInfo);

                    } while (--maxLevelInfo > tree.Depth());
                }

                if (savedLevelContext != null)
                {
                    //this line was written based on what I saw on a parentheticExpr - '(' + where(c=>(c.fname=="" && c.lname="")) + ')'
                    //that led me to believe:
                    ////never pass the current path upward
                    ////always pass the currentCode upward
                    savedLevelContext.LevelInfo[savedLevelContext.LevelInfo.Keys.Max()].ConvertedCode += stackInfo.LevelInfo[stackInfo.LevelInfo.Keys.Min()].ConvertedCode;
                    
                    //if we had to save a context off, replace it...
                    //does this constitute a swap-a-dop-olis?
                    //todo need to figure out if anything from this context needs to be passed back to the outer context...at a minimum it seems like the converted code will need to be rolled up ward
                    stackInfo = savedLevelContext;
                }

            }
            else if (tree.ChildCount == 0)
            {
                //debug termNode?
                Console.WriteLine("term nodes should already be handled elsewhere");
            }
            else
            {
                Console.WriteLine("well, something");//debug 
            }
        }

        private static void HandleTerminalNode(TerminalNodeImpl termNode, LevelInformationStack stackInfo)
        {
            //the terminal node itself isn't a ruleContext object, so there is some trickery that happens below
            var termParentCheck = termNode.Parent as RuleContext;

            if (termParentCheck != null)
                stackInfo.LevelInfo.Add(termParentCheck.Depth() + 1, new LevelInformation());
            else
                Console.WriteLine("well, what is it then?");  //debug 

            //if this termNode is just a path step, ignore it, it'll get solved 
            if (termNode.GetText() == "/") return;

            #region check Parent node to know what to do with current line

            //path traversal nodes are the node parents that directly (meh, kind of) sire terminal nodes that correspond to a transition in the path
            //the only thing to do is add the current path to the current depth
            if (PathTraverseNodes.Contains(BranchingParentName(termNode)))
            {
                stackInfo.LevelInfo[((RuleContext)termNode.Parent).Depth() + 1].CurrentPartialPath = termNode.GetText() ;

                stackInfo.LevelInfo[((RuleContext)termNode.Parent).Depth() + 1].ConvertedCode = termNode.GetText() + ".";
            }

            //predicate nodes are parent nodes that directly (really directly) sire sub query filtering code
            //have to create the beginning filtering code and set the partialPath, and this partialPath should be a new levelInfo so as not to corrupt the main stack
            else if (PredicateNodes.Contains(BranchingParentName(termNode)))
            {
                //'[' chars are known points of entry for 'Where(' clause
                if (termNode.GetText() == "[")
                {
                    stackInfo.LevelInfo[((RuleContext)termNode.Parent).Depth() + 1].ConvertedCode = "Where(c" + (((RuleContext)termNode.Parent).Depth() + 1) + "=>";
                    stackInfo.LevelInfo[((RuleContext)termNode.Parent).Depth() + 1].CurrentPartialPath = "c" + (((RuleContext)termNode.Parent).Depth() + 1);
                }
                //when we hit the end of the predicate portion, box it off with a ')' instead of the ']'
                else if (termNode.GetText() == "]")
                {
                    stackInfo.LevelInfo[((RuleContext)termNode.Parent).Depth() + 1].ConvertedCode = ")";
                }
                //any other chars that exist under a predicate node are just added
                //debug this should not be hit?
                else
                {
                    stackInfo.LevelInfo[((RuleContext)termNode.Parent).Depth() + 1].CurrentPartialPath = termNode.GetText();
                }
            }

            //parenthesized nodes are parent nodes that directly (again, meh, kind of) sire sub queries to be grouped together
            //they are similar in function to predicate nodes, but do not require inserting a custom code transformation like the 'Where(' clause for predicates
            else if (ParenthesizedNodes.Contains(BranchingParentName(termNode)))
            {
                //the end caps always get added to provide the subquery portion
                if (termNode.GetText() == "(" || termNode.GetText() == ")")
                {
                    stackInfo.LevelInfo[((RuleContext) termNode.Parent).Depth() + 1].ConvertedCode = termNode.GetText();
                }
                else
                {
                    //debug
                    Console.WriteLine("I expected different things");
                }
            }

            else if (LogicNodes.Contains(BranchingParentName(termNode)))
            {
                switch (BranchingParentName(termNode))
                {
                    case nameof(XPath1W3Parser.AndExprContext):
                        stackInfo.LevelInfo[((RuleContext) termNode.Parent).Depth() + 1].ConvertedCode = " && ";
                        break;
                    case nameof(XPath1W3Parser.OrExprContext):
                        stackInfo.LevelInfo[((RuleContext)termNode.Parent).Depth() + 1].ConvertedCode = " || ";
                        break;
                }
            }

            else if (BranchingParentName(termNode) == nameof(XPath1W3Parser.AbbrevForwardStepContext))
            {
                //the @ sign denotes a specific attribute to test
                //need to swap the @ for the current partial path and put it into the code
                //the partial path doesn't change because the next step
                if (termNode.GetText() == "@")
                {
                    stackInfo.LevelInfo[((RuleContext)termNode.Parent).Depth() + 1].ConvertedCode = stackInfo.RollUpPartialPath();
                }
                //if it's not a "@" then it has to be the name of the attribute we are testing
                else
                {
                    stackInfo.LevelInfo[((RuleContext)termNode.Parent).Depth() + 1].ConvertedCode = "." + termNode.GetText(); 
                }
            }

            else if (BranchingParentName(termNode) == nameof(XPath1W3Parser.EqualityExprContext))
            {
                //if we have an equality operator - add it straight away
                if (termNode.GetText() == "=" || termNode.GetText() == "!=")
                {
                    string s = termNode.GetText();
                    if (s == "=") s += "="; //gotta be == for c# or we might assign value at worst, at best it will fail compilation

                    stackInfo.LevelInfo[((RuleContext) termNode.Parent).Depth() + 1].ConvertedCode = s;
                }
                //else if the string starts with a string literal character and the node type is a primary expression, add it straight away as well
                else if (termNode.GetText()[0] == '\'' && termNode.Parent.GetType().Name == nameof(XPath1W3Parser.PrimaryExprContext))
                {
                    //strip the ' off and replace with ", but only on the frist and last chars because if they exist anywhere else, we need to retain them
                    //todo - does this also mean we need to look for \' characters and replace them with single 's?
                    var s = termNode.GetText().ToCharArray();
                    s[0] = '"';
                    s[s.Length - 1] = '"';

                    stackInfo.LevelInfo[((RuleContext)termNode.Parent).Depth() + 1].ConvertedCode = new string(s);
                }
                else if (termNode.GetText() == "." && termNode.Parent.GetType().Name == nameof(XPath1W3Parser.ContextItemExprContext)) //this should
                {
                    string fullBackString = "";
                    int index = 0;
                    foreach (var lvl in stackInfo.LevelInfo.OrderByDescending(c=>c.Key))
                    {
                        index++;

                        if (!string.IsNullOrWhiteSpace(lvl.Value.ConvertedCode))
                        {
                            fullBackString = lvl.Value.ConvertedCode;
                            break;
                        }
                    }

                    if (index != stackInfo.LevelInfo.Keys.Count())
                    {
                        Console.WriteLine("crap?");
                    }
                    else if (fullBackString.StartsWith("Where(c") && fullBackString.EndsWith("=>"))
                    {
                        //this should mean - there is only 1 level in the stackInfo that has convert code in one level, and it should be Where(c=> with c as the partial path
                        //this needs to be removed and reset to work on the parent element

                        stackInfo.LevelInfo[stackInfo.LevelInfo.Keys.ToList()[0]].ConvertedCode = null;
                        stackInfo.LevelInfo[stackInfo.LevelInfo.Keys.ToList()[0]].CurrentPartialPath = stackInfo.CallingParentFullPath;
                    }

                    stackInfo.LevelInfo[((RuleContext)termNode.Parent).Depth() + 1].ConvertedCode += " " + stackInfo.RollUpPartialPath();

                }
                else
                {
                    //debug
                    Console.WriteLine("well what do we have here? prolly a value, like 5 or 11, or some other non-string literal, duh");
                }
            }

            //This feels like the same type of node as an EqualityExprContext, but it shouldn't ever have strings because string1>string2 doesn't make a lot of sense in XPath (or most other areas)
            //so, might as well make it a seperate chunk, just incase more differences are observed 
            else if (BranchingParentName(termNode) == nameof(XPath1W3Parser.RelationalExprContext))
            {
                //if this is an operator, add it straight away
                if (termNode.GetText() == "<" || termNode.GetText() == ">" || termNode.GetText() == "<=" || termNode.GetText() == ">=")
                {
                    stackInfo.LevelInfo[((RuleContext) termNode.Parent).Depth() + 1].ConvertedCode = termNode.GetText();
                }
                else
                {
                    //debug
                    Console.WriteLine("I am really not sure if this is expected or not...why are we here future self?");
                }
            }

            else
            {
                //debug
                Console.WriteLine("Srsly, wtf are we doing here...someone didn't think that through enough");
            }
            #endregion
        }

        /// <summary>
        /// Find the depth of the branching parent by walking up parse tree
        /// </summary>
        /// <param name="tree">the current node in the parse tree</param>
        /// <returns>the ((RuleContext)IParseTree).Depth() of the first branching parent, walking up the tree</returns>
        private static int BranchingParentDepth(RuleContext tree)
        {
            return ((RuleContext)BranchingParent(tree)).Depth();
        }

        /// <summary>
        /// Find the node/context name of the branching parent by walking up parse tree
        /// </summary>
        /// <param name="tree">the current node in the parse tree</param>
        /// <returns>the GetType.Name() of the returned IParseTree node</returns>
        private static string BranchingParentName(IParseTree tree)
        {
            return BranchingParent(tree).GetType().Name;
        }

        /// <summary>
        /// nothing should call this execpt for methods that start with BarnchingParent*(something tree)
        /// this is the most basic up walk method, it should be wrapped by something a little user friendly so you know what you're getting out of it
        /// </summary>
        /// <param name="tree">the current node in the parse tree</param>
        /// <param name="firstPass">don't set when calling externally, this is state persistance</param>
        /// <returns>a generic IParseTree node representing the parent that branched to create this node</returns>
        private static IParseTree BranchingParent(IParseTree tree, bool firstPass = true)
        {
            if (tree.Parent == null) return tree;

            //if this is the first call, don't return itself as the branching parent, can't be your own parent
            if (firstPass) return BranchingParent(tree.Parent, false);

            //if the parent has only 1 child, it is a simple pass through node and we need to look higher
            if (tree.ChildCount == 1) return BranchingParent(tree.Parent, false);

            //this should mean the parent has a child count > 1 and is at least 1 level higher than we started
            return tree;
        }
    }

    /// <summary>
    /// State information for each level of the parse tree
    /// This contains the converted code built at each level
    /// As well as the current partial path that
    /// Code and PartialPath are kept isolated at each level and the calling code must determine when to roll this data up or discard as appropriate
    /// </summary>
    public class LevelInformation
    {
        public string ConvertedCode = "";
        public string CurrentPartialPath = ""; 
    }

    public class LevelInformationStack
    {
        public string CallingParentFullPath; 
        public readonly Dictionary<int, LevelInformation> LevelInfo;

        public LevelInformationStack()
        {
            LevelInfo = new Dictionary<int, LevelInformation>();
        }

        public LevelInformationStack(int i, LevelInformation li) : this() { LevelInfo.Add(i, li); }

        /// <summary>
        /// Start at the top of the given stack and walk down, combining the partial paths into a single statement
        /// </summary> 
        /// <returns>a full path in current scope</returns>
        public string RollUpPartialPath(bool useCallingParentRoot=false)
        {
            string fullPath ="";

            if (useCallingParentRoot) fullPath = CallingParentFullPath;

            foreach (var source in LevelInfo.OrderBy(c => c.Key))
            {
                if (string.IsNullOrWhiteSpace(source.Value.CurrentPartialPath)) continue;

                if (!string.IsNullOrWhiteSpace(fullPath))
                    fullPath += ".";

                fullPath += source.Value.CurrentPartialPath;
            }
            return fullPath;
        } 
    }
}
