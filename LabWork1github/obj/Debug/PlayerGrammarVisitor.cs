//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Dana\source\repos\visual studio 2019\LabWork1github\g4 files\PlayerGrammar.g4 by ANTLR 4.6.6

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
/// by <see cref="PlayerGrammarParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface IPlayerGrammarVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="PlayerGrammarParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] PlayerGrammarParser.ProgramContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlayerGrammarParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] PlayerGrammarParser.StatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlayerGrammarParser.direction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDirection([NotNull] PlayerGrammarParser.DirectionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlayerGrammarParser.movingStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMovingStatement([NotNull] PlayerGrammarParser.MovingStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlayerGrammarParser.shootingStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitShootingStatement([NotNull] PlayerGrammarParser.ShootingStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlayerGrammarParser.healthCheckStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHealthCheckStatement([NotNull] PlayerGrammarParser.HealthCheckStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlayerGrammarParser.helpStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHelpStatement([NotNull] PlayerGrammarParser.HelpStatementContext context);
}
} // namespace LabWork1github
