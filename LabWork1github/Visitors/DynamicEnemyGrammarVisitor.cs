using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using LabWork1github.Commands;
using LabWork1github.EventHandling;
using LabWork1github.static_constants;
using LabWork1github.Symbols;
using LabWork1github.Visitors;
using static LabWork1github.DynamicEnemyGrammarParser;

namespace LabWork1github
{
    public class DynamicEnemyGrammarVisitor : DynamicEnemyGrammarBaseVisitor<object>
    {
        //TODO: eventHandling class that is for a specific when block
        //TODO: parameter change Command
        private string typeName = "";
        private string type = null;
        private List<int> ConditionCount = new List<int>();
        private List<Command> ConditionalCommands = new List<Command>();
        private bool CommandListing { get; set; } = false;
        public string Error = "";
        public bool ErrorFound = false;
        public Scope CurrentScope { get; set; } = new Scope(null);

        public override object VisitDefinition([NotNull] DefinitionContext context)
        {
            foreach (var child in context.statementList())
            {
                typeName = "";
                type = null;
                ConditionCount = new List<int>();
                ConditionalCommands = new List<Command>();
                Error = "";
                ErrorFound = false;
                CommandListing = false;

                VisitStatementList(child);
            }


            //since I manually visit every children of the definition, no need to return the base visit function, only a null
            return null;
        }
        public override object VisitTrapNameDeclaration([NotNull] TrapNameDeclarationContext context)
        {
            if (Program.GetCharacterType(typeName) != null)
            {
                Error += ErrorMessages.ParameterDeclarationError.TRAP_TYPE_ALREADY_EXISTS;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else
            {
                type = Types.TRAP;
                Program.CharacterTypes.Add(new TrapType(context.name().GetText()));
            }
            typeName = context.name().GetText();
            Program.GetCharacterType(typeName).Damage = Program.GetCharacterType(Types.DEFAULT_TRAP).Damage;
            return base.VisitTrapNameDeclaration(context);
        }
        public override object VisitMonsterNameDeclaration([NotNull] MonsterNameDeclarationContext context)
        {
            if (Program.GetCharacterType(typeName) != null)
            {
                Error += ErrorMessages.ParameterDeclarationError.MONSTER_TYPE_ALREADY_EXISTS;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else
            {
                type = Types.MONSTER;
                Program.CharacterTypes.Add(new MonsterType(context.name().GetText()));
            }
            typeName = context.name().GetText();
            Program.GetCharacterType(typeName).Health = Program.GetCharacterType(Types.DEFAULT_MONSTER).Health;
            Program.GetCharacterType(typeName).Health = Program.GetCharacterType(Types.DEFAULT_MONSTER).Damage;
            return base.VisitMonsterNameDeclaration(context);
        }
        public override object VisitDeclarations([NotNull] DeclarationsContext context)
        {
            foreach(var child in context.declareStatements())
            {
                VisitDeclareStatements(child);
            }
            if (context.COMMANDS() != null)
                CommandListing = true;
            return null;
        }
        public override object VisitHealthDeclaration([NotNull] HealthDeclarationContext context)
        {
            if (type == Types.TRAP)
            {
                Error += ErrorMessages.ParameterDeclarationError.TRAP_HAS_NO_HEALTH;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else if(CommandListing)
            {
                NumberParameterDeclareCommand newCommand = new NumberParameterDeclareCommand();
                newCommand.Number = int.Parse(context.NUMBER().GetText());
                newCommand.NumberParameterDeclareDelegate = new NumberParameterDeclareDelegate(HealthChange);
                AddCommand(newCommand);
            }
            else
                Program.GetCharacterType(typeName).Health = int.Parse(context.NUMBER().GetText());
            return base.VisitHealthDeclaration(context);
        }
        public override object VisitHealAmountDeclaration([NotNull] HealAmountDeclarationContext context)
        {
            if (type != Types.TRAP)
            {
                Error += ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_HEAL;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else if (CommandListing)
            {
                NumberParameterDeclareCommand newCommand = new NumberParameterDeclareCommand();
                newCommand.Number = int.Parse(context.NUMBER().GetText());
                newCommand.NumberParameterDeclareDelegate = new NumberParameterDeclareDelegate(HealChange);
                AddCommand(newCommand);
            }
            else
                Program.GetCharacterType(typeName).Heal = int.Parse(context.NUMBER().GetText());
            return base.VisitHealAmountDeclaration(context);
        }
        public override object VisitDamageAmountDeclaration([NotNull] DamageAmountDeclarationContext context)
        {
            if (CommandListing)
            {
                NumberParameterDeclareCommand newCommand = new NumberParameterDeclareCommand();
                newCommand.Number = int.Parse(context.NUMBER().GetText());
                newCommand.NumberParameterDeclareDelegate = new NumberParameterDeclareDelegate(DamageChange);
                AddCommand(newCommand);
            }
            else
                Program.GetCharacterType(typeName).Damage = int.Parse(context.NUMBER().GetText());
            return base.VisitDamageAmountDeclaration(context);
        }
        public override object VisitTeleportPointDeclaration([NotNull] TeleportPointDeclarationContext context)
        {
            if (type != Types.TRAP)
            {
                Error += ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_TELEPORT;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else if (CommandListing)
            {
                PlaceParameterDeclareCommand newCommand = new PlaceParameterDeclareCommand();
                newCommand.Place = new Place(int.Parse(context.place().x().GetText()), int.Parse(context.place().y().GetText()));
                newCommand.PlaceParameterDeclareDelegate = new PlaceParameterDeclareDelegate(TeleportPlaceChange);
                AddCommand(newCommand);
            }
            else
                Program.GetCharacterType(typeName).TeleportPlace = 
                    new Place(int.Parse(context.place().x().GetText()), int.Parse(context.place().y().GetText()));
            return base.VisitTeleportPointDeclaration(context);
        }
        public override object VisitSpawnPointDeclaration([NotNull] SpawnPointDeclarationContext context)
        {
            if (type != Types.TRAP)
            {
                Error += ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_SPAWN_TO_PLACE;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else if (CommandListing)
            {
                PlaceParameterDeclareCommand newCommand = new PlaceParameterDeclareCommand();
                newCommand.Place = new Place(int.Parse(context.place().x().GetText()), int.Parse(context.place().y().GetText()));
                newCommand.PlaceParameterDeclareDelegate = new PlaceParameterDeclareDelegate(SpawnPlaceChange);
                AddCommand(newCommand);
            }
            else
                Program.GetCharacterType(typeName).SpawnPlace = 
                    new Place(int.Parse(context.place().x().GetText()), int.Parse(context.place().y().GetText()));
            return base.VisitSpawnPointDeclaration(context);
        }
        public override object VisitSpawnTypeDeclaration([NotNull] SpawnTypeDeclarationContext context)
        {
            if (type != Types.TRAP)
            {
                Error += ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_SPAWN_TYPE;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else if (CommandListing)
            {
                TypeParameterDeclareCommand newCommand = new TypeParameterDeclareCommand();
                newCommand.CharacterType = new MonsterType(context.name().GetText());
                newCommand.TypeParameterDeclareDelegate = new TypeParameterDeclareDelegate(SpawnTypeChange);
                AddCommand(newCommand);
            }
            else
                Program.GetCharacterType(typeName).SpawnType = new MonsterType(context.name().GetText());
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
                if (!(direction.GetText().Equals(Directions.FORWARD) || direction.GetText().Equals(Directions.LEFT) || direction.GetText().Equals(Directions.BACKWARDS) ||
                    direction.GetText().Equals(Directions.RIGHT)))
                {
                    Error += ErrorMessages.MoveError.WRONG_DIRECTION;
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
                Error += ErrorMessages.ShootError.ONLY_MONSTER_CAN_SHOOT;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else
            {
                ShootCommand newCommand = new ShootCommand();
                AddCommand(VisitHealthChangeOption(context.healthChangeOption(), newCommand));
            }
            return base.VisitShootDeclaration(context);
        }

        public override object VisitTeleportDeclaration([NotNull] TeleportDeclarationContext context)
        {
            if (!type.Equals(Types.TRAP))
            {
                Error += ErrorMessages.TeleportError.ONLY_TRAP_CAN_TELEPORT;
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
            {
                if (Program.GetCharacterType(typeName).TeleportPlace.Equals(new Place(-1, -1)) && context.RANDOM() == null)
                {
                    Error += ErrorMessages.TeleportError.TELEPORTING_WITHOUT_PLACE_GIVEN;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                newCommand.TargetPlace = Program.GetCharacterType(typeName).TeleportPlace;
            }
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
                        Error += ErrorMessages.TeleportError.TRYING_TO_TELEPORT_YOURSELF;
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
                Error += ErrorMessages.SpawnError.ONLY_TRAP_CAN_SPAWN;
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
            else
            {
                if (Program.GetCharacterType(typeName).SpawnPlace.Equals(new Place(-1, -1)))
                {
                    Error += ErrorMessages.SpawnError.SPAWN_WITHOUT_PLACE_GIVEN;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                else
                    newCommand.TargetPlace = Program.GetCharacterType(typeName).SpawnPlace;
            }
            if (context.MONSTER() != null)
            {
                newCommand.TargetCharacterType = new MonsterType(context.name().GetText());
            }
            else
            {
                if (Program.GetCharacterType(typeName).SpawnType == null)
                {
                    Error += ErrorMessages.SpawnError.SPAWN_WITHOUT_TYPE_GIVEN;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                else
                    newCommand.TargetCharacterType = Program.GetCharacterType(typeName).SpawnType;
            }
            newCommand.SpawnDelegate = new SpawnDelegate(Spawn);
            AddCommand(newCommand);
            return base.VisitSpawnDeclaration(context);
        }

        public override object VisitDamageDeclaration([NotNull] DamageDeclarationContext context)
        {
            if (type.Equals(Types.MONSTER))
            {
                Error += ErrorMessages.DamageError.ONLY_TRAP_CAN_DAMAGE;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            DamageCommand newCommand = new DamageCommand();
            AddCommand(VisitHealthChangeOption(context.healthChangeOption(), newCommand));
            return base.VisitDamageDeclaration(context);
        }

        public override object VisitHealDeclaration([NotNull] HealDeclarationContext context)
        {
            if (type.Equals(Types.MONSTER))
            {
                Error += ErrorMessages.HealError.ONLY_TRAP_CAN_HEAL;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            HealCommand newCommand = new HealCommand();
            AddCommand(VisitHealthChangeOption(context.healthChangeOption(), newCommand));
            return base.VisitHealDeclaration(context);
        }

        public override object VisitIfExpression([NotNull] IfExpressionContext context)
        {
            ExpressionVisitor ConditionHelper = new ExpressionVisitor(context.boolExpression(), type);
            ConditionHelper.CheckBool(context.boolExpression());
            if (ConditionHelper.CheckFailed)
            {
                Error += ErrorMessages.ConditionError.CONDITION_CHECK_FAIL;
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
            return base.VisitIfExpression(context);
        }

        public override object VisitWhileExpression([NotNull] WhileExpressionContext context)
        {
            //It doesn't seem to contain the whole while expression, or doesn't recognize it
            ExpressionVisitor ConditionHelper = new ExpressionVisitor(context.boolExpression(), type);
            ConditionHelper.CheckBool(context.boolExpression());
            if (ConditionHelper.CheckFailed)
            {
                Error += ErrorMessages.ConditionError.CONDITION_CHECK_FAIL;
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
            return base.VisitWhileExpression(context);
        }
        //TODO: check if using CharacterType for partner is possible, if so then test it in Game class
        public override object VisitWhenExpression([NotNull] WhenExpressionContext context)
        {
            TriggerEventHandler eventHandler = new TriggerEventHandler();
            TriggerEvent triggerEvent = VisitEvent(context.triggerEvent(), eventHandler);
            
            return base.VisitWhenExpression(context);
        }

        public TriggerEvent VisitEvent(TriggerEventContext context, TriggerEventHandler eventHandler)
        {
            TriggerEvent resultTrigger = new TriggerEvent();
            if(context.PLAYER() != null)
            {
                EventCollection.PlayerHealthCheck += eventHandler.OnEvent;
                resultTrigger.SourceCharacter = new PlayerType();
                return resultTrigger;
            }
            if (context.character() != null)
            {
                if(context.character().PLAYER() != null)
                {
                    if(context.action() == null)
                    {
                        Error += ErrorMessages.EventError.EVENT_WITHOUT_ACTION;
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                    resultTrigger.SourceCharacter = new PlayerType();
                    if (context.action().place() != null)
                    {
                        resultTrigger.TargetPlace = new Place(int.Parse(context.action().place().x().GetText()),
                                               int.Parse(context.action().place().y().GetText()));
                    }
                    if (context.action().MOVE() != null)
                    {
                        EventCollection.PlayerMoved += eventHandler.OnEvent;
                        if (context.action().fromPlace() != null)
                            resultTrigger.SourcePlace = new Place(int.Parse(context.action().place().x().GetText()),
                                                    int.Parse(context.action().place().y().GetText()));
                        return resultTrigger;
                    }
                    if(context.action().DIE() != null)
                    {
                        EventCollection.PlayerDied += eventHandler.OnEvent;
                        return resultTrigger;
                    }
                    if (context.action().STAY() != null)
                    {
                        EventCollection.PlayerStayed += eventHandler.OnEvent;
                        return resultTrigger;
                    }
                    if(context.action().SHOOT() != null)
                    {
                        EventCollection.PlayerShot += eventHandler.OnEvent;
                        if (context.action().NUMBER() != null)
                        {
                            resultTrigger.Amount = double.Parse(context.action().NUMBER().GetText());
                        }
                        if(context.action().character() != null)
                        {
                            if(context.action().character().PLAYER() != null)
                            {
                                Error += ErrorMessages.EventError.PLAYER_SHOOTING_ITSELF;
                                Error += context.GetText() + "\n";
                                ErrorFound = true;
                            }
                            if (context.action().character().MONSTER() != null || (type.Equals(Types.MONSTER) && context.action().character().ME() != null ))
                                resultTrigger.TargetCharacter = new MonsterType();
                            if (context.action().character().TRAP() != null || (type.Equals(Types.TRAP) && context.action().character().ME() != null ))
                                resultTrigger.TargetCharacter = new TrapType();
                            return resultTrigger;
                        }
                        if(context.action().place() == null)
                        {
                            Error += ErrorMessages.EventError.ACTION_WITHOUT_CHARACTER_OR_PLACE;
                            Error += context.GetText() + "\n";
                            ErrorFound = true;
                        }
                    }
                    if(context.action().DAMAGE() != null)
                    {
                        Error += ErrorMessages.EventError.ONLY_TRAP_CAN_DAMAGE;
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                    if (context.action().HEAL() != null)
                    {
                        Error += ErrorMessages.EventError.ONLY_TRAP_CAN_HEAL;
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                    if (context.action().TELEPORT_T() != null)
                    {
                        Error += ErrorMessages.EventError.ONLY_TRAP_CAN_TELEPORT;
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                    if (context.action().SPAWN() != null)
                    {
                        Error += ErrorMessages.EventError.ONLY_TRAP_CAN_SPAWN;
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                }
                if (context.character().MONSTER() != null)
                {
                    resultTrigger = VisitMonsterActionContext(context, resultTrigger, eventHandler);
                }
                if (context.character().TRAP() != null)
                {
                    resultTrigger = VisitTrapActionContext(context, resultTrigger, eventHandler);
                }
                if(context.character().ME() != null)
                {
                    if(type.Equals(Types.MONSTER))
                        resultTrigger = VisitMonsterActionContext(context, resultTrigger, eventHandler);
                    if(type.Equals(Types.TRAP))
                        resultTrigger = VisitTrapActionContext(context, resultTrigger, eventHandler);
                }
            }
            else
            {
                Error += ErrorMessages.EventError.ACTION_WITHOUT_CHARACTER;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            return resultTrigger;
        }

        private TriggerEvent VisitMonsterActionContext(TriggerEventContext context, TriggerEvent resultTrigger, TriggerEventHandler eventHandler)
        {
            if (context.action() == null)
            {
                Error += ErrorMessages.EventError.EVENT_WITHOUT_ACTION;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            resultTrigger.SourceCharacter = new MonsterType();
            if (context.action().place() != null)
            {
                resultTrigger.TargetPlace = new Place(int.Parse(context.action().place().x().GetText()),
                                       int.Parse(context.action().place().y().GetText()));
            }
            if (context.action().MOVE() != null)
            {
                EventCollection.MonsterMoved += eventHandler.OnEvent;
                if (context.action().fromPlace() != null)
                    resultTrigger.SourcePlace = new Place(int.Parse(context.action().place().x().GetText()),
                                            int.Parse(context.action().place().y().GetText()));
                return resultTrigger;
            }
            if (context.action().DIE() != null)
            {
                EventCollection.MonsterDied += eventHandler.OnEvent;
                return resultTrigger;
            }
            if (context.action().STAY() != null)
            {
                EventCollection.MonsterStayed += eventHandler.OnEvent;
                return resultTrigger;
            }
            if (context.action().SHOOT() != null)
            {
                EventCollection.MonsterShot += eventHandler.OnEvent;
                if (context.action().NUMBER() != null)
                {
                    resultTrigger.Amount = double.Parse(context.action().NUMBER().GetText());
                }
                if (context.action().character() != null)
                {
                    if (context.action().character().PLAYER() != null)
                        resultTrigger.TargetCharacter = new PlayerType();
                    if (context.action().character().MONSTER() != null || (type.Equals(Types.MONSTER) && context.action().character().ME() != null ))
                    {
                        Error += ErrorMessages.EventError.MONSTER_SHOOTING_MONSTER;
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                    if (context.action().character().TRAP() != null || (type.Equals(Types.TRAP) && context.action().character().ME() != null ))
                    {
                        Error += ErrorMessages.EventError.MONSTER_SHOOTING_TRAP;
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                    return resultTrigger;
                }
                if (context.action().place() == null)
                {
                    Error += "When command action doesn't have character nor place as target:\n";
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
            }
            if (context.action().DAMAGE() != null)
            {
                Error += "When command action error, monster can't damage, try shoot instead at:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            if (context.action().HEAL() != null)
            {
                Error += "When command action error, monster can't heal, try trap heal to monster instead at:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            if (context.action().TELEPORT_T() != null)
            {
                Error += "When command action error, monster can't teleport, try trap teleport monster instead:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            if (context.action().SPAWN() != null)
            {
                Error += "When command action error, monster can't spawn:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            return resultTrigger;
        }

        private TriggerEvent VisitTrapActionContext(TriggerEventContext context, TriggerEvent resultTrigger, TriggerEventHandler eventHandler)
        {
            if (context.action() == null)
            {
                Error += "When command doesn't have action at:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            resultTrigger.SourceCharacter = new TrapType();
            if (context.action().place() != null)
            {
                resultTrigger.TargetPlace = new Place(int.Parse(context.action().place().x().GetText()),
                                       int.Parse(context.action().place().y().GetText()));
            }
            if (context.action().MOVE() != null)
            {
                EventCollection.TrapMoved += eventHandler.OnEvent;
                if (context.action().fromPlace() != null)
                    resultTrigger.SourcePlace = new Place(int.Parse(context.action().place().x().GetText()),
                                            int.Parse(context.action().place().y().GetText()));
                return resultTrigger;
            }
            if (context.action().DIE() != null)
            {
                Error += "Traps can't die, error at:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            if (context.action().STAY() != null)
            {
                EventCollection.TrapStayed += eventHandler.OnEvent;
                return resultTrigger;
            }
            if (context.action().SHOOT() != null)
            {
                Error += "When command action error, trap can't shoot, error at:\n";
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            if (context.action().DAMAGE() != null)
            {
                EventCollection.TrapDamaged += eventHandler.OnEvent;
                if (context.action().NUMBER() != null)
                {
                    resultTrigger.Amount = double.Parse(context.action().NUMBER().GetText());
                }
                if (context.action().character() != null)
                {
                    if (context.action().character().PLAYER() != null)
                        resultTrigger.TargetCharacter = new PlayerType();
                    if (context.action().character().MONSTER() != null || (type.Equals(Types.MONSTER) && context.action().character().ME() != null ))
                    {
                        resultTrigger.TargetCharacter = new MonsterType();
                    }
                    if (context.action().character().TRAP() != null || (type.Equals(Types.TRAP) && context.action().character().ME() != null ))
                    {
                        Error += "When command action error, trap can't damage trap or itself:\n";
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                    return resultTrigger;
                }
                if (context.action().place() == null)
                {
                    Error += "When command action doesn't have character nor place as target:\n";
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
            }
            if (context.action().HEAL() != null)
            {
                EventCollection.TrapHealed += eventHandler.OnEvent;
                if (context.action().NUMBER() != null)
                {
                    resultTrigger.Amount = double.Parse(context.action().NUMBER().GetText());
                }
                if (context.action().character() != null)
                {
                    if (context.action().character().PLAYER() != null)
                        resultTrigger.TargetCharacter = new PlayerType();
                    if (context.action().character().MONSTER() != null || (type.Equals(Types.MONSTER) && context.action().character().ME() != null ))
                    {
                        resultTrigger.TargetCharacter = new MonsterType();
                    }
                    if (context.action().character().TRAP() != null || (type.Equals(Types.TRAP) && context.action().character().ME() != null ))
                    {
                        Error += "When command action error, trap can't heal trap or itself:\n";
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                    return resultTrigger;
                }
                if (context.action().place() == null)
                {
                    Error += "When command action doesn't have character nor place as target:\n";
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
            }
            if (context.action().TELEPORT_T() != null)
            {
                EventCollection.TrapTeleported += eventHandler.OnEvent;
                if (context.action().character().PLAYER() != null)
                    resultTrigger.TargetCharacter = new PlayerType();
                if (context.action().character().MONSTER() != null || (type.Equals(Types.MONSTER) && context.action().character().ME() != null ))
                {
                    resultTrigger.TargetCharacter = new MonsterType();
                }
                if (context.action().character().TRAP() != null || (type.Equals(Types.TRAP) && context.action().character().ME() != null ))
                {
                    Error += "When command action error, trap can't teleport trap or itself:\n";
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                if (context.action().place() != null)
                {
                    resultTrigger.TargetPlace = new Place(int.Parse(context.action().place().x().GetText()),
                                           int.Parse(context.action().place().y().GetText()));
                    return resultTrigger;
                }
            }
            if (context.action().SPAWN() != null)
            {
                EventCollection.TrapTeleported += eventHandler.OnEvent;
                if (context.action().character().PLAYER() != null)
                {
                    Error += "When command action error, trap can't spawn a player:\n";
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                if (context.action().character().MONSTER() != null || (type.Equals(Types.MONSTER) && context.action().character().ME() != null ))
                {
                    resultTrigger.TargetCharacter = new MonsterType();
                }
                if (context.action().character().TRAP() != null || (type.Equals(Types.TRAP) && context.action().character().ME() != null ))
                {
                    Error += "When command action error, trap can't spawn trap or itself:\n";
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                if (context.action().place() != null)
                {
                    resultTrigger.TargetPlace = new Place(int.Parse(context.action().place().x().GetText()),
                                           int.Parse(context.action().place().y().GetText()));
                    return resultTrigger;
                }
            }
            Error += "When command action unexpected error at:\n";
            Error += context.GetText() + "\n";
            ErrorFound = true;
            return resultTrigger;
        }

        public bool GetCondition(GameParamProvider provider, [NotNull] BoolExpressionContext context)
        {
            ConditionVisitor visitor = new ConditionVisitor(provider, context);
            return visitor.CheckConditions();
        }


        //TODO: collision detectation fucntion should be created and called, whenever we want to move someone or teleport or spawn.


        public void Spawn(GameParamProvider provider, SpawnCommand command)
        {
            if (provider.OccupiedOrNot(command.TargetPlace))
                return;
            if (Program.GetCharacterType(command.TargetCharacterType.Name) == null && provider.GetMe().GetCharacterType().SpawnType == null)
            {
                return;
            }
            if (!(Program.GetCharacterType(command.TargetCharacterType.Name) is MonsterType) && provider.GetMe().GetCharacterType().SpawnType == null)
                return;
            if (provider.GetMe().GetCharacterType().SpawnType == null)
                command.TargetCharacterType = Program.GetCharacterType(command.TargetCharacterType.Name);
            else
                command.TargetCharacterType = provider.GetMe().GetCharacterType().SpawnType;
            
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

        //TODO: check if fall check this way okay or not
        public void MoveDirection(GameParamProvider provider, MoveCommand command)
        {

            switch (command.Direction)
            {
                case Directions.FORWARD:
                    if ((int)provider.GetMonster().Place.X - command.Distance >= 0)
                        provider.GetMonster().Place.X -= (int)command.Distance;
                    break;
                case Directions.BACKWARDS:
                    if ((int)provider.GetMonster().Place.X + command.Distance <= provider.GetBoard().Height)
                        provider.GetMonster().Place.X += (int)command.Distance;
                    break;
                case Directions.LEFT:
                    if ((int)provider.GetMonster().Place.Y - command.Distance >= 0)
                        provider.GetMonster().Place.Y -= (int)command.Distance;
                    break;
                case Directions.RIGHT:
                    if ((int)provider.GetMonster().Place.Y + command.Distance <= provider.GetBoard().Width)
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
                case Directions.FORWARD:
                    if (provider.GetPlayer().Place.Y != provider.GetMe().Place.Y)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.X - i >= 0)
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X - (int)command.Distance)
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                    }
                    break;
                case Directions.BACKWARDS:
                    if (provider.GetPlayer().Place.Y != provider.GetMe().Place.Y)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if((int)provider.GetMe().Place.X + i <= provider.GetBoard().Height)
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X + (int)command.Distance)
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                    }
                    break;
                case Directions.LEFT:
                    if (provider.GetPlayer().Place.X != provider.GetMe().Place.X)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.Y - i >= 0)
                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y - (int)command.Distance)
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                    }
                    break;
                case Directions.RIGHT:
                    if (provider.GetPlayer().Place.X != provider.GetMe().Place.X)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.Y + i <= provider.GetBoard().Width)
                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y + (int)command.Distance)
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                    }
                    break;
            }
        }

        public void ShootToPlace(GameParamProvider provider, ShootCommand command)
        {
            if (provider.GetPlayer().Place.Equals(command.TargetPlace))
                provider.GetPlayer().Damage(command.HealthChangeAmount);
        }

        public void ShootToPlayer(GameParamProvider provider, ShootCommand command)
        {
            provider.GetPlayer().Damage(command.HealthChangeAmount);
        }

        public void ShootRandom(GameParamProvider provider, ShootCommand command)
        {
            Random rand = new Random();
            int damage = (int)((rand.Next() % provider.GetPlayer().GetHealth()) / 3);
            int XPos = (int)(rand.Next() % provider.GetBoard().Height);
            int YPos = (int)(rand.Next() % provider.GetBoard().Width);
            command.HealthChangeAmount = damage;
            command.TargetPlace = new Place(XPos, YPos);
            ShootToPlace(provider, command);
        }



        public void DamageDirection(GameParamProvider provider, DamageCommand command)
        {
            switch (command.Direction)
            {
                case Directions.FORWARD:
                    if (provider.GetPlayer().Place.Y != provider.GetMe().Place.Y)
                        break;
                    //distance is 1 as default value
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.X - i >= 0)
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X - i)
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                    }
                    foreach(Monster monster in provider.GetMonsters())
                    {
                        for (int i = 0; i < command.Distance; i++)
                        {
                            if ((int)provider.GetMe().Place.X - i >= 0)
                                if (monster.Place.X == provider.GetMe().Place.X - i)
                                    monster.Damage(command.HealthChangeAmount);
                        }
                    }
                    break;
                case Directions.BACKWARDS:
                    if (provider.GetPlayer().Place.Y != provider.GetMe().Place.Y)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.X + i <= provider.GetBoard().Height)
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X + i)
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                    }
                    foreach(Monster monster in provider.GetMonsters())
                    {
                        for (int i = 0; i < command.Distance; i++)
                        {
                            if ((int)provider.GetMe().Place.X + i <= provider.GetBoard().Height)
                                if (monster.Place.X == provider.GetMe().Place.X + i)
                                    monster.Damage(command.HealthChangeAmount);
                        }
                    }
                    break;
                case Directions.LEFT:
                    if (provider.GetPlayer().Place.X != provider.GetMe().Place.X)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.Y - i >= 0)
                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y - i)
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                    }
                    foreach (Monster monster in provider.GetMonsters())
                    {
                        for (int i = 0; i < command.Distance; i++)
                        {
                            if ((int)provider.GetMe().Place.Y - i >= 0)
                                if (monster.Place.Y == provider.GetMe().Place.Y - i)
                                    monster.Damage(command.HealthChangeAmount);
                        }
                    }
                    break;
                case Directions.RIGHT:
                    if (provider.GetPlayer().Place.X != provider.GetMe().Place.X)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.Y + i <= provider.GetBoard().Width)
                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y + i)
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                    }
                    foreach (Monster monster in provider.GetMonsters())
                    {
                        for (int i = 0; i < command.Distance; i++)
                        {
                            if ((int)provider.GetMe().Place.Y + i <= provider.GetBoard().Width)
                                if (monster.Place.Y == provider.GetMe().Place.Y + i)
                                monster.Damage(command.HealthChangeAmount);
                        }
                    }
                    break;
            }
        }

        public void DamageToPlace(GameParamProvider provider, DamageCommand command)
        {
            if (provider.GetMe().Place.DirectionTo(command.TargetPlace).Equals("away"))
            {
                provider.GetDrawer().WriteCommand("Trap with type" + provider.GetMe().GetCharacterType() + "tried to damage far away target.");
                return;
            }
            if (provider.GetPlayer().Place.Equals(command.TargetPlace))
                provider.GetPlayer().Damage(command.HealthChangeAmount);
            foreach(Monster monster in provider.GetMonsters())
            {
                if (monster.Place.Equals(command.TargetPlace))
                    monster.Damage(command.HealthChangeAmount);
            }
        }

        public void DamageToPlayer(GameParamProvider provider, DamageCommand command)
        {
            provider.GetPlayer().Damage(command.HealthChangeAmount);
        }

        public void DamageToMonster(GameParamProvider provider, DamageCommand command)
        {
            provider.GetMonster().Damage(command.HealthChangeAmount);
        }
        public void DamageRandom(GameParamProvider provider, DamageCommand command)
        {
            Random rand = new Random();
            int damage = (int)((rand.Next() % provider.GetPlayer().GetHealth()) / 3);
            int XPos = (int)(rand.Next() % provider.GetBoard().Height);
            int YPos = (int)(rand.Next() % provider.GetBoard().Width);
            command.HealthChangeAmount = damage;
            command.TargetPlace = new Place(XPos, YPos);
            DamageToPlace(provider, command);
        }

        public void HealDirection(GameParamProvider provider, HealCommand command)
        {
            switch (command.Direction)
            {
                case Directions.FORWARD:
                    if (provider.GetPlayer().Place.Y != provider.GetMe().Place.Y)
                        break;
                    //distance is 1 as default value
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.X - i >= 0)
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X - i)
                                provider.GetPlayer().Heal(command.HealthChangeAmount);
                    }
                    foreach (Monster monster in provider.GetMonsters())
                    {
                        for (int i = 0; i < command.Distance; i++)
                        {
                            if ((int)provider.GetMe().Place.X - i >= 0)
                                if (monster.Place.X == provider.GetMe().Place.X - i)
                                    monster.Heal(command.HealthChangeAmount);
                        }
                    }
                    break;
                case Directions.BACKWARDS:
                    if (provider.GetPlayer().Place.Y != provider.GetMe().Place.Y)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.X + i <= provider.GetBoard().Height)
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X + i)
                                provider.GetPlayer().Heal(command.HealthChangeAmount);
                    }
                    foreach (Monster monster in provider.GetMonsters())
                    {
                        for (int i = 0; i < command.Distance; i++)
                        {
                            if ((int)provider.GetMe().Place.X + i <= provider.GetBoard().Height)
                                if (monster.Place.X == provider.GetMe().Place.X + i)
                                    monster.Heal(command.HealthChangeAmount);
                        }
                    }
                    break;
                case Directions.LEFT:
                    if (provider.GetPlayer().Place.X != provider.GetMe().Place.X)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.Y - i >= 0)
                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y - i)
                                provider.GetPlayer().Heal(command.HealthChangeAmount);
                    }
                    foreach (Monster monster in provider.GetMonsters())
                    {
                        for (int i = 0; i < command.Distance; i++)
                        {
                            if ((int)provider.GetMe().Place.Y - i >= 0)
                                if (monster.Place.Y == provider.GetMe().Place.Y - i)
                                    monster.Heal(command.HealthChangeAmount);
                        }
                    }
                    break;
                case Directions.RIGHT:
                    if (provider.GetPlayer().Place.X != provider.GetMe().Place.X)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.Y + i <= provider.GetBoard().Width)
                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y + i)
                                provider.GetPlayer().Heal(command.HealthChangeAmount);
                    }
                    foreach (Monster monster in provider.GetMonsters())
                    {
                        for (int i = 0; i < command.Distance; i++)
                        {
                            if ((int)provider.GetMe().Place.Y + i <= provider.GetBoard().Width)
                                if (monster.Place.Y == provider.GetMe().Place.Y + i)
                                    monster.Heal(command.HealthChangeAmount);
                        }
                    }
                    break;
            }
        }

        public void HealToPlace(GameParamProvider provider, HealCommand command)
        {
            if (provider.GetPlayer().Place.Equals(command.TargetPlace))
                provider.GetPlayer().Heal(command.HealthChangeAmount);
            foreach (Monster monster in provider.GetMonsters())
            {
                if (monster.Place.Equals(command.TargetPlace))
                    monster.Heal(command.HealthChangeAmount);
            }
        }

        public void HealToPlayer(GameParamProvider provider, HealCommand command)
        {
            provider.GetPlayer().Heal(command.HealthChangeAmount);
        }

        public void HealToMonster(GameParamProvider provider, HealCommand command)
        {
            provider.GetMonster().Heal(command.HealthChangeAmount);
        }
        public void HealRandom(GameParamProvider provider, HealCommand command)
        {
            Random rand = new Random();
            int Heal = (int)((rand.Next() % provider.GetPlayer().GetHealth()) / 5);
            int XPos = (int)(rand.Next() % provider.GetBoard().Height);
            int YPos = (int)(rand.Next() % provider.GetBoard().Width);
            command.HealthChangeAmount = Heal;
            command.TargetPlace = new Place(XPos, YPos);
            HealToPlace(provider, command);
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

        public void HealthChange(GameParamProvider provider, NumberParameterDeclareCommand command)
        {
            foreach(Character character in provider.GetCharacters())
            {
                if (this.type == Types.MONSTER)
                    if (character.GetCharacterType() is MonsterType && character.GetCharacterType().Name.Equals(this.typeName))
                        character.GetCharacterType().Health = command.Number;
            }
        }
        public void HealChange(GameParamProvider provider, NumberParameterDeclareCommand command)
        {
            foreach (Character character in provider.GetCharacters())
            {
                if (this.type == Types.TRAP)
                    if (character.GetCharacterType() is TrapType && character.GetCharacterType().Name.Equals(this.typeName))
                        character.GetCharacterType().Heal = command.Number;
            }
        }
        public void DamageChange(GameParamProvider provider, NumberParameterDeclareCommand command)
        {
            foreach (Character character in provider.GetCharacters())
            {
                if (this.type == Types.TRAP)
                    if (character.GetCharacterType() is TrapType && character.GetCharacterType().Name.Equals(this.typeName))
                        character.GetCharacterType().Damage = command.Number;
                if (this.type == Types.MONSTER)
                    if (character.GetCharacterType() is MonsterType && character.GetCharacterType().Name.Equals(this.typeName))
                        character.GetCharacterType().Damage = command.Number;
            }
        }
        public void TeleportPlaceChange(GameParamProvider provider, PlaceParameterDeclareCommand command)
        {
            foreach (Character character in provider.GetCharacters())
            {
                if (this.type == Types.TRAP)
                    if (character.GetCharacterType() is TrapType && character.GetCharacterType().Name.Equals(this.typeName))
                        character.GetCharacterType().TeleportPlace = command.Place;
            }
        }
        public void SpawnPlaceChange(GameParamProvider provider, PlaceParameterDeclareCommand command)
        {
            foreach (Character character in provider.GetCharacters())
            {
                if (this.type == Types.TRAP)
                    if (character.GetCharacterType() is TrapType && character.GetCharacterType().Name.Equals(this.typeName))
                        character.GetCharacterType().SpawnPlace = command.Place;
            }
        }
        public void SpawnTypeChange(GameParamProvider provider, TypeParameterDeclareCommand command)
        {
            foreach (Character character in provider.GetCharacters())
            {
                if (this.type == Types.TRAP)
                    if (character.GetCharacterType() is TrapType && character.GetCharacterType().Name.Equals(this.typeName))
                        character.GetCharacterType().SpawnType = command.CharacterType;
            }
        }

        public HealthChangerCommand VisitHealthChangeOption([NotNull] HealthChangeOptionContext context, HealthChangerCommand command)
        {

            if (context.distanceDeclare() != null)
                command.Distance = int.Parse(context.distanceDeclare().NUMBER().GetText());

            if (context.hpChangeAmountDeclaration() != null)
            {
                if (context.hpChangeAmountDeclaration().damageAmountDeclaration() != null)
                {
                    command.HealthChangeAmount = int.Parse(context.hpChangeAmountDeclaration().damageAmountDeclaration().NUMBER().GetText());
                }
                else
                {
                    command.HealthChangeAmount = int.Parse(context.hpChangeAmountDeclaration().healAmountDeclaration().NUMBER().GetText());
                }
            }

            var direction = context.DIRECTION();
            if (direction != null)
            {
                if (!(direction.GetText().Equals(Directions.FORWARD) || direction.GetText().Equals(Directions.LEFT) ||
                    direction.GetText().Equals(Directions.BACKWARDS) || direction.GetText().Equals(Directions.RIGHT)))
                {
                    Error += "Wrong direction used:\n";
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                command.Direction = direction.GetText();
                if (command is ShootCommand)
                {
                    ((ShootCommand)command).ShootDelegate = new ShootDelegate(ShootDirection);
                }
                if (command is DamageCommand)
                {
                    ((DamageCommand)command).DamageDelegate = new DamageDelegate(DamageDirection);
                }
                if (command is HealCommand)
                {
                    ((HealCommand)command).HealDelegate = new HealDelegate(HealDirection);
                }
                return command;;
            }
            PlaceContext place = context.place();
            if (place != null)
            {
                int xPos = int.Parse(place.x().GetText());
                int yPos = int.Parse(place.y().GetText());
                command.TargetPlace = new Place(xPos, yPos);
                if (command is ShootCommand)
                {
                    ((ShootCommand)command).ShootDelegate = new ShootDelegate(ShootToPlace);
                }
                if (command is DamageCommand)
                {
                    ((DamageCommand)command).DamageDelegate = new DamageDelegate(DamageToPlace);
                }
                if (command is HealCommand)
                {
                    ((HealCommand)command).HealDelegate = new HealDelegate(HealToPlace);
                }
                return command;;
            }

            if (context.character() != null)
            {
                if (context.character().TRAP() != null || context.character().ME() != null)
                {
                    Error += "This character doesn't have health, or you can't change it yourself:\n";
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                if (command is ShootCommand)
                {
                    if (context.character().MONSTER() != null)
                    {
                        Error += "You can't shoot a monster:\n";
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                    if (context.character().PLAYER() != null)
                        ((ShootCommand)command).ShootDelegate = new ShootDelegate(ShootToPlayer);
                }
                if (command is DamageCommand)
                {
                    if (context.character().MONSTER() != null)
                        ((DamageCommand)command).DamageDelegate = new DamageDelegate(DamageToMonster);
                    if (context.character().PLAYER() != null)
                        ((DamageCommand)command).DamageDelegate = new DamageDelegate(DamageToPlayer);
                }
                if (command is HealCommand)
                {
                    if (context.character().MONSTER() != null)
                        ((HealCommand)command).HealDelegate = new HealDelegate(HealToMonster);
                    if (context.character().PLAYER() != null)
                        ((HealCommand)command).HealDelegate = new HealDelegate(HealToPlayer);
                }
                return command;;
            }

            var random = context.RANDOM();
            if (random != null)
            {
                if (command is ShootCommand)
                    ((ShootCommand)command).ShootDelegate = new ShootDelegate(ShootRandom);
                if (command is DamageCommand)
                    ((DamageCommand)command).DamageDelegate = new DamageDelegate(DamageRandom);
                if (command is HealCommand)
                    ((HealCommand)command).HealDelegate = new HealDelegate(HealRandom);
                return command;
            }
            return command;
        }
    }
}
