using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using LabWork1github.Commands;
using LabWork1github.Visitors;
using static LabWork1github.DynamicEnemyGrammarParser;

namespace LabWork1github
{
    public class DynamicEnemyGrammarVisitor : DynamicEnemyGrammarBaseVisitor<object>
    {
        private string typeName = "";
        private string type = null;
        private List<int> ConditionCount = new List<int>();
        private List<Command> ConditionalCommands = new List<Command>();
        private bool HealthDeclare { get; set; } = false;
        private bool HealAmountDeclare { get; set; } = false;
        private bool DamageAmountDeclare { get; set; } = false;
        private bool TeleportPointDeclare { get; set; } = false;
        private bool SpawnTypeDeclare { get; set; } = false;
        private bool SpawnPointDeclare { get; set; } = false;
        public string Error = "";
        public bool ErrorFound = false;
        public override object VisitDefinition([NotNull] DefinitionContext context)
        {
            foreach (var child in context.statementList())
            {
                typeName = "";
                type = null;
                ConditionCount = new List<int>();
                ConditionalCommands = new List<Command>();
                HealthDeclare = false;
                HealAmountDeclare = false;
                DamageAmountDeclare = false;
                TeleportPointDeclare = false;
                SpawnTypeDeclare = false;
                SpawnPointDeclare = false;
                Error = "";
                ErrorFound = false;

                VisitStatementList(child);
            }
                if(type.Equals(Types.MONSTER) && (!HealthDeclare || !DamageAmountDeclare))
                {
                    if (!HealthDeclare)
                        Program.GetCharacterType(typeName).Health = Program.GetCharacterType("DefaultMonster").Health;
                    if (!DamageAmountDeclare)
                        Program.GetCharacterType(typeName).Health = Program.GetCharacterType("DefaultMonster").Damage;
                }
                if (type.Equals(Types.TRAP) && !HealAmountDeclare && !DamageAmountDeclare && (!SpawnPointDeclare || !SpawnTypeDeclare) && !TeleportPointDeclare) {
                    Program.GetCharacterType(typeName).Damage = Program.GetCharacterType("DefaultTrap").Damage;
                }

            //since I manually visit every children of the definition, no need to return the base visit function, only a null
            return null;
        }
        public override object VisitTrapNameDeclaration([NotNull] TrapNameDeclarationContext context)
        {
            if (Program.GetCharacterType(typeName) != null)
            {
                Error += "Trap with this type already exists:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else
            {
                type = Types.TRAP;
                Program.CharacterTypes.Add(new TrapType(context.name().GetText()));
            }
            typeName = context.name().GetText();
            return base.VisitTrapNameDeclaration(context);
        }
        public override object VisitMonsterNameDeclaration([NotNull] MonsterNameDeclarationContext context)
        {
            if (Program.GetCharacterType(typeName) != null)
            {
                Error += "Monster with this type already exists:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else
            {
                type = Types.MONSTER;
                Program.CharacterTypes.Add(new MonsterType(context.name().GetText()));
            }
            typeName = context.name().GetText();
            HealthDeclare = false;
            HealAmountDeclare = false;
            DamageAmountDeclare = false;
            TeleportPointDeclare = false;
            SpawnPointDeclare = false;
            SpawnTypeDeclare = false;

            return base.VisitMonsterNameDeclaration(context);
        }
        public override object VisitHealthDeclaration([NotNull] HealthDeclarationContext context)
        {
            if (type == Types.TRAP)
            {
                Error += "Trap doesn't have health:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else if (HealthDeclare)
            {
                Error += "Health amount was already declared:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else
            {
                Program.GetCharacterType(typeName).Health = int.Parse(context.NUMBER().GetText());
                HealthDeclare = true;
            }
                return base.VisitHealthDeclaration(context);
            
        }
        public override object VisitHealAmountDeclaration([NotNull] HealAmountDeclarationContext context)
        {
            if (type != Types.TRAP)
            {
                Error += "A non Trap wants to heal:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else if (HealAmountDeclare)
            {
                Error += "Heal amount was already declared:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else
            {
                Program.GetCharacterType(typeName).Heal = int.Parse(context.NUMBER().GetText());
                HealAmountDeclare = true;
            }
            return base.VisitHealAmountDeclaration(context);
        }
        public override object VisitDamageAmountDeclaration([NotNull] DamageAmountDeclarationContext context)
        {
            if (DamageAmountDeclare)
            {
                Error += "Damage amount was already declared:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else
            {
                Program.GetCharacterType(typeName).Damage = int.Parse(context.NUMBER().GetText());
                DamageAmountDeclare = true;
            }
            return base.VisitDamageAmountDeclaration(context);
        }
        public override object VisitTeleportPointDeclaration([NotNull] TeleportPointDeclarationContext context)
        {
            if (type != Types.TRAP)
            {
                Error += "A non Trap wants to teleport:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            if (TeleportPointDeclare)
            {
                Error += "Teleport destination was already declared:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else
            {
                Program.GetCharacterType(typeName).TeleportPlace = new Place(int.Parse(context.place().x().GetText()), int.Parse(context.place().y().GetText()));
                TeleportPointDeclare = true;
            }
            return base.VisitTeleportPointDeclaration(context);
        }
        public override object VisitSpawnPointDeclaration([NotNull] SpawnPointDeclarationContext context)
        {
            if (type != Types.TRAP)
            {
                Error += "A non Trap wants to spawn:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            if (SpawnPointDeclare)
            {
                Error += "Spawn destination was already declared:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else
            {
                Program.GetCharacterType(typeName).SpawnPlace = new Place(int.Parse(context.place().x().GetText()), int.Parse(context.place().y().GetText()));
                SpawnPointDeclare = true;
            }
            return base.VisitSpawnPointDeclaration(context);
        }
        public override object VisitSpawnTypeDeclaration([NotNull] SpawnTypeDeclarationContext context)
        {
            if (type != Types.TRAP)
            {
                Error += "A non Trap wants to spawn:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            if (SpawnTypeDeclare)
            {
                Error += "Spawning enemy type has been already declared:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            if(Program.GetCharacterType(context.name().GetText()) == null)
            {
                Error += "Spawning enemy type doesn't exist at place:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else
            {
                Program.GetCharacterType(typeName).SpawnType = Program.GetCharacterType(context.name().GetText());
                SpawnTypeDeclare = true;
            }
            return base.VisitSpawnTypeDeclaration(context);
        }
        public override object VisitMoveDeclaration([NotNull] MoveDeclarationContext context)
        {

            MoveCommand newCommand = new MoveCommand();
            if (context.distanceDeclare() != null)
                newCommand.Distance = int.Parse(context.distanceDeclare().NUMBER().GetText());
            var direction = context.DIRECTION();
            if (direction != null)
            {
                if (!(direction.GetText().Equals("F") || direction.GetText().Equals("L") || direction.GetText().Equals("B") ||
                    direction.GetText().Equals("R")))
                {
                    Error += "Wrong direction used:\n";
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                    
                newCommand.Direction = direction.GetText();
                newCommand.MoveDelegate = new MoveDelegate(MoveDirection);
                AddCommand(newCommand);
                return base.VisitMoveDeclaration(context);
            }
            PlaceContext place = context.place();
            if (place != null)
            {
                int xPos = int.Parse(place.x().GetText());
                int yPos = int.Parse(place.y().GetText());
                newCommand.TargetPlace = new Place(xPos, yPos);
                newCommand.MoveDelegate = new MoveDelegate(MoveToPlace);
                AddCommand(newCommand);
                return base.VisitMoveDeclaration(context);
            }
            var helpPlayer = context.PLAYER();
            if (helpPlayer != null)
            {
                newCommand.MoveDelegate = new MoveDelegate(MoveToPlayer);
                AddCommand(newCommand);
                return base.VisitMoveDeclaration(context);
            }

            var random = context.RANDOM();
            if (random != null)
            {
                newCommand.MoveDelegate = new MoveDelegate(MoveRandom);
                AddCommand(newCommand);
                return base.VisitMoveDeclaration(context);

            }
            return base.VisitMoveDeclaration(context);
        }

        public override object VisitShootDeclaration([NotNull] ShootDeclarationContext context)
        {
            if (type.Equals(Types.TRAP))
            {
                Error += "Trap wants to shoot:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else
            {
                ShootCommand newCommand = new ShootCommand();
                if (context.distanceDeclare() != null)
                    newCommand.Distance = int.Parse(context.distanceDeclare().NUMBER().GetText());

                if (context.damageAmountDeclaration() != null)
                {
                    newCommand.Damage = int.Parse(context.damageAmountDeclaration().NUMBER().GetText());
                }

                var direction = context.DIRECTION();
                if (direction != null)
                {
                    if (!(direction.GetText().Equals("F") || direction.GetText().Equals("L") ||
                        direction.GetText().Equals("B") || direction.GetText().Equals("R")))
                    {
                        Error += "Wrong direction used:\n";
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                    newCommand.Direction = direction.GetText();
                    newCommand.ShootDelegate = new ShootDelegate(ShootDirection);
                    AddCommand(newCommand);
                    return base.VisitShootDeclaration(context);
                }
                PlaceContext place = context.place();
                if (place != null)
                {
                    int xPos = int.Parse(place.x().GetText());
                    int yPos = int.Parse(place.y().GetText());
                    newCommand.TargetPlace = new Place(xPos, yPos);
                    newCommand.ShootDelegate = new ShootDelegate(ShootToPlace);
                    AddCommand(newCommand);
                    return base.VisitShootDeclaration(context);
                }

                var helpPlayer = context.PLAYER();
                if (helpPlayer != null)
                {
                    newCommand.ShootDelegate = new ShootDelegate(ShootToPlayer);
                    AddCommand(newCommand);
                    return base.VisitShootDeclaration(context);
                }

                var random = context.RANDOM();
                if (random != null)
                {
                    newCommand.ShootDelegate = new ShootDelegate(ShootRandom);
                    AddCommand(newCommand);
                    return base.VisitShootDeclaration(context);
                }
            }
            return base.VisitShootDeclaration(context);
        }

        public override object VisitTeleportDeclaration([NotNull] TeleportDeclarationContext context)
        {
            if (!type.Equals(Types.TRAP))
            {
                Error += "A non trap wants to teleport:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }

            TeleportCommand newCommand = new TeleportCommand();
            if (context.place() != null)
            {
                newCommand.TargetPlace = new Place(int.Parse(context.place().x().GetText()),
                                                    int.Parse(context.place().y().GetText()));
            }
            else
                newCommand.TargetPlace = Program.GetCharacterType(typeName).TeleportPlace;
            if(context.character() != null)
            {
                switch (context.character().GetText())
                {
                    case Types.PLAYER:
                        newCommand.TeleportDelegate = new TeleportDelegate(TeleportPlayer);
                        AddCommand(newCommand);
                        break;
                    case Types.MONSTER:
                        newCommand.TeleportDelegate = new TeleportDelegate(TeleportMonster);
                        AddCommand(newCommand);
                        break;
                    case Types.TRAP:
                        newCommand.TeleportDelegate = new TeleportDelegate(TeleportTrap);
                        AddCommand(newCommand);
                        break;
                    case "me":
                        Error += "You can't teleport yourself:\n";
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                        break;
                }
            }
            if (context.RANDOM() != null)
            {
                newCommand.Random = true;
            }
            return base.VisitTeleportDeclaration(context);
        }

        public override object VisitSpawnDeclaration([NotNull] SpawnDeclarationContext context)
        {
            if (!type.Equals(Types.TRAP))
            {
                Error += "A non trap wants to spawn:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }

            SpawnCommand newCommand = new SpawnCommand();
            if (context.RANDOM() != null)
            {
                newCommand.SpawnDelegate = new SpawnDelegate(SpawnRandom);
                AddCommand(newCommand);
                return base.VisitSpawnDeclaration(context);
            }
            if (context.place() != null)
            {
                newCommand.TargetPlace = new Place(int.Parse(context.place().x().GetText()),
                                                    int.Parse(context.place().y().GetText()));
            }
            else if (!SpawnPointDeclare)
            {
                Error += "Spawning point not given:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }

            if (context.MONSTER() != null)
            {
                if (Program.GetCharacterType(context.name().GetText()) == null)
                {
                    Error += "No existing spawning type given:\n";
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                else
                newCommand.TargetCharacterType = Program.GetCharacterType(context.name().GetText()).SpawnType;
            }
            else if (!SpawnTypeDeclare)
            {
                Error += "Spawning type not given:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            newCommand.SpawnDelegate = new SpawnDelegate(Spawn);
            AddCommand(newCommand);
            return base.VisitSpawnDeclaration(context);
        }

        public override object VisitDamageDeclaration([NotNull] DamageDeclarationContext context)
        {
            if (type.Equals(Types.MONSTER))
            {
                Error += "Monster wants to damage:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            if (context.character().TRAP() != null)
            {
                Error += "Can't damage trap:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else
            {
                DamageCommand newCommand = new DamageCommand();
                if (context.distanceDeclare() != null)
                    newCommand.Distance = int.Parse(context.distanceDeclare().NUMBER().GetText());

                if(context.damageAmountDeclaration() != null)
                {
                    newCommand.Damage = int.Parse(context.damageAmountDeclaration().NUMBER().GetText());
                }

                var direction = context.DIRECTION();
                if (direction != null)
                {
                    if (!(direction.GetText().Equals("F") || direction.GetText().Equals("L") ||
                        direction.GetText().Equals("B") || direction.GetText().Equals("R")))
                    {
                        Error += "Wrong direction used:\n";
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                    newCommand.Direction = direction.GetText();
                    newCommand.DamageDelegate = new DamageDelegate(DamageDirection);
                    AddCommand(newCommand);
                    return base.VisitDamageDeclaration(context);
                }
                PlaceContext place = context.place();
                if (place != null)
                {
                    int xPos = int.Parse(place.x().GetText());
                    int yPos = int.Parse(place.y().GetText());
                    newCommand.TargetPlace = new Place(xPos, yPos);
                    newCommand.DamageDelegate = new DamageDelegate(DamageToPlace);
                    AddCommand(newCommand);
                    return base.VisitDamageDeclaration(context);
                }
                var helpPlayer = context.character().PLAYER();
                if (helpPlayer != null)
                {
                    newCommand.DamageDelegate = new DamageDelegate(DamageToPlayer);
                    AddCommand(newCommand);
                    return base.VisitDamageDeclaration(context);
                }
                var helpMonster = context.character().MONSTER();
                if (helpPlayer != null)
                {
                    newCommand.DamageDelegate = new DamageDelegate(DamageToMonster);
                    AddCommand(newCommand);
                    return base.VisitDamageDeclaration(context);
                }
                var random = context.RANDOM();
                if (random != null)
                {
                    newCommand.DamageDelegate = new DamageDelegate(DamageRandom);
                    AddCommand(newCommand);
                    return base.VisitDamageDeclaration(context);
                }
            }

            return base.VisitDamageDeclaration(context);
        }

        public override object VisitIfexpression([NotNull] IfexpressionContext context)
        {
            ExpressionVisitor ConditionHelper = new ExpressionVisitor(context.boolExpression(), type);
            ConditionHelper.CheckBool(context.boolExpression());
            if (ConditionHelper.CheckFailed)
            {
                Error += "Condition check failed\n";
                Error += ConditionHelper.ErrorList;
                ErrorFound = true;
            }
            else
            {
                IfCommand newCommand = new IfCommand
                {
                    MyContext = context.boolExpression(),
                    Condition = (GetCondition)
                };
                this.ConditionalCommands.Add(newCommand);
                this.ConditionCount.Add(context.block().ChildCount - 2);
                if (context.block().ChildCount - 2 == 0)
                {
                    this.ConditionalCommands.Remove(this.ConditionalCommands.ElementAt(this.ConditionalCommands.Count - 1));
                    this.ConditionCount.Remove(this.ConditionCount.ElementAt(this.ConditionCount.Count - 1));
                    AddCommand(newCommand);
                }
            }
            return base.VisitIfexpression(context);
        }

        public override object VisitWhileexpression([NotNull] WhileexpressionContext context)
        {
            //It doesn't seem to contain the whole while expression, or doesn't recognize it
            ExpressionVisitor ConditionHelper = new ExpressionVisitor(context.boolExpression(), type);
            ConditionHelper.CheckBool(context.boolExpression());
            if (ConditionHelper.CheckFailed)
            {
                Error += "Condition check failed\n";
                Error += ConditionHelper.ErrorList;
                ErrorFound = true;
            }
            else
            {
                WhileCommand newCommand = new WhileCommand
                {
                    MyContext = context.boolExpression(),
                    Condition = (GetCondition)
                };
                this.ConditionalCommands.Add(newCommand);
                this.ConditionCount.Add(context.block().ChildCount);
                if (context.block().ChildCount == 0)
                {
                    this.ConditionalCommands.Remove(this.ConditionalCommands.ElementAt(this.ConditionalCommands.Count - 1));
                    this.ConditionCount.Remove(this.ConditionCount.ElementAt(this.ConditionCount.Count - 1));
                    AddCommand(newCommand);
                }
            }
            return base.VisitWhileexpression(context);
        }








        public bool GetCondition(GameParamProvider provider, BoolExpressionContext context)
        {
            ConditionVisitor visitor = new ConditionVisitor(provider, context);
            return visitor.CheckConditions();
        }


        //TODO: collision detectation fucntion should be created and called, whenever we want to move someone or teleport or spawn.


        public void Spawn(GameParamProvider provider, SpawnCommand command)
        {

            if (provider.OccupiedOrNot(command.TargetPlace))
                return;
            Monster newMonster = new Monster(command.TargetCharacterType.Health, (MonsterType)command.TargetCharacterType, command.TargetPlace);
            provider.GetMonsters().Add(newMonster);
            provider.GetBoard().Monsters.Add(newMonster);
            //TODO: check if it works
        }

        public void SpawnRandom(GameParamProvider provider, SpawnCommand command)
        {
            Random rand = new Random();
                int XPos = (int)(rand.Next() % provider.GetBoard().Height);
                int YPos = (int)(rand.Next() % provider.GetBoard().Width);
                command.TargetPlace = new Place(XPos, YPos);
            
                bool found = false;
                while (!found)
                {
                    int index = (int)(rand.Next() % Program.CharacterTypes.Count);
                    if(Program.CharacterTypes.ElementAt(index) is MonsterType)
                    {
                        command.TargetCharacterType = Program.CharacterTypes.ElementAt(index);
                        found = true;
                    }
                }
            Spawn(provider, command);
        }


        public void TeleportTrap(GameParamProvider provider, TeleportCommand command)
        {
            foreach (Trap Trap in provider.GetTraps())
            {
                if (Trap.Place.DirectionTo(provider.GetTrap().Place).Equals("collision"))
                {
                    if (command.Random)
                    {
                        Random rand = new Random();
                        int XPos = (int)(rand.Next() % provider.GetBoard().Height);
                        int YPos = (int)(rand.Next() % provider.GetBoard().Width);
                        command.TargetPlace = new Place(XPos, YPos);
                    }
                    if (!provider.OccupiedOrNot(command.TargetPlace))
                        Trap.Place = command.TargetPlace;
                }
            }
        }
        public void TeleportMonster(GameParamProvider provider, TeleportCommand command)
        {
            foreach (Monster monster in provider.GetMonsters()) {
                if (monster.Place.DirectionTo(provider.GetTrap().Place).Equals("collision"))
                {
                    if (command.Random)
                    {
                        Random rand = new Random();
                        int XPos = (int)(rand.Next() % provider.GetBoard().Height);
                        int YPos = (int)(rand.Next() % provider.GetBoard().Width);
                        command.TargetPlace = new Place(XPos, YPos);
                    }
                    if (!provider.OccupiedOrNot(command.TargetPlace))
                        monster.Place = command.TargetPlace;
                }
            }
        }
        public void TeleportPlayer(GameParamProvider provider, TeleportCommand command)
        {
            if (provider.GetPlayer().Place.DirectionTo(provider.GetTrap().Place).Equals("collision"))
            {
                if (command.Random)
                {
                    Random rand = new Random();
                    int XPos = (int)(rand.Next() % provider.GetBoard().Height);
                    int YPos = (int)(rand.Next() % provider.GetBoard().Width);
                    command.TargetPlace = new Place(XPos, YPos);
                }
                if (!provider.OccupiedOrNot(command.TargetPlace))
                    provider.GetPlayer().Place = command.TargetPlace;
            }

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
            provider.GetMe().Place = command.TargetPlace;
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
        public void MoveRandom(GameParamProvider provider, MoveCommand command)
        {
            Random rand = new Random();
            int XPos = (int)(rand.Next() % provider.GetBoard().Height);
            int YPos = (int)(rand.Next() % provider.GetBoard().Width);
            command.TargetPlace = new Place(XPos, YPos);
            MoveToPlace(provider, command);
        }

        public void ShootDirection(GameParamProvider provider, ShootCommand command)
        {
            switch (command.Direction)
            {
                case "F":
                    if (provider.GetPlayer().Place.Y != provider.GetMe().Place.Y)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.X - i >= 0)
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X - (int)command.Distance)
                                provider.GetPlayer().Damage(command.Damage);
                    }
                    break;
                case "B":
                    if (provider.GetPlayer().Place.Y != provider.GetMe().Place.Y)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if (provider.GetPlayer().Place.X == provider.GetMe().Place.X + (int)command.Distance)
                            provider.GetPlayer().Damage(command.Damage);
                    }
                    break;
                case "L":
                    if (provider.GetPlayer().Place.X != provider.GetMe().Place.X)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.Y - i >= 0)
                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y - (int)command.Distance)
                                provider.GetPlayer().Damage(command.Damage);
                    }
                    break;
                case "R":
                    if (provider.GetPlayer().Place.X != provider.GetMe().Place.X)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y + (int)command.Distance)
                            provider.GetPlayer().Damage(command.Damage);
                    }
                    break;
            }
        }

        public void ShootToPlace(GameParamProvider provider, ShootCommand command)
        {
            if (provider.GetPlayer().Place.Equals(command.TargetPlace))
                provider.GetPlayer().Damage(command.Damage);
        }

        public void ShootToPlayer(GameParamProvider provider, ShootCommand command)
        {
            provider.GetPlayer().Damage(command.Damage);
        }

        public void ShootRandom(GameParamProvider provider, ShootCommand command)
        {
            Random rand = new Random();
            int damage = (int)((rand.Next() % provider.GetPlayer().GetHealth()) / 3);
            int XPos = (int)(rand.Next() % provider.GetBoard().Height);
            int YPos = (int)(rand.Next() % provider.GetBoard().Width);
            command.Damage = damage;
            command.TargetPlace = new Place(XPos, YPos);
            ShootToPlace(provider, command);
        }



        public void DamageDirection(GameParamProvider provider, DamageCommand command)
        {
            switch (command.Direction)
            {
                case "F":
                    if (provider.GetPlayer().Place.Y != provider.GetMe().Place.Y)
                        break;
                    //distance is 1 as default value
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.X - i >= 0)
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X - (int)command.Distance)
                                provider.GetPlayer().Damage(command.Damage);
                    }
                    foreach(Monster monster in provider.GetMonsters())
                    {
                        if (monster.Place.Y != provider.GetMe().Place.Y)
                            break;
                        for (int i = 0; i < command.Distance; i++)
                        {
                            if ((int)provider.GetMe().Place.X - i >= 0)
                                if (monster.Place.X == provider.GetMe().Place.X - (int)command.Distance)
                                    monster.Damage(command.Damage);
                        }
                    }
                    break;
                case "B":
                    if (provider.GetPlayer().Place.Y != provider.GetMe().Place.Y)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if (provider.GetPlayer().Place.X == provider.GetMe().Place.X + (int)command.Distance)
                            provider.GetPlayer().Damage(command.Damage);
                    }
                    foreach(Monster monster in provider.GetMonsters())
                    {
                        if (monster.Place.Y != provider.GetMe().Place.Y)
                            break;
                        if (monster.Place.X == provider.GetMe().Place.X + (int)command.Distance)
                            monster.Damage(command.Damage);
                    }
                    break;
                case "L":
                    if (provider.GetPlayer().Place.X != provider.GetMe().Place.X)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.Y - i >= 0)
                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y - (int)command.Distance)
                                provider.GetPlayer().Damage(command.Damage);
                    }
                    foreach (Monster monster in provider.GetMonsters())
                    {
                        if (monster.Place.X != provider.GetMe().Place.X)
                            break;
                        for (int i = 0; i < command.Distance; i++)
                        {
                            if ((int)provider.GetMe().Place.Y - i >= 0)
                                if (monster.Place.Y == provider.GetMe().Place.Y - (int)command.Distance)
                                    monster.Damage(command.Damage);
                        }
                    }
                    break;
                case "R":
                    if (provider.GetPlayer().Place.X != provider.GetMe().Place.X)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y + (int)command.Distance)
                            provider.GetPlayer().Damage(command.Damage);
                    }
                    foreach (Monster monster in provider.GetMonsters())
                    {
                        if (monster.Place.X != provider.GetMe().Place.X)
                            break;
                        if (monster.Place.Y == provider.GetMe().Place.Y + (int)command.Distance)
                            monster.Damage(command.Damage);
                    }
                    break;
            }
        }

        public void DamageToPlace(GameParamProvider provider, DamageCommand command)
        {
            if (provider.GetPlayer().Place.Equals(command.TargetPlace))
                provider.GetPlayer().Damage(command.Damage);
            foreach(Monster monster in provider.GetMonsters())
            {
                if (monster.Place.Equals(command.TargetPlace))
                    monster.Damage(command.Damage);
            }
        }

        public void DamageToPlayer(GameParamProvider provider, DamageCommand command)
        {
            provider.GetPlayer().Damage(command.Damage);
        }

        public void DamageToMonster(GameParamProvider provider, DamageCommand command)
        {
            provider.GetMonster().Damage(command.Damage);
        }
        public void DamageRandom(GameParamProvider provider, DamageCommand command)
        {
            Random rand = new Random();
            int damage = (int)((rand.Next() % provider.GetPlayer().GetHealth()) / 3);
            int XPos = (int)(rand.Next() % provider.GetBoard().Height);
            int YPos = (int)(rand.Next() % provider.GetBoard().Width);
            command.Damage = damage;
            command.TargetPlace = new Place(XPos, YPos);
            DamageToPlace(provider, command);
        }


        public void AddCommand(Command newCommand)
        {
            if(ConditionCount.Count == 0 || ConditionalCommands.Count==0)
                Program.GetCharacterType(typeName).Commands.Add(newCommand);
            else
                if (ConditionCount.ElementAt(ConditionCount.Count - 1) > 0)
                {
                    ConditionalCommands.ElementAt(ConditionalCommands.Count - 1).CommandList.Add(newCommand);
                    int helperCount = ConditionCount.ElementAt(ConditionCount.Count - 1);
                    ConditionCount.Remove(ConditionCount.ElementAt(ConditionCount.Count - 1));
                    if(helperCount-1 == 0)
                    {
                        Command helperCommand = ConditionalCommands.ElementAt(ConditionalCommands.Count - 1);
                        Program.GetCharacterType(typeName).Commands.Add(helperCommand);
                        ConditionalCommands.Remove(ConditionalCommands.ElementAt(ConditionalCommands.Count - 1));
                    }
                    else
                    ConditionCount.Add(helperCount - 1);
                }
        }
    }
}
