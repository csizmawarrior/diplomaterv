//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Dana\source\repos\LabWork1github\LabWork1github\g4 files\DynamicEnemyGrammar.g4 by ANTLR 4.6.6

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
/// <see cref="DynamicEnemyGrammarParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface IDynamicEnemyGrammarListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.definition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDefinition([NotNull] DynamicEnemyGrammarParser.DefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.definition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDefinition([NotNull] DynamicEnemyGrammarParser.DefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterName([NotNull] DynamicEnemyGrammarParser.NameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitName([NotNull] DynamicEnemyGrammarParser.NameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.statementList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatementList([NotNull] DynamicEnemyGrammarParser.StatementListContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.statementList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatementList([NotNull] DynamicEnemyGrammarParser.StatementListContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement([NotNull] DynamicEnemyGrammarParser.StatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement([NotNull] DynamicEnemyGrammarParser.StatementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.nameDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNameDeclaration([NotNull] DynamicEnemyGrammarParser.NameDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.nameDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNameDeclaration([NotNull] DynamicEnemyGrammarParser.NameDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.trapNameDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTrapNameDeclaration([NotNull] DynamicEnemyGrammarParser.TrapNameDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.trapNameDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTrapNameDeclaration([NotNull] DynamicEnemyGrammarParser.TrapNameDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.monsterNameDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMonsterNameDeclaration([NotNull] DynamicEnemyGrammarParser.MonsterNameDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.monsterNameDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMonsterNameDeclaration([NotNull] DynamicEnemyGrammarParser.MonsterNameDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.healthDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterHealthDeclaration([NotNull] DynamicEnemyGrammarParser.HealthDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.healthDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitHealthDeclaration([NotNull] DynamicEnemyGrammarParser.HealthDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.healAmountDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterHealAmountDeclaration([NotNull] DynamicEnemyGrammarParser.HealAmountDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.healAmountDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitHealAmountDeclaration([NotNull] DynamicEnemyGrammarParser.HealAmountDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.damageAmountDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDamageAmountDeclaration([NotNull] DynamicEnemyGrammarParser.DamageAmountDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.damageAmountDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDamageAmountDeclaration([NotNull] DynamicEnemyGrammarParser.DamageAmountDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.teleportPointDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTeleportPointDeclaration([NotNull] DynamicEnemyGrammarParser.TeleportPointDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.teleportPointDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTeleportPointDeclaration([NotNull] DynamicEnemyGrammarParser.TeleportPointDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.spawnPointDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSpawnPointDeclaration([NotNull] DynamicEnemyGrammarParser.SpawnPointDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.spawnPointDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSpawnPointDeclaration([NotNull] DynamicEnemyGrammarParser.SpawnPointDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.spawnTypeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSpawnTypeDeclaration([NotNull] DynamicEnemyGrammarParser.SpawnTypeDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.spawnTypeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSpawnTypeDeclaration([NotNull] DynamicEnemyGrammarParser.SpawnTypeDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.distanceDeclare"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDistanceDeclare([NotNull] DynamicEnemyGrammarParser.DistanceDeclareContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.distanceDeclare"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDistanceDeclare([NotNull] DynamicEnemyGrammarParser.DistanceDeclareContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.moveDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMoveDeclaration([NotNull] DynamicEnemyGrammarParser.MoveDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.moveDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMoveDeclaration([NotNull] DynamicEnemyGrammarParser.MoveDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.shootDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterShootDeclaration([NotNull] DynamicEnemyGrammarParser.ShootDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.shootDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitShootDeclaration([NotNull] DynamicEnemyGrammarParser.ShootDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.damageDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDamageDeclaration([NotNull] DynamicEnemyGrammarParser.DamageDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.damageDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDamageDeclaration([NotNull] DynamicEnemyGrammarParser.DamageDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.healDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterHealDeclaration([NotNull] DynamicEnemyGrammarParser.HealDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.healDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitHealDeclaration([NotNull] DynamicEnemyGrammarParser.HealDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.spawnDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSpawnDeclaration([NotNull] DynamicEnemyGrammarParser.SpawnDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.spawnDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSpawnDeclaration([NotNull] DynamicEnemyGrammarParser.SpawnDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.teleportDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTeleportDeclaration([NotNull] DynamicEnemyGrammarParser.TeleportDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.teleportDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTeleportDeclaration([NotNull] DynamicEnemyGrammarParser.TeleportDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.ifexpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIfexpression([NotNull] DynamicEnemyGrammarParser.IfexpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.ifexpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIfexpression([NotNull] DynamicEnemyGrammarParser.IfexpressionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.whileexpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWhileexpression([NotNull] DynamicEnemyGrammarParser.WhileexpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.whileexpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWhileexpression([NotNull] DynamicEnemyGrammarParser.WhileexpressionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlock([NotNull] DynamicEnemyGrammarParser.BlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlock([NotNull] DynamicEnemyGrammarParser.BlockContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.character"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCharacter([NotNull] DynamicEnemyGrammarParser.CharacterContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.character"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCharacter([NotNull] DynamicEnemyGrammarParser.CharacterContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.possibleAttributes"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPossibleAttributes([NotNull] DynamicEnemyGrammarParser.PossibleAttributesContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.possibleAttributes"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPossibleAttributes([NotNull] DynamicEnemyGrammarParser.PossibleAttributesContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.attributeReference"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAttributeReference([NotNull] DynamicEnemyGrammarParser.AttributeReferenceContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.attributeReference"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAttributeReference([NotNull] DynamicEnemyGrammarParser.AttributeReferenceContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.place"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPlace([NotNull] DynamicEnemyGrammarParser.PlaceContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.place"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPlace([NotNull] DynamicEnemyGrammarParser.PlaceContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.x"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterX([NotNull] DynamicEnemyGrammarParser.XContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.x"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitX([NotNull] DynamicEnemyGrammarParser.XContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.y"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterY([NotNull] DynamicEnemyGrammarParser.YContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.y"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitY([NotNull] DynamicEnemyGrammarParser.YContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression([NotNull] DynamicEnemyGrammarParser.ExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression([NotNull] DynamicEnemyGrammarParser.ExpressionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.something"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSomething([NotNull] DynamicEnemyGrammarParser.SomethingContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.something"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSomething([NotNull] DynamicEnemyGrammarParser.SomethingContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="DynamicEnemyGrammarParser.operation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperation([NotNull] DynamicEnemyGrammarParser.OperationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="DynamicEnemyGrammarParser.operation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperation([NotNull] DynamicEnemyGrammarParser.OperationContext context);
}
} // namespace LabWork1github
