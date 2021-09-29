//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Dana\source\repos\LabWork1github\LabWork1github\DynamicTrap.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace LabWork1github {
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public partial class DynamicTrapLexer : Lexer {
	public const int
		DISTANCE=1, DAMAGE=2, DIRECTION=3, IN=4, TRAP=5, MONSTER=6, ROUND=7, ME=8, 
		IF=9, RANDOM=10, TO=11, PLACE_T=12, NEAR=13, IS=14, ON=15, WHILE=16, HEALTH=17, 
		ALIVE=18, MOVE=19, SPAWN=20, TELEPORT_T=21, HEAL=22, RANGE_T=23, NAME_T=24, 
		PLAYER=25, EFFECT_T=26, EQUALS=27, ABSOLUTE=28, EXPRESSIONCONNECTER=29, 
		COMPARE=30, NUMOPERATION=31, PARENTHESISSTART=32, PARENTHESISCLOSE=33, 
		BRACKETCLOSE=34, BRACKETSTART=35, COLON=36, SEMI=37, ATTRIBUTE=38, COMMA=39, 
		NUMBER=40, ID=41, WS=42;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"DISTANCE", "DAMAGE", "DIRECTION", "IN", "TRAP", "MONSTER", "ROUND", "ME", 
		"IF", "RANDOM", "TO", "PLACE_T", "NEAR", "IS", "ON", "WHILE", "HEALTH", 
		"ALIVE", "MOVE", "SPAWN", "TELEPORT_T", "HEAL", "RANGE_T", "NAME_T", "PLAYER", 
		"EFFECT_T", "EQUALS", "ABSOLUTE", "EXPRESSIONCONNECTER", "COMPARE", "NUMOPERATION", 
		"PARENTHESISSTART", "PARENTHESISCLOSE", "BRACKETCLOSE", "BRACKETSTART", 
		"COLON", "SEMI", "ATTRIBUTE", "COMMA", "NUMBER", "ID", "WS"
	};


	public DynamicTrapLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, "'distance'", "'damage'", null, "'in'", "'trap'", "'monster'", "'round'", 
		"'me'", "'if'", "'random'", "'to'", "'place'", "'near'", "'is'", "'ON'", 
		"'while'", "'HP'", "'alive'", "'move'", "'spawn'", "'teleport'", "'heal'", 
		"'range'", "'name'", "'player'", "'effect'", "'='", "'|'", null, null, 
		null, "'('", "')'", "'}'", "'{'", "':'", "';'", "'.'", "','"
	};
	private static readonly string[] _SymbolicNames = {
		null, "DISTANCE", "DAMAGE", "DIRECTION", "IN", "TRAP", "MONSTER", "ROUND", 
		"ME", "IF", "RANDOM", "TO", "PLACE_T", "NEAR", "IS", "ON", "WHILE", "HEALTH", 
		"ALIVE", "MOVE", "SPAWN", "TELEPORT_T", "HEAL", "RANGE_T", "NAME_T", "PLAYER", 
		"EFFECT_T", "EQUALS", "ABSOLUTE", "EXPRESSIONCONNECTER", "COMPARE", "NUMOPERATION", 
		"PARENTHESISSTART", "PARENTHESISCLOSE", "BRACKETCLOSE", "BRACKETSTART", 
		"COLON", "SEMI", "ATTRIBUTE", "COMMA", "NUMBER", "ID", "WS"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[System.Obsolete("Use Vocabulary instead.")]
	public static readonly string[] tokenNames = GenerateTokenNames(DefaultVocabulary, _SymbolicNames.Length);

	private static string[] GenerateTokenNames(IVocabulary vocabulary, int length) {
		string[] tokenNames = new string[length];
		for (int i = 0; i < tokenNames.Length; i++) {
			tokenNames[i] = vocabulary.GetLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = vocabulary.GetSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}

		return tokenNames;
	}

	[System.Obsolete("Use IRecognizer.Vocabulary instead.")]
	public override string[] TokenNames
	{
		get
		{
			return tokenNames;
		}
	}

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "DynamicTrap.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2,\x114\b\x1\x4\x2"+
		"\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4"+
		"\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10"+
		"\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15\t\x15"+
		"\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A\x4\x1B"+
		"\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 \t \x4!"+
		"\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t(\x4)\t)\x4*\t"+
		"*\x4+\t+\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x3\x3"+
		"\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\x3\x6"+
		"\x3\x6\x3\x6\x3\x6\x3\x6\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\b"+
		"\x3\b\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t\x3\t\x3\n\x3\n\x3\n\x3\v\x3\v\x3\v"+
		"\x3\v\x3\v\x3\v\x3\v\x3\f\x3\f\x3\f\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3\xE"+
		"\x3\xE\x3\xE\x3\xE\x3\xE\x3\xF\x3\xF\x3\xF\x3\x10\x3\x10\x3\x10\x3\x11"+
		"\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x12\x3\x12\x3\x12\x3\x13\x3\x13"+
		"\x3\x13\x3\x13\x3\x13\x3\x13\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x15"+
		"\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16"+
		"\x3\x16\x3\x16\x3\x16\x3\x16\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x18"+
		"\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19"+
		"\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1B"+
		"\x3\x1B\x3\x1B\x3\x1B\x3\x1B\x3\x1C\x3\x1C\x3\x1D\x3\x1D\x3\x1E\x3\x1E"+
		"\x3\x1E\x3\x1E\x5\x1E\xEA\n\x1E\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x5"+
		"\x1F\xF1\n\x1F\x3 \x3 \x3!\x3!\x3\"\x3\"\x3#\x3#\x3$\x3$\x3%\x3%\x3&\x3"+
		"&\x3\'\x3\'\x3(\x3(\x3)\x6)\x106\n)\r)\xE)\x107\x3*\x3*\a*\x10C\n*\f*"+
		"\xE*\x10F\v*\x3+\x3+\x3+\x3+\x2\x2\x2,\x3\x2\x3\x5\x2\x4\a\x2\x5\t\x2"+
		"\x6\v\x2\a\r\x2\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17\x2\r\x19\x2\xE"+
		"\x1B\x2\xF\x1D\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14\'\x2\x15)\x2"+
		"\x16+\x2\x17-\x2\x18/\x2\x19\x31\x2\x1A\x33\x2\x1B\x35\x2\x1C\x37\x2\x1D"+
		"\x39\x2\x1E;\x2\x1F=\x2 ?\x2!\x41\x2\"\x43\x2#\x45\x2$G\x2%I\x2&K\x2\'"+
		"M\x2(O\x2)Q\x2*S\x2+U\x2,\x3\x2\t\x6\x2\x44\x44HHNNTT\x4\x2>>@@\x6\x2"+
		"\'\',-//\x31\x31\x3\x2\x32;\x4\x2\x43\\\x63|\x6\x2\x32;\x43\\\x61\x61"+
		"\x63|\x5\x2\v\f\xF\xF\"\"\x118\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2"+
		"\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF"+
		"\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2"+
		"\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2"+
		"\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2"+
		"\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2\x2+\x3\x2\x2\x2\x2-\x3\x2\x2\x2\x2"+
		"/\x3\x2\x2\x2\x2\x31\x3\x2\x2\x2\x2\x33\x3\x2\x2\x2\x2\x35\x3\x2\x2\x2"+
		"\x2\x37\x3\x2\x2\x2\x2\x39\x3\x2\x2\x2\x2;\x3\x2\x2\x2\x2=\x3\x2\x2\x2"+
		"\x2?\x3\x2\x2\x2\x2\x41\x3\x2\x2\x2\x2\x43\x3\x2\x2\x2\x2\x45\x3\x2\x2"+
		"\x2\x2G\x3\x2\x2\x2\x2I\x3\x2\x2\x2\x2K\x3\x2\x2\x2\x2M\x3\x2\x2\x2\x2"+
		"O\x3\x2\x2\x2\x2Q\x3\x2\x2\x2\x2S\x3\x2\x2\x2\x2U\x3\x2\x2\x2\x3W\x3\x2"+
		"\x2\x2\x5`\x3\x2\x2\x2\ag\x3\x2\x2\x2\ti\x3\x2\x2\x2\vl\x3\x2\x2\x2\r"+
		"q\x3\x2\x2\x2\xFy\x3\x2\x2\x2\x11\x7F\x3\x2\x2\x2\x13\x82\x3\x2\x2\x2"+
		"\x15\x85\x3\x2\x2\x2\x17\x8C\x3\x2\x2\x2\x19\x8F\x3\x2\x2\x2\x1B\x95\x3"+
		"\x2\x2\x2\x1D\x9A\x3\x2\x2\x2\x1F\x9D\x3\x2\x2\x2!\xA0\x3\x2\x2\x2#\xA6"+
		"\x3\x2\x2\x2%\xA9\x3\x2\x2\x2\'\xAF\x3\x2\x2\x2)\xB4\x3\x2\x2\x2+\xBA"+
		"\x3\x2\x2\x2-\xC3\x3\x2\x2\x2/\xC8\x3\x2\x2\x2\x31\xCE\x3\x2\x2\x2\x33"+
		"\xD3\x3\x2\x2\x2\x35\xDA\x3\x2\x2\x2\x37\xE1\x3\x2\x2\x2\x39\xE3\x3\x2"+
		"\x2\x2;\xE9\x3\x2\x2\x2=\xF0\x3\x2\x2\x2?\xF2\x3\x2\x2\x2\x41\xF4\x3\x2"+
		"\x2\x2\x43\xF6\x3\x2\x2\x2\x45\xF8\x3\x2\x2\x2G\xFA\x3\x2\x2\x2I\xFC\x3"+
		"\x2\x2\x2K\xFE\x3\x2\x2\x2M\x100\x3\x2\x2\x2O\x102\x3\x2\x2\x2Q\x105\x3"+
		"\x2\x2\x2S\x109\x3\x2\x2\x2U\x110\x3\x2\x2\x2WX\a\x66\x2\x2XY\ak\x2\x2"+
		"YZ\au\x2\x2Z[\av\x2\x2[\\\a\x63\x2\x2\\]\ap\x2\x2]^\a\x65\x2\x2^_\ag\x2"+
		"\x2_\x4\x3\x2\x2\x2`\x61\a\x66\x2\x2\x61\x62\a\x63\x2\x2\x62\x63\ao\x2"+
		"\x2\x63\x64\a\x63\x2\x2\x64\x65\ai\x2\x2\x65\x66\ag\x2\x2\x66\x6\x3\x2"+
		"\x2\x2gh\t\x2\x2\x2h\b\x3\x2\x2\x2ij\ak\x2\x2jk\ap\x2\x2k\n\x3\x2\x2\x2"+
		"lm\av\x2\x2mn\at\x2\x2no\a\x63\x2\x2op\ar\x2\x2p\f\x3\x2\x2\x2qr\ao\x2"+
		"\x2rs\aq\x2\x2st\ap\x2\x2tu\au\x2\x2uv\av\x2\x2vw\ag\x2\x2wx\at\x2\x2"+
		"x\xE\x3\x2\x2\x2yz\at\x2\x2z{\aq\x2\x2{|\aw\x2\x2|}\ap\x2\x2}~\a\x66\x2"+
		"\x2~\x10\x3\x2\x2\x2\x7F\x80\ao\x2\x2\x80\x81\ag\x2\x2\x81\x12\x3\x2\x2"+
		"\x2\x82\x83\ak\x2\x2\x83\x84\ah\x2\x2\x84\x14\x3\x2\x2\x2\x85\x86\at\x2"+
		"\x2\x86\x87\a\x63\x2\x2\x87\x88\ap\x2\x2\x88\x89\a\x66\x2\x2\x89\x8A\a"+
		"q\x2\x2\x8A\x8B\ao\x2\x2\x8B\x16\x3\x2\x2\x2\x8C\x8D\av\x2\x2\x8D\x8E"+
		"\aq\x2\x2\x8E\x18\x3\x2\x2\x2\x8F\x90\ar\x2\x2\x90\x91\an\x2\x2\x91\x92"+
		"\a\x63\x2\x2\x92\x93\a\x65\x2\x2\x93\x94\ag\x2\x2\x94\x1A\x3\x2\x2\x2"+
		"\x95\x96\ap\x2\x2\x96\x97\ag\x2\x2\x97\x98\a\x63\x2\x2\x98\x99\at\x2\x2"+
		"\x99\x1C\x3\x2\x2\x2\x9A\x9B\ak\x2\x2\x9B\x9C\au\x2\x2\x9C\x1E\x3\x2\x2"+
		"\x2\x9D\x9E\aQ\x2\x2\x9E\x9F\aP\x2\x2\x9F \x3\x2\x2\x2\xA0\xA1\ay\x2\x2"+
		"\xA1\xA2\aj\x2\x2\xA2\xA3\ak\x2\x2\xA3\xA4\an\x2\x2\xA4\xA5\ag\x2\x2\xA5"+
		"\"\x3\x2\x2\x2\xA6\xA7\aJ\x2\x2\xA7\xA8\aR\x2\x2\xA8$\x3\x2\x2\x2\xA9"+
		"\xAA\a\x63\x2\x2\xAA\xAB\an\x2\x2\xAB\xAC\ak\x2\x2\xAC\xAD\ax\x2\x2\xAD"+
		"\xAE\ag\x2\x2\xAE&\x3\x2\x2\x2\xAF\xB0\ao\x2\x2\xB0\xB1\aq\x2\x2\xB1\xB2"+
		"\ax\x2\x2\xB2\xB3\ag\x2\x2\xB3(\x3\x2\x2\x2\xB4\xB5\au\x2\x2\xB5\xB6\a"+
		"r\x2\x2\xB6\xB7\a\x63\x2\x2\xB7\xB8\ay\x2\x2\xB8\xB9\ap\x2\x2\xB9*\x3"+
		"\x2\x2\x2\xBA\xBB\av\x2\x2\xBB\xBC\ag\x2\x2\xBC\xBD\an\x2\x2\xBD\xBE\a"+
		"g\x2\x2\xBE\xBF\ar\x2\x2\xBF\xC0\aq\x2\x2\xC0\xC1\at\x2\x2\xC1\xC2\av"+
		"\x2\x2\xC2,\x3\x2\x2\x2\xC3\xC4\aj\x2\x2\xC4\xC5\ag\x2\x2\xC5\xC6\a\x63"+
		"\x2\x2\xC6\xC7\an\x2\x2\xC7.\x3\x2\x2\x2\xC8\xC9\at\x2\x2\xC9\xCA\a\x63"+
		"\x2\x2\xCA\xCB\ap\x2\x2\xCB\xCC\ai\x2\x2\xCC\xCD\ag\x2\x2\xCD\x30\x3\x2"+
		"\x2\x2\xCE\xCF\ap\x2\x2\xCF\xD0\a\x63\x2\x2\xD0\xD1\ao\x2\x2\xD1\xD2\a"+
		"g\x2\x2\xD2\x32\x3\x2\x2\x2\xD3\xD4\ar\x2\x2\xD4\xD5\an\x2\x2\xD5\xD6"+
		"\a\x63\x2\x2\xD6\xD7\a{\x2\x2\xD7\xD8\ag\x2\x2\xD8\xD9\at\x2\x2\xD9\x34"+
		"\x3\x2\x2\x2\xDA\xDB\ag\x2\x2\xDB\xDC\ah\x2\x2\xDC\xDD\ah\x2\x2\xDD\xDE"+
		"\ag\x2\x2\xDE\xDF\a\x65\x2\x2\xDF\xE0\av\x2\x2\xE0\x36\x3\x2\x2\x2\xE1"+
		"\xE2\a?\x2\x2\xE2\x38\x3\x2\x2\x2\xE3\xE4\a~\x2\x2\xE4:\x3\x2\x2\x2\xE5"+
		"\xE6\a~\x2\x2\xE6\xEA\a~\x2\x2\xE7\xE8\a(\x2\x2\xE8\xEA\a(\x2\x2\xE9\xE5"+
		"\x3\x2\x2\x2\xE9\xE7\x3\x2\x2\x2\xEA<\x3\x2\x2\x2\xEB\xF1\t\x3\x2\x2\xEC"+
		"\xED\a?\x2\x2\xED\xF1\a?\x2\x2\xEE\xEF\a#\x2\x2\xEF\xF1\a?\x2\x2\xF0\xEB"+
		"\x3\x2\x2\x2\xF0\xEC\x3\x2\x2\x2\xF0\xEE\x3\x2\x2\x2\xF1>\x3\x2\x2\x2"+
		"\xF2\xF3\t\x4\x2\x2\xF3@\x3\x2\x2\x2\xF4\xF5\a*\x2\x2\xF5\x42\x3\x2\x2"+
		"\x2\xF6\xF7\a+\x2\x2\xF7\x44\x3\x2\x2\x2\xF8\xF9\a\x7F\x2\x2\xF9\x46\x3"+
		"\x2\x2\x2\xFA\xFB\a}\x2\x2\xFBH\x3\x2\x2\x2\xFC\xFD\a<\x2\x2\xFDJ\x3\x2"+
		"\x2\x2\xFE\xFF\a=\x2\x2\xFFL\x3\x2\x2\x2\x100\x101\a\x30\x2\x2\x101N\x3"+
		"\x2\x2\x2\x102\x103\a.\x2\x2\x103P\x3\x2\x2\x2\x104\x106\t\x5\x2\x2\x105"+
		"\x104\x3\x2\x2\x2\x106\x107\x3\x2\x2\x2\x107\x105\x3\x2\x2\x2\x107\x108"+
		"\x3\x2\x2\x2\x108R\x3\x2\x2\x2\x109\x10D\t\x6\x2\x2\x10A\x10C\t\a\x2\x2"+
		"\x10B\x10A\x3\x2\x2\x2\x10C\x10F\x3\x2\x2\x2\x10D\x10B\x3\x2\x2\x2\x10D"+
		"\x10E\x3\x2\x2\x2\x10ET\x3\x2\x2\x2\x10F\x10D\x3\x2\x2\x2\x110\x111\t"+
		"\b\x2\x2\x111\x112\x3\x2\x2\x2\x112\x113\b+\x2\x2\x113V\x3\x2\x2\x2\a"+
		"\x2\xE9\xF0\x107\x10D\x3\b\x2\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace LabWork1github