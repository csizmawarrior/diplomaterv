//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Dana\Source\Repos\LabWork1github\LabWork1github\TrapGrammar.g4 by ANTLR 4.6.6

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
using Antlr4.Runtime.Tree;
using System.Collections.Generic;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public partial class TrapGrammarParser : Parser {
	public const int
		SPAWN=1, TELEPORT_T=2, HEAL=3, DAMAGE=4, RANGE=5, NAME=6, EFFECT_T=7, 
		MOVEROUNDS=8, COLON=9, SEMI=10, COMMA=11, NUMBER=12, ID=13, WS=14;
	public const int
		RULE_definition = 0, RULE_name = 1, RULE_statementList = 2, RULE_statement = 3, 
		RULE_nameDeclaration = 4, RULE_effectDeclaration = 5, RULE_moveData = 6, 
		RULE_effect = 7, RULE_damage = 8, RULE_heal = 9, RULE_monsterSpawn = 10, 
		RULE_teleport = 11, RULE_rangeDeclaration = 12, RULE_moveRoundDeclaration = 13, 
		RULE_place = 14, RULE_x = 15, RULE_y = 16;
	public static readonly string[] ruleNames = {
		"definition", "name", "statementList", "statement", "nameDeclaration", 
		"effectDeclaration", "moveData", "effect", "damage", "heal", "monsterSpawn", 
		"teleport", "rangeDeclaration", "moveRoundDeclaration", "place", "x", 
		"y"
	};

	private static readonly string[] _LiteralNames = {
		null, "'spawn'", "'teleport'", "'heal'", "'damage'", "'range'", "'name'", 
		"'effect'", "'moverounds'", "':'", "';'", "','"
	};
	private static readonly string[] _SymbolicNames = {
		null, "SPAWN", "TELEPORT_T", "HEAL", "DAMAGE", "RANGE", "NAME", "EFFECT_T", 
		"MOVEROUNDS", "COLON", "SEMI", "COMMA", "NUMBER", "ID", "WS"
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

	public override string GrammarFileName { get { return "TrapGrammar.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public TrapGrammarParser(ITokenStream input)
		: base(input)
	{
		_interp = new ParserATNSimulator(this,_ATN);
	}
	public partial class DefinitionContext : ParserRuleContext {
		public StatementListContext[] statementList() {
			return GetRuleContexts<StatementListContext>();
		}
		public StatementListContext statementList(int i) {
			return GetRuleContext<StatementListContext>(i);
		}
		public DefinitionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_definition; } }
		public override void EnterRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.EnterDefinition(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.ExitDefinition(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITrapGrammarVisitor<TResult> typedVisitor = visitor as ITrapGrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitDefinition(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public DefinitionContext definition() {
		DefinitionContext _localctx = new DefinitionContext(_ctx, State);
		EnterRule(_localctx, 0, RULE_definition);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 37;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==NAME) {
				{
				{
				State = 34; statementList();
				}
				}
				State = 39;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class NameContext : ParserRuleContext {
		public ITerminalNode ID() { return GetToken(TrapGrammarParser.ID, 0); }
		public NameContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_name; } }
		public override void EnterRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.EnterName(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.ExitName(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITrapGrammarVisitor<TResult> typedVisitor = visitor as ITrapGrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitName(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public NameContext name() {
		NameContext _localctx = new NameContext(_ctx, State);
		EnterRule(_localctx, 2, RULE_name);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 40; Match(ID);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class StatementListContext : ParserRuleContext {
		public NameDeclarationContext nameDeclaration() {
			return GetRuleContext<NameDeclarationContext>(0);
		}
		public StatementContext[] statement() {
			return GetRuleContexts<StatementContext>();
		}
		public StatementContext statement(int i) {
			return GetRuleContext<StatementContext>(i);
		}
		public StatementListContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_statementList; } }
		public override void EnterRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.EnterStatementList(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.ExitStatementList(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITrapGrammarVisitor<TResult> typedVisitor = visitor as ITrapGrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitStatementList(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public StatementListContext statementList() {
		StatementListContext _localctx = new StatementListContext(_ctx, State);
		EnterRule(_localctx, 4, RULE_statementList);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 42; nameDeclaration();
			State = 46;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==RANGE || _la==EFFECT_T) {
				{
				{
				State = 43; statement();
				}
				}
				State = 48;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class StatementContext : ParserRuleContext {
		public EffectDeclarationContext effectDeclaration() {
			return GetRuleContext<EffectDeclarationContext>(0);
		}
		public RangeDeclarationContext rangeDeclaration() {
			return GetRuleContext<RangeDeclarationContext>(0);
		}
		public MoveDataContext moveData() {
			return GetRuleContext<MoveDataContext>(0);
		}
		public StatementContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_statement; } }
		public override void EnterRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.EnterStatement(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.ExitStatement(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITrapGrammarVisitor<TResult> typedVisitor = visitor as ITrapGrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitStatement(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public StatementContext statement() {
		StatementContext _localctx = new StatementContext(_ctx, State);
		EnterRule(_localctx, 6, RULE_statement);
		try {
			State = 52;
			_errHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(_input,2,_ctx) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 49; effectDeclaration();
				}
				break;

			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 50; rangeDeclaration();
				}
				break;

			case 3:
				EnterOuterAlt(_localctx, 3);
				{
				State = 51; moveData();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class NameDeclarationContext : ParserRuleContext {
		public ITerminalNode NAME() { return GetToken(TrapGrammarParser.NAME, 0); }
		public NameContext name() {
			return GetRuleContext<NameContext>(0);
		}
		public NameDeclarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_nameDeclaration; } }
		public override void EnterRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.EnterNameDeclaration(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.ExitNameDeclaration(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITrapGrammarVisitor<TResult> typedVisitor = visitor as ITrapGrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitNameDeclaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public NameDeclarationContext nameDeclaration() {
		NameDeclarationContext _localctx = new NameDeclarationContext(_ctx, State);
		EnterRule(_localctx, 8, RULE_nameDeclaration);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 54; Match(NAME);
			State = 55; Match(COLON);
			State = 56; name();
			State = 57; Match(SEMI);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class EffectDeclarationContext : ParserRuleContext {
		public ITerminalNode EFFECT_T() { return GetToken(TrapGrammarParser.EFFECT_T, 0); }
		public EffectContext effect() {
			return GetRuleContext<EffectContext>(0);
		}
		public EffectDeclarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_effectDeclaration; } }
		public override void EnterRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.EnterEffectDeclaration(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.ExitEffectDeclaration(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITrapGrammarVisitor<TResult> typedVisitor = visitor as ITrapGrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitEffectDeclaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public EffectDeclarationContext effectDeclaration() {
		EffectDeclarationContext _localctx = new EffectDeclarationContext(_ctx, State);
		EnterRule(_localctx, 10, RULE_effectDeclaration);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 59; Match(EFFECT_T);
			State = 60; Match(COLON);
			State = 61; effect();
			State = 62; Match(SEMI);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class MoveDataContext : ParserRuleContext {
		public RangeDeclarationContext rangeDeclaration() {
			return GetRuleContext<RangeDeclarationContext>(0);
		}
		public MoveRoundDeclarationContext moveRoundDeclaration() {
			return GetRuleContext<MoveRoundDeclarationContext>(0);
		}
		public MoveDataContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_moveData; } }
		public override void EnterRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.EnterMoveData(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.ExitMoveData(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITrapGrammarVisitor<TResult> typedVisitor = visitor as ITrapGrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMoveData(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public MoveDataContext moveData() {
		MoveDataContext _localctx = new MoveDataContext(_ctx, State);
		EnterRule(_localctx, 12, RULE_moveData);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 64; rangeDeclaration();
			State = 65; Match(COMMA);
			State = 66; moveRoundDeclaration();
			State = 67; Match(SEMI);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class EffectContext : ParserRuleContext {
		public DamageContext damage() {
			return GetRuleContext<DamageContext>(0);
		}
		public HealContext heal() {
			return GetRuleContext<HealContext>(0);
		}
		public TeleportContext teleport() {
			return GetRuleContext<TeleportContext>(0);
		}
		public MonsterSpawnContext monsterSpawn() {
			return GetRuleContext<MonsterSpawnContext>(0);
		}
		public EffectContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_effect; } }
		public override void EnterRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.EnterEffect(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.ExitEffect(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITrapGrammarVisitor<TResult> typedVisitor = visitor as ITrapGrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitEffect(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public EffectContext effect() {
		EffectContext _localctx = new EffectContext(_ctx, State);
		EnterRule(_localctx, 14, RULE_effect);
		try {
			State = 73;
			_errHandler.Sync(this);
			switch (_input.La(1)) {
			case DAMAGE:
				EnterOuterAlt(_localctx, 1);
				{
				State = 69; damage();
				}
				break;
			case HEAL:
				EnterOuterAlt(_localctx, 2);
				{
				State = 70; heal();
				}
				break;
			case TELEPORT_T:
				EnterOuterAlt(_localctx, 3);
				{
				State = 71; teleport();
				}
				break;
			case SPAWN:
				EnterOuterAlt(_localctx, 4);
				{
				State = 72; monsterSpawn();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class DamageContext : ParserRuleContext {
		public ITerminalNode DAMAGE() { return GetToken(TrapGrammarParser.DAMAGE, 0); }
		public ITerminalNode NUMBER() { return GetToken(TrapGrammarParser.NUMBER, 0); }
		public DamageContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_damage; } }
		public override void EnterRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.EnterDamage(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.ExitDamage(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITrapGrammarVisitor<TResult> typedVisitor = visitor as ITrapGrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitDamage(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public DamageContext damage() {
		DamageContext _localctx = new DamageContext(_ctx, State);
		EnterRule(_localctx, 16, RULE_damage);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 75; Match(DAMAGE);
			State = 76; Match(NUMBER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class HealContext : ParserRuleContext {
		public ITerminalNode HEAL() { return GetToken(TrapGrammarParser.HEAL, 0); }
		public ITerminalNode NUMBER() { return GetToken(TrapGrammarParser.NUMBER, 0); }
		public HealContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_heal; } }
		public override void EnterRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.EnterHeal(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.ExitHeal(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITrapGrammarVisitor<TResult> typedVisitor = visitor as ITrapGrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitHeal(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public HealContext heal() {
		HealContext _localctx = new HealContext(_ctx, State);
		EnterRule(_localctx, 18, RULE_heal);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 78; Match(HEAL);
			State = 79; Match(NUMBER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class MonsterSpawnContext : ParserRuleContext {
		public ITerminalNode SPAWN() { return GetToken(TrapGrammarParser.SPAWN, 0); }
		public PlaceContext place() {
			return GetRuleContext<PlaceContext>(0);
		}
		public MonsterSpawnContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_monsterSpawn; } }
		public override void EnterRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.EnterMonsterSpawn(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.ExitMonsterSpawn(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITrapGrammarVisitor<TResult> typedVisitor = visitor as ITrapGrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMonsterSpawn(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public MonsterSpawnContext monsterSpawn() {
		MonsterSpawnContext _localctx = new MonsterSpawnContext(_ctx, State);
		EnterRule(_localctx, 20, RULE_monsterSpawn);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 81; Match(SPAWN);
			State = 82; place();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class TeleportContext : ParserRuleContext {
		public ITerminalNode TELEPORT_T() { return GetToken(TrapGrammarParser.TELEPORT_T, 0); }
		public PlaceContext place() {
			return GetRuleContext<PlaceContext>(0);
		}
		public TeleportContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_teleport; } }
		public override void EnterRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.EnterTeleport(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.ExitTeleport(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITrapGrammarVisitor<TResult> typedVisitor = visitor as ITrapGrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitTeleport(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public TeleportContext teleport() {
		TeleportContext _localctx = new TeleportContext(_ctx, State);
		EnterRule(_localctx, 22, RULE_teleport);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 84; Match(TELEPORT_T);
			State = 85; place();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class RangeDeclarationContext : ParserRuleContext {
		public ITerminalNode RANGE() { return GetToken(TrapGrammarParser.RANGE, 0); }
		public ITerminalNode NUMBER() { return GetToken(TrapGrammarParser.NUMBER, 0); }
		public RangeDeclarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_rangeDeclaration; } }
		public override void EnterRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.EnterRangeDeclaration(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.ExitRangeDeclaration(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITrapGrammarVisitor<TResult> typedVisitor = visitor as ITrapGrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitRangeDeclaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public RangeDeclarationContext rangeDeclaration() {
		RangeDeclarationContext _localctx = new RangeDeclarationContext(_ctx, State);
		EnterRule(_localctx, 24, RULE_rangeDeclaration);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 87; Match(RANGE);
			State = 88; Match(COLON);
			State = 89; Match(NUMBER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class MoveRoundDeclarationContext : ParserRuleContext {
		public ITerminalNode MOVEROUNDS() { return GetToken(TrapGrammarParser.MOVEROUNDS, 0); }
		public ITerminalNode NUMBER() { return GetToken(TrapGrammarParser.NUMBER, 0); }
		public MoveRoundDeclarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_moveRoundDeclaration; } }
		public override void EnterRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.EnterMoveRoundDeclaration(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.ExitMoveRoundDeclaration(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITrapGrammarVisitor<TResult> typedVisitor = visitor as ITrapGrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMoveRoundDeclaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public MoveRoundDeclarationContext moveRoundDeclaration() {
		MoveRoundDeclarationContext _localctx = new MoveRoundDeclarationContext(_ctx, State);
		EnterRule(_localctx, 26, RULE_moveRoundDeclaration);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 91; Match(MOVEROUNDS);
			State = 92; Match(COLON);
			State = 93; Match(NUMBER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class PlaceContext : ParserRuleContext {
		public XContext x() {
			return GetRuleContext<XContext>(0);
		}
		public YContext y() {
			return GetRuleContext<YContext>(0);
		}
		public PlaceContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_place; } }
		public override void EnterRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.EnterPlace(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.ExitPlace(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITrapGrammarVisitor<TResult> typedVisitor = visitor as ITrapGrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitPlace(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public PlaceContext place() {
		PlaceContext _localctx = new PlaceContext(_ctx, State);
		EnterRule(_localctx, 28, RULE_place);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 95; x();
			State = 96; Match(COMMA);
			State = 97; y();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class XContext : ParserRuleContext {
		public ITerminalNode NUMBER() { return GetToken(TrapGrammarParser.NUMBER, 0); }
		public XContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_x; } }
		public override void EnterRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.EnterX(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.ExitX(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITrapGrammarVisitor<TResult> typedVisitor = visitor as ITrapGrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitX(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public XContext x() {
		XContext _localctx = new XContext(_ctx, State);
		EnterRule(_localctx, 30, RULE_x);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 99; Match(NUMBER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class YContext : ParserRuleContext {
		public ITerminalNode NUMBER() { return GetToken(TrapGrammarParser.NUMBER, 0); }
		public YContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_y; } }
		public override void EnterRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.EnterY(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ITrapGrammarListener typedListener = listener as ITrapGrammarListener;
			if (typedListener != null) typedListener.ExitY(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITrapGrammarVisitor<TResult> typedVisitor = visitor as ITrapGrammarVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitY(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public YContext y() {
		YContext _localctx = new YContext(_ctx, State);
		EnterRule(_localctx, 32, RULE_y);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 101; Match(NUMBER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x3\x10j\x4\x2\t\x2"+
		"\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4\t\t"+
		"\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10\t"+
		"\x10\x4\x11\t\x11\x4\x12\t\x12\x3\x2\a\x2&\n\x2\f\x2\xE\x2)\v\x2\x3\x3"+
		"\x3\x3\x3\x4\x3\x4\a\x4/\n\x4\f\x4\xE\x4\x32\v\x4\x3\x5\x3\x5\x3\x5\x5"+
		"\x5\x37\n\x5\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\a\x3\a\x3\a\x3\a\x3\a\x3"+
		"\b\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t\x3\t\x3\t\x5\tL\n\t\x3\n\x3\n\x3\n\x3"+
		"\v\x3\v\x3\v\x3\f\x3\f\x3\f\x3\r\x3\r\x3\r\x3\xE\x3\xE\x3\xE\x3\xE\x3"+
		"\xF\x3\xF\x3\xF\x3\xF\x3\x10\x3\x10\x3\x10\x3\x10\x3\x11\x3\x11\x3\x12"+
		"\x3\x12\x3\x12\x2\x2\x2\x13\x2\x2\x4\x2\x6\x2\b\x2\n\x2\f\x2\xE\x2\x10"+
		"\x2\x12\x2\x14\x2\x16\x2\x18\x2\x1A\x2\x1C\x2\x1E\x2 \x2\"\x2\x2\x2_\x2"+
		"\'\x3\x2\x2\x2\x4*\x3\x2\x2\x2\x6,\x3\x2\x2\x2\b\x36\x3\x2\x2\x2\n\x38"+
		"\x3\x2\x2\x2\f=\x3\x2\x2\x2\xE\x42\x3\x2\x2\x2\x10K\x3\x2\x2\x2\x12M\x3"+
		"\x2\x2\x2\x14P\x3\x2\x2\x2\x16S\x3\x2\x2\x2\x18V\x3\x2\x2\x2\x1AY\x3\x2"+
		"\x2\x2\x1C]\x3\x2\x2\x2\x1E\x61\x3\x2\x2\x2 \x65\x3\x2\x2\x2\"g\x3\x2"+
		"\x2\x2$&\x5\x6\x4\x2%$\x3\x2\x2\x2&)\x3\x2\x2\x2\'%\x3\x2\x2\x2\'(\x3"+
		"\x2\x2\x2(\x3\x3\x2\x2\x2)\'\x3\x2\x2\x2*+\a\xF\x2\x2+\x5\x3\x2\x2\x2"+
		",\x30\x5\n\x6\x2-/\x5\b\x5\x2.-\x3\x2\x2\x2/\x32\x3\x2\x2\x2\x30.\x3\x2"+
		"\x2\x2\x30\x31\x3\x2\x2\x2\x31\a\x3\x2\x2\x2\x32\x30\x3\x2\x2\x2\x33\x37"+
		"\x5\f\a\x2\x34\x37\x5\x1A\xE\x2\x35\x37\x5\xE\b\x2\x36\x33\x3\x2\x2\x2"+
		"\x36\x34\x3\x2\x2\x2\x36\x35\x3\x2\x2\x2\x37\t\x3\x2\x2\x2\x38\x39\a\b"+
		"\x2\x2\x39:\a\v\x2\x2:;\x5\x4\x3\x2;<\a\f\x2\x2<\v\x3\x2\x2\x2=>\a\t\x2"+
		"\x2>?\a\v\x2\x2?@\x5\x10\t\x2@\x41\a\f\x2\x2\x41\r\x3\x2\x2\x2\x42\x43"+
		"\x5\x1A\xE\x2\x43\x44\a\r\x2\x2\x44\x45\x5\x1C\xF\x2\x45\x46\a\f\x2\x2"+
		"\x46\xF\x3\x2\x2\x2GL\x5\x12\n\x2HL\x5\x14\v\x2IL\x5\x18\r\x2JL\x5\x16"+
		"\f\x2KG\x3\x2\x2\x2KH\x3\x2\x2\x2KI\x3\x2\x2\x2KJ\x3\x2\x2\x2L\x11\x3"+
		"\x2\x2\x2MN\a\x6\x2\x2NO\a\xE\x2\x2O\x13\x3\x2\x2\x2PQ\a\x5\x2\x2QR\a"+
		"\xE\x2\x2R\x15\x3\x2\x2\x2ST\a\x3\x2\x2TU\x5\x1E\x10\x2U\x17\x3\x2\x2"+
		"\x2VW\a\x4\x2\x2WX\x5\x1E\x10\x2X\x19\x3\x2\x2\x2YZ\a\a\x2\x2Z[\a\v\x2"+
		"\x2[\\\a\xE\x2\x2\\\x1B\x3\x2\x2\x2]^\a\n\x2\x2^_\a\v\x2\x2_`\a\xE\x2"+
		"\x2`\x1D\x3\x2\x2\x2\x61\x62\x5 \x11\x2\x62\x63\a\r\x2\x2\x63\x64\x5\""+
		"\x12\x2\x64\x1F\x3\x2\x2\x2\x65\x66\a\xE\x2\x2\x66!\x3\x2\x2\x2gh\a\xE"+
		"\x2\x2h#\x3\x2\x2\x2\x6\'\x30\x36K";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace LabWork1github
