using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using static LabWork1github.DynamicEnemyGrammarParser;

namespace LabWork1github
{
    public class DynamicEnemyGrammarVisitor : DynamicEnemyGrammarBaseVisitor<object>
    {
        private string typeName = "";
        private string type = null;

        public override object VisitTrapNameDeclaration([NotNull] TrapNameDeclarationContext context)
        {
            type = Types.TRAP;
            Program.EnemyTypes.Add(new TrapType(context.name().GetText()));
            typeName = context.name().GetText();
            return base.VisitTrapNameDeclaration(context);
        }
        public override object VisitMonsterNameDeclaration([NotNull] MonsterNameDeclarationContext context)
        {
            type = Types.MONSTER;
            Program.EnemyTypes.Add(new MonsterType(context.name().GetText()));
            typeName = context.name().GetText();
            return base.VisitMonsterNameDeclaration(context);
        }
        public override object VisitHealthDeclaration([NotNull] HealthDeclarationContext context)
        {
            if (type == Types.TRAP)
                throw new ArrayTypeMismatchException("Traps don't have Health");
            Program.GetEnemyType(typeName).Health = int.Parse(context.NUMBER().GetText());
            return base.VisitHealthDeclaration(context);
        }
        public override object VisitHealAmountDeclaration([NotNull] HealAmountDeclarationContext context)
        {
            if (type != Types.TRAP)
                throw new ArrayTypeMismatchException("Traps don't have Health");
            Program.GetEnemyType(typeName).Heal = int.Parse(context.NUMBER().GetText());
            return base.VisitHealAmountDeclaration(context);
        }
        public override object VisitDamageAmountDeclaration([NotNull] DamageAmountDeclarationContext context)
        {
            Program.GetEnemyType(typeName).Damage = int.Parse(context.NUMBER().GetText());
            return base.VisitDamageAmountDeclaration(context);
        }
        public override object VisitTeleportPointDeclaration([NotNull] TeleportPointDeclarationContext context)
        {
            if (type != Types.TRAP)
                throw new ArrayTypeMismatchException("Traps don't have Health");
            Program.GetEnemyType(typeName).TeleportPlace = new Place(uint.Parse(context.place().x().GetText()), uint.Parse(context.place().y().GetText()));
            return base.VisitTeleportPointDeclaration(context);
        }
        public override object VisitSpawnPointDeclaration([NotNull] SpawnPointDeclarationContext context)
        {
            if (type != Types.TRAP)
                throw new ArrayTypeMismatchException("Traps don't have Health");
            Program.GetEnemyType(typeName).SpawnPlace = new Place(uint.Parse(context.place().x().GetText()), uint.Parse(context.place().y().GetText()));
            return base.VisitSpawnPointDeclaration(context);
        }
        public override object VisitSpawnTypeDeclaration([NotNull] SpawnTypeDeclarationContext context)
        {
            if (type != Types.TRAP)
                throw new ArrayTypeMismatchException("Traps don't have Health");
            Program.GetEnemyType(typeName).SpawnType = Program.GetEnemyType(context.name().GetText());
            return base.VisitSpawnTypeDeclaration(context);
        }
        public override object VisitMoveDeclaration([NotNull] MoveDeclarationContext context)
        {
            if (Program.GetEnemyType(typeName) == null)
                throw new NullReferenceException("The type doesn't exist");

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
                Program.GetEnemyType(typeName).Commands.Add(newCommand);
                return base.VisitMoveDeclaration(context);
            }
            PlaceContext place = context.place();
            if (place != null)
            {
                uint xPos = uint.Parse(place.x().GetText());
                uint yPos = uint.Parse(place.y().GetText());
                newCommand.targetPlace = new Place(xPos, yPos);
                newCommand.MoveDelegate = new MoveDelegate(MoveToPlace);
                Program.GetEnemyType(typeName).Commands.Add(newCommand);
                return base.VisitMoveDeclaration(context);
            }
            var helpPlayer = context.PLAYER();
            if (helpPlayer != null)
            {
                newCommand.targetPlace = Program.Board.Player.Place;
                Program.GetEnemyType(typeName).Commands.Add(newCommand);
                return base.VisitMoveDeclaration(context);
            }
            var random = context.RANDOM();
            if (random != null)
            {
                Random rand = new Random();
                uint XPos = (uint)(rand.Next() % Program.Board.Height);
                uint YPos = (uint)(rand.Next() % Program.Board.Width);
                newCommand.targetPlace = new Place(XPos, YPos);
                newCommand.MoveDelegate = new MoveDelegate(MoveToPlace);
                Program.GetEnemyType(typeName).Commands.Add(newCommand);
                return base.VisitMoveDeclaration(context);

            }
            return base.VisitMoveDeclaration(context);
        }

