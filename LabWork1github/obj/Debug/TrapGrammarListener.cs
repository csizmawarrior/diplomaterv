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
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="TrapGrammarParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface ITrapGrammarListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="TrapGrammarParser.definition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDefinition([NotNull] TrapGrammarParser.DefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="TrapGrammarParser.definition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDefinition([NotNull] TrapGrammarParser.DefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="TrapGrammarParser.name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterName([NotNull] TrapGrammarParser.NameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="TrapGrammarParser.name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitName([NotNull] TrapGrammarParser.NameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="TrapGrammarParser.statementList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatementList([NotNull] TrapGrammarParser.StatementListContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="TrapGrammarParser.statementList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatementList([NotNull] TrapGrammarParser.StatementListContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="TrapGrammarParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement([NotNull] TrapGrammarParser.StatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="TrapGrammarParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement([NotNull] TrapGrammarParser.StatementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="TrapGrammarParser.nameDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNameDeclaration([NotNull] TrapGrammarParser.NameDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="TrapGrammarParser.nameDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNameDeclaration([NotNull] TrapGrammarParser.NameDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="TrapGrammarParser.effectDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEffectDeclaration([NotNull] TrapGrammarParser.EffectDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="TrapGrammarParser.effectDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEffectDeclaration([NotNull] TrapGrammarParser.EffectDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="TrapGrammarParser.moveData"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMoveData([NotNull] TrapGrammarParser.MoveDataContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="TrapGrammarParser.moveData"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMoveData([NotNull] TrapGrammarParser.MoveDataContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="TrapGrammarParser.effect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEffect([NotNull] TrapGrammarParser.EffectContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="TrapGrammarParser.effect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEffect([NotNull] TrapGrammarParser.EffectContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="TrapGrammarParser.damage"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDamage([NotNull] TrapGrammarParser.DamageContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="TrapGrammarParser.damage"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDamage([NotNull] TrapGrammarParser.DamageContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="TrapGrammarParser.heal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterHeal([NotNull] TrapGrammarParser.HealContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="TrapGrammarParser.heal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitHeal([NotNull] TrapGrammarParser.HealContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="TrapGrammarParser.monsterSpawn"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMonsterSpawn([NotNull] TrapGrammarParser.MonsterSpawnContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="TrapGrammarParser.monsterSpawn"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMonsterSpawn([NotNull] TrapGrammarParser.MonsterSpawnContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="TrapGrammarParser.teleport"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTeleport([NotNull] TrapGrammarParser.TeleportContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="TrapGrammarParser.teleport"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTeleport([NotNull] TrapGrammarParser.TeleportContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="TrapGrammarParser.rangeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRangeDeclaration([NotNull] TrapGrammarParser.RangeDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="TrapGrammarParser.rangeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRangeDeclaration([NotNull] TrapGrammarParser.RangeDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="TrapGrammarParser.moveRoundDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMoveRoundDeclaration([NotNull] TrapGrammarParser.MoveRoundDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="TrapGrammarParser.moveRoundDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMoveRoundDeclaration([NotNull] TrapGrammarParser.MoveRoundDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="TrapGrammarParser.place"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPlace([NotNull] TrapGrammarParser.PlaceContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="TrapGrammarParser.place"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPlace([NotNull] TrapGrammarParser.PlaceContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="TrapGrammarParser.x"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterX([NotNull] TrapGrammarParser.XContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="TrapGrammarParser.x"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitX([NotNull] TrapGrammarParser.XContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="TrapGrammarParser.y"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterY([NotNull] TrapGrammarParser.YContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="TrapGrammarParser.y"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitY([NotNull] TrapGrammarParser.YContext context);
}
} // namespace LabWork1github
