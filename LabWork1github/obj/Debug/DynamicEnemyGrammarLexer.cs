//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Dana\source\repos\visual studio 2019\LabWork1github\g4 files\DynamicEnemyGrammar.g4 by ANTLR 4.6.6

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
public partial class DynamicEnemyGrammarLexer : Lexer {
	public const int
		TELEPORT_PLACE=1, SPAWN_PLACE=2, SPAWN_TYPE=3, RANDOM=4, DISTANCE=5, DAMAGE=6, 
		HEALTH_CHECK=7, DIRECTION=8, NAME_T=9, TRAP=10, MONSTER=11, PLAYER=12, 
		PLACE_T=13, ROUND=14, NEAR=15, IS=16, ME=17, IF=18, TO=19, COMMANDS=20, 
		WHILE=21, HEALTH=22, ALIVE=23, MOVE=24, ON=25, SHOOT=26, SPAWN=27, TELEPORT_T=28, 
		PARTNER=29, HEAL=30, FROM=31, WHEN=32, DIE=33, STAY=34, AT=35, EQUALS=36, 
		ABSOLUTESTART=37, NEGATE=38, BOOLCONNECTER=39, COMPARE=40, NUMCOMPARE=41, 
		NUMCONNECTERMULTIP=42, NUMCONNECTERADD=43, PARENTHESISSTART=44, PARENTHESISCLOSE=45, 
		BRACKETCLOSE=46, BRACKETSTART=47, COLON=48, SEMI=49, COMMA=50, NUMBER=51, 
		DOT=52, ID=53, WS=54;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"TELEPORT_PLACE", "SPAWN_PLACE", "SPAWN_TYPE", "RANDOM", "DISTANCE", "DAMAGE", 
		"HEALTH_CHECK", "DIRECTION", "NAME_T", "TRAP", "MONSTER", "PLAYER", "PLACE_T", 
		"ROUND", "NEAR", "IS", "ME", "IF", "TO", "COMMANDS", "WHILE", "HEALTH", 
		"ALIVE", "MOVE", "ON", "SHOOT", "SPAWN", "TELEPORT_T", "PARTNER", "HEAL", 
		"FROM", "WHEN", "DIE", "STAY", "AT", "EQUALS", "ABSOLUTESTART", "NEGATE", 
		"BOOLCONNECTER", "COMPARE", "NUMCOMPARE", "NUMCONNECTERMULTIP", "NUMCONNECTERADD", 
		"PARENTHESISSTART", "PARENTHESISCLOSE", "BRACKETCLOSE", "BRACKETSTART", 
		"COLON", "SEMI", "COMMA", "NUMBER", "DOT", "ID", "WS"
	};


	public DynamicEnemyGrammarLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, "'teleport_place'", "'spawn_place'", "'spawn_type'", "'random'", 
		"'distance'", "'damage'", "'health_check'", null, "'name'", "'trap'", 
		"'monster'", "'player'", "'place'", "'round'", "'near'", "'is'", "'me'", 
		"'if'", "'to'", "'commands'", "'while'", "'health'", "'alive'", "'move'", 
		"'on'", "'shoot'", "'spawn'", "'teleport'", "'partner'", "'heal'", "'from'", 
		"'when'", "'die'", "'stay'", "'at'", "'='", null, "'!'", null, null, null, 
		null, null, "'('", "')'", "'}'", "'{'", "':'", "';'", "','", null, "'.'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "TELEPORT_PLACE", "SPAWN_PLACE", "SPAWN_TYPE", "RANDOM", "DISTANCE", 
		"DAMAGE", "HEALTH_CHECK", "DIRECTION", "NAME_T", "TRAP", "MONSTER", "PLAYER", 
		"PLACE_T", "ROUND", "NEAR", "IS", "ME", "IF", "TO", "COMMANDS", "WHILE", 
		"HEALTH", "ALIVE", "MOVE", "ON", "SHOOT", "SPAWN", "TELEPORT_T", "PARTNER", 
		"HEAL", "FROM", "WHEN", "DIE", "STAY", "AT", "EQUALS", "ABSOLUTESTART", 
		"NEGATE", "BOOLCONNECTER", "COMPARE", "NUMCOMPARE", "NUMCONNECTERMULTIP", 
		"NUMCONNECTERADD", "PARENTHESISSTART", "PARENTHESISCLOSE", "BRACKETCLOSE", 
		"BRACKETSTART", "COLON", "SEMI", "COMMA", "NUMBER", "DOT", "ID", "WS"
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

	public override string GrammarFileName { get { return "DynamicEnemyGrammar.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\x38\x191\b\x1\x4"+
		"\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b"+
		"\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4"+
		"\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15"+
		"\t\x15\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A"+
		"\x4\x1B\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 "+
		"\t \x4!\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t(\x4)\t"+
		")\x4*\t*\x4+\t+\x4,\t,\x4-\t-\x4.\t.\x4/\t/\x4\x30\t\x30\x4\x31\t\x31"+
		"\x4\x32\t\x32\x4\x33\t\x33\x4\x34\t\x34\x4\x35\t\x35\x4\x36\t\x36\x4\x37"+
		"\t\x37\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3"+
		"\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3"+
		"\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3"+
		"\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5"+
		"\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\a\x3\a\x3\a"+
		"\x3\a\x3\a\x3\a\x3\a\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b"+
		"\x3\b\x3\b\x3\b\x3\t\x3\t\x3\n\x3\n\x3\n\x3\n\x3\n\x3\v\x3\v\x3\v\x3\v"+
		"\x3\v\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\r\x3\r\x3\r\x3\r\x3\r"+
		"\x3\r\x3\r\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xF\x3\xF\x3\xF\x3\xF"+
		"\x3\xF\x3\xF\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x11\x3\x11\x3\x11\x3"+
		"\x12\x3\x12\x3\x12\x3\x13\x3\x13\x3\x13\x3\x14\x3\x14\x3\x14\x3\x15\x3"+
		"\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x16\x3\x16\x3"+
		"\x16\x3\x16\x3\x16\x3\x16\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3"+
		"\x17\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x19\x3\x19\x3\x19\x3"+
		"\x19\x3\x19\x3\x1A\x3\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1B\x3\x1B\x3\x1B\x3"+
		"\x1B\x3\x1C\x3\x1C\x3\x1C\x3\x1C\x3\x1C\x3\x1C\x3\x1D\x3\x1D\x3\x1D\x3"+
		"\x1D\x3\x1D\x3\x1D\x3\x1D\x3\x1D\x3\x1D\x3\x1E\x3\x1E\x3\x1E\x3\x1E\x3"+
		"\x1E\x3\x1E\x3\x1E\x3\x1E\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3 \x3 \x3"+
		" \x3 \x3 \x3!\x3!\x3!\x3!\x3!\x3\"\x3\"\x3\"\x3\"\x3#\x3#\x3#\x3#\x3#"+
		"\x3$\x3$\x3$\x3%\x3%\x3&\x3&\x3&\x3&\x3&\x3&\x3\'\x3\'\x3(\x3(\x3(\x3"+
		"(\x5(\x15C\n(\x3)\x3)\x3)\x3)\x5)\x162\n)\x3*\x3*\x3+\x3+\x3,\x3,\x3-"+
		"\x3-\x3.\x3.\x3/\x3/\x3\x30\x3\x30\x3\x31\x3\x31\x3\x32\x3\x32\x3\x33"+
		"\x3\x33\x3\x34\x6\x34\x179\n\x34\r\x34\xE\x34\x17A\x3\x34\x3\x34\x6\x34"+
		"\x17F\n\x34\r\x34\xE\x34\x180\x5\x34\x183\n\x34\x3\x35\x3\x35\x3\x36\x3"+
		"\x36\a\x36\x189\n\x36\f\x36\xE\x36\x18C\v\x36\x3\x37\x3\x37\x3\x37\x3"+
		"\x37\x2\x2\x2\x38\x3\x2\x3\x5\x2\x4\a\x2\x5\t\x2\x6\v\x2\a\r\x2\b\xF\x2"+
		"\t\x11\x2\n\x13\x2\v\x15\x2\f\x17\x2\r\x19\x2\xE\x1B\x2\xF\x1D\x2\x10"+
		"\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14\'\x2\x15)\x2\x16+\x2\x17-\x2\x18/"+
		"\x2\x19\x31\x2\x1A\x33\x2\x1B\x35\x2\x1C\x37\x2\x1D\x39\x2\x1E;\x2\x1F"+
		"=\x2 ?\x2!\x41\x2\"\x43\x2#\x45\x2$G\x2%I\x2&K\x2\'M\x2(O\x2)Q\x2*S\x2"+
		"+U\x2,W\x2-Y\x2.[\x2/]\x2\x30_\x2\x31\x61\x2\x32\x63\x2\x33\x65\x2\x34"+
		"g\x2\x35i\x2\x36k\x2\x37m\x2\x38\x3\x2\n\x6\x2\x44\x44HHNNTT\x4\x2>>@"+
		"@\x5\x2\'\',,\x31\x31\x4\x2--//\x3\x2\x32;\x4\x2\x43\\\x63|\x6\x2\x32"+
		";\x43\\\x61\x61\x63|\x5\x2\v\f\xF\xF\"\"\x196\x2\x3\x3\x2\x2\x2\x2\x5"+
		"\x3\x2\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3"+
		"\x2\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15"+
		"\x3\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2"+
		"\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3\x2\x2\x2"+
		"\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2\x2+\x3\x2\x2\x2\x2-"+
		"\x3\x2\x2\x2\x2/\x3\x2\x2\x2\x2\x31\x3\x2\x2\x2\x2\x33\x3\x2\x2\x2\x2"+
		"\x35\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2\x2\x39\x3\x2\x2\x2\x2;\x3\x2\x2\x2"+
		"\x2=\x3\x2\x2\x2\x2?\x3\x2\x2\x2\x2\x41\x3\x2\x2\x2\x2\x43\x3\x2\x2\x2"+
		"\x2\x45\x3\x2\x2\x2\x2G\x3\x2\x2\x2\x2I\x3\x2\x2\x2\x2K\x3\x2\x2\x2\x2"+
		"M\x3\x2\x2\x2\x2O\x3\x2\x2\x2\x2Q\x3\x2\x2\x2\x2S\x3\x2\x2\x2\x2U\x3\x2"+
		"\x2\x2\x2W\x3\x2\x2\x2\x2Y\x3\x2\x2\x2\x2[\x3\x2\x2\x2\x2]\x3\x2\x2\x2"+
		"\x2_\x3\x2\x2\x2\x2\x61\x3\x2\x2\x2\x2\x63\x3\x2\x2\x2\x2\x65\x3\x2\x2"+
		"\x2\x2g\x3\x2\x2\x2\x2i\x3\x2\x2\x2\x2k\x3\x2\x2\x2\x2m\x3\x2\x2\x2\x3"+
		"o\x3\x2\x2\x2\x5~\x3\x2\x2\x2\a\x8A\x3\x2\x2\x2\t\x95\x3\x2\x2\x2\v\x9C"+
		"\x3\x2\x2\x2\r\xA5\x3\x2\x2\x2\xF\xAC\x3\x2\x2\x2\x11\xB9\x3\x2\x2\x2"+
		"\x13\xBB\x3\x2\x2\x2\x15\xC0\x3\x2\x2\x2\x17\xC5\x3\x2\x2\x2\x19\xCD\x3"+
		"\x2\x2\x2\x1B\xD4\x3\x2\x2\x2\x1D\xDA\x3\x2\x2\x2\x1F\xE0\x3\x2\x2\x2"+
		"!\xE5\x3\x2\x2\x2#\xE8\x3\x2\x2\x2%\xEB\x3\x2\x2\x2\'\xEE\x3\x2\x2\x2"+
		")\xF1\x3\x2\x2\x2+\xFA\x3\x2\x2\x2-\x100\x3\x2\x2\x2/\x107\x3\x2\x2\x2"+
		"\x31\x10D\x3\x2\x2\x2\x33\x112\x3\x2\x2\x2\x35\x115\x3\x2\x2\x2\x37\x11B"+
		"\x3\x2\x2\x2\x39\x121\x3\x2\x2\x2;\x12A\x3\x2\x2\x2=\x132\x3\x2\x2\x2"+
		"?\x137\x3\x2\x2\x2\x41\x13C\x3\x2\x2\x2\x43\x141\x3\x2\x2\x2\x45\x145"+
		"\x3\x2\x2\x2G\x14A\x3\x2\x2\x2I\x14D\x3\x2\x2\x2K\x14F\x3\x2\x2\x2M\x155"+
		"\x3\x2\x2\x2O\x15B\x3\x2\x2\x2Q\x161\x3\x2\x2\x2S\x163\x3\x2\x2\x2U\x165"+
		"\x3\x2\x2\x2W\x167\x3\x2\x2\x2Y\x169\x3\x2\x2\x2[\x16B\x3\x2\x2\x2]\x16D"+
		"\x3\x2\x2\x2_\x16F\x3\x2\x2\x2\x61\x171\x3\x2\x2\x2\x63\x173\x3\x2\x2"+
		"\x2\x65\x175\x3\x2\x2\x2g\x178\x3\x2\x2\x2i\x184\x3\x2\x2\x2k\x186\x3"+
		"\x2\x2\x2m\x18D\x3\x2\x2\x2op\av\x2\x2pq\ag\x2\x2qr\an\x2\x2rs\ag\x2\x2"+
		"st\ar\x2\x2tu\aq\x2\x2uv\at\x2\x2vw\av\x2\x2wx\a\x61\x2\x2xy\ar\x2\x2"+
		"yz\an\x2\x2z{\a\x63\x2\x2{|\a\x65\x2\x2|}\ag\x2\x2}\x4\x3\x2\x2\x2~\x7F"+
		"\au\x2\x2\x7F\x80\ar\x2\x2\x80\x81\a\x63\x2\x2\x81\x82\ay\x2\x2\x82\x83"+
		"\ap\x2\x2\x83\x84\a\x61\x2\x2\x84\x85\ar\x2\x2\x85\x86\an\x2\x2\x86\x87"+
		"\a\x63\x2\x2\x87\x88\a\x65\x2\x2\x88\x89\ag\x2\x2\x89\x6\x3\x2\x2\x2\x8A"+
		"\x8B\au\x2\x2\x8B\x8C\ar\x2\x2\x8C\x8D\a\x63\x2\x2\x8D\x8E\ay\x2\x2\x8E"+
		"\x8F\ap\x2\x2\x8F\x90\a\x61\x2\x2\x90\x91\av\x2\x2\x91\x92\a{\x2\x2\x92"+
		"\x93\ar\x2\x2\x93\x94\ag\x2\x2\x94\b\x3\x2\x2\x2\x95\x96\at\x2\x2\x96"+
		"\x97\a\x63\x2\x2\x97\x98\ap\x2\x2\x98\x99\a\x66\x2\x2\x99\x9A\aq\x2\x2"+
		"\x9A\x9B\ao\x2\x2\x9B\n\x3\x2\x2\x2\x9C\x9D\a\x66\x2\x2\x9D\x9E\ak\x2"+
		"\x2\x9E\x9F\au\x2\x2\x9F\xA0\av\x2\x2\xA0\xA1\a\x63\x2\x2\xA1\xA2\ap\x2"+
		"\x2\xA2\xA3\a\x65\x2\x2\xA3\xA4\ag\x2\x2\xA4\f\x3\x2\x2\x2\xA5\xA6\a\x66"+
		"\x2\x2\xA6\xA7\a\x63\x2\x2\xA7\xA8\ao\x2\x2\xA8\xA9\a\x63\x2\x2\xA9\xAA"+
		"\ai\x2\x2\xAA\xAB\ag\x2\x2\xAB\xE\x3\x2\x2\x2\xAC\xAD\aj\x2\x2\xAD\xAE"+
		"\ag\x2\x2\xAE\xAF\a\x63\x2\x2\xAF\xB0\an\x2\x2\xB0\xB1\av\x2\x2\xB1\xB2"+
		"\aj\x2\x2\xB2\xB3\a\x61\x2\x2\xB3\xB4\a\x65\x2\x2\xB4\xB5\aj\x2\x2\xB5"+
		"\xB6\ag\x2\x2\xB6\xB7\a\x65\x2\x2\xB7\xB8\am\x2\x2\xB8\x10\x3\x2\x2\x2"+
		"\xB9\xBA\t\x2\x2\x2\xBA\x12\x3\x2\x2\x2\xBB\xBC\ap\x2\x2\xBC\xBD\a\x63"+
		"\x2\x2\xBD\xBE\ao\x2\x2\xBE\xBF\ag\x2\x2\xBF\x14\x3\x2\x2\x2\xC0\xC1\a"+
		"v\x2\x2\xC1\xC2\at\x2\x2\xC2\xC3\a\x63\x2\x2\xC3\xC4\ar\x2\x2\xC4\x16"+
		"\x3\x2\x2\x2\xC5\xC6\ao\x2\x2\xC6\xC7\aq\x2\x2\xC7\xC8\ap\x2\x2\xC8\xC9"+
		"\au\x2\x2\xC9\xCA\av\x2\x2\xCA\xCB\ag\x2\x2\xCB\xCC\at\x2\x2\xCC\x18\x3"+
		"\x2\x2\x2\xCD\xCE\ar\x2\x2\xCE\xCF\an\x2\x2\xCF\xD0\a\x63\x2\x2\xD0\xD1"+
		"\a{\x2\x2\xD1\xD2\ag\x2\x2\xD2\xD3\at\x2\x2\xD3\x1A\x3\x2\x2\x2\xD4\xD5"+
		"\ar\x2\x2\xD5\xD6\an\x2\x2\xD6\xD7\a\x63\x2\x2\xD7\xD8\a\x65\x2\x2\xD8"+
		"\xD9\ag\x2\x2\xD9\x1C\x3\x2\x2\x2\xDA\xDB\at\x2\x2\xDB\xDC\aq\x2\x2\xDC"+
		"\xDD\aw\x2\x2\xDD\xDE\ap\x2\x2\xDE\xDF\a\x66\x2\x2\xDF\x1E\x3\x2\x2\x2"+
		"\xE0\xE1\ap\x2\x2\xE1\xE2\ag\x2\x2\xE2\xE3\a\x63\x2\x2\xE3\xE4\at\x2\x2"+
		"\xE4 \x3\x2\x2\x2\xE5\xE6\ak\x2\x2\xE6\xE7\au\x2\x2\xE7\"\x3\x2\x2\x2"+
		"\xE8\xE9\ao\x2\x2\xE9\xEA\ag\x2\x2\xEA$\x3\x2\x2\x2\xEB\xEC\ak\x2\x2\xEC"+
		"\xED\ah\x2\x2\xED&\x3\x2\x2\x2\xEE\xEF\av\x2\x2\xEF\xF0\aq\x2\x2\xF0("+
		"\x3\x2\x2\x2\xF1\xF2\a\x65\x2\x2\xF2\xF3\aq\x2\x2\xF3\xF4\ao\x2\x2\xF4"+
		"\xF5\ao\x2\x2\xF5\xF6\a\x63\x2\x2\xF6\xF7\ap\x2\x2\xF7\xF8\a\x66\x2\x2"+
		"\xF8\xF9\au\x2\x2\xF9*\x3\x2\x2\x2\xFA\xFB\ay\x2\x2\xFB\xFC\aj\x2\x2\xFC"+
		"\xFD\ak\x2\x2\xFD\xFE\an\x2\x2\xFE\xFF\ag\x2\x2\xFF,\x3\x2\x2\x2\x100"+
		"\x101\aj\x2\x2\x101\x102\ag\x2\x2\x102\x103\a\x63\x2\x2\x103\x104\an\x2"+
		"\x2\x104\x105\av\x2\x2\x105\x106\aj\x2\x2\x106.\x3\x2\x2\x2\x107\x108"+
		"\a\x63\x2\x2\x108\x109\an\x2\x2\x109\x10A\ak\x2\x2\x10A\x10B\ax\x2\x2"+
		"\x10B\x10C\ag\x2\x2\x10C\x30\x3\x2\x2\x2\x10D\x10E\ao\x2\x2\x10E\x10F"+
		"\aq\x2\x2\x10F\x110\ax\x2\x2\x110\x111\ag\x2\x2\x111\x32\x3\x2\x2\x2\x112"+
		"\x113\aq\x2\x2\x113\x114\ap\x2\x2\x114\x34\x3\x2\x2\x2\x115\x116\au\x2"+
		"\x2\x116\x117\aj\x2\x2\x117\x118\aq\x2\x2\x118\x119\aq\x2\x2\x119\x11A"+
		"\av\x2\x2\x11A\x36\x3\x2\x2\x2\x11B\x11C\au\x2\x2\x11C\x11D\ar\x2\x2\x11D"+
		"\x11E\a\x63\x2\x2\x11E\x11F\ay\x2\x2\x11F\x120\ap\x2\x2\x120\x38\x3\x2"+
		"\x2\x2\x121\x122\av\x2\x2\x122\x123\ag\x2\x2\x123\x124\an\x2\x2\x124\x125"+
		"\ag\x2\x2\x125\x126\ar\x2\x2\x126\x127\aq\x2\x2\x127\x128\at\x2\x2\x128"+
		"\x129\av\x2\x2\x129:\x3\x2\x2\x2\x12A\x12B\ar\x2\x2\x12B\x12C\a\x63\x2"+
		"\x2\x12C\x12D\at\x2\x2\x12D\x12E\av\x2\x2\x12E\x12F\ap\x2\x2\x12F\x130"+
		"\ag\x2\x2\x130\x131\at\x2\x2\x131<\x3\x2\x2\x2\x132\x133\aj\x2\x2\x133"+
		"\x134\ag\x2\x2\x134\x135\a\x63\x2\x2\x135\x136\an\x2\x2\x136>\x3\x2\x2"+
		"\x2\x137\x138\ah\x2\x2\x138\x139\at\x2\x2\x139\x13A\aq\x2\x2\x13A\x13B"+
		"\ao\x2\x2\x13B@\x3\x2\x2\x2\x13C\x13D\ay\x2\x2\x13D\x13E\aj\x2\x2\x13E"+
		"\x13F\ag\x2\x2\x13F\x140\ap\x2\x2\x140\x42\x3\x2\x2\x2\x141\x142\a\x66"+
		"\x2\x2\x142\x143\ak\x2\x2\x143\x144\ag\x2\x2\x144\x44\x3\x2\x2\x2\x145"+
		"\x146\au\x2\x2\x146\x147\av\x2\x2\x147\x148\a\x63\x2\x2\x148\x149\a{\x2"+
		"\x2\x149\x46\x3\x2\x2\x2\x14A\x14B\a\x63\x2\x2\x14B\x14C\av\x2\x2\x14C"+
		"H\x3\x2\x2\x2\x14D\x14E\a?\x2\x2\x14EJ\x3\x2\x2\x2\x14F\x150\a\x43\x2"+
		"\x2\x150\x151\a\x64\x2\x2\x151\x152\au\x2\x2\x152\x153\x3\x2\x2\x2\x153"+
		"\x154\x5Y-\x2\x154L\x3\x2\x2\x2\x155\x156\a#\x2\x2\x156N\x3\x2\x2\x2\x157"+
		"\x158\a~\x2\x2\x158\x15C\a~\x2\x2\x159\x15A\a(\x2\x2\x15A\x15C\a(\x2\x2"+
		"\x15B\x157\x3\x2\x2\x2\x15B\x159\x3\x2\x2\x2\x15CP\x3\x2\x2\x2\x15D\x15E"+
		"\a?\x2\x2\x15E\x162\a?\x2\x2\x15F\x160\a#\x2\x2\x160\x162\a?\x2\x2\x161"+
		"\x15D\x3\x2\x2\x2\x161\x15F\x3\x2\x2\x2\x162R\x3\x2\x2\x2\x163\x164\t"+
		"\x3\x2\x2\x164T\x3\x2\x2\x2\x165\x166\t\x4\x2\x2\x166V\x3\x2\x2\x2\x167"+
		"\x168\t\x5\x2\x2\x168X\x3\x2\x2\x2\x169\x16A\a*\x2\x2\x16AZ\x3\x2\x2\x2"+
		"\x16B\x16C\a+\x2\x2\x16C\\\x3\x2\x2\x2\x16D\x16E\a\x7F\x2\x2\x16E^\x3"+
		"\x2\x2\x2\x16F\x170\a}\x2\x2\x170`\x3\x2\x2\x2\x171\x172\a<\x2\x2\x172"+
		"\x62\x3\x2\x2\x2\x173\x174\a=\x2\x2\x174\x64\x3\x2\x2\x2\x175\x176\a."+
		"\x2\x2\x176\x66\x3\x2\x2\x2\x177\x179\t\x6\x2\x2\x178\x177\x3\x2\x2\x2"+
		"\x179\x17A\x3\x2\x2\x2\x17A\x178\x3\x2\x2\x2\x17A\x17B\x3\x2\x2\x2\x17B"+
		"\x182\x3\x2\x2\x2\x17C\x17E\x5i\x35\x2\x17D\x17F\t\x6\x2\x2\x17E\x17D"+
		"\x3\x2\x2\x2\x17F\x180\x3\x2\x2\x2\x180\x17E\x3\x2\x2\x2\x180\x181\x3"+
		"\x2\x2\x2\x181\x183\x3\x2\x2\x2\x182\x17C\x3\x2\x2\x2\x182\x183\x3\x2"+
		"\x2\x2\x183h\x3\x2\x2\x2\x184\x185\a\x30\x2\x2\x185j\x3\x2\x2\x2\x186"+
		"\x18A\t\a\x2\x2\x187\x189\t\b\x2\x2\x188\x187\x3\x2\x2\x2\x189\x18C\x3"+
		"\x2\x2\x2\x18A\x188\x3\x2\x2\x2\x18A\x18B\x3\x2\x2\x2\x18Bl\x3\x2\x2\x2"+
		"\x18C\x18A\x3\x2\x2\x2\x18D\x18E\t\t\x2\x2\x18E\x18F\x3\x2\x2\x2\x18F"+
		"\x190\b\x37\x2\x2\x190n\x3\x2\x2\x2\t\x2\x15B\x161\x17A\x180\x182\x18A"+
		"\x3\b\x2\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace LabWork1github
