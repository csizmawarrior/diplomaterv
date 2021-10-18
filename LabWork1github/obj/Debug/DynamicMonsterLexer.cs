//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Dana\source\repos\LabWork1github\LabWork1github\DynamicMonster.g4 by ANTLR 4.6.6

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
		RANDOM=1, DISTANCE=2, DAMAGE=3, DIRECTION=4, NAME_T=5, IN=6, TRAP=7, PLAYER=8, 
		PLACE_T=9, ROUND=10, NEAR=11, IS=12, ME=13, IF=14, TO=15, WHILE=16, HEALTH=17, 
		ALIVE=18, MOVE=19, SHOOT=20, EQUALS=21, ABSOLUTE=22, NEGATE=23, BOOLCONNECTER=24, 
		COMPARE=25, NUMCONNECTER=26, PARENTHESISSTART=27, PARENTHESISCLOSE=28, 
		BRACKETCLOSE=29, BRACKETSTART=30, COLON=31, SEMI=32, COMMA=33, ATTRIBUTE=34, 
		NUMBER=35, ID=36, WS=37;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"RANDOM", "DISTANCE", "DAMAGE", "DIRECTION", "NAME_T", "IN", "TRAP", "PLAYER", 
		"PLACE_T", "ROUND", "NEAR", "IS", "ME", "IF", "TO", "WHILE", "HEALTH", 
		"ALIVE", "MOVE", "SHOOT", "EQUALS", "ABSOLUTE", "NEGATE", "BOOLCONNECTER", 
		"COMPARE", "NUMCONNECTER", "PARENTHESISSTART", "PARENTHESISCLOSE", "BRACKETCLOSE", 
		"BRACKETSTART", "COLON", "SEMI", "COMMA", "ATTRIBUTE", "NUMBER", "ID", 
		"WS"
	};


	public DynamicMonsterLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, "'random'", "'distance'", "'damage'", null, "'name'", "'in'", "'trap'", 
		"'player'", "'place'", "'round'", "'near'", "'is'", "'me'", "'if'", "'to'", 
		"'while'", "'HP'", "'alive'", "'move'", "'shoot'", "'='", "'|'", "'!'", 
		null, null, null, "'('", "')'", "'}'", "'{'", "':'", "';'", "','", "'.'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "RANDOM", "DISTANCE", "DAMAGE", "DIRECTION", "NAME_T", "IN", "TRAP", 
		"PLAYER", "PLACE_T", "ROUND", "NEAR", "IS", "ME", "IF", "TO", "WHILE", 
		"HEALTH", "ALIVE", "MOVE", "SHOOT", "EQUALS", "ABSOLUTE", "NEGATE", "BOOLCONNECTER", 
		"COMPARE", "NUMCONNECTER", "PARENTHESISSTART", "PARENTHESISCLOSE", "BRACKETCLOSE", 
		"BRACKETSTART", "COLON", "SEMI", "COMMA", "ATTRIBUTE", "NUMBER", "ID", 
		"WS"
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
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\'\xE6\b\x1\x4\x2"+
		"\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4"+
		"\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10"+
		"\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15\t\x15"+
		"\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A\x4\x1B"+
		"\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 \t \x4!"+
		"\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2"+
		"\x3\x2\x3\x2\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3"+
		"\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x5\x3\x5\x3\x6\x3\x6\x3\x6"+
		"\x3\x6\x3\x6\x3\a\x3\a\x3\a\x3\b\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t\x3\t\x3"+
		"\t\x3\t\x3\t\x3\t\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\v\x3\v\x3\v\x3\v\x3"+
		"\v\x3\v\x3\f\x3\f\x3\f\x3\f\x3\f\x3\r\x3\r\x3\r\x3\xE\x3\xE\x3\xE\x3\xF"+
		"\x3\xF\x3\xF\x3\x10\x3\x10\x3\x10\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3"+
		"\x11\x3\x12\x3\x12\x3\x12\x3\x13\x3\x13\x3\x13\x3\x13\x3\x13\x3\x13\x3"+
		"\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3"+
		"\x15\x3\x16\x3\x16\x3\x17\x3\x17\x3\x18\x3\x18\x3\x19\x3\x19\x3\x19\x3"+
		"\x19\x5\x19\xBC\n\x19\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x5\x1A\xC3\n"+
		"\x1A\x3\x1B\x3\x1B\x3\x1C\x3\x1C\x3\x1D\x3\x1D\x3\x1E\x3\x1E\x3\x1F\x3"+
		"\x1F\x3 \x3 \x3!\x3!\x3\"\x3\"\x3#\x3#\x3$\x6$\xD8\n$\r$\xE$\xD9\x3%\x3"+
		"%\a%\xDE\n%\f%\xE%\xE1\v%\x3&\x3&\x3&\x3&\x2\x2\x2\'\x3\x2\x3\x5\x2\x4"+
		"\a\x2\x5\t\x2\x6\v\x2\a\r\x2\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17"+
		"\x2\r\x19\x2\xE\x1B\x2\xF\x1D\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14"+
		"\'\x2\x15)\x2\x16+\x2\x17-\x2\x18/\x2\x19\x31\x2\x1A\x33\x2\x1B\x35\x2"+
		"\x1C\x37\x2\x1D\x39\x2\x1E;\x2\x1F=\x2 ?\x2!\x41\x2\"\x43\x2#\x45\x2$"+
		"G\x2%I\x2&K\x2\'\x3\x2\t\x6\x2\x44\x44HHNNTT\x4\x2>>@@\x6\x2\'\',-//\x31"+
		"\x31\x3\x2\x32;\x4\x2\x43\\\x63|\x6\x2\x32;\x43\\\x61\x61\x63|\x5\x2\v"+
		"\f\xF\xF\"\"\xEA\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2\x2"+
		"\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF\x3\x2\x2\x2"+
		"\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2\x2\x17\x3\x2"+
		"\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2\x1F"+
		"\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'\x3\x2"+
		"\x2\x2\x2)\x3\x2\x2\x2\x2+\x3\x2\x2\x2\x2-\x3\x2\x2\x2\x2/\x3\x2\x2\x2"+
		"\x2\x31\x3\x2\x2\x2\x2\x33\x3\x2\x2\x2\x2\x35\x3\x2\x2\x2\x2\x37\x3\x2"+
		"\x2\x2\x2\x39\x3\x2\x2\x2\x2;\x3\x2\x2\x2\x2=\x3\x2\x2\x2\x2?\x3\x2\x2"+
		"\x2\x2\x41\x3\x2\x2\x2\x2\x43\x3\x2\x2\x2\x2\x45\x3\x2\x2\x2\x2G\x3\x2"+
		"\x2\x2\x2I\x3\x2\x2\x2\x2K\x3\x2\x2\x2\x3M\x3\x2\x2\x2\x5T\x3\x2\x2\x2"+
		"\a]\x3\x2\x2\x2\t\x64\x3\x2\x2\x2\v\x66\x3\x2\x2\x2\rk\x3\x2\x2\x2\xF"+
		"n\x3\x2\x2\x2\x11s\x3\x2\x2\x2\x13z\x3\x2\x2\x2\x15\x80\x3\x2\x2\x2\x17"+
		"\x86\x3\x2\x2\x2\x19\x8B\x3\x2\x2\x2\x1B\x8E\x3\x2\x2\x2\x1D\x91\x3\x2"+
		"\x2\x2\x1F\x94\x3\x2\x2\x2!\x97\x3\x2\x2\x2#\x9D\x3\x2\x2\x2%\xA0\x3\x2"+
		"\x2\x2\'\xA6\x3\x2\x2\x2)\xAB\x3\x2\x2\x2+\xB1\x3\x2\x2\x2-\xB3\x3\x2"+
		"\x2\x2/\xB5\x3\x2\x2\x2\x31\xBB\x3\x2\x2\x2\x33\xC2\x3\x2\x2\x2\x35\xC4"+
		"\x3\x2\x2\x2\x37\xC6\x3\x2\x2\x2\x39\xC8\x3\x2\x2\x2;\xCA\x3\x2\x2\x2"+
		"=\xCC\x3\x2\x2\x2?\xCE\x3\x2\x2\x2\x41\xD0\x3\x2\x2\x2\x43\xD2\x3\x2\x2"+
		"\x2\x45\xD4\x3\x2\x2\x2G\xD7\x3\x2\x2\x2I\xDB\x3\x2\x2\x2K\xE2\x3\x2\x2"+
		"\x2MN\at\x2\x2NO\a\x63\x2\x2OP\ap\x2\x2PQ\a\x66\x2\x2QR\aq\x2\x2RS\ao"+
		"\x2\x2S\x4\x3\x2\x2\x2TU\a\x66\x2\x2UV\ak\x2\x2VW\au\x2\x2WX\av\x2\x2"+
		"XY\a\x63\x2\x2YZ\ap\x2\x2Z[\a\x65\x2\x2[\\\ag\x2\x2\\\x6\x3\x2\x2\x2]"+
		"^\a\x66\x2\x2^_\a\x63\x2\x2_`\ao\x2\x2`\x61\a\x63\x2\x2\x61\x62\ai\x2"+
		"\x2\x62\x63\ag\x2\x2\x63\b\x3\x2\x2\x2\x64\x65\t\x2\x2\x2\x65\n\x3\x2"+
		"\x2\x2\x66g\ap\x2\x2gh\a\x63\x2\x2hi\ao\x2\x2ij\ag\x2\x2j\f\x3\x2\x2\x2"+
		"kl\ak\x2\x2lm\ap\x2\x2m\xE\x3\x2\x2\x2no\av\x2\x2op\at\x2\x2pq\a\x63\x2"+
		"\x2qr\ar\x2\x2r\x10\x3\x2\x2\x2st\ar\x2\x2tu\an\x2\x2uv\a\x63\x2\x2vw"+
		"\a{\x2\x2wx\ag\x2\x2xy\at\x2\x2y\x12\x3\x2\x2\x2z{\ar\x2\x2{|\an\x2\x2"+
		"|}\a\x63\x2\x2}~\a\x65\x2\x2~\x7F\ag\x2\x2\x7F\x14\x3\x2\x2\x2\x80\x81"+
		"\at\x2\x2\x81\x82\aq\x2\x2\x82\x83\aw\x2\x2\x83\x84\ap\x2\x2\x84\x85\a"+
		"\x66\x2\x2\x85\x16\x3\x2\x2\x2\x86\x87\ap\x2\x2\x87\x88\ag\x2\x2\x88\x89"+
		"\a\x63\x2\x2\x89\x8A\at\x2\x2\x8A\x18\x3\x2\x2\x2\x8B\x8C\ak\x2\x2\x8C"+
		"\x8D\au\x2\x2\x8D\x1A\x3\x2\x2\x2\x8E\x8F\ao\x2\x2\x8F\x90\ag\x2\x2\x90"+
		"\x1C\x3\x2\x2\x2\x91\x92\ak\x2\x2\x92\x93\ah\x2\x2\x93\x1E\x3\x2\x2\x2"+
		"\x94\x95\av\x2\x2\x95\x96\aq\x2\x2\x96 \x3\x2\x2\x2\x97\x98\ay\x2\x2\x98"+
		"\x99\aj\x2\x2\x99\x9A\ak\x2\x2\x9A\x9B\an\x2\x2\x9B\x9C\ag\x2\x2\x9C\""+
		"\x3\x2\x2\x2\x9D\x9E\aJ\x2\x2\x9E\x9F\aR\x2\x2\x9F$\x3\x2\x2\x2\xA0\xA1"+
		"\a\x63\x2\x2\xA1\xA2\an\x2\x2\xA2\xA3\ak\x2\x2\xA3\xA4\ax\x2\x2\xA4\xA5"+
		"\ag\x2\x2\xA5&\x3\x2\x2\x2\xA6\xA7\ao\x2\x2\xA7\xA8\aq\x2\x2\xA8\xA9\a"+
		"x\x2\x2\xA9\xAA\ag\x2\x2\xAA(\x3\x2\x2\x2\xAB\xAC\au\x2\x2\xAC\xAD\aj"+
		"\x2\x2\xAD\xAE\aq\x2\x2\xAE\xAF\aq\x2\x2\xAF\xB0\av\x2\x2\xB0*\x3\x2\x2"+
		"\x2\xB1\xB2\a?\x2\x2\xB2,\x3\x2\x2\x2\xB3\xB4\a~\x2\x2\xB4.\x3\x2\x2\x2"+
		"\xB5\xB6\a#\x2\x2\xB6\x30\x3\x2\x2\x2\xB7\xB8\a~\x2\x2\xB8\xBC\a~\x2\x2"+
		"\xB9\xBA\a(\x2\x2\xBA\xBC\a(\x2\x2\xBB\xB7\x3\x2\x2\x2\xBB\xB9\x3\x2\x2"+
		"\x2\xBC\x32\x3\x2\x2\x2\xBD\xC3\t\x3\x2\x2\xBE\xBF\a?\x2\x2\xBF\xC3\a"+
		"?\x2\x2\xC0\xC1\a#\x2\x2\xC1\xC3\a?\x2\x2\xC2\xBD\x3\x2\x2\x2\xC2\xBE"+
		"\x3\x2\x2\x2\xC2\xC0\x3\x2\x2\x2\xC3\x34\x3\x2\x2\x2\xC4\xC5\t\x4\x2\x2"+
		"\xC5\x36\x3\x2\x2\x2\xC6\xC7\a*\x2\x2\xC7\x38\x3\x2\x2\x2\xC8\xC9\a+\x2"+
		"\x2\xC9:\x3\x2\x2\x2\xCA\xCB\a\x7F\x2\x2\xCB<\x3\x2\x2\x2\xCC\xCD\a}\x2"+
		"\x2\xCD>\x3\x2\x2\x2\xCE\xCF\a<\x2\x2\xCF@\x3\x2\x2\x2\xD0\xD1\a=\x2\x2"+
		"\xD1\x42\x3\x2\x2\x2\xD2\xD3\a.\x2\x2\xD3\x44\x3\x2\x2\x2\xD4\xD5\a\x30"+
		"\x2\x2\xD5\x46\x3\x2\x2\x2\xD6\xD8\t\x5\x2\x2\xD7\xD6\x3\x2\x2\x2\xD8"+
		"\xD9\x3\x2\x2\x2\xD9\xD7\x3\x2\x2\x2\xD9\xDA\x3\x2\x2\x2\xDAH\x3\x2\x2"+
		"\x2\xDB\xDF\t\x6\x2\x2\xDC\xDE\t\a\x2\x2\xDD\xDC\x3\x2\x2\x2\xDE\xE1\x3"+
		"\x2\x2\x2\xDF\xDD\x3\x2\x2\x2\xDF\xE0\x3\x2\x2\x2\xE0J\x3\x2\x2\x2\xE1"+
		"\xDF\x3\x2\x2\x2\xE2\xE3\t\b\x2\x2\xE3\xE4\x3\x2\x2\x2\xE4\xE5\b&\x2\x2"+
		"\xE5L\x3\x2\x2\x2\a\x2\xBB\xC2\xD9\xDF\x3\b\x2\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace LabWork1github
