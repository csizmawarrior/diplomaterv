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
                    newCommand.Round = round;
                    var direction = context.DIRECTION();
                    if (direction != null)
                    {
                        if (!(direction.Equals("F") || direction.Equals("L") || direction.Equals("B") || direction.Equals("R")))
                            throw new NotSupportedException("Wrong direction in Monster commandlist");
                        newCommand.Direction = direction.GetText();
                        Program.monsterTypes.ElementAt(i).Moves.Add(newCommand);
                        return base.VisitMoveDeclaration(context);
                    }
                    PlaceContext place = context.place();
                    if (place != null) {
                        uint xPos = uint.Parse(place.x().GetText());
                        uint yPos = uint.Parse(place.y().GetText());
                        newCommand.targetPlace = new Place(xPos, yPos);
                        Program.monsterTypes.ElementAt(i).Moves.Add(newCommand);
                        return base.VisitMoveDeclaration(context);
                    }
                    var helpPlayer = context.PLAYER();
                    if(helpPlayer != null)
                    {
                        newCommand.targetPlace = Program.Board.Player.Place;
                        Program.monsterTypes.ElementAt(i).Moves.Add(newCommand);
                        return base.VisitMoveDeclaration(context);
                    }
                    var random = context.RANDOM();
                    if(random != null)
                    {
                        Random rand = new Random();
                        uint XPos = (uint)(rand.Next() % Program.Board.Height);
                        uint YPos = (uint)(rand.Next() % Program.Board.Width);
                        newCommand.targetPlace = new Place(XPos, YPos);
                    }

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
                        {
                            int dist = int.Parse(context.NUMBER().GetText());
                            if (dist < 0)
                                throw new NotSupportedException("negative distance not supported");
                                command.Distance = dist;
                        }
                    }
                }
            }
            return base.VisitDistanceDeclare(context);
        }

        public override object VisitDamageDeclaration([NotNull] DamageDeclarationContext context)
        {
            int damage = int.Parse(context.NUMBER().GetText());
            if (damage < 0)
                throw new NotSupportedException("Negative damage is not supported");

            for (int i = 0; i < Program.monsterTypes.Count; i++)
            {
                if (Program.monsterTypes.ElementAt(i).Name.Equals(typeName))
                {
                    foreach (var command in Program.monsterTypes.ElementAt(i).Moves)
                    {
                        if (command.Round == round)
                        { 
                            command.Damage = damage;
                            return base.VisitDamageDeclaration(context);
                        }
                     }

                    Program.monsterTypes.ElementAt(i).Damage = damage;
                }
            }
            return base.VisitDamageDeclaration(context);
        }

        public override object VisitShootDeclaration([NotNull] ShootDeclarationContext context)
        {

            for (int i = 0; i < Program.monsterTypes.Count; i++)
            {
                if (Program.monsterTypes.ElementAt(i).Name.Equals(typeName))
                {
                    ShootCommand newCommand = new ShootCommand();
                    newCommand.Round = round;
                    var direction = context.DIRECTION();
                    if (direction != null)
                    {
                        if (!(direction.Equals("F") || direction.Equals("L") || direction.Equals("B") || direction.Equals("R")))
                            throw new NotSupportedException("Wrong direction in Monster commandlist");
                        newCommand.Direction = direction.GetText();
                        Program.monsterTypes.ElementAt(i).Shoots.Add(newCommand);
                        return base.VisitShootDeclaration(context);
                    }
                    PlaceContext place = context.place();
                    if (place != null)
                    {
                        uint xPos = uint.Parse(place.x().GetText());
                        uint yPos = uint.Parse(place.y().GetText());
                        newCommand.targetPlace = new Place(xPos, yPos);
                        Program.monsterTypes.ElementAt(i).Shoots.Add(newCommand);
                        return base.VisitShootDeclaration(context);
                    }
                    //delegate
                    var helpPlayer = context.PLAYER();
                    if (helpPlayer != null)
                    {
                        newCommand.targetPlace = Program.Board.Player.Place;
                        Program.monsterTypes.ElementAt(i).Shoots.Add(newCommand);
                        return base.VisitShootDeclaration(context);
                    }
                    var random = context.RANDOM();
                    if (random != null)
                    {
                        Random rand = new Random();
                        uint XPos = (uint)(rand.Next() % Program.Board.Height);
                        uint YPos = (uint)(rand.Next() % Program.Board.Width);
                        newCommand.targetPlace = new Place(XPos, YPos);
                    }
                }
            }

                    return base.VisitShootDeclaration(context);
        }

        public void moveDirection(Player player, List<Monster> monsters, List<Trap> traps, int round)
        {
            for (int i = 0; i < Program.monsterTypes.Count; i++)
            {

                if (Program.monsterTypes.ElementAt(i).Name.Equals(typeName))
                {

                }
            }
        }

        public override object VisitIfexpression([NotNull] IfexpressionContext context)
        {

            return base.VisitIfexpression(context);
        }
    }
}
