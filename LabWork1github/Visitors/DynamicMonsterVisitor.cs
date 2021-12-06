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
                    if (context.distanceDeclare() != null)
                        newCommand.Distance = int.Parse(context.distanceDeclare().NUMBER().GetText());
                   
                    var direction = context.DIRECTION();
                    if (direction != null)
                    {
                        if (!(direction.Equals("F") || direction.Equals("L") || direction.Equals("B") || direction.Equals("R")))
                            throw new NotSupportedException("Wrong direction in Monster commandlist");
                        newCommand.Direction = direction.GetText();
                        newCommand.MoveDelegate = new MoveDelegate(MoveDirection);
                        Program.monsterTypes.ElementAt(i).Commands.Add(newCommand);
                        return base.VisitMoveDeclaration(context);
                    }
                    PlaceContext place = context.place();
                    if (place != null) {
                        int xPos = int.Parse(place.x().GetText());
                        int yPos = int.Parse(place.y().GetText());
                        newCommand.targetPlace = new Place(xPos, yPos);
                        newCommand.MoveDelegate = new MoveDelegate(MoveToPlace);
                        Program.monsterTypes.ElementAt(i).Commands.Add(newCommand);
                        return base.VisitMoveDeclaration(context);
                    }
                    var helpPlayer = context.PLAYER();
                    if(helpPlayer != null)
                    {
                        newCommand.targetPlace = Program.Board.Player.Place;
                        Program.monsterTypes.ElementAt(i).Commands.Add(newCommand);
                        return base.VisitMoveDeclaration(context);
                    }
                    var random = context.RANDOM();
                    if(random != null)
                    {
                        Random rand = new Random();
                        int XPos = (int)(rand.Next() % Program.Board.Height);
                        int YPos = (int)(rand.Next() % Program.Board.Width);
                        newCommand.targetPlace = new Place(XPos, YPos);
                        newCommand.MoveDelegate = new MoveDelegate(MoveToPlace);
                        Program.monsterTypes.ElementAt(i).Commands.Add(newCommand);
                        return base.VisitMoveDeclaration(context);

                    }

                }
            }
            return base.VisitMoveDeclaration(context);
        }


        public override object VisitDamageDeclaration([NotNull] DamageDeclarationContext context)
        {
            int damage = int.Parse(context.NUMBER().GetText());
            if (damage < 0)
                throw new NotSupportedException("Negative damage is not supported");

            return base.VisitDamageDeclaration(context);
        }

        public override object VisitShootDeclaration([NotNull] ShootDeclarationContext context)
        {
           
            for (int i = 0; i < Program.monsterTypes.Count; i++)
            {
                if (Program.monsterTypes.ElementAt(i).Name.Equals(typeName))
                {
                   
                    ShootCommand newCommand = new ShootCommand();
                    if (context.distanceDeclare() != null)
                        newCommand.Distance = int.Parse(context.distanceDeclare().NUMBER().GetText());

                    var direction = context.DIRECTION();
                    if (direction != null)
                    {
                        if (!(direction.Equals("F") || direction.Equals("L") || direction.Equals("B") || direction.Equals("R")))
                            throw new NotSupportedException("Wrong direction in Monster commandlist");
                        newCommand.Direction = direction.GetText();
                        newCommand.ShootDelegate = new ShootDelegate(ShootDirection);
                        Program.monsterTypes.ElementAt(i).Commands.Add(newCommand);
                        return base.VisitShootDeclaration(context);
                    }
                    PlaceContext place = context.place();
                    if (place != null)
                    {
                        int xPos = int.Parse(place.x().GetText());
                        int yPos = int.Parse(place.y().GetText());
                        newCommand.targetPlace = new Place(xPos, yPos);
                        newCommand.ShootDelegate = new ShootDelegate(ShootToPlace);
                        Program.monsterTypes.ElementAt(i).Commands.Add(newCommand);
                        return base.VisitShootDeclaration(context);
                    }
                    var helpPlayer = context.PLAYER();
                    if (helpPlayer != null)
                    {
                        newCommand.targetPlace = Program.Board.Player.Place;
                        newCommand.ShootDelegate = new ShootDelegate(ShootToPlayer);
                        Program.monsterTypes.ElementAt(i).Commands.Add(newCommand);
                        return base.VisitShootDeclaration(context);
                    }
                    var random = context.RANDOM();
                    if (random != null)
                    {
                        Random rand = new Random();
                        int XPos = (int)(rand.Next() % Program.Board.Height);
                        int YPos = (int)(rand.Next() % Program.Board.Width);
                        newCommand.targetPlace = new Place(XPos, YPos);
                        newCommand.ShootDelegate = new ShootDelegate(ShootToPlace);
                        Program.monsterTypes.ElementAt(i).Commands.Add(newCommand);
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
                    IfCommand newCommand = new IfCommand();
                   // newCommand.CommandCount = context.block().ChildCount - 2;

                    

                    Program.monsterTypes.ElementAt(i).Commands.Add(newCommand);
                    return base.VisitIfexpression(context);
                }
            }
            return base.VisitIfexpression(context);
        }

        public void MoveDirection(GameParamProvider provider, MoveCommand command)
        {
            
            switch (command.Direction)
            {
                case "F":
                    if ((int)provider.GetMonster().Place.X - command.Distance >= 0)
                        provider.GetMonster().Place.X -= (int)command.Distance;
                    break;
                case "B":
                    provider.GetMonster().Place.X += (int)command.Distance;
                    break;
                case "L":
                    if ((int)provider.GetMonster().Place.Y - command.Distance >= 0)
                        provider.GetMonster().Place.Y -= (int)command.Distance;
                    break;
                case "R":
                    provider.GetMonster().Place.Y += (int)command.Distance;
                    break;
            }
        }
        public void MoveToPlace(GameParamProvider provider, MoveCommand command)
        {
            provider.GetMonster().Place = command.targetPlace;
        }
        public void MoveToPlayer(GameParamProvider provider, MoveCommand command)
        {
            Random rand = new Random();
            if (rand.Next() % 2 == 0) {
                if (rand.Next() % 2 == 0)
                    provider.GetMonster().Place.X = provider.GetPlayer().Place.X + 1;
                else
                    provider.GetMonster().Place.X = provider.GetPlayer().Place.X - 1;
               }
            if(rand.Next() % 2 == 0)
                provider.GetMonster().Place.Y = provider.GetPlayer().Place.Y + 1;
            else
                provider.GetMonster().Place.Y = provider.GetPlayer().Place.Y - 1;

        }

        public void ShootDirection(GameParamProvider provider, ShootCommand command)
        {
            switch (command.Direction)
            {
                case "F":
                    if (provider.GetPlayer().Place.Y != provider.GetMonster().Place.Y)
                        break;
                    for (int i =0; i < command.Distance; i++) {
                        if ((int)provider.GetMonster().Place.X - i >= 0)
                            if (provider.GetPlayer().Place.X == provider.GetMonster().Place.X - (int)command.Distance)
                                provider.GetPlayer().Damage(command.Damage);
            }
                    break;
                case "B":
                    if (provider.GetPlayer().Place.Y != provider.GetMonster().Place.Y)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                            if (provider.GetPlayer().Place.X == provider.GetMonster().Place.X + (int)command.Distance)
                                provider.GetPlayer().Damage(command.Damage);
                    }
                    break;
                case "L":
                    if (provider.GetPlayer().Place.X != provider.GetMonster().Place.X)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMonster().Place.Y - i >= 0)
                            if (provider.GetPlayer().Place.Y == provider.GetMonster().Place.Y - (int)command.Distance)
                                provider.GetPlayer().Damage(command.Damage);
                    }
                    break;
                case "R":
                    if (provider.GetPlayer().Place.X != provider.GetMonster().Place.X)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                            if (provider.GetPlayer().Place.Y == provider.GetMonster().Place.Y + (int)command.Distance)
                                provider.GetPlayer().Damage(command.Damage);
                    }
                    break;
            }
        }

        public void ShootToPlace(GameParamProvider provider, ShootCommand command)
        {
            if (provider.GetPlayer().Place.Equals(command.targetPlace))
                provider.GetPlayer().Damage(command.Damage);
        }

        public void ShootToPlayer(GameParamProvider provider, ShootCommand command)
        {
                provider.GetPlayer().Damage(command.Damage);
        }

    }
}
