//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Dana\source\repos\LabWork1github\LabWork1github\g4 files\DynamicMonster.g4 by ANTLR 4.6.6

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
public partial class DynamicMonsterLexer : Lexer {
	public const int
		NOTHING=1, RANDOM=2, DISTANCE=3, DAMAGE=4, DIRECTION=5, NAME_T=6, IN=7, 
		TRAP=8, PLAYER=9, PLACE_T=10, ROUND=11, NEAR=12, IS=13, ME=14, IF=15, 
		TO=16, WHILE=17, HEALTH=18, ALIVE=19, MOVE=20, SHOOT=21, EQUALS=22, ABSOLUTE=23, 
		NEGATE=24, BOOLCONNECTER=25, COMPARE=26, NUMCONNECTER=27, PARENTHESISSTART=28, 
		PARENTHESISCLOSE=29, BRACKETCLOSE=30, BRACKETSTART=31, COLON=32, SEMI=33, 
		COMMA=34, ATTRIBUTE=35, NUMBER=36, ID=37, WS=38;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"NOTHING", "RANDOM", "DISTANCE", "DAMAGE", "DIRECTION", "NAME_T", "IN", 
		"TRAP", "PLAYER", "PLACE_T", "ROUND", "NEAR", "IS", "ME", "IF", "TO", 
		"WHILE", "HEALTH", "ALIVE", "MOVE", "SHOOT", "EQUALS", "ABSOLUTE", "NEGATE", 
		"BOOLCONNECTER", "COMPARE", "NUMCONNECTER", "PARENTHESISSTART", "PARENTHESISCLOSE", 
		"BRACKETCLOSE", "BRACKETSTART", "COLON", "SEMI", "COMMA", "ATTRIBUTE", 
		"NUMBER", "ID", "WS"
	};


	public DynamicMonsterLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, "'nothing'", "'random'", "'distance'", "'damage'", null, "'name'", 
		"'in'", "'trap'", "'player'", "'place'", "'round'", "'near'", "'is'", 
		"'me'", "'if'", "'to'", "'while'", "'health'", "'alive'", "'move'", "'shoot'", 
		"'='", "'|'", "'!'", null, null, null, "'('", "')'", "'}'", "'{'", "':'", 
		"';'", "','", "'.'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "NOTHING", "RANDOM", "DISTANCE", "DAMAGE", "DIRECTION", "NAME_T", 
		"IN", "TRAP", "PLAYER", "PLACE_T", "ROUND", "NEAR", "IS", "ME", "IF", 
		"TO", "WHILE", "HEALTH", "ALIVE", "MOVE", "SHOOT", "EQUALS", "ABSOLUTE", 
		"NEGATE", "BOOLCONNECTER", "COMPARE", "NUMCONNECTER", "PARENTHESISSTART", 
		"PARENTHESISCLOSE", "BRACKETCLOSE", "BRACKETSTART", "COLON", "SEMI", "COMMA", 
		"ATTRIBUTE", "NUMBER", "ID", "WS"
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

	public override string GrammarFileName { get { return "DynamicMonster.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2(\xFC\b\x1\x4\x2"+
		"\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4"+
		"\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10"+
		"\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15\t\x15"+
		"\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A\x4\x1B"+
		"\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 \t \x4!"+
		"\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x3\x2\x3\x2\x3\x2\x3"+
		"\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3"+
		"\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x5\x3\x5\x3"+
		"\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x6\x3\x6\x3\a\x3\a\x3\a\x3\a\x3\a\x3\b"+
		"\x3\b\x3\b\x3\t\x3\t\x3\t\x3\t\x3\t\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n"+
		"\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\r\x3\r"+
		"\x3\r\x3\r\x3\r\x3\xE\x3\xE\x3\xE\x3\xF\x3\xF\x3\xF\x3\x10\x3\x10\x3\x10"+
		"\x3\x11\x3\x11\x3\x11\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x13"+
		"\x3\x13\x3\x13\x3\x13\x3\x13\x3\x13\x3\x13\x3\x14\x3\x14\x3\x14\x3\x14"+
		"\x3\x14\x3\x14\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x16\x3\x16\x3\x16"+
		"\x3\x16\x3\x16\x3\x16\x3\x17\x3\x17\x3\x18\x3\x18\x3\x19\x3\x19\x3\x1A"+
		"\x3\x1A\x3\x1A\x3\x1A\x5\x1A\xCA\n\x1A\x3\x1B\x3\x1B\x3\x1B\x3\x1B\x3"+
		"\x1B\x5\x1B\xD1\n\x1B\x3\x1C\x3\x1C\x3\x1D\x3\x1D\x3\x1E\x3\x1E\x3\x1F"+
		"\x3\x1F\x3 \x3 \x3!\x3!\x3\"\x3\"\x3#\x3#\x3$\x3$\x3%\x6%\xE6\n%\r%\xE"+
		"%\xE7\x3%\x3%\x6%\xEC\n%\r%\xE%\xED\x5%\xF0\n%\x3&\x3&\a&\xF4\n&\f&\xE"+
		"&\xF7\v&\x3\'\x3\'\x3\'\x3\'\x2\x2\x2(\x3\x2\x3\x5\x2\x4\a\x2\x5\t\x2"+
		"\x6\v\x2\a\r\x2\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17\x2\r\x19\x2\xE"+
		"\x1B\x2\xF\x1D\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14\'\x2\x15)\x2"+
		"\x16+\x2\x17-\x2\x18/\x2\x19\x31\x2\x1A\x33\x2\x1B\x35\x2\x1C\x37\x2\x1D"+
		"\x39\x2\x1E;\x2\x1F=\x2 ?\x2!\x41\x2\"\x43\x2#\x45\x2$G\x2%I\x2&K\x2\'"+
		"M\x2(\x3\x2\t\x6\x2\x44\x44HHNNTT\x4\x2>>@@\x6\x2\'\',-//\x31\x31\x3\x2"+
		"\x32;\x4\x2\x43\\\x63|\x6\x2\x32;\x43\\\x61\x61\x63|\x5\x2\v\f\xF\xF\""+
		"\"\x102\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2"+
		"\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2"+
		"\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19"+
		"\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2"+
		"\x2!\x3\x2\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)"+
		"\x3\x2\x2\x2\x2+\x3\x2\x2\x2\x2-\x3\x2\x2\x2\x2/\x3\x2\x2\x2\x2\x31\x3"+
		"\x2\x2\x2\x2\x33\x3\x2\x2\x2\x2\x35\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2\x2"+
		"\x39\x3\x2\x2\x2\x2;\x3\x2\x2\x2\x2=\x3\x2\x2\x2\x2?\x3\x2\x2\x2\x2\x41"+
		"\x3\x2\x2\x2\x2\x43\x3\x2\x2\x2\x2\x45\x3\x2\x2\x2\x2G\x3\x2\x2\x2\x2"+
		"I\x3\x2\x2\x2\x2K\x3\x2\x2\x2\x2M\x3\x2\x2\x2\x3O\x3\x2\x2\x2\x5W\x3\x2"+
		"\x2\x2\a^\x3\x2\x2\x2\tg\x3\x2\x2\x2\vn\x3\x2\x2\x2\rp\x3\x2\x2\x2\xF"+
		"u\x3\x2\x2\x2\x11x\x3\x2\x2\x2\x13}\x3\x2\x2\x2\x15\x84\x3\x2\x2\x2\x17"+
		"\x8A\x3\x2\x2\x2\x19\x90\x3\x2\x2\x2\x1B\x95\x3\x2\x2\x2\x1D\x98\x3\x2"+
		"\x2\x2\x1F\x9B\x3\x2\x2\x2!\x9E\x3\x2\x2\x2#\xA1\x3\x2\x2\x2%\xA7\x3\x2"+
		"\x2\x2\'\xAE\x3\x2\x2\x2)\xB4\x3\x2\x2\x2+\xB9\x3\x2\x2\x2-\xBF\x3\x2"+
		"\x2\x2/\xC1\x3\x2\x2\x2\x31\xC3\x3\x2\x2\x2\x33\xC9\x3\x2\x2\x2\x35\xD0"+
		"\x3\x2\x2\x2\x37\xD2\x3\x2\x2\x2\x39\xD4\x3\x2\x2\x2;\xD6\x3\x2\x2\x2"+
		"=\xD8\x3\x2\x2\x2?\xDA\x3\x2\x2\x2\x41\xDC\x3\x2\x2\x2\x43\xDE\x3\x2\x2"+
		"\x2\x45\xE0\x3\x2\x2\x2G\xE2\x3\x2\x2\x2I\xE5\x3\x2\x2\x2K\xF1\x3\x2\x2"+
		"\x2M\xF8\x3\x2\x2\x2OP\ap\x2\x2PQ\aq\x2\x2QR\av\x2\x2RS\aj\x2\x2ST\ak"+
		"\x2\x2TU\ap\x2\x2UV\ai\x2\x2V\x4\x3\x2\x2\x2WX\at\x2\x2XY\a\x63\x2\x2"+
		"YZ\ap\x2\x2Z[\a\x66\x2\x2[\\\aq\x2\x2\\]\ao\x2\x2]\x6\x3\x2\x2\x2^_\a"+
		"\x66\x2\x2_`\ak\x2\x2`\x61\au\x2\x2\x61\x62\av\x2\x2\x62\x63\a\x63\x2"+
		"\x2\x63\x64\ap\x2\x2\x64\x65\a\x65\x2\x2\x65\x66\ag\x2\x2\x66\b\x3\x2"+
		"\x2\x2gh\a\x66\x2\x2hi\a\x63\x2\x2ij\ao\x2\x2jk\a\x63\x2\x2kl\ai\x2\x2"+
		"lm\ag\x2\x2m\n\x3\x2\x2\x2no\t\x2\x2\x2o\f\x3\x2\x2\x2pq\ap\x2\x2qr\a"+
		"\x63\x2\x2rs\ao\x2\x2st\ag\x2\x2t\xE\x3\x2\x2\x2uv\ak\x2\x2vw\ap\x2\x2"+
		"w\x10\x3\x2\x2\x2xy\av\x2\x2yz\at\x2\x2z{\a\x63\x2\x2{|\ar\x2\x2|\x12"+
		"\x3\x2\x2\x2}~\ar\x2\x2~\x7F\an\x2\x2\x7F\x80\a\x63\x2\x2\x80\x81\a{\x2"+
		"\x2\x81\x82\ag\x2\x2\x82\x83\at\x2\x2\x83\x14\x3\x2\x2\x2\x84\x85\ar\x2"+
		"\x2\x85\x86\an\x2\x2\x86\x87\a\x63\x2\x2\x87\x88\a\x65\x2\x2\x88\x89\a"+
		"g\x2\x2\x89\x16\x3\x2\x2\x2\x8A\x8B\at\x2\x2\x8B\x8C\aq\x2\x2\x8C\x8D"+
		"\aw\x2\x2\x8D\x8E\ap\x2\x2\x8E\x8F\a\x66\x2\x2\x8F\x18\x3\x2\x2\x2\x90"+
		"\x91\ap\x2\x2\x91\x92\ag\x2\x2\x92\x93\a\x63\x2\x2\x93\x94\at\x2\x2\x94"+
		"\x1A\x3\x2\x2\x2\x95\x96\ak\x2\x2\x96\x97\au\x2\x2\x97\x1C\x3\x2\x2\x2"+
		"\x98\x99\ao\x2\x2\x99\x9A\ag\x2\x2\x9A\x1E\x3\x2\x2\x2\x9B\x9C\ak\x2\x2"+
		"\x9C\x9D\ah\x2\x2\x9D \x3\x2\x2\x2\x9E\x9F\av\x2\x2\x9F\xA0\aq\x2\x2\xA0"+
		"\"\x3\x2\x2\x2\xA1\xA2\ay\x2\x2\xA2\xA3\aj\x2\x2\xA3\xA4\ak\x2\x2\xA4"+
		"\xA5\an\x2\x2\xA5\xA6\ag\x2\x2\xA6$\x3\x2\x2\x2\xA7\xA8\aj\x2\x2\xA8\xA9"+
		"\ag\x2\x2\xA9\xAA\a\x63\x2\x2\xAA\xAB\an\x2\x2\xAB\xAC\av\x2\x2\xAC\xAD"+
		"\aj\x2\x2\xAD&\x3\x2\x2\x2\xAE\xAF\a\x63\x2\x2\xAF\xB0\an\x2\x2\xB0\xB1"+
		"\ak\x2\x2\xB1\xB2\ax\x2\x2\xB2\xB3\ag\x2\x2\xB3(\x3\x2\x2\x2\xB4\xB5\a"+
		"o\x2\x2\xB5\xB6\aq\x2\x2\xB6\xB7\ax\x2\x2\xB7\xB8\ag\x2\x2\xB8*\x3\x2"+
		"\x2\x2\xB9\xBA\au\x2\x2\xBA\xBB\aj\x2\x2\xBB\xBC\aq\x2\x2\xBC\xBD\aq\x2"+
		"\x2\xBD\xBE\av\x2\x2\xBE,\x3\x2\x2\x2\xBF\xC0\a?\x2\x2\xC0.\x3\x2\x2\x2"+
		"\xC1\xC2\a~\x2\x2\xC2\x30\x3\x2\x2\x2\xC3\xC4\a#\x2\x2\xC4\x32\x3\x2\x2"+
		"\x2\xC5\xC6\a~\x2\x2\xC6\xCA\a~\x2\x2\xC7\xC8\a(\x2\x2\xC8\xCA\a(\x2\x2"+
		"\xC9\xC5\x3\x2\x2\x2\xC9\xC7\x3\x2\x2\x2\xCA\x34\x3\x2\x2\x2\xCB\xD1\t"+
		"\x3\x2\x2\xCC\xCD\a?\x2\x2\xCD\xD1\a?\x2\x2\xCE\xCF\a#\x2\x2\xCF\xD1\a"+
		"?\x2\x2\xD0\xCB\x3\x2\x2\x2\xD0\xCC\x3\x2\x2\x2\xD0\xCE\x3\x2\x2\x2\xD1"+
		"\x36\x3\x2\x2\x2\xD2\xD3\t\x4\x2\x2\xD3\x38\x3\x2\x2\x2\xD4\xD5\a*\x2"+
		"\x2\xD5:\x3\x2\x2\x2\xD6\xD7\a+\x2\x2\xD7<\x3\x2\x2\x2\xD8\xD9\a\x7F\x2"+
		"\x2\xD9>\x3\x2\x2\x2\xDA\xDB\a}\x2\x2\xDB@\x3\x2\x2\x2\xDC\xDD\a<\x2\x2"+
		"\xDD\x42\x3\x2\x2\x2\xDE\xDF\a=\x2\x2\xDF\x44\x3\x2\x2\x2\xE0\xE1\a.\x2"+
		"\x2\xE1\x46\x3\x2\x2\x2\xE2\xE3\a\x30\x2\x2\xE3H\x3\x2\x2\x2\xE4\xE6\t"+
		"\x5\x2\x2\xE5\xE4\x3\x2\x2\x2\xE6\xE7\x3\x2\x2\x2\xE7\xE5\x3\x2\x2\x2"+
		"\xE7\xE8\x3\x2\x2\x2\xE8\xEF\x3\x2\x2\x2\xE9\xEB\a\x30\x2\x2\xEA\xEC\t"+
		"\x5\x2\x2\xEB\xEA\x3\x2\x2\x2\xEC\xED\x3\x2\x2\x2\xED\xEB\x3\x2\x2\x2"+
		"\xED\xEE\x3\x2\x2\x2\xEE\xF0\x3\x2\x2\x2\xEF\xE9\x3\x2\x2\x2\xEF\xF0\x3"+
		"\x2\x2\x2\xF0J\x3\x2\x2\x2\xF1\xF5\t\x6\x2\x2\xF2\xF4\t\a\x2\x2\xF3\xF2"+
		"\x3\x2\x2\x2\xF4\xF7\x3\x2\x2\x2\xF5\xF3\x3\x2\x2\x2\xF5\xF6\x3\x2\x2"+
		"\x2\xF6L\x3\x2\x2\x2\xF7\xF5\x3\x2\x2\x2\xF8\xF9\t\b\x2\x2\xF9\xFA\x3"+
		"\x2\x2\x2\xFA\xFB\b\'\x2\x2\xFBN\x3\x2\x2\x2\t\x2\xC9\xD0\xE7\xED\xEF"+
		"\xF5\x3\b\x2\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace LabWork1github
