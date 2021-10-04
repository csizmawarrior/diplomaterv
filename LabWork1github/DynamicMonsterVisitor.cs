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

        private bool condition = false;

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
                   
                    var direction = context.DIRECTION();
                    if (direction != null)
                    {
                        if (!(direction.Equals("F") || direction.Equals("L") || direction.Equals("B") || direction.Equals("R")))
                            throw new NotSupportedException("Wrong direction in Monster commandlist");
                        newCommand.Direction = direction.GetText();
                        newCommand.MoveDelegate = new MoveDelegate(moveDirection);
                        Program.monsterTypes.ElementAt(i).Moves.Add(newCommand);
                        return base.VisitMoveDeclaration(context);
                    }
                    PlaceContext place = context.place();
                    if (place != null) {
                        uint xPos = uint.Parse(place.x().GetText());
                        uint yPos = uint.Parse(place.y().GetText());
                        newCommand.targetPlace = new Place(xPos, yPos);
                        newCommand.MoveDelegate = new MoveDelegate(moveToPlace);
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
                        newCommand.MoveDelegate = new MoveDelegate(moveToPlace);
                        Program.monsterTypes.ElementAt(i).Moves.Add(newCommand);
                        return base.VisitMoveDeclaration(context);

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
                       
                            int dist = int.Parse(context.NUMBER().GetText());
                            if (dist < 0)
                                throw new NotSupportedException("negative distance not supported");
                                command.Distance = dist;
                        
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
                       
                            command.Damage = damage;
                            return base.VisitDamageDeclaration(context);
                        
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
                    var direction = context.DIRECTION();
                    if (direction != null)
                    {
                        if (!(direction.Equals("F") || direction.Equals("L") || direction.Equals("B") || direction.Equals("R")))
                            throw new NotSupportedException("Wrong direction in Monster commandlist");
                        newCommand.Direction = direction.GetText();
                        newCommand.ShootDelegate = new ShootDelegate(shootDirection);
                        Program.monsterTypes.ElementAt(i).Shoots.Add(newCommand);
                        return base.VisitShootDeclaration(context);
                    }
                    PlaceContext place = context.place();
                    if (place != null)
                    {
                        uint xPos = uint.Parse(place.x().GetText());
                        uint yPos = uint.Parse(place.y().GetText());
                        newCommand.targetPlace = new Place(xPos, yPos);
                        newCommand.ShootDelegate = new ShootDelegate(shootToPlace);
                        Program.monsterTypes.ElementAt(i).Shoots.Add(newCommand);
                        return base.VisitShootDeclaration(context);
                    }
                    var helpPlayer = context.PLAYER();
                    if (helpPlayer != null)
                    {
                        newCommand.targetPlace = Program.Board.Player.Place;
                        newCommand.ShootDelegate = new ShootDelegate(shootToPlayer);
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
                        newCommand.ShootDelegate = new ShootDelegate(shootToPlace);
                        Program.monsterTypes.ElementAt(i).Shoots.Add(newCommand);
                        return base.VisitShootDeclaration(context);
                    }
                }
            }

                    return base.VisitShootDeclaration(context);
        }

        public override object VisitIfexpression([NotNull] IfexpressionContext context)
        {
            
            for (int i = 0; i < Program.monsterTypes.Count; i++)
            {
                if (Program.monsterTypes.ElementAt(i).Name.Equals(typeName))
                {
                    //for (int j = 0; j < Program.monsterTypes.ElementAt(i).Whiles.Count; j++)
                    //{
                    //    
                    //}
                    IfCommand newCommand = new IfCommand();
                    Program.monsterTypes.ElementAt(i).Ifs.Add(newCommand);
                    return base.VisitIfexpression(context);
                }
            }
            return base.VisitIfexpression(context);
        }
        public override object VisitBooloperation([NotNull] BooloperationContext context)
        {

            return base.VisitBooloperation(context);
        }

        public void moveDirection(Player player, Monster monster, MoveCommand command)
        {
            
            switch (command.Direction)
            {
                case "F":
                    if ((int)monster.Place.X - command.Distance >= 0)
                        monster.Place.X -= (uint)command.Distance;
                    break;
                case "B":
                    monster.Place.X += (uint)command.Distance;
                    break;
                case "L":
                    if ((int)monster.Place.Y - command.Distance >= 0)
                        monster.Place.Y -= (uint)command.Distance;
                    break;
                case "R":
                    monster.Place.Y += (uint)command.Distance;
                    break;
            }
        }
        public void moveToPlace(Player player, Monster monster, MoveCommand command)
        {
            monster.Place = command.targetPlace;
        }
        public void moveToPlayer(Player player, List<Monster> monsters, Monster monster, List<Trap> traps, MoveCommand command)
        {
            Random rand = new Random();
            if (rand.Next() % 2 == 0) {
                if (rand.Next() % 2 == 0)
                    monster.Place.X = player.Place.X + 1;
                else
                    monster.Place.X = player.Place.X - 1;
               }
            if(rand.Next() % 2 == 0)
                monster.Place.Y = player.Place.Y + 1;
            else
                monster.Place.Y = player.Place.Y - 1;

        }

        public void shootDirection(Player player, Monster monster, ShootCommand command)
        {
            switch (command.Direction)
            {
                case "F":
                    if (player.Place.Y != monster.Place.Y)
                        break;
                    for (int i =0; i < command.Distance; i++) {
                        if ((int)monster.Place.X - i >= 0)
                            if (player.Place.X == monster.Place.X - (uint)command.Distance)
                                player.Damage(command.Damage);
            }
                    break;
                case "B":
                    if (player.Place.Y != monster.Place.Y)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                            if (player.Place.X == monster.Place.X + (uint)command.Distance)
                                player.Damage(command.Damage);
                    }
                    break;
                case "L":
                    if (player.Place.X != monster.Place.X)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)monster.Place.Y - i >= 0)
                            if (player.Place.Y == monster.Place.Y - (uint)command.Distance)
                                player.Damage(command.Damage);
                    }
                    break;
                case "R":
                    if (player.Place.X != monster.Place.X)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                            if (player.Place.Y == monster.Place.Y + (uint)command.Distance)
                                player.Damage(command.Damage);
                    }
                    break;
            }
        }

        public void shootToPlace(Player player, Monster monster, ShootCommand command)
        {
            if (player.Place.Equals(command.targetPlace))
                player.Damage(command.Damage);
        }

        public void shootToPlayer(Player player, Monster monster, ShootCommand command)
        {
                player.Damage(command.Damage);
        }


    }
}
