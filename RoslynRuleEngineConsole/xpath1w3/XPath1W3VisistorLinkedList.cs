using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace RoslynRuleEngineConsole.xpath1w3
{
    public class NodeLinkedList
    { 
        public string NodeName;
        public string NodeType;
        public string NodePath;
    }

    public class XPath1W3VisistorLinkedList : XPath1W3BaseVisitor<string>
    {
        private static readonly List<string> PathTraverseNodes = new List<string> { nameof(XPath1W3Parser.PathExprContext), nameof(XPath1W3Parser.RelativePathExprContext), nameof(XPath1W3Parser.AxisStepContext) };
        private static readonly List<string> PredicateNodes = new List<string> { nameof(XPath1W3Parser.PredicateContext) };
        private static readonly List<string> ParenthesizedNodes = new List<string> { nameof(XPath1W3Parser.ParenthesizedExprContext) };
        private static readonly List<string> LogicNodes = new List<string> { nameof(XPath1Parser.AndExprContext), nameof(XPath1Parser.OrExprContext) };

        private LinkedList<NodeLinkedList> parserLinkedList = new LinkedList<NodeLinkedList>(); 

        public override string VisitStart(XPath1W3Parser.StartContext context)
        {  
            WalkSyntaxTree(context);
            base.VisitStart(context);
            BuildConditionalStatement(parserLinkedList.First);

            return "";
        }

        private void BuildConditionalStatement(LinkedListNode<NodeLinkedList> nodes)
        {
            if (nodes.Previous != null) nodes.Value.NodePath = nodes.Previous.Value.NodePath;

            if (PathTraverseNodes.Contains(nodes.Value.NodeType))
            {
                if (nodes.Previous != null && !string.IsNullOrWhiteSpace(nodes.Value.NodePath))
                    nodes.Value.NodePath += ".";
            }
        }

        private void WalkSyntaxTree(RuleContext tree)
        {
            if (tree.ChildCount > 0)
            {
                if (tree.ChildCount > 1)
                {
                    parserLinkedList.AddLast(new NodeLinkedList {NodeName = null, NodeType = tree.GetType().Name});
                }

                for (int i = 0; i < tree.ChildCount; i++)
                {
                    //if this is a terminal node, it is not the same interface as a normal parse node and has to be handled seperately
                    var termTest = tree.GetChild(i) as TerminalNodeImpl;

                    if (termTest != null)
                    {
                        parserLinkedList.AddLast(new NodeLinkedList {NodeName = termTest.GetText(), NodeType = termTest.GetType().Name});
                        
                    }
                    else WalkSyntaxTree((RuleContext)tree.GetChild(i));
                }
            }
        }
    }
}
