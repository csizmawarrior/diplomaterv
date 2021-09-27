using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabWork1github.DynamicMonsterParser;
using LabWork1github;

namespace LabWork1github
{
    class DynamicMonsterVisitor : DynamicMonsterBaseVisitor<object>
    {
        private string typeName = "";
        private int round = 0;

        public override object Visit([NotNull] IParseTree tree)
        {
            return base.Visit(tree);
        }

        public override object VisitStatementList(StatementListContext context)
        {
            return base.VisitStatementList(context);
        }

        public override object VisitStatement([NotNull] StatementContext context)
        {
            round++;
            return base.VisitStatement(context);
        }

        public override object VisitNameDeclaration([NotNull] NameDeclarationContext context)
        {
            typeName = context.name().GetText();
            Program.monsterTypes.Add(new MonsterType(typeName));
            return base.VisitNameDeclaration(context);
        }
        public override object VisitRangeDeclaration([NotNull] RangeDeclarationContext context)
        {
            for(int i = 0; i < Program.monsterTypes.Count; i++)
            {
                if (Program.monsterTypes.ElementAt(i).Name.Equals(typeName))
                    Program.monsterTypes.ElementAt(i).Range = int.Parse(context.NUMBER().GetText());
            }
            return base.VisitRangeDeclaration(context);
        }
        public override object VisitHealthDeclaration([NotNull] HealthDeclarationContext context)
        {
            for (int i = 0; i < Program.monsterTypes.Count; i++)
            {
                if (Program.monsterTypes.ElementAt(i).Name.Equals(typeName))
                    Program.monsterTypes.ElementAt(i).Health = int.Parse(context.NUMBER().GetText());
            }
            return base.VisitHealthDeclaration(context);
        }
        public override object VisitMoveDeclaration([NotNull] MoveDeclarationContext context)
        {
            
            for (int i = 0; i < Program.monsterTypes.Count; i++)
            {
                if (Program.monsterTypes.ElementAt(i).Name.Equals(typeName)) {
                    MoveCommand newCommand = new MoveCommand();
                    var direction = context.DIRECTION();
                    if (direction != null)
                        newCommand.Direction = direction.GetText();
                    PlaceContext place = context.place();
                    if (place != null) {
                        uint xPos = uint.Parse(place.x().GetText());
                        uint yPos = uint.Parse(place.y().GetText());
                        newCommand.targetPlace = new Place(xPos, yPos);
                    }
                    var helpPlayer = context.PLAYER();
                    if(helpPlayer != null)
                    {
                        newCommand.targetPlace = Program.Board.Player.Place;
                    }
                    newCommand.Round = round;
                    Program.monsterTypes.ElementAt(i).Moves.Add(newCommand);
                }
            }
            return base.VisitMoveDeclaration(context);
        }
        public override object VisitDistanceDeclare([NotNull] DistanceDeclareContext context)
        {
            for (int i = 0; i < Program.monsterTypes.Count; i++)
            {
                if (Program.monsterTypes.ElementAt(i).Name.Equals(typeName))
                {
                    foreach(var command in Program.monsterTypes.ElementAt(i).Moves)
                    {
                        if (command.Round == round)
                            command.Distance = int.Parse(context.NUMBER().GetText());
                    }
                }
            }
            return base.VisitDistanceDeclare(context);
        }
    }
}
