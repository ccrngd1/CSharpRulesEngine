//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from XPath1W3.g4 by ANTLR 4.5.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5.1")]
[System.CLSCompliant(false)]
public partial class XPath1W3Lexer : Lexer {
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, T__20=21, T__21=22, T__22=23, T__23=24, 
		T__24=25, T__25=26, T__26=27, T__27=28, T__28=29, T__29=30, T__30=31, 
		T__31=32, T__32=33, T__33=34, T__34=35, T__35=36, T__36=37, T__37=38, 
		T__38=39, T__39=40, T__40=41, T__41=42, T__42=43, Literal=44, NumericLiteral=45, 
		PITest=46, StringLiteral=47, IntegerLiteral=48, DecimalLiteral=49, Number=50, 
		Digits=51, AxisName=52, LiteralChar=53, WS=54, NCName=55, NCNameStartChar=56, 
		NCNameChar=57;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
		"T__17", "T__18", "T__19", "T__20", "T__21", "T__22", "T__23", "T__24", 
		"T__25", "T__26", "T__27", "T__28", "T__29", "T__30", "T__31", "T__32", 
		"T__33", "T__34", "T__35", "T__36", "T__37", "T__38", "T__39", "T__40", 
		"T__41", "T__42", "Literal", "NumericLiteral", "PITest", "StringLiteral", 
		"IntegerLiteral", "DecimalLiteral", "Number", "Digits", "AxisName", "LiteralChar", 
		"WS", "NCName", "NCNameStartChar", "NCNameChar"
	};


	public XPath1W3Lexer(ICharStream input)
		: base(input)
	{
		Interpreter = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, "'or'", "'and'", "'='", "'!='", "'<'", "'>'", "'<='", "'>='", "'+'", 
		"'-'", "'*'", "'div'", "'mod'", "'|'", "'/'", "'//'", "'child'", "'::'", 
		"'descendant'", "'attribute'", "'self'", "'descendant-or-self'", "'following-sibling'", 
		"'following'", "'namespace'", "'@'", "'parent'", "'ancestor'", "'preceding-sibling'", 
		"'preceding'", "'ancestor-or-self'", "'..'", "':'", "'['", "']'", "'$'", 
		"'('", "')'", "'.'", "','", "'node'", "'test'", "'comment'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, "Literal", "NumericLiteral", 
		"PITest", "StringLiteral", "IntegerLiteral", "DecimalLiteral", "Number", 
		"Digits", "AxisName", "LiteralChar", "WS", "NCName", "NCNameStartChar", 
		"NCNameChar"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "XPath1W3.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\x430\xD6D1\x8206\xAD2D\x4417\xAEF1\x8D80\xAADD\x2;\x264\b\x1\x4\x2"+
		"\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4"+
		"\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10"+
		"\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15\t\x15"+
		"\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A\x4\x1B"+
		"\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 \t \x4!"+
		"\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t(\x4)\t)\x4*\t"+
		"*\x4+\t+\x4,\t,\x4-\t-\x4.\t.\x4/\t/\x4\x30\t\x30\x4\x31\t\x31\x4\x32"+
		"\t\x32\x4\x33\t\x33\x4\x34\t\x34\x4\x35\t\x35\x4\x36\t\x36\x4\x37\t\x37"+
		"\x4\x38\t\x38\x4\x39\t\x39\x4:\t:\x3\x2\x3\x2\x3\x2\x3\x3\x3\x3\x3\x3"+
		"\x3\x3\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\x3\x6\x3\x6\x3\a\x3\a\x3\b\x3\b\x3"+
		"\b\x3\t\x3\t\x3\t\x3\n\x3\n\x3\v\x3\v\x3\f\x3\f\x3\r\x3\r\x3\r\x3\r\x3"+
		"\xE\x3\xE\x3\xE\x3\xE\x3\xF\x3\xF\x3\x10\x3\x10\x3\x11\x3\x11\x3\x11\x3"+
		"\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x13\x3\x13\x3\x13\x3\x14\x3"+
		"\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3"+
		"\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3"+
		"\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3"+
		"\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3"+
		"\x17\x3\x17\x3\x17\x3\x17\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3"+
		"\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3"+
		"\x18\x3\x18\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3"+
		"\x19\x3\x19\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3"+
		"\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1C\x3\x1C\x3\x1C\x3\x1C\x3\x1C\x3\x1C\x3"+
		"\x1C\x3\x1D\x3\x1D\x3\x1D\x3\x1D\x3\x1D\x3\x1D\x3\x1D\x3\x1D\x3\x1D\x3"+
		"\x1E\x3\x1E\x3\x1E\x3\x1E\x3\x1E\x3\x1E\x3\x1E\x3\x1E\x3\x1E\x3\x1E\x3"+
		"\x1E\x3\x1E\x3\x1E\x3\x1E\x3\x1E\x3\x1E\x3\x1E\x3\x1E\x3\x1F\x3\x1F\x3"+
		"\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3 \x3 \x3 \x3 "+
		"\x3 \x3 \x3 \x3 \x3 \x3 \x3 \x3 \x3 \x3 \x3 \x3 \x3 \x3!\x3!\x3!\x3\""+
		"\x3\"\x3#\x3#\x3$\x3$\x3%\x3%\x3&\x3&\x3\'\x3\'\x3(\x3(\x3)\x3)\x3*\x3"+
		"*\x3*\x3*\x3*\x3+\x3+\x3+\x3+\x3+\x3,\x3,\x3,\x3,\x3,\x3,\x3,\x3,\x3-"+
		"\x3-\x5-\x163\n-\x3.\x3.\x5.\x167\n.\x3/\x3/\x3/\x3/\x3/\x3/\x3/\x3/\x3"+
		"/\x3/\x3/\x3/\x3/\x3/\x3/\x3/\x3/\x3/\x3/\x3/\x3/\x3/\x3/\x3/\x3/\x5/"+
		"\x182\n/\x3/\x3/\x3\x30\x3\x30\a\x30\x188\n\x30\f\x30\xE\x30\x18B\v\x30"+
		"\x3\x30\x6\x30\x18E\n\x30\r\x30\xE\x30\x18F\x3\x30\x3\x30\x3\x31\x6\x31"+
		"\x195\n\x31\r\x31\xE\x31\x196\x3\x32\x6\x32\x19A\n\x32\r\x32\xE\x32\x19B"+
		"\x3\x32\x3\x32\x6\x32\x1A0\n\x32\r\x32\xE\x32\x1A1\x3\x33\x3\x33\x3\x33"+
		"\x5\x33\x1A7\n\x33\x5\x33\x1A9\n\x33\x3\x33\x3\x33\x5\x33\x1AD\n\x33\x3"+
		"\x34\x6\x34\x1B0\n\x34\r\x34\xE\x34\x1B1\x3\x35\x3\x35\x3\x35\x3\x35\x3"+
		"\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3"+
		"\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3"+
		"\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3"+
		"\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3"+
		"\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3"+
		"\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3"+
		"\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3"+
		"\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3"+
		"\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3"+
		"\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3"+
		"\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3"+
		"\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3"+
		"\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3\x35\x3"+
		"\x35\x3\x35\x3\x35\x5\x35\x23D\n\x35\x3\x36\x3\x36\a\x36\x241\n\x36\f"+
		"\x36\xE\x36\x244\v\x36\x3\x36\x3\x36\x3\x36\a\x36\x249\n\x36\f\x36\xE"+
		"\x36\x24C\v\x36\x3\x36\x5\x36\x24F\n\x36\x3\x37\x6\x37\x252\n\x37\r\x37"+
		"\xE\x37\x253\x3\x37\x3\x37\x3\x38\x3\x38\a\x38\x25A\n\x38\f\x38\xE\x38"+
		"\x25D\v\x38\x3\x39\x3\x39\x3:\x3:\x5:\x263\n:\x2\x2;\x3\x3\x5\x4\a\x5"+
		"\t\x6\v\a\r\b\xF\t\x11\n\x13\v\x15\f\x17\r\x19\xE\x1B\xF\x1D\x10\x1F\x11"+
		"!\x12#\x13%\x14\'\x15)\x16+\x17-\x18/\x19\x31\x1A\x33\x1B\x35\x1C\x37"+
		"\x1D\x39\x1E;\x1F= ?!\x41\"\x43#\x45$G%I&K\'M(O)Q*S+U,W-Y.[/]\x30_\x31"+
		"\x61\x32\x63\x33\x65\x34g\x35i\x36k\x37m\x38o\x39q:s;\x3\x2\b\x4\x2$$"+
		"))\x3\x2$$\x3\x2))\x5\x2\v\f\xF\xF\"\"\x10\x2\x43\\\x61\x61\x63|\xC2\xD8"+
		"\xDA\xF8\xFA\x301\x372\x37F\x381\x2001\x200E\x200F\x2072\x2191\x2C02\x2FF1"+
		"\x3003\xD801\xF902\xFDD1\xFDF2\xFFFF\a\x2/\x30\x32;\xB9\xB9\x302\x371"+
		"\x2041\x2042\x281\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2\x2"+
		"\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF\x3\x2\x2\x2"+
		"\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2\x2\x17\x3\x2"+
		"\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2\x1F"+
		"\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'\x3\x2"+
		"\x2\x2\x2)\x3\x2\x2\x2\x2+\x3\x2\x2\x2\x2-\x3\x2\x2\x2\x2/\x3\x2\x2\x2"+
		"\x2\x31\x3\x2\x2\x2\x2\x33\x3\x2\x2\x2\x2\x35\x3\x2\x2\x2\x2\x37\x3\x2"+
		"\x2\x2\x2\x39\x3\x2\x2\x2\x2;\x3\x2\x2\x2\x2=\x3\x2\x2\x2\x2?\x3\x2\x2"+
		"\x2\x2\x41\x3\x2\x2\x2\x2\x43\x3\x2\x2\x2\x2\x45\x3\x2\x2\x2\x2G\x3\x2"+
		"\x2\x2\x2I\x3\x2\x2\x2\x2K\x3\x2\x2\x2\x2M\x3\x2\x2\x2\x2O\x3\x2\x2\x2"+
		"\x2Q\x3\x2\x2\x2\x2S\x3\x2\x2\x2\x2U\x3\x2\x2\x2\x2W\x3\x2\x2\x2\x2Y\x3"+
		"\x2\x2\x2\x2[\x3\x2\x2\x2\x2]\x3\x2\x2\x2\x2_\x3\x2\x2\x2\x2\x61\x3\x2"+
		"\x2\x2\x2\x63\x3\x2\x2\x2\x2\x65\x3\x2\x2\x2\x2g\x3\x2\x2\x2\x2i\x3\x2"+
		"\x2\x2\x2k\x3\x2\x2\x2\x2m\x3\x2\x2\x2\x2o\x3\x2\x2\x2\x2q\x3\x2\x2\x2"+
		"\x2s\x3\x2\x2\x2\x3u\x3\x2\x2\x2\x5x\x3\x2\x2\x2\a|\x3\x2\x2\x2\t~\x3"+
		"\x2\x2\x2\v\x81\x3\x2\x2\x2\r\x83\x3\x2\x2\x2\xF\x85\x3\x2\x2\x2\x11\x88"+
		"\x3\x2\x2\x2\x13\x8B\x3\x2\x2\x2\x15\x8D\x3\x2\x2\x2\x17\x8F\x3\x2\x2"+
		"\x2\x19\x91\x3\x2\x2\x2\x1B\x95\x3\x2\x2\x2\x1D\x99\x3\x2\x2\x2\x1F\x9B"+
		"\x3\x2\x2\x2!\x9D\x3\x2\x2\x2#\xA0\x3\x2\x2\x2%\xA6\x3\x2\x2\x2\'\xA9"+
		"\x3\x2\x2\x2)\xB4\x3\x2\x2\x2+\xBE\x3\x2\x2\x2-\xC3\x3\x2\x2\x2/\xD6\x3"+
		"\x2\x2\x2\x31\xE8\x3\x2\x2\x2\x33\xF2\x3\x2\x2\x2\x35\xFC\x3\x2\x2\x2"+
		"\x37\xFE\x3\x2\x2\x2\x39\x105\x3\x2\x2\x2;\x10E\x3\x2\x2\x2=\x120\x3\x2"+
		"\x2\x2?\x12A\x3\x2\x2\x2\x41\x13B\x3\x2\x2\x2\x43\x13E\x3\x2\x2\x2\x45"+
		"\x140\x3\x2\x2\x2G\x142\x3\x2\x2\x2I\x144\x3\x2\x2\x2K\x146\x3\x2\x2\x2"+
		"M\x148\x3\x2\x2\x2O\x14A\x3\x2\x2\x2Q\x14C\x3\x2\x2\x2S\x14E\x3\x2\x2"+
		"\x2U\x153\x3\x2\x2\x2W\x158\x3\x2\x2\x2Y\x162\x3\x2\x2\x2[\x166\x3\x2"+
		"\x2\x2]\x168\x3\x2\x2\x2_\x185\x3\x2\x2\x2\x61\x194\x3\x2\x2\x2\x63\x199"+
		"\x3\x2\x2\x2\x65\x1AC\x3\x2\x2\x2g\x1AF\x3\x2\x2\x2i\x23C\x3\x2\x2\x2"+
		"k\x24E\x3\x2\x2\x2m\x251\x3\x2\x2\x2o\x257\x3\x2\x2\x2q\x25E\x3\x2\x2"+
		"\x2s\x262\x3\x2\x2\x2uv\aq\x2\x2vw\at\x2\x2w\x4\x3\x2\x2\x2xy\a\x63\x2"+
		"\x2yz\ap\x2\x2z{\a\x66\x2\x2{\x6\x3\x2\x2\x2|}\a?\x2\x2}\b\x3\x2\x2\x2"+
		"~\x7F\a#\x2\x2\x7F\x80\a?\x2\x2\x80\n\x3\x2\x2\x2\x81\x82\a>\x2\x2\x82"+
		"\f\x3\x2\x2\x2\x83\x84\a@\x2\x2\x84\xE\x3\x2\x2\x2\x85\x86\a>\x2\x2\x86"+
		"\x87\a?\x2\x2\x87\x10\x3\x2\x2\x2\x88\x89\a@\x2\x2\x89\x8A\a?\x2\x2\x8A"+
		"\x12\x3\x2\x2\x2\x8B\x8C\a-\x2\x2\x8C\x14\x3\x2\x2\x2\x8D\x8E\a/\x2\x2"+
		"\x8E\x16\x3\x2\x2\x2\x8F\x90\a,\x2\x2\x90\x18\x3\x2\x2\x2\x91\x92\a\x66"+
		"\x2\x2\x92\x93\ak\x2\x2\x93\x94\ax\x2\x2\x94\x1A\x3\x2\x2\x2\x95\x96\a"+
		"o\x2\x2\x96\x97\aq\x2\x2\x97\x98\a\x66\x2\x2\x98\x1C\x3\x2\x2\x2\x99\x9A"+
		"\a~\x2\x2\x9A\x1E\x3\x2\x2\x2\x9B\x9C\a\x31\x2\x2\x9C \x3\x2\x2\x2\x9D"+
		"\x9E\a\x31\x2\x2\x9E\x9F\a\x31\x2\x2\x9F\"\x3\x2\x2\x2\xA0\xA1\a\x65\x2"+
		"\x2\xA1\xA2\aj\x2\x2\xA2\xA3\ak\x2\x2\xA3\xA4\an\x2\x2\xA4\xA5\a\x66\x2"+
		"\x2\xA5$\x3\x2\x2\x2\xA6\xA7\a<\x2\x2\xA7\xA8\a<\x2\x2\xA8&\x3\x2\x2\x2"+
		"\xA9\xAA\a\x66\x2\x2\xAA\xAB\ag\x2\x2\xAB\xAC\au\x2\x2\xAC\xAD\a\x65\x2"+
		"\x2\xAD\xAE\ag\x2\x2\xAE\xAF\ap\x2\x2\xAF\xB0\a\x66\x2\x2\xB0\xB1\a\x63"+
		"\x2\x2\xB1\xB2\ap\x2\x2\xB2\xB3\av\x2\x2\xB3(\x3\x2\x2\x2\xB4\xB5\a\x63"+
		"\x2\x2\xB5\xB6\av\x2\x2\xB6\xB7\av\x2\x2\xB7\xB8\at\x2\x2\xB8\xB9\ak\x2"+
		"\x2\xB9\xBA\a\x64\x2\x2\xBA\xBB\aw\x2\x2\xBB\xBC\av\x2\x2\xBC\xBD\ag\x2"+
		"\x2\xBD*\x3\x2\x2\x2\xBE\xBF\au\x2\x2\xBF\xC0\ag\x2\x2\xC0\xC1\an\x2\x2"+
		"\xC1\xC2\ah\x2\x2\xC2,\x3\x2\x2\x2\xC3\xC4\a\x66\x2\x2\xC4\xC5\ag\x2\x2"+
		"\xC5\xC6\au\x2\x2\xC6\xC7\a\x65\x2\x2\xC7\xC8\ag\x2\x2\xC8\xC9\ap\x2\x2"+
		"\xC9\xCA\a\x66\x2\x2\xCA\xCB\a\x63\x2\x2\xCB\xCC\ap\x2\x2\xCC\xCD\av\x2"+
		"\x2\xCD\xCE\a/\x2\x2\xCE\xCF\aq\x2\x2\xCF\xD0\at\x2\x2\xD0\xD1\a/\x2\x2"+
		"\xD1\xD2\au\x2\x2\xD2\xD3\ag\x2\x2\xD3\xD4\an\x2\x2\xD4\xD5\ah\x2\x2\xD5"+
		".\x3\x2\x2\x2\xD6\xD7\ah\x2\x2\xD7\xD8\aq\x2\x2\xD8\xD9\an\x2\x2\xD9\xDA"+
		"\an\x2\x2\xDA\xDB\aq\x2\x2\xDB\xDC\ay\x2\x2\xDC\xDD\ak\x2\x2\xDD\xDE\a"+
		"p\x2\x2\xDE\xDF\ai\x2\x2\xDF\xE0\a/\x2\x2\xE0\xE1\au\x2\x2\xE1\xE2\ak"+
		"\x2\x2\xE2\xE3\a\x64\x2\x2\xE3\xE4\an\x2\x2\xE4\xE5\ak\x2\x2\xE5\xE6\a"+
		"p\x2\x2\xE6\xE7\ai\x2\x2\xE7\x30\x3\x2\x2\x2\xE8\xE9\ah\x2\x2\xE9\xEA"+
		"\aq\x2\x2\xEA\xEB\an\x2\x2\xEB\xEC\an\x2\x2\xEC\xED\aq\x2\x2\xED\xEE\a"+
		"y\x2\x2\xEE\xEF\ak\x2\x2\xEF\xF0\ap\x2\x2\xF0\xF1\ai\x2\x2\xF1\x32\x3"+
		"\x2\x2\x2\xF2\xF3\ap\x2\x2\xF3\xF4\a\x63\x2\x2\xF4\xF5\ao\x2\x2\xF5\xF6"+
		"\ag\x2\x2\xF6\xF7\au\x2\x2\xF7\xF8\ar\x2\x2\xF8\xF9\a\x63\x2\x2\xF9\xFA"+
		"\a\x65\x2\x2\xFA\xFB\ag\x2\x2\xFB\x34\x3\x2\x2\x2\xFC\xFD\a\x42\x2\x2"+
		"\xFD\x36\x3\x2\x2\x2\xFE\xFF\ar\x2\x2\xFF\x100\a\x63\x2\x2\x100\x101\a"+
		"t\x2\x2\x101\x102\ag\x2\x2\x102\x103\ap\x2\x2\x103\x104\av\x2\x2\x104"+
		"\x38\x3\x2\x2\x2\x105\x106\a\x63\x2\x2\x106\x107\ap\x2\x2\x107\x108\a"+
		"\x65\x2\x2\x108\x109\ag\x2\x2\x109\x10A\au\x2\x2\x10A\x10B\av\x2\x2\x10B"+
		"\x10C\aq\x2\x2\x10C\x10D\at\x2\x2\x10D:\x3\x2\x2\x2\x10E\x10F\ar\x2\x2"+
		"\x10F\x110\at\x2\x2\x110\x111\ag\x2\x2\x111\x112\a\x65\x2\x2\x112\x113"+
		"\ag\x2\x2\x113\x114\a\x66\x2\x2\x114\x115\ak\x2\x2\x115\x116\ap\x2\x2"+
		"\x116\x117\ai\x2\x2\x117\x118\a/\x2\x2\x118\x119\au\x2\x2\x119\x11A\a"+
		"k\x2\x2\x11A\x11B\a\x64\x2\x2\x11B\x11C\an\x2\x2\x11C\x11D\ak\x2\x2\x11D"+
		"\x11E\ap\x2\x2\x11E\x11F\ai\x2\x2\x11F<\x3\x2\x2\x2\x120\x121\ar\x2\x2"+
		"\x121\x122\at\x2\x2\x122\x123\ag\x2\x2\x123\x124\a\x65\x2\x2\x124\x125"+
		"\ag\x2\x2\x125\x126\a\x66\x2\x2\x126\x127\ak\x2\x2\x127\x128\ap\x2\x2"+
		"\x128\x129\ai\x2\x2\x129>\x3\x2\x2\x2\x12A\x12B\a\x63\x2\x2\x12B\x12C"+
		"\ap\x2\x2\x12C\x12D\a\x65\x2\x2\x12D\x12E\ag\x2\x2\x12E\x12F\au\x2\x2"+
		"\x12F\x130\av\x2\x2\x130\x131\aq\x2\x2\x131\x132\at\x2\x2\x132\x133\a"+
		"/\x2\x2\x133\x134\aq\x2\x2\x134\x135\at\x2\x2\x135\x136\a/\x2\x2\x136"+
		"\x137\au\x2\x2\x137\x138\ag\x2\x2\x138\x139\an\x2\x2\x139\x13A\ah\x2\x2"+
		"\x13A@\x3\x2\x2\x2\x13B\x13C\a\x30\x2\x2\x13C\x13D\a\x30\x2\x2\x13D\x42"+
		"\x3\x2\x2\x2\x13E\x13F\a<\x2\x2\x13F\x44\x3\x2\x2\x2\x140\x141\a]\x2\x2"+
		"\x141\x46\x3\x2\x2\x2\x142\x143\a_\x2\x2\x143H\x3\x2\x2\x2\x144\x145\a"+
		"&\x2\x2\x145J\x3\x2\x2\x2\x146\x147\a*\x2\x2\x147L\x3\x2\x2\x2\x148\x149"+
		"\a+\x2\x2\x149N\x3\x2\x2\x2\x14A\x14B\a\x30\x2\x2\x14BP\x3\x2\x2\x2\x14C"+
		"\x14D\a.\x2\x2\x14DR\x3\x2\x2\x2\x14E\x14F\ap\x2\x2\x14F\x150\aq\x2\x2"+
		"\x150\x151\a\x66\x2\x2\x151\x152\ag\x2\x2\x152T\x3\x2\x2\x2\x153\x154"+
		"\av\x2\x2\x154\x155\ag\x2\x2\x155\x156\au\x2\x2\x156\x157\av\x2\x2\x157"+
		"V\x3\x2\x2\x2\x158\x159\a\x65\x2\x2\x159\x15A\aq\x2\x2\x15A\x15B\ao\x2"+
		"\x2\x15B\x15C\ao\x2\x2\x15C\x15D\ag\x2\x2\x15D\x15E\ap\x2\x2\x15E\x15F"+
		"\av\x2\x2\x15FX\x3\x2\x2\x2\x160\x163\x5[.\x2\x161\x163\x5_\x30\x2\x162"+
		"\x160\x3\x2\x2\x2\x162\x161\x3\x2\x2\x2\x163Z\x3\x2\x2\x2\x164\x167\x5"+
		"\x61\x31\x2\x165\x167\x5\x63\x32\x2\x166\x164\x3\x2\x2\x2\x166\x165\x3"+
		"\x2\x2\x2\x167\\\x3\x2\x2\x2\x168\x169\ar\x2\x2\x169\x16A\at\x2\x2\x16A"+
		"\x16B\aq\x2\x2\x16B\x16C\a\x65\x2\x2\x16C\x16D\ag\x2\x2\x16D\x16E\au\x2"+
		"\x2\x16E\x16F\au\x2\x2\x16F\x170\ak\x2\x2\x170\x171\ap\x2\x2\x171\x172"+
		"\ai\x2\x2\x172\x173\a/\x2\x2\x173\x174\ak\x2\x2\x174\x175\ap\x2\x2\x175"+
		"\x176\au\x2\x2\x176\x177\av\x2\x2\x177\x178\at\x2\x2\x178\x179\aw\x2\x2"+
		"\x179\x17A\a\x65\x2\x2\x17A\x17B\av\x2\x2\x17B\x17C\ak\x2\x2\x17C\x17D"+
		"\aq\x2\x2\x17D\x17E\ap\x2\x2\x17E\x17F\x3\x2\x2\x2\x17F\x181\a*\x2\x2"+
		"\x180\x182\x5_\x30\x2\x181\x180\x3\x2\x2\x2\x181\x182\x3\x2\x2\x2\x182"+
		"\x183\x3\x2\x2\x2\x183\x184\a+\x2\x2\x184^\x3\x2\x2\x2\x185\x18D\t\x2"+
		"\x2\x2\x186\x188\x5g\x34\x2\x187\x186\x3\x2\x2\x2\x188\x18B\x3\x2\x2\x2"+
		"\x189\x187\x3\x2\x2\x2\x189\x18A\x3\x2\x2\x2\x18A\x18C\x3\x2\x2\x2\x18B"+
		"\x189\x3\x2\x2\x2\x18C\x18E\x5s:\x2\x18D\x189\x3\x2\x2\x2\x18E\x18F\x3"+
		"\x2\x2\x2\x18F\x18D\x3\x2\x2\x2\x18F\x190\x3\x2\x2\x2\x190\x191\x3\x2"+
		"\x2\x2\x191\x192\t\x2\x2\x2\x192`\x3\x2\x2\x2\x193\x195\x5g\x34\x2\x194"+
		"\x193\x3\x2\x2\x2\x195\x196\x3\x2\x2\x2\x196\x194\x3\x2\x2\x2\x196\x197"+
		"\x3\x2\x2\x2\x197\x62\x3\x2\x2\x2\x198\x19A\x5g\x34\x2\x199\x198\x3\x2"+
		"\x2\x2\x19A\x19B\x3\x2\x2\x2\x19B\x199\x3\x2\x2\x2\x19B\x19C\x3\x2\x2"+
		"\x2\x19C\x19D\x3\x2\x2\x2\x19D\x19F\a\x30\x2\x2\x19E\x1A0\x5g\x34\x2\x19F"+
		"\x19E\x3\x2\x2\x2\x1A0\x1A1\x3\x2\x2\x2\x1A1\x19F\x3\x2\x2\x2\x1A1\x1A2"+
		"\x3\x2\x2\x2\x1A2\x64\x3\x2\x2\x2\x1A3\x1A8\x5g\x34\x2\x1A4\x1A6\a\x30"+
		"\x2\x2\x1A5\x1A7\x5g\x34\x2\x1A6\x1A5\x3\x2\x2\x2\x1A6\x1A7\x3\x2\x2\x2"+
		"\x1A7\x1A9\x3\x2\x2\x2\x1A8\x1A4\x3\x2\x2\x2\x1A8\x1A9\x3\x2\x2\x2\x1A9"+
		"\x1AD\x3\x2\x2\x2\x1AA\x1AB\a\x30\x2\x2\x1AB\x1AD\x5g\x34\x2\x1AC\x1A3"+
		"\x3\x2\x2\x2\x1AC\x1AA\x3\x2\x2\x2\x1AD\x66\x3\x2\x2\x2\x1AE\x1B0\x4\x32"+
		";\x2\x1AF\x1AE\x3\x2\x2\x2\x1B0\x1B1\x3\x2\x2\x2\x1B1\x1AF\x3\x2\x2\x2"+
		"\x1B1\x1B2\x3\x2\x2\x2\x1B2h\x3\x2\x2\x2\x1B3\x1B4\a\x63\x2\x2\x1B4\x1B5"+
		"\ap\x2\x2\x1B5\x1B6\a\x65\x2\x2\x1B6\x1B7\ag\x2\x2\x1B7\x1B8\au\x2\x2"+
		"\x1B8\x1B9\av\x2\x2\x1B9\x1BA\aq\x2\x2\x1BA\x23D\at\x2\x2\x1BB\x1BC\a"+
		"\x63\x2\x2\x1BC\x1BD\ap\x2\x2\x1BD\x1BE\a\x65\x2\x2\x1BE\x1BF\ag\x2\x2"+
		"\x1BF\x1C0\au\x2\x2\x1C0\x1C1\av\x2\x2\x1C1\x1C2\aq\x2\x2\x1C2\x1C3\a"+
		"t\x2\x2\x1C3\x1C4\a/\x2\x2\x1C4\x1C5\aq\x2\x2\x1C5\x1C6\at\x2\x2\x1C6"+
		"\x1C7\a/\x2\x2\x1C7\x1C8\au\x2\x2\x1C8\x1C9\ag\x2\x2\x1C9\x1CA\an\x2\x2"+
		"\x1CA\x23D\ah\x2\x2\x1CB\x1CC\a\x63\x2\x2\x1CC\x1CD\av\x2\x2\x1CD\x1CE"+
		"\av\x2\x2\x1CE\x1CF\at\x2\x2\x1CF\x1D0\ak\x2\x2\x1D0\x1D1\a\x64\x2\x2"+
		"\x1D1\x1D2\aw\x2\x2\x1D2\x1D3\av\x2\x2\x1D3\x23D\ag\x2\x2\x1D4\x1D5\a"+
		"\x65\x2\x2\x1D5\x1D6\aj\x2\x2\x1D6\x1D7\ak\x2\x2\x1D7\x1D8\an\x2\x2\x1D8"+
		"\x23D\a\x66\x2\x2\x1D9\x1DA\a\x66\x2\x2\x1DA\x1DB\ag\x2\x2\x1DB\x1DC\a"+
		"u\x2\x2\x1DC\x1DD\a\x65\x2\x2\x1DD\x1DE\ag\x2\x2\x1DE\x1DF\ap\x2\x2\x1DF"+
		"\x1E0\a\x66\x2\x2\x1E0\x1E1\a\x63\x2\x2\x1E1\x1E2\ap\x2\x2\x1E2\x23D\a"+
		"v\x2\x2\x1E3\x1E4\a\x66\x2\x2\x1E4\x1E5\ag\x2\x2\x1E5\x1E6\au\x2\x2\x1E6"+
		"\x1E7\a\x65\x2\x2\x1E7\x1E8\ag\x2\x2\x1E8\x1E9\ap\x2\x2\x1E9\x1EA\a\x66"+
		"\x2\x2\x1EA\x1EB\a\x63\x2\x2\x1EB\x1EC\ap\x2\x2\x1EC\x1ED\av\x2\x2\x1ED"+
		"\x1EE\a/\x2\x2\x1EE\x1EF\aq\x2\x2\x1EF\x1F0\at\x2\x2\x1F0\x1F1\a/\x2\x2"+
		"\x1F1\x1F2\au\x2\x2\x1F2\x1F3\ag\x2\x2\x1F3\x1F4\an\x2\x2\x1F4\x23D\a"+
		"h\x2\x2\x1F5\x1F6\ah\x2\x2\x1F6\x1F7\aq\x2\x2\x1F7\x1F8\an\x2\x2\x1F8"+
		"\x1F9\an\x2\x2\x1F9\x1FA\aq\x2\x2\x1FA\x1FB\ay\x2\x2\x1FB\x1FC\ak\x2\x2"+
		"\x1FC\x1FD\ap\x2\x2\x1FD\x23D\ai\x2\x2\x1FE\x1FF\ah\x2\x2\x1FF\x200\a"+
		"q\x2\x2\x200\x201\an\x2\x2\x201\x202\an\x2\x2\x202\x203\aq\x2\x2\x203"+
		"\x204\ay\x2\x2\x204\x205\ak\x2\x2\x205\x206\ap\x2\x2\x206\x207\ai\x2\x2"+
		"\x207\x208\a/\x2\x2\x208\x209\au\x2\x2\x209\x20A\ak\x2\x2\x20A\x20B\a"+
		"\x64\x2\x2\x20B\x20C\an\x2\x2\x20C\x20D\ak\x2\x2\x20D\x20E\ap\x2\x2\x20E"+
		"\x23D\ai\x2\x2\x20F\x210\ap\x2\x2\x210\x211\a\x63\x2\x2\x211\x212\ao\x2"+
		"\x2\x212\x213\ag\x2\x2\x213\x214\au\x2\x2\x214\x215\ar\x2\x2\x215\x216"+
		"\a\x63\x2\x2\x216\x217\a\x65\x2\x2\x217\x23D\ag\x2\x2\x218\x219\ar\x2"+
		"\x2\x219\x21A\a\x63\x2\x2\x21A\x21B\at\x2\x2\x21B\x21C\ag\x2\x2\x21C\x21D"+
		"\ap\x2\x2\x21D\x23D\av\x2\x2\x21E\x21F\ar\x2\x2\x21F\x220\at\x2\x2\x220"+
		"\x221\ag\x2\x2\x221\x222\a\x65\x2\x2\x222\x223\ag\x2\x2\x223\x224\a\x66"+
		"\x2\x2\x224\x225\ak\x2\x2\x225\x226\ap\x2\x2\x226\x23D\ai\x2\x2\x227\x228"+
		"\ar\x2\x2\x228\x229\at\x2\x2\x229\x22A\ag\x2\x2\x22A\x22B\a\x65\x2\x2"+
		"\x22B\x22C\ag\x2\x2\x22C\x22D\a\x66\x2\x2\x22D\x22E\ak\x2\x2\x22E\x22F"+
		"\ap\x2\x2\x22F\x230\ai\x2\x2\x230\x231\a/\x2\x2\x231\x232\au\x2\x2\x232"+
		"\x233\ak\x2\x2\x233\x234\a\x64\x2\x2\x234\x235\an\x2\x2\x235\x236\ak\x2"+
		"\x2\x236\x237\ap\x2\x2\x237\x23D\ai\x2\x2\x238\x239\au\x2\x2\x239\x23A"+
		"\ag\x2\x2\x23A\x23B\an\x2\x2\x23B\x23D\ah\x2\x2\x23C\x1B3\x3\x2\x2\x2"+
		"\x23C\x1BB\x3\x2\x2\x2\x23C\x1CB\x3\x2\x2\x2\x23C\x1D4\x3\x2\x2\x2\x23C"+
		"\x1D9\x3\x2\x2\x2\x23C\x1E3\x3\x2\x2\x2\x23C\x1F5\x3\x2\x2\x2\x23C\x1FE"+
		"\x3\x2\x2\x2\x23C\x20F\x3\x2\x2\x2\x23C\x218\x3\x2\x2\x2\x23C\x21E\x3"+
		"\x2\x2\x2\x23C\x227\x3\x2\x2\x2\x23C\x238\x3\x2\x2\x2\x23Dj\x3\x2\x2\x2"+
		"\x23E\x242\a$\x2\x2\x23F\x241\n\x3\x2\x2\x240\x23F\x3\x2\x2\x2\x241\x244"+
		"\x3\x2\x2\x2\x242\x240\x3\x2\x2\x2\x242\x243\x3\x2\x2\x2\x243\x245\x3"+
		"\x2\x2\x2\x244\x242\x3\x2\x2\x2\x245\x24F\a$\x2\x2\x246\x24A\a)\x2\x2"+
		"\x247\x249\n\x4\x2\x2\x248\x247\x3\x2\x2\x2\x249\x24C\x3\x2\x2\x2\x24A"+
		"\x248\x3\x2\x2\x2\x24A\x24B\x3\x2\x2\x2\x24B\x24D\x3\x2\x2\x2\x24C\x24A"+
		"\x3\x2\x2\x2\x24D\x24F\a)\x2\x2\x24E\x23E\x3\x2\x2\x2\x24E\x246\x3\x2"+
		"\x2\x2\x24Fl\x3\x2\x2\x2\x250\x252\t\x5\x2\x2\x251\x250\x3\x2\x2\x2\x252"+
		"\x253\x3\x2\x2\x2\x253\x251\x3\x2\x2\x2\x253\x254\x3\x2\x2\x2\x254\x255"+
		"\x3\x2\x2\x2\x255\x256\b\x37\x2\x2\x256n\x3\x2\x2\x2\x257\x25B\x5q\x39"+
		"\x2\x258\x25A\x5s:\x2\x259\x258\x3\x2\x2\x2\x25A\x25D\x3\x2\x2\x2\x25B"+
		"\x259\x3\x2\x2\x2\x25B\x25C\x3\x2\x2\x2\x25Cp\x3\x2\x2\x2\x25D\x25B\x3"+
		"\x2\x2\x2\x25E\x25F\t\x6\x2\x2\x25Fr\x3\x2\x2\x2\x260\x263\x5q\x39\x2"+
		"\x261\x263\t\a\x2\x2\x262\x260\x3\x2\x2\x2\x262\x261\x3\x2\x2\x2\x263"+
		"t\x3\x2\x2\x2\x16\x2\x162\x166\x181\x189\x18F\x196\x19B\x1A1\x1A6\x1A8"+
		"\x1AC\x1B1\x23C\x242\x24A\x24E\x253\x25B\x262\x3\b\x2\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
