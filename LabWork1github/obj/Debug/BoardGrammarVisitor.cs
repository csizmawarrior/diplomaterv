//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Dana\source\repos\visual studio 2019\LabWork1github\g4 files\BoardGrammar.g4 by ANTLR 4.6.6

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
/// by <see cref="BoardGrammarParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface IBoardGrammarVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="BoardGrammarParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] BoardGrammarParser.ProgramContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="BoardGrammarParser.statementList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementList([NotNull] BoardGrammarParser.StatementListContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="BoardGrammarParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] BoardGrammarParser.StatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="BoardGrammarParser.typeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeName([NotNull] BoardGrammarParser.TypeNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="BoardGrammarParser.place"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPlace([NotNull] BoardGrammarParser.PlaceContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="BoardGrammarParser.x"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitX([NotNull] BoardGrammarParser.XContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="BoardGrammarParser.y"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitY([NotNull] BoardGrammarParser.YContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="BoardGrammarParser.boardCreation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBoardCreation([NotNull] BoardGrammarParser.BoardCreationContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="BoardGrammarParser.playerPlacement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPlayerPlacement([NotNull] BoardGrammarParser.PlayerPlacementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="BoardGrammarParser.monsterPlacement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMonsterPlacement([NotNull] BoardGrammarParser.MonsterPlacementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="BoardGrammarParser.trapPlacement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTrapPlacement([NotNull] BoardGrammarParser.TrapPlacementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="BoardGrammarParser.nameDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNameDeclaration([NotNull] BoardGrammarParser.NameDeclarationContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="BoardGrammarParser.partnerDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPartnerDeclaration([NotNull] BoardGrammarParser.PartnerDeclarationContext context);
}
} // namespace LabWork1github
