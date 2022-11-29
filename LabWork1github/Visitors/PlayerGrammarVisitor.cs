using Antlr4.Runtime.Misc;
using static LabWork1github.PlayerGrammarParser;

namespace LabWork1github
{
    class PlayerGrammarVisitor : PlayerGrammarBaseVisitor<object>
    {
        public override object VisitHealthCheckStatement([NotNull] HealthCheckStatementContext context)
        {
            Game.Move.CommandType = CommandType.health;
            return base.VisitHealthCheckStatement(context);
        }
        public override object VisitMovingStatement([NotNull] MovingStatementContext context)
        {
            Game.Move.CommandType = CommandType.move;
            Game.Move.Direction = context.direction().GetText();
            return base.VisitMovingStatement(context);
        }
        public override object VisitShootingStatement([NotNull] ShootingStatementContext context)
        {
            Game.Move.CommandType = CommandType.shoot;
            Game.Move.Direction = context.direction().GetText();
            return base.VisitShootingStatement(context);
        }
        public override object VisitHelpStatement([NotNull] HelpStatementContext context)
        {
            Game.Move.CommandType = CommandType.help;
            return base.VisitHelpStatement(context);
        }
    }
}
