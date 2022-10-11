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
        private string typeName = "";
        private string type = null;
        private List<int> ConditionCount = new List<int>();
        private List<Command> ConditionalCommands = new List<Command>();
        private TriggerEventHandler TriggerEventHandler { get; set; } = null;
        private Command ConditionalCommand { get; set; } = null;
        private TypeCreationStage CreationStage { get; set; } = TypeCreationStage.ParameterDeclare;
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
                CreationStage = TypeCreationStage.ParameterDeclare;

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
                CreationStage = TypeCreationStage.CommandListing;
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
            else if(CreationStage.Equals(TypeCreationStage.ParameterDeclare))
                Program.GetCharacterType(typeName).Health = int.Parse(context.NUMBER().GetText());
            else
            {
                NumberParameterDeclareCommand newCommand = new NumberParameterDeclareCommand();
                newCommand.Number = int.Parse(context.NUMBER().GetText());
                newCommand.NumberParameterDeclareDelegate = new NumberParameterDeclareDelegate(HealthChange);
                AddCommand(newCommand);
            }
            
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
            else if (CreationStage.Equals(TypeCreationStage.ParameterDeclare))
                    Program.GetCharacterType(typeName).Heal = int.Parse(context.NUMBER().GetText());
            else
            {
                NumberParameterDeclareCommand newCommand = new NumberParameterDeclareCommand();
                newCommand.Number = int.Parse(context.NUMBER().GetText());
                newCommand.NumberParameterDeclareDelegate = new NumberParameterDeclareDelegate(HealChange);
                AddCommand(newCommand);
            }
            
            return base.VisitHealAmountDeclaration(context);
        }
        public override object VisitDamageAmountDeclaration([NotNull] DamageAmountDeclarationContext context)
        {
            if (CreationStage.Equals(TypeCreationStage.ParameterDeclare))
                Program.GetCharacterType(typeName).Damage = int.Parse(context.NUMBER().GetText());
            else
            {
                NumberParameterDeclareCommand newCommand = new NumberParameterDeclareCommand();
                newCommand.Number = int.Parse(context.NUMBER().GetText());
                newCommand.NumberParameterDeclareDelegate = new NumberParameterDeclareDelegate(DamageChange);
                AddCommand(newCommand);
            }
            
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
            else if (CreationStage.Equals(TypeCreationStage.ParameterDeclare))
                Program.GetCharacterType(typeName).TeleportPlace =
                    new Place(int.Parse(context.place().x().GetText())-1, int.Parse(context.place().y().GetText())-1);
            else
            {
                PlaceParameterDeclareCommand newCommand = new PlaceParameterDeclareCommand();
                newCommand.Place = new Place(int.Parse(context.place().x().GetText())-1, int.Parse(context.place().y().GetText())-1);
                newCommand.PlaceParameterDeclareDelegate = new PlaceParameterDeclareDelegate(TeleportPlaceChange);
                AddCommand(newCommand);
            }
            
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
            else if (CreationStage.Equals(TypeCreationStage.ParameterDeclare))
                Program.GetCharacterType(typeName).SpawnPlace =
                    new Place(int.Parse(context.place().x().GetText())-1, int.Parse(context.place().y().GetText())-1);
            else
            {
                PlaceParameterDeclareCommand newCommand = new PlaceParameterDeclareCommand();
                newCommand.Place = new Place(int.Parse(context.place().x().GetText())-1, int.Parse(context.place().y().GetText())-1);
                newCommand.PlaceParameterDeclareDelegate = new PlaceParameterDeclareDelegate(SpawnPlaceChange);
                AddCommand(newCommand);
            }
            
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
            else if (CreationStage.Equals(TypeCreationStage.ParameterDeclare))
                Program.GetCharacterType(typeName).SpawnType = new MonsterType(context.name().GetText());
            else
            {
                TypeParameterDeclareCommand newCommand = new TypeParameterDeclareCommand();
                newCommand.CharacterType = new MonsterType(context.name().GetText());
                newCommand.TypeParameterDeclareDelegate = new TypeParameterDeclareDelegate(SpawnTypeChange);
                AddCommand(newCommand);
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
                newCommand.TargetPlace = new Place(int.Parse(context.place().x().GetText())-1,
                                                    int.Parse(context.place().y().GetText())-1);
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
                newCommand.TargetPlace = new Place(int.Parse(context.place().x().GetText())-1,
                                                    int.Parse(context.place().y().GetText())-1);
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

        public override object VisitBlock([NotNull] BlockContext context)
        {
            if(CreationStage.Equals(TypeCreationStage.ConditionalCommandBlock) || CreationStage.Equals(TypeCreationStage.EventCommandBlock))
                foreach (var child in context.statement())
                {
                    VisitStatement(child);
                }
            return base.VisitBlock(context);
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
                if (CreationStage.Equals(TypeCreationStage.CommandListing))
                {
                    CreationStage = TypeCreationStage.ConditionalCommandBlock;
                    ConditionalCommand = newCommand;
                    VisitBlock(context.block());
                    ConditionalCommand = null;
                    CreationStage = TypeCreationStage.CommandListing;
                    AddCommand(newCommand);
                }
                if (CreationStage.Equals(TypeCreationStage.ConditionalCommandBlock))
                {
                    Command previousConditionalCommand = ConditionalCommand;
                    ConditionalCommand = newCommand;
                    VisitBlock(context.block());
                    ConditionalCommand = previousConditionalCommand;
                    AddCommand(newCommand);
                }
                if (CreationStage.Equals(TypeCreationStage.EventCommandBlock))
                {
                    CreationStage = TypeCreationStage.ConditionalCommandBlock;
                    TriggerEventHandler previousEventHandler = TriggerEventHandler;
                    ConditionalCommand = newCommand;
                    VisitBlock(context.block());
                    TriggerEventHandler = previousEventHandler;
                    CreationStage = TypeCreationStage.EventCommandBlock;
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
                if (CreationStage.Equals(TypeCreationStage.CommandListing))
                {
                    CreationStage = TypeCreationStage.ConditionalCommandBlock;
                    ConditionalCommand = newCommand;
                    VisitBlock(context.block());
                    ConditionalCommand = null;
                    CreationStage = TypeCreationStage.CommandListing;
                    AddCommand(newCommand);
                }
                if (CreationStage.Equals(TypeCreationStage.ConditionalCommandBlock))
                {
                    Command previousConditionalCommand = ConditionalCommand;
                    ConditionalCommand = newCommand;
                    VisitBlock(context.block());
                    ConditionalCommand = previousConditionalCommand;
                    AddCommand(newCommand);
                }
                if (CreationStage.Equals(TypeCreationStage.EventCommandBlock))
                {
                    CreationStage = TypeCreationStage.ConditionalCommandBlock;
                    TriggerEventHandler previousEventHandler = TriggerEventHandler;
                    ConditionalCommand = newCommand;
                    VisitBlock(context.block());
                    ConditionalCommand = null;
                    TriggerEventHandler = previousEventHandler;
                    CreationStage = TypeCreationStage.EventCommandBlock;
                    AddCommand(newCommand);
                }
            }
            return base.VisitWhileExpression(context);
        }
        //TODO: check if using CharacterType for partner is possible, if so then test it in Game class
        public override object VisitWhenExpression([NotNull] WhenExpressionContext context)
        {
            TriggerEventHandler EventHandler = new TriggerEventHandler();
            TriggerEvent TriggerEvent = VisitEvent(context.triggerEvent(), EventHandler);
            EventHandler.TriggeringEvent = TriggerEvent;

            if (CreationStage.Equals(TypeCreationStage.CommandListing))
            {
                CreationStage = TypeCreationStage.EventCommandBlock;
                TriggerEventHandler = EventHandler;
                VisitBlock(context.block());
                TriggerEventHandler = null;
                CreationStage = TypeCreationStage.CommandListing;
                Program.GetCharacterType(this.typeName).EventHandlers.Add(EventHandler);
            }
            if (CreationStage.Equals(TypeCreationStage.ConditionalCommandBlock))
            {
                Command previousConditionalCommand = ConditionalCommand;
                WhenCommand newCommand = new WhenCommand
                {
                    TriggerEventHandler = EventHandler
                };
                ConditionalCommand = newCommand;
                VisitBlock(context.block());
                ConditionalCommand = previousConditionalCommand;
                AddCommand(newCommand);
            }
            if (CreationStage.Equals(TypeCreationStage.EventCommandBlock))
            {
                CreationStage = TypeCreationStage.ConditionalCommandBlock;
                WhenCommand newCommand = new WhenCommand
                {
                    TriggerEventHandler = EventHandler
                };
                ConditionalCommand = newCommand;
                VisitBlock(context.block());
                ConditionalCommand = null;
                CreationStage = TypeCreationStage.EventCommandBlock;
                AddCommand(newCommand);
            }    

            return base.VisitWhenExpression(context);
        }

        public TriggerEvent VisitEvent(TriggerEventContext context, TriggerEventHandler eventHandler)
        {
            TriggerEvent resultTrigger = new TriggerEvent();
            if(context.HEALTH_CHECK() != null)
            {
                EventCollection.PlayerHealthCheck += eventHandler.OnEvent;
                resultTrigger.SourceCharacter = new PlayerType();
                return resultTrigger;
            }
            if (context.character() != null)
            {
                if(context.character().PLAYER() != null)
                {
                    resultTrigger = VisitPlayerActionContext(context, resultTrigger, eventHandler);
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

        private TriggerEvent VisitPlayerActionContext(TriggerEventContext context, TriggerEvent resultTrigger, TriggerEventHandler eventHandler)
        {
            if (context.action() == null)
            {
                Error += ErrorMessages.EventError.EVENT_WITHOUT_ACTION;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            resultTrigger.SourceCharacter = new PlayerType();
            if (context.action().place() != null)
            {
                resultTrigger.TargetPlace = new Place(int.Parse(context.action().place().x().GetText())-1,
                                       int.Parse(context.action().place().y().GetText())-1);
            }
            if (context.action().MOVE() != null)
            {
                EventCollection.PlayerMoved += eventHandler.OnEvent;
                if (context.action().fromPlace() != null)
                    resultTrigger.SourcePlace = new Place(int.Parse(context.action().place().x().GetText())-1,
                                            int.Parse(context.action().place().y().GetText())-1);
                return resultTrigger;
            }
            if (context.action().DIE() != null)
            {
                EventCollection.PlayerDied += eventHandler.OnEvent;
                return resultTrigger;
            }
            if (context.action().SHOOT() != null)
            {
                EventCollection.PlayerShot += eventHandler.OnEvent;
                if (context.action().NUMBER() != null)
                {
                    resultTrigger.Amount = double.Parse(context.action().NUMBER().GetText());
                }
                if (context.action().character() != null)
                {
                    if (context.action().character().PLAYER() != null)
                    {
                        Error += ErrorMessages.EventError.PLAYER_SHOOTING_ITSELF;
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                    if (context.action().character().MONSTER() != null || (type.Equals(Types.MONSTER) && context.action().character().ME() != null))
                        resultTrigger.TargetCharacter = new MonsterType();
                    if (context.action().character().TRAP() != null || (type.Equals(Types.TRAP) && context.action().character().ME() != null))
                        resultTrigger.TargetCharacter = new TrapType();
                    return resultTrigger;
                }
                if (context.action().place() == null)
                {
                    Error += ErrorMessages.EventError.ACTION_WITHOUT_CHARACTER_OR_PLACE;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
            }
            if (context.action().DAMAGE() != null)
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
                resultTrigger.TargetPlace = new Place(int.Parse(context.action().place().x().GetText())-1,
                                       int.Parse(context.action().place().y().GetText())-1);
            }
            if (context.action().MOVE() != null)
            {
                EventCollection.MonsterMoved += eventHandler.OnEvent;
                if (context.action().fromPlace() != null)
                    resultTrigger.SourcePlace = new Place(int.Parse(context.action().place().x().GetText())-1,
                                            int.Parse(context.action().place().y().GetText())-1);
                return resultTrigger;
            }
            if (context.action().DIE() != null)
            {
                EventCollection.MonsterDied += eventHandler.OnEvent;
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
                    Error += ErrorMessages.EventError.ACTION_WITHOUT_CHARACTER_OR_PLACE;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
            }
            if (context.action().DAMAGE() != null)
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
            return resultTrigger;
        }

        private TriggerEvent VisitTrapActionContext(TriggerEventContext context, TriggerEvent resultTrigger, TriggerEventHandler eventHandler)
        {
            if (context.action() == null)
            {
                Error += ErrorMessages.EventError.EVENT_WITHOUT_ACTION;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            resultTrigger.SourceCharacter = new TrapType();
            if (context.action().place() != null)
            {
                resultTrigger.TargetPlace = new Place(int.Parse(context.action().place().x().GetText())-1,
                                       int.Parse(context.action().place().y().GetText()));
            }
            if (context.action().MOVE() != null)
            {
                EventCollection.TrapMoved += eventHandler.OnEvent;
                if (context.action().fromPlace() != null)
                    resultTrigger.SourcePlace = new Place(int.Parse(context.action().place().x().GetText())-1,
                                            int.Parse(context.action().place().y().GetText())-1);
                return resultTrigger;
            }
            if (context.action().DIE() != null)
            {
                Error += ErrorMessages.EventError.TRAPS_DO_NOT_DIE;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            if (context.action().SHOOT() != null)
            {
                Error += ErrorMessages.EventError.ONLY_MONSTER_CAN_SHOOT;
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
                        Error += ErrorMessages.EventError.TRAP_DAMAGING_TRAP;
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                    return resultTrigger;
                }
                if (context.action().place() == null)
                {
                    Error += ErrorMessages.EventError.ACTION_WITHOUT_CHARACTER_OR_PLACE;
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
                        Error += ErrorMessages.EventError.TRAP_HEALING_TRAP;
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                    return resultTrigger;
                }
                if (context.action().place() == null)
                {
                    Error += ErrorMessages.EventError.ACTION_WITHOUT_CHARACTER_OR_PLACE;
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
                    Error += ErrorMessages.EventError.TRAP_TELEPORTING_TRAP;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                if (context.action().place() != null)
                {
                    resultTrigger.TargetPlace = new Place(int.Parse(context.action().place().x().GetText())-1,
                                           int.Parse(context.action().place().y().GetText())-1);
                    return resultTrigger;
                }
            }
            if (context.action().SPAWN() != null)
            {
                EventCollection.TrapTeleported += eventHandler.OnEvent;
                if (context.action().character().PLAYER() != null)
                {
                    Error += ErrorMessages.EventError.TRAP_SPAWNING_PLAYER;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                if (context.action().character().MONSTER() != null || (type.Equals(Types.MONSTER) && context.action().character().ME() != null ))
                {
                    resultTrigger.TargetCharacter = new MonsterType();
                }
                if (context.action().character().TRAP() != null || (type.Equals(Types.TRAP) && context.action().character().ME() != null ))
                {
                    Error += ErrorMessages.EventError.TRAP_SPAWNING_TRAP;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                if (context.action().place() != null)
                {
                    resultTrigger.TargetPlace = new Place(int.Parse(context.action().place().x().GetText())-1,
                                           int.Parse(context.action().place().y().GetText())-1);
                    return resultTrigger;
                }
            }
            Error += ErrorMessages.EventError.UNEXPECTED_ERROR;
            Error += context.GetText() + "\n";
            ErrorFound = true;
            return resultTrigger;
        }

        public bool GetCondition(GameParamProvider provider, [NotNull] BoolExpressionContext context)
        {
            ConditionVisitor visitor = new ConditionVisitor(provider, context);
            return visitor.CheckConditions();
        }


        public void AddCommand(Command newCommand)
        {
            switch (CreationStage)
            {
                case TypeCreationStage.CommandListing:
                    Program.GetCharacterType(typeName).Commands.Add(newCommand);
                    break;
                case TypeCreationStage.ConditionalCommandBlock:
                    ConditionalCommand.CommandList.Add(newCommand);
                    break;
                case TypeCreationStage.EventCommandBlock:
                    TriggerEventHandler.Commands.Add(newCommand);
                    break;
                default:
                    Error += ErrorMessages.CommandAddingError.UNEXPECTED_ERROR;
                    ErrorFound = true;
                    break;
            }
        }


        //TODO: collision detectation fucntion should be created and called, whenever we want to move someone or teleport or spawn.


        public void Spawn(GameParamProvider provider, SpawnCommand command)
        {
            TriggerEvent spawnEvent = new TriggerEvent
            {
                SourceCharacter = new TrapType(),
                TargetCharacter = new MonsterType()
            };

            if (provider.IsFreePlace(command.TargetPlace))
                return;
            if ( ( command.TargetCharacterType == null || Program.GetCharacterType(command.TargetCharacterType.Name) == null )
                && provider.GetMe().GetCharacterType().SpawnType == null)
            {
                return;
            }
            if ((command.TargetCharacterType == null ||  ( ! (Program.GetCharacterType(command.TargetCharacterType.Name) is MonsterType) ) )
                && provider.GetMe().GetCharacterType().SpawnType == null)
                return;
            if (provider.GetMe().GetCharacterType().SpawnType == null)
                command.TargetCharacterType = Program.GetCharacterType(command.TargetCharacterType.Name);
            else
            {
                if ( ! (Program.GetCharacterType(provider.GetMe().GetCharacterType().SpawnType.Name) is MonsterType) )
                    return;
                else
                    command.TargetCharacterType = provider.GetMe().GetCharacterType().SpawnType;
            }
            
            Monster newMonster = new Monster(command.TargetCharacterType.Health, (MonsterType)command.TargetCharacterType, command.TargetPlace);
            spawnEvent.TargetPlace = command.TargetPlace;

            provider.SpawnMonster(newMonster);
            provider.GetBoard().Monsters.Add(newMonster);
            EventCollection.InvokeTrapSpawned(provider.GetMe(), spawnEvent);
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
            TriggerEvent teleportEvent = new TriggerEvent
            {
                EventType = EventType.Teleport,
                TargetCharacter = new TrapType(),
                SourceCharacter = new TrapType(),
                SourcePlace = provider.GetMe().Place
            };
            foreach (Trap Trap in provider.GetTraps())
            {
                if (Trap.Place.DirectionTo(provider.GetMe().Place).Equals(Directions.COLLISION) && !Trap.Equals(provider.GetMe()) )
                {
                    if (command.Random)
                    {
                        Random rand = new Random();
                        int XPos = (int)(rand.Next() % provider.GetBoard().Height);
                        int YPos = (int)(rand.Next() % provider.GetBoard().Width);
                        command.TargetPlace = new Place(XPos, YPos);
                    }
                    if (!provider.IsFreePlace(command.TargetPlace))
                    {
                        teleportEvent.TargetPlace = command.TargetPlace;
                        Trap.Place = command.TargetPlace;
                        EventCollection.InvokeTrapTeleported(provider.GetMe(), teleportEvent);
                    }
                }
            }
        }
        public void TeleportMonster(GameParamProvider provider, TeleportCommand command)
        {
            TriggerEvent teleportEvent = new TriggerEvent
            {
                EventType = EventType.Teleport,
                TargetCharacter = new MonsterType(),
                SourceCharacter = new TrapType(),
                SourcePlace = provider.GetMe().Place
            };
            foreach (Monster monster in provider.GetMonsters()) {
                if (monster.Place.DirectionTo(provider.GetTrap().Place).Equals(Directions.COLLISION))
                {
                    if (command.Random)
                    {
                        Random rand = new Random();
                        int XPos = (int)(rand.Next() % provider.GetBoard().Height);
                        int YPos = (int)(rand.Next() % provider.GetBoard().Width);
                        command.TargetPlace = new Place(XPos, YPos);
                    }
                    if (!provider.IsFreePlace(command.TargetPlace))
                    {
                        teleportEvent.TargetPlace = command.TargetPlace;
                        monster.Place = command.TargetPlace;
                        EventCollection.InvokeTrapTeleported(provider.GetMe(), teleportEvent);
                    }
                }
            }
        }
        public void TeleportPlayer(GameParamProvider provider, TeleportCommand command)
        {
            TriggerEvent teleportEvent = new TriggerEvent
            {
                EventType = EventType.Teleport,
                TargetCharacter = new PlayerType(),
                SourceCharacter = new TrapType(),
                SourcePlace = provider.GetMe().Place
            };
            if (provider.GetPlayer().Place.DirectionTo(provider.GetTrap().Place).Equals(Directions.COLLISION))
            {
                if (command.Random)
                {
                    Random rand = new Random();
                    int XPos = (int)(rand.Next() % provider.GetBoard().Height);
                    int YPos = (int)(rand.Next() % provider.GetBoard().Width);
                    command.TargetPlace = new Place(XPos, YPos);
                }
                if (!provider.IsFreePlace(command.TargetPlace))
                {
                    teleportEvent.TargetPlace = command.TargetPlace;
                    provider.GetPlayer().Place = command.TargetPlace;
                    EventCollection.InvokeTrapTeleported(provider.GetMe(), teleportEvent);
                }
            }

        }

        //TODO: check with test if fall check this way okay or not
        public void MoveDirection(GameParamProvider provider, MoveCommand command)
        {
            TriggerEvent moveEvent = new TriggerEvent
            {
                EventType = EventType.Move,
                SourcePlace = provider.GetMe().Place,
                SourceCharacter = provider.GetMe().GetCharacterType()
            };
            switch (command.Direction)
            {
                case Directions.FORWARD:
                    if (provider.IsFreePlace(new Place( (int)provider.GetMe().Place.X - command.Distance, (int)provider.GetMe().Place.Y ) ) )
                        provider.GetMe().Place.X -= (int)command.Distance;
                    break;
                case Directions.BACKWARDS:
                    if (provider.IsFreePlace(new Place( (int)provider.GetMe().Place.X + command.Distance, (int)provider.GetMe().Place.Y) ) )
                        provider.GetMe().Place.X += (int)command.Distance;
                    break;
                case Directions.LEFT:
                    if (provider.IsFreePlace(new Place((int)provider.GetMe().Place.X, (int)provider.GetMe().Place.Y - command.Distance)))
                        provider.GetMe().Place.Y -= (int)command.Distance;
                    break;
                case Directions.RIGHT:
                    if (provider.IsFreePlace(new Place((int)provider.GetMe().Place.X, (int)provider.GetMe().Place.Y + command.Distance)))
                        provider.GetMe().Place.Y += (int)command.Distance;
                    break;
            }
            moveEvent.TargetPlace = provider.GetMe().Place;
            if (provider.GetMe().GetCharacterType() is MonsterType)
            {
                EventCollection.InvokeMonsterMoved(provider.GetMe(), moveEvent);
            }
            if (provider.GetMe().GetCharacterType() is TrapType)
            {
                EventCollection.InvokeTrapMoved(provider.GetMe(), moveEvent);
            }
        }
        public void MoveToPlace(GameParamProvider provider, MoveCommand command)
        {
            TriggerEvent moveEvent = new TriggerEvent
            {
                EventType = EventType.Move,
                SourcePlace = provider.GetMe().Place,
                SourceCharacter = provider.GetMe().GetCharacterType()
            };
            if(provider.IsFreePlace(command.TargetPlace))
                provider.GetMe().Place = command.TargetPlace;

            moveEvent.TargetPlace = provider.GetMe().Place;
            if (provider.GetMe().GetCharacterType() is MonsterType)
            {
                EventCollection.InvokeMonsterMoved(provider.GetMe(), moveEvent);
            }
            if (provider.GetMe().GetCharacterType() is TrapType)
            {
                EventCollection.InvokeTrapMoved(provider.GetMe(), moveEvent);
            }
        }

        //TODO: fix this logic 
        public void MoveToPlayer(GameParamProvider provider, MoveCommand command)
        {
            TriggerEvent moveEvent = new TriggerEvent
            {
                EventType = EventType.Move,
                SourcePlace = provider.GetMe().Place,
                SourceCharacter = provider.GetMe().GetCharacterType()
            };

            Random rand = new Random();
                if(provider.GetMe().Place.X < provider.GetPlayer().Place.X)
                {
                    int xChange = rand.Next() % (provider.GetPlayer().Place.X - provider.GetMe().Place.X);
                    if (provider.IsFreePlace(new Place( provider.GetMe().Place.X + xChange,  provider.GetMe().Place.Y)))
                        provider.GetMe().Place.X += xChange; 
                }
                if(provider.GetMe().Place.X > provider.GetPlayer().Place.X)
                {
                    int xChange = rand.Next() % (provider.GetMe().Place.X - provider.GetPlayer().Place.X);
                    if (provider.IsFreePlace(new Place(provider.GetMe().Place.X - xChange, provider.GetMe().Place.Y)))
                        provider.GetMe().Place.X -= xChange;
                }
                if (provider.GetMe().Place.Y < provider.GetPlayer().Place.Y)
                {
                    int yChange = rand.Next() % (provider.GetPlayer().Place.Y - provider.GetMe().Place.Y);
                    if (provider.IsFreePlace(new Place(provider.GetMe().Place.Y + yChange, provider.GetMe().Place.Y)))
                        provider.GetMe().Place.Y += yChange;
                }
                if (provider.GetMe().Place.Y > provider.GetPlayer().Place.Y)
                {
                    int yChange = rand.Next() % (provider.GetMe().Place.Y - provider.GetPlayer().Place.Y);
                    if (provider.IsFreePlace(new Place(provider.GetMe().Place.Y - yChange, provider.GetMe().Place.Y)))
                        provider.GetMe().Place.Y -= yChange;
                }

            moveEvent.TargetPlace = provider.GetMe().Place;
            if (provider.GetMe().GetCharacterType() is MonsterType)
            {
                EventCollection.InvokeMonsterMoved(provider.GetMe(), moveEvent);
            }
            if (provider.GetMe().GetCharacterType() is TrapType)
            {
                EventCollection.InvokeTrapMoved(provider.GetMe(), moveEvent);
            }
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
            TriggerEvent shootEvent = new TriggerEvent
            {
                EventType = EventType.Shoot,
                SourceCharacter = new MonsterType(),
                SourcePlace = provider.GetMe().Place,
                Amount = command.HealthChangeAmount
        };
            switch (command.Direction)
            {
                case Directions.FORWARD:
                    for (int i = 0; i <= command.Distance; i++)
                    {
                        if (provider.GetMe().Place.X - i >= 0) {
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X - i
                                && provider.GetPlayer().Place.Y == provider.GetMe().Place.Y)
                            {
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                                shootEvent.TargetCharacter = new PlayerType();
                            }
                            shootEvent.TargetPlace = new Place(provider.GetMe().Place.X - i, provider.GetMe().Place.Y);
                            EventCollection.InvokeMonsterShot(provider.GetMe(), shootEvent);
                        }
                    }
                    break;
                case Directions.BACKWARDS:
                    for (int i = 0; i <= command.Distance; i++)
                    {
                        if (provider.GetMe().Place.X + i < provider.GetBoard().Height)
                        {
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X + i
                                && provider.GetPlayer().Place.Y == provider.GetMe().Place.Y)
                            {
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                                shootEvent.TargetCharacter = new PlayerType();
                            }
                            shootEvent.TargetPlace = new Place(provider.GetMe().Place.X + i, provider.GetMe().Place.Y);
                            EventCollection.InvokeMonsterShot(provider.GetMe(), shootEvent);
                        }
                    }
                    break;
                case Directions.LEFT:
                    for (int i = 0; i <= command.Distance; i++)
                    {
                        if (provider.GetMe().Place.Y - i >= 0)
                        {
                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y - i
                                && provider.GetPlayer().Place.X == provider.GetMe().Place.X)
                            {
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                                shootEvent.TargetCharacter = new PlayerType();
                            }
                            shootEvent.TargetPlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y - i);
                            EventCollection.InvokeMonsterShot(provider.GetMe(), shootEvent);
                        }
                    }
                    break;
                case Directions.RIGHT:
                    for (int i = 0; i <= command.Distance; i++)
                    {
                        if (provider.GetMe().Place.Y + i < provider.GetBoard().Width)
                        {
                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y + i
                                && provider.GetPlayer().Place.X != provider.GetMe().Place.X)
                            {
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                                shootEvent.TargetCharacter = new PlayerType();
                            }
                            shootEvent.TargetPlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y + i);
                            EventCollection.InvokeMonsterShot(provider.GetMe(), shootEvent);
                        }
                    }
                    break;
            }
        }

        public void ShootToPlace(GameParamProvider provider, ShootCommand command)
        {
            TriggerEvent shootEvent = new TriggerEvent
            {
                EventType = EventType.Shoot,
                SourceCharacter = new MonsterType(),
                SourcePlace = provider.GetMe().Place,
                TargetPlace = command.TargetPlace,
                Amount = command.HealthChangeAmount
            };
            if (provider.GetPlayer().Place.Equals(command.TargetPlace))
            {
                provider.GetPlayer().Damage(command.HealthChangeAmount);
                shootEvent.TargetCharacter = new PlayerType();
            }
            EventCollection.InvokeMonsterShot(provider.GetMe(), shootEvent);
        }

        public void ShootToPlayer(GameParamProvider provider, ShootCommand command)
        {
            TriggerEvent shootEvent = new TriggerEvent
            {
                EventType = EventType.Shoot,
                SourceCharacter = new MonsterType(),
                SourcePlace = provider.GetMe().Place,
                TargetCharacter = new PlayerType(),
                TargetPlace = provider.GetPlayer().Place,
                Amount = command.HealthChangeAmount
            };
            provider.GetPlayer().Damage(command.HealthChangeAmount);
            EventCollection.InvokeMonsterShot(provider.GetMe(), shootEvent);
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
            TriggerEvent damageEvent = new TriggerEvent
            {
                EventType = EventType.Damage,
                SourceCharacter = new TrapType(),
                SourcePlace = provider.GetMe().Place,
                Amount = command.HealthChangeAmount
            };
            switch (command.Direction)
            {
                case Directions.FORWARD:
                    //distance is 1 as default value
                    for (int i = 0; i <= command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.X - i >= 0)
                        {
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X - i
                                && provider.GetPlayer().Place.Y == provider.GetMe().Place.Y)
                            {
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                                damageEvent.TargetCharacter = new PlayerType();
                            }
                            foreach(Monster monster in provider.GetMonsters())
                            {
                                if (monster.Place.X == provider.GetMe().Place.X - i
                                    && monster.Place.Y == provider.GetMe().Place.Y)
                                {
                                    monster.Damage(command.HealthChangeAmount);
                                    damageEvent.TargetCharacter = new MonsterType();
                                }
                            }
                            damageEvent.TargetPlace = new Place(provider.GetMe().Place.X - i, provider.GetMe().Place.Y);
                            EventCollection.InvokeTrapDamaged(provider.GetMe(), damageEvent);
                        }
                    }
                    break;
                case Directions.BACKWARDS:
                    for (int i = 0; i <= command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.X + i < provider.GetBoard().Height)
                        {
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X + i
                                && provider.GetPlayer().Place.Y == provider.GetMe().Place.Y)
                            {
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                                damageEvent.TargetCharacter = new PlayerType();
                            }
                            foreach (Monster monster in provider.GetMonsters())
                            {
                                if (monster.Place.X == provider.GetMe().Place.X + i
                                    && monster.Place.Y == provider.GetMe().Place.Y)
                                {
                                    monster.Damage(command.HealthChangeAmount);
                                    damageEvent.TargetCharacter = new MonsterType();
                                }
                            }
                            damageEvent.TargetPlace = new Place(provider.GetMe().Place.X + i, provider.GetMe().Place.Y);
                            EventCollection.InvokeTrapDamaged(provider.GetMe(), damageEvent);
                        }
                    }
                    break;
                case Directions.LEFT:
                    for (int i = 0; i <= command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.Y - i >= 0)
                        {
                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y - i
                                && provider.GetPlayer().Place.X == provider.GetMe().Place.X)
                            {
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                                damageEvent.TargetCharacter = new PlayerType();
                            }
                            foreach (Monster monster in provider.GetMonsters())
                            {
                                if (monster.Place.Y == provider.GetMe().Place.Y - i
                                    && monster.Place.X == provider.GetMe().Place.X)
                                {
                                    monster.Damage(command.HealthChangeAmount);
                                    damageEvent.TargetCharacter = new MonsterType();
                                }
                            }
                            damageEvent.TargetPlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y - i);
                            EventCollection.InvokeTrapDamaged(provider.GetMe(), damageEvent);
                        }
                    }
                    break;
                case Directions.RIGHT:
                    for (int i = 0; i <= command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.Y + i < provider.GetBoard().Width) {

                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y + i
                                && provider.GetPlayer().Place.X == provider.GetMe().Place.X) 
                            { 
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                                damageEvent.TargetCharacter = new PlayerType();
                            }
                            foreach (Monster monster in provider.GetMonsters())
                            {
                                if (monster.Place.Y == provider.GetMe().Place.Y + i
                                    && monster.Place.X == provider.GetMe().Place.X)
                                {
                                    monster.Damage(command.HealthChangeAmount);
                                    damageEvent.TargetCharacter = new MonsterType();
                                }
                            }
                            damageEvent.TargetPlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y + i);
                            EventCollection.InvokeTrapDamaged(provider.GetMe(), damageEvent);
                        }
                    }
                    break;
            }
        }

        public void DamageToPlace(GameParamProvider provider, DamageCommand command)
        {
            TriggerEvent damageEvent = new TriggerEvent
            {
                EventType = EventType.Damage,
                SourceCharacter = new TrapType(),
                SourcePlace = provider.GetMe().Place,
                Amount = command.HealthChangeAmount,
                TargetPlace = command.TargetPlace
            };
            if (provider.GetPlayer().Place.Equals(command.TargetPlace))
            {
                provider.GetPlayer().Damage(command.HealthChangeAmount);
                damageEvent.TargetCharacter = new PlayerType();
            }
            foreach(Monster monster in provider.GetMonsters())
            {
                if (monster.Place.Equals(command.TargetPlace))
                {
                    monster.Damage(command.HealthChangeAmount);
                    damageEvent.TargetCharacter = new MonsterType();
                }
            }
            EventCollection.InvokeTrapDamaged(provider.GetMe(), damageEvent);
        }

        public void DamageToPlayer(GameParamProvider provider, DamageCommand command)
        {
            TriggerEvent damageEvent = new TriggerEvent
            {
                EventType = EventType.Damage,
                SourceCharacter = new TrapType(),
                SourcePlace = provider.GetMe().Place,
                Amount = command.HealthChangeAmount,
                TargetPlace = provider.GetPlayer().Place,
                TargetCharacter = new PlayerType()
            };
            provider.GetPlayer().Damage(command.HealthChangeAmount);
            EventCollection.InvokeTrapDamaged(provider.GetMe(), damageEvent);
        }

        public void DamageToMonster(GameParamProvider provider, DamageCommand command)
        {
            TriggerEvent damageEvent = new TriggerEvent
            {
                EventType = EventType.Damage,
                SourceCharacter = new TrapType(),
                SourcePlace = provider.GetMe().Place,
                Amount = command.HealthChangeAmount,
                TargetPlace = provider.GetMonster().Place,
                TargetCharacter = new MonsterType()
            };
            provider.GetMonster().Damage(command.HealthChangeAmount);
            EventCollection.InvokeTrapDamaged(provider.GetMe(), damageEvent);
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
            TriggerEvent healEvent = new TriggerEvent
            {
                EventType = EventType.Heal,
                SourceCharacter = new TrapType(),
                SourcePlace = provider.GetMe().Place,
                Amount = command.HealthChangeAmount
            };
            switch (command.Direction)
            {
                case Directions.FORWARD:
                    //distance is 1 as default value
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.X - i >= 0)
                        {
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X - i
                                && provider.GetPlayer().Place.Y == provider.GetMe().Place.Y)
                            {
                                provider.GetPlayer().Heal(command.HealthChangeAmount);
                                healEvent.TargetCharacter = new PlayerType();
                            }
                            foreach (Monster monster in provider.GetMonsters())
                            {
                                if (monster.Place.X == provider.GetMe().Place.X - i
                                && monster.Place.Y == provider.GetMe().Place.Y)
                                {
                                    monster.Heal(command.HealthChangeAmount);
                                    healEvent.TargetCharacter = new MonsterType();
                                }
                            }
                        }
                    }
                    break;
                case Directions.BACKWARDS:
                    if (provider.GetPlayer().Place.Y != provider.GetMe().Place.Y)
                        break;
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.X + i < provider.GetBoard().Height)
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X + i)
                                provider.GetPlayer().Heal(command.HealthChangeAmount);
                    }
                    foreach (Monster monster in provider.GetMonsters())
                    {
                        for (int i = 0; i < command.Distance; i++)
                        {
                            if ((int)provider.GetMe().Place.X + i < provider.GetBoard().Height)
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
                        if ((int)provider.GetMe().Place.Y + i < provider.GetBoard().Width)
                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y + i)
                                provider.GetPlayer().Heal(command.HealthChangeAmount);
                    }
                    foreach (Monster monster in provider.GetMonsters())
                    {
                        for (int i = 0; i < command.Distance; i++)
                        {
                            if ((int)provider.GetMe().Place.Y + i < provider.GetBoard().Width)
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
                    Error += ErrorMessages.HealthChangeError.WRONG_DIRECTION;
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
                    Error += ErrorMessages.HealthChangeError.CHARACTER_HAS_NO_HEALTH;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                if (command is ShootCommand)
                {
                    if (context.character().MONSTER() != null)
                    {
                        Error += ErrorMessages.ShootError.MONSTER_CANNOT_BE_SHOT;
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