        public override object VisitShootDeclaration([NotNull] ShootDeclarationContext context)
        {
            if (Program.GetEnemyType(typeName) == null)
                throw new NullReferenceException("The type doesn't exist");

            if(type.Equals(Types.TRAP))
                throw new ArrayTypeMismatchException("Traps can't shoot");

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
                Program.GetEnemyType(typeName).Commands.Add(newCommand);
                return base.VisitShootDeclaration(context);
            }
            PlaceContext place = context.place();
            if (place != null)
            {
                uint xPos = uint.Parse(place.x().GetText());
                uint yPos = uint.Parse(place.y().GetText());
                newCommand.targetPlace = new Place(xPos, yPos);
                newCommand.ShootDelegate = new ShootDelegate(ShootToPlace);
                Program.GetEnemyType(typeName).Commands.Add(newCommand);
                return base.VisitShootDeclaration(context);
            }
            var helpPlayer = context.PLAYER();
            if (helpPlayer != null)
            {
                newCommand.targetPlace = Program.Board.Player.Place;
                newCommand.ShootDelegate = new ShootDelegate(ShootToPlayer);
                Program.GetEnemyType(typeName).Commands.Add(newCommand);
                return base.VisitShootDeclaration(context);
            }
            var random = context.RANDOM();
            if (random != null)
            {
                Random rand = new Random();
                uint XPos = (uint)(rand.Next() % Program.Board.Height);
                uint YPos = (uint)(rand.Next() % Program.Board.Width);
                newCommand.targetPlace = new Place(XPos, YPos);
                newCommand.ShootDelegate = new ShootDelegate(ShootToPlace);
                Program.GetEnemyType(typeName).Commands.Add(newCommand);
                return base.VisitShootDeclaration(context);
            }

            return base.VisitShootDeclaration(context);
        }

        public override object VisitTeleportDeclaration([NotNull] TeleportDeclarationContext context)
        {
            if (Program.GetEnemyType(typeName) == null)
                throw new NullReferenceException("The type doesn't exist");

            if (!type.Equals(Types.TRAP))
                throw new ArrayTypeMismatchException("Monsters can't teleport");

            TeleportCommand newCommand = new TeleportCommand();
            if (context.place() != null)
            {
                newCommand.TargetPlace = new Place(uint.Parse(context.place().x().GetText()),
                                                    uint.Parse(context.place().y().GetText()));
            }
            else if (context.RANDOM() != null)
            {
                Random rand = new Random();
                uint XPos = (uint)(rand.Next() % Program.Board.Height);
                uint YPos = (uint)(rand.Next() % Program.Board.Width);
                newCommand.TargetPlace = new Place(XPos, YPos);
            }
            else
                newCommand.TargetPlace = Program.GetEnemyType(typeName).TeleportPlace;
            if(context.character() != null)
            {
                switch (context.character().GetText())
                {
                    case "trap":
                        throw new ArrayTypeMismatchException("Traps can't be teleported.");
                    case "player":
                        break;
                        //TODO: reafactor program then return here
                }
            }


            return base.VisitTeleportDeclaration(context);
        }




        public void TeleportCharacter(GameParamProvider provider, TeleportCommand command)
        {

        }

        public void MoveDirection(GameParamProvider provider, MoveCommand command)
        {

            switch (command.Direction)
            {
                case "F":
                    if ((int)provider.GetMonster().Place.X - command.Distance >= 0)
                        provider.GetMonster().Place.X -= (uint)command.Distance;
                    break;
                case "B":
                    provider.GetMonster().Place.X += (uint)command.Distance;
                    break;
                case "L":
                    if ((int)provider.GetMonster().Place.Y - command.Distance >= 0)
                        provider.GetMonster().Place.Y -= (uint)command.Distance;
                    break;
                case "R":
                    provider.GetMonster().Place.Y += (uint)command.Distance;
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
            if (rand.Next() % 2 == 0)
            {
                if (rand.Next() % 2 == 0)
                    provider.GetMonster().Place.X = provider.GetPlayer().Place.X + 1;
                else
                    provider.GetMonster().Place.X = provider.GetPlayer().Place.X - 1;
            }
            if (rand.Next() % 2 == 0)
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
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMonster().Place.X - i >= 0)
                            if (provider.GetPlayer().Place.X == provider.GetMonster().Place.X - (uint)command.Distance)
                                provider.GetPlayer().Damage(command.Damage);
                    }
                    break;
                case "B":
                    if (provider.GetPlayer().Place.Y != provider.GetMonster().Place.Y)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if (provider.GetPlayer().Place.X == provider.GetMonster().Place.X + (uint)command.Distance)
                            provider.GetPlayer().Damage(command.Damage);
                    }
                    break;
                case "L":
                    if (provider.GetPlayer().Place.X != provider.GetMonster().Place.X)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMonster().Place.Y - i >= 0)
                            if (provider.GetPlayer().Place.Y == provider.GetMonster().Place.Y - (uint)command.Distance)
                                provider.GetPlayer().Damage(command.Damage);
                    }
                    break;
                case "R":
                    if (provider.GetPlayer().Place.X != provider.GetMonster().Place.X)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if (provider.GetPlayer().Place.Y == provider.GetMonster().Place.Y + (uint)command.Distance)
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
