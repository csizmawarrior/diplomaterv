using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using LabWork1github;
using LabWork1github;
using static LabWork1github.PlayerGrammarParser;

namespace LabWork1github
{
    class PlayerGrammarVisitor : PlayerGrammarBaseVisitor<object>
    {
        public override object VisitHealthCheckStatement([NotNull] HealthCheckStatementContext context)
        {
            Game.move.CommandType = CommandType.health;
            return base.VisitHealthCheckStatement(context);
        }
        public override object VisitMovingStatement([NotNull] MovingStatementContext context)
        {
            Game.move.CommandType = CommandType.move;
            Game.move.Direction = context.direction().GetText();
            return base.VisitMovingStatement(context);
        }
        public override object VisitShootingStatement([NotNull] ShootingStatementContext context)
        {
            Game.move.CommandType = CommandType.shoot;
            Game.move.Direction = context.direction().GetText();
            return base.VisitShootingStatement(context);
        }
        public override object VisitHelpStatement([NotNull] HelpStatementContext context)
        {
            Game.move.CommandType = CommandType.help;
            return base.VisitHelpStatement(context);
        }
    }
}
