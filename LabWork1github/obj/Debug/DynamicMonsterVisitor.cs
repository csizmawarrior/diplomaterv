//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Dana\Source\Repos\LabWork1github\LabWork1github\DynamicMonster.g4 by ANTLR 4.6.6

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
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="DynamicMonsterParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface IDynamicMonsterVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.definition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDefinition([NotNull] DynamicMonsterParser.DefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitName([NotNull] DynamicMonsterParser.NameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.statementList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementList([NotNull] DynamicMonsterParser.StatementListContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] DynamicMonsterParser.StatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.nameDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNameDeclaration([NotNull] DynamicMonsterParser.NameDeclarationContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.healthDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHealthDeclaration([NotNull] DynamicMonsterParser.HealthDeclarationContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.damageDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDamageDeclaration([NotNull] DynamicMonsterParser.DamageDeclarationContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.distanceDeclare"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDistanceDeclare([NotNull] DynamicMonsterParser.DistanceDeclareContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.moveDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMoveDeclaration([NotNull] DynamicMonsterParser.MoveDeclarationContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.shootDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitShootDeclaration([NotNull] DynamicMonsterParser.ShootDeclarationContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.ifexpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfexpression([NotNull] DynamicMonsterParser.IfexpressionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.whileexpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileexpression([NotNull] DynamicMonsterParser.WhileexpressionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlock([NotNull] DynamicMonsterParser.BlockContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.character"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCharacter([NotNull] DynamicMonsterParser.CharacterContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.possibleAttributes"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPossibleAttributes([NotNull] DynamicMonsterParser.PossibleAttributesContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.place"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPlace([NotNull] DynamicMonsterParser.PlaceContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.x"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitX([NotNull] DynamicMonsterParser.XContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.y"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitY([NotNull] DynamicMonsterParser.YContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] DynamicMonsterParser.ExpressionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.something"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSomething([NotNull] DynamicMonsterParser.SomethingContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="DynamicMonsterParser.operation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOperation([NotNull] DynamicMonsterParser.OperationContext context);
}
} // namespace LabWork1github
