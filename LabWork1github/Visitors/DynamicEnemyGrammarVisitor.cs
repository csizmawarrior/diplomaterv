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
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            typeName = context.name().GetText();
            if (Program.GetCharacterType(typeName) != null)
            {
                Error += ErrorMessages.ParameterDeclarationError.TRAP_TYPE_ALREADY_EXISTS;
                Error += context.GetText() + "\n";
                ErrorFound = true;
                return null;
            }
            else
            {
                type = Types.TRAP;
                Program.CharacterTypes.Add(new TrapType(typeName));
            }
            Program.GetCharacterType(typeName).Damage = StaticStartValues.STARTER_TRAP_DAMAGE;
            return base.VisitTrapNameDeclaration(context);
        }
        public override object VisitMonsterNameDeclaration([NotNull] MonsterNameDeclarationContext context)
        {
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            typeName = context.name().GetText();
            if (Program.GetCharacterType(typeName) != null)
            {
                Error += ErrorMessages.ParameterDeclarationError.MONSTER_TYPE_ALREADY_EXISTS;
                Error += context.GetText() + "\n";
                ErrorFound = true;
                return null;
            }
            else
            {
                type = Types.MONSTER;
                Program.CharacterTypes.Add(new MonsterType(typeName));
            }
            Program.GetCharacterType(typeName).Health = StaticStartValues.STARTER_MONSTER_HP;
            Program.GetCharacterType(typeName).Damage = StaticStartValues.STARTER_MONSTER_DAMAGE;
            return base.VisitMonsterNameDeclaration(context);
        }
        public override object VisitDeclarations([NotNull] DeclarationsContext context)
        {
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            foreach (var child in context.declareStatements())
            {
                VisitDeclareStatements(child);
            }
            if (context.COMMANDS() != null)
            {
                CreationStage = TypeCreationStage.CommandListing;
            }
            return null;
        }
        public override object VisitHealthDeclaration([NotNull] HealthDeclarationContext context)
        {
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            if (type == Types.TRAP)
            {
                Error += ErrorMessages.ParameterDeclarationError.TRAP_HAS_NO_HEALTH;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else if (CreationStage.Equals(TypeCreationStage.ParameterDeclare))
            {
                Program.GetCharacterType(typeName).Health = Parsers.DoubleParseFromNumber(context.NUMBER().GetText());
                if(Program.GetCharacterType(typeName).Health == 0)
                {
                    Error += ErrorMessages.ParameterDeclarationError.MONSTER_ZERO_HEALTH;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                    Program.GetCharacterType(typeName).Health = StaticStartValues.PLACEHOLDER_HEALTH;
                }
            }
            else
            {
                NumberParameterDeclareCommand newCommand = new NumberParameterDeclareCommand();
                newCommand.Number = Parsers.DoubleParseFromNumber(context.NUMBER().GetText());
                newCommand.NumberParameterDeclareDelegate = new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.HealthChange);
                AddCommand(newCommand);
            }
            
            return base.VisitHealthDeclaration(context);
        }
        public override object VisitHealAmountDeclaration([NotNull] HealAmountDeclarationContext context)
        {
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            if (type != Types.TRAP)
            {
                Error += ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_HEAL;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else if (CreationStage.Equals(TypeCreationStage.ParameterDeclare))
                    Program.GetCharacterType(typeName).Heal = Parsers.DoubleParseFromNumber(context.NUMBER().GetText());
            else
            {
                NumberParameterDeclareCommand newCommand = new NumberParameterDeclareCommand();
                newCommand.Number = Parsers.DoubleParseFromNumber(context.NUMBER().GetText());
                newCommand.NumberParameterDeclareDelegate = new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.HealChange);
                AddCommand(newCommand);
            }
            
            return base.VisitHealAmountDeclaration(context);
        }
        public override object VisitDamageAmountDeclaration([NotNull] DamageAmountDeclarationContext context)
        {
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            if (CreationStage.Equals(TypeCreationStage.ParameterDeclare))
                Program.GetCharacterType(typeName).Damage = Parsers.DoubleParseFromNumber(context.NUMBER().GetText());
            else
            {
                NumberParameterDeclareCommand newCommand = new NumberParameterDeclareCommand();
                newCommand.Number = Parsers.DoubleParseFromNumber(context.NUMBER().GetText());
                newCommand.NumberParameterDeclareDelegate = new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.DamageChange);
                AddCommand(newCommand);
            }
            
            return base.VisitDamageAmountDeclaration(context);
        }
        public override object VisitTeleportPointDeclaration([NotNull] TeleportPointDeclarationContext context)
        {
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            if (type != Types.TRAP)
            {
                Error += ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_TELEPORT;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else if (CreationStage.Equals(TypeCreationStage.ParameterDeclare))
            {
                Program.GetCharacterType(typeName).TeleportPlace = Parsers.PlaceParseFromNumbers(context.place());
                if (Program.GetCharacterType(typeName).TeleportPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                {
                    Error += ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
            }
            else
            {
                PlaceParameterDeclareCommand newCommand = new PlaceParameterDeclareCommand();
                newCommand.Place = Parsers.PlaceParseFromNumbers(context.place());
                if (newCommand.Place.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                {
                    Error += ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                newCommand.PlaceParameterDeclareDelegate = new PlaceParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportPlaceChange);
                AddCommand(newCommand);
            }
            
            return base.VisitTeleportPointDeclaration(context);
        }
        public override object VisitSpawnPointDeclaration([NotNull] SpawnPointDeclarationContext context)
        {
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            if (type != Types.TRAP)
            {
                Error += ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_SPAWN_TO_PLACE;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else if (CreationStage.Equals(TypeCreationStage.ParameterDeclare))
            {
                Program.GetCharacterType(typeName).SpawnPlace = Parsers.PlaceParseFromNumbers(context.place());
                if (Program.GetCharacterType(typeName).SpawnPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                {
                    Error += ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
            }
            else
            {
                PlaceParameterDeclareCommand newCommand = new PlaceParameterDeclareCommand();
                newCommand.Place = Parsers.PlaceParseFromNumbers(context.place());
                if (newCommand.Place.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                {
                    Error += ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                newCommand.PlaceParameterDeclareDelegate = new PlaceParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.SpawnPlaceChange);
                AddCommand(newCommand);
            }
            
            return base.VisitSpawnPointDeclaration(context);
        }
        public override object VisitSpawnTypeDeclaration([NotNull] SpawnTypeDeclarationContext context)
        {
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
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
                newCommand.TypeParameterDeclareDelegate = new TypeParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.SpawnTypeChange);
                AddCommand(newCommand);
            }
            
            return base.VisitSpawnTypeDeclaration(context);
        }
        public override object VisitMoveDeclaration([NotNull] MoveDeclarationContext context)
        {
            if(String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            MoveCommand newCommand = new MoveCommand();
            if (context.distanceDeclare() != null)
            {
                newCommand.Distance = Parsers.IntParseFromNumber(context.distanceDeclare().NUMBER().GetText());
                if (newCommand.Distance == StaticStartValues.PLACEHOLDER_INT)
                {
                    Error += ErrorMessages.ParseError.UNABLE_TO_PARSE_INT;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                    newCommand.Distance = StaticStartValues.STARTER_DISTANCE;
                }
                if (newCommand.Distance <= 0)
                {
                    Error += ErrorMessages.MoveError.ZERO_DISTANCE;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                    newCommand.Distance = StaticStartValues.STARTER_DISTANCE;
                }
            }
            var direction = context.DIRECTION();
            if (direction != null)
            {       
                newCommand.Direction = direction.GetText();
                newCommand.MoveDelegate = new MoveDelegate(DynamicEnemyGrammarVisitorDelegates.MoveDirection);
                AddCommand(newCommand);
                return base.VisitMoveDeclaration(context);
            }
            PlaceContext place = context.place();
            if (place != null)
            {
                newCommand.TargetPlace = Parsers.PlaceParseFromNumbers(place);
                if (newCommand.TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                {
                    Error += ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                newCommand.MoveDelegate = new MoveDelegate(DynamicEnemyGrammarVisitorDelegates.MoveToPlace);
                AddCommand(newCommand);
                return base.VisitMoveDeclaration(context);
            }
            var helpPlayer = context.PLAYER();
            if (helpPlayer != null)
            {
                newCommand.MoveDelegate = new MoveDelegate(DynamicEnemyGrammarVisitorDelegates.MoveToPlayer);
                AddCommand(newCommand);
                return base.VisitMoveDeclaration(context);
            }

            var random = context.RANDOM();
            if (random != null)
            {
                newCommand.MoveDelegate = new MoveDelegate(DynamicEnemyGrammarVisitorDelegates.MoveRandom);
                AddCommand(newCommand);
                return base.VisitMoveDeclaration(context);

            }
            return base.VisitMoveDeclaration(context);
        }

        public override object VisitShootDeclaration([NotNull] ShootDeclarationContext context)
        {
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
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
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            if (!type.Equals(Types.TRAP))
            {
                Error += ErrorMessages.TeleportError.ONLY_TRAP_CAN_TELEPORT;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }

            TeleportCommand newCommand = new TeleportCommand();
            if (context.place() != null)
            {
                newCommand.TargetPlace = Parsers.PlaceParseFromNumbers(context.place());
                if (newCommand.TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                {
                    Error += ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
            }
            else
            {
                if (Program.GetCharacterType(typeName).TeleportPlace.DirectionTo(StaticStartValues.PLACEHOLDER_PLACE) 
                        == Directions.COLLISION &&  context.RANDOM() == null)
                {
                    Error += ErrorMessages.TeleportError.TELEPORTING_WITHOUT_PLACE_GIVEN;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                else
                    newCommand.TargetPlace = Program.GetCharacterType(typeName).TeleportPlace;
            }
            if(context.character() != null)
            {
                switch (context.character().GetText())
                {
                    case Types.PLAYER:
                        newCommand.TeleportDelegate = new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportPlayer);
                        break;
                    case Types.MONSTER:
                        newCommand.TeleportDelegate = new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportMonster);
                        break;
                    case Types.TRAP:
                        newCommand.TeleportDelegate = new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportTrap);
                        break;
                    case Types.PARTNER:
                        newCommand.TeleportDelegate = new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportPartner);
                        break;
                    case Types.ME:
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
                AddCommand(newCommand);
            return base.VisitTeleportDeclaration(context);
        }

        public override object VisitSpawnDeclaration([NotNull] SpawnDeclarationContext context)
        {
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            if (!type.Equals(Types.TRAP))
            {
                Error += ErrorMessages.SpawnError.ONLY_TRAP_CAN_SPAWN;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            SpawnCommand newCommand = new SpawnCommand();
            if (context.RANDOM() != null)
            {
                newCommand.SpawnDelegate = new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.SpawnRandom);
                AddCommand(newCommand);
                return base.VisitSpawnDeclaration(context);
            }
            if (context.place() != null)
            {
                newCommand.TargetPlace = Parsers.PlaceParseFromNumbers(context.place());
                if (newCommand.TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                {
                    Error += ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
            }
            if (Program.GetCharacterType(typeName).SpawnPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE) 
                    && (newCommand.TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE)))
            {
                Error += ErrorMessages.SpawnError.SPAWN_WITHOUT_PLACE_GIVEN;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            else
                if(newCommand.TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                    newCommand.TargetPlace = Program.GetCharacterType(typeName).SpawnPlace;
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
            newCommand.SpawnDelegate = new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn);
            AddCommand(newCommand);
            return base.VisitSpawnDeclaration(context);
        }

        public override object VisitDamageDeclaration([NotNull] DamageDeclarationContext context)
        {
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
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
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
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
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            if (CreationStage != TypeCreationStage.CommandListing && CreationStage != TypeCreationStage.ParameterDeclare)
            {
                foreach (var child in context.statement())
                {
                    Visit(child);
                }
            }
            return null;
        }

        public override object VisitIfExpression([NotNull] IfExpressionContext context)
        {
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
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
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
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
        public override object VisitWhenExpression([NotNull] WhenExpressionContext context)
        {
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            TriggerEventHandler EventHandler = new TriggerEventHandler
            {
                Owner = Program.GetCharacterType(this.typeName)
            };
            TriggerEvent TriggerEvent = VisitEvent(context.triggerEvent(), EventHandler);
            EventHandler.TriggeringEvent = TriggerEvent;

            if (CreationStage.Equals(TypeCreationStage.CommandListing))
            {
                CreationStage = TypeCreationStage.EventCommandBlock;
                TriggerEventHandler = EventHandler;
                VisitBlock(context.block());
                EventHandler = TriggerEventHandler;
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
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            TriggerEvent resultTrigger = new TriggerEvent();
            if(context.HEALTH_CHECK() != null)
            {
                EventCollection.PlayerHealthCheck += eventHandler.OnEvent;
                resultTrigger.SourceCharacter = CharacterOptions.Player;
                resultTrigger.EventType = EventType.HealthCheck;
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
                if (context.character().PARTNER() != null)
                {
                    resultTrigger = VisitPartnerActionContext(context, resultTrigger, eventHandler);
                }
                if (context.character().ME() != null)
                {
                    if(type.Equals(Types.MONSTER))
                        resultTrigger = VisitMonsterActionContext(context, resultTrigger, eventHandler);
                    if (type.Equals(Types.TRAP))
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
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            if (context.action() == null)
            {
                Error += ErrorMessages.EventError.EVENT_WITHOUT_ACTION;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            resultTrigger.SourceCharacter = CharacterOptions.Player;
            if (context.action().place() != null)
            {
                resultTrigger.TargetPlace = Parsers.PlaceParseFromNumbers(context.action().place());
                if (resultTrigger.TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                {
                    Error += ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
            }
            if (context.action().MOVE() != null)
            {
                EventCollection.SomeoneMoved += eventHandler.OnEvent;
                if (context.action().fromPlace() != null)
                {
                    resultTrigger.SourcePlace = Parsers.PlaceParseFromNumbers(context.action().place());
                    if (resultTrigger.SourcePlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                    {
                        Error += ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE;
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                }
                return resultTrigger;
            }
            if (context.action().DIE() != null)
            {
                EventCollection.SomeoneDied += eventHandler.OnEvent;
                return resultTrigger;
            }
            if (context.action().SHOOT() != null)
            {
                EventCollection.SomeoneShot += eventHandler.OnEvent;
                if (context.action().NUMBER() != null)
                {
                    resultTrigger.Amount = Parsers.DoubleParseFromNumber(context.action().NUMBER().GetText());
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
                    {
                        resultTrigger.TargetCharacterOption = CharacterOptions.Monster;
                        if (context.action().character().ME() != null)
                            resultTrigger.TargetCharacterOption = CharacterOptions.Me;
                    }
                    if(context.action().character().PARTNER() != null)
                            resultTrigger.TargetCharacterOption = CharacterOptions.Partner;
                    if (context.action().character().TRAP() != null || (type.Equals(Types.TRAP) && context.action().character().ME() != null))
                    {
                        Error += ErrorMessages.EventError.PLAYER_SHOOTING_TRAP;
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

        private TriggerEvent VisitMonsterActionContext(TriggerEventContext context, TriggerEvent resultTrigger, TriggerEventHandler eventHandler)
        {
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            if (context.action() == null)
            {
                Error += ErrorMessages.EventError.EVENT_WITHOUT_ACTION;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            if(context.character().MONSTER() != null)
                resultTrigger.SourceCharacter = CharacterOptions.Monster;
            if (context.character().ME() != null)
                resultTrigger.SourceCharacter = CharacterOptions.Me;
            if (context.action().place() != null)
            {
                resultTrigger.TargetPlace = Parsers.PlaceParseFromNumbers(context.action().place());
                if (resultTrigger.TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                {
                    Error += ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
            }
            if (context.action().MOVE() != null)
            {
                EventCollection.SomeoneMoved += eventHandler.OnEvent;
                if (context.action().fromPlace() != null)
                {
                    resultTrigger.SourcePlace = Parsers.PlaceParseFromNumbers(context.action().place());
                    if (resultTrigger.SourcePlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                    {
                        Error += ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE;
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                }
                return resultTrigger;
            }
            if (context.action().DIE() != null)
            {
                EventCollection.SomeoneDied += eventHandler.OnEvent;
                return resultTrigger;
            }
            if (context.action().SHOOT() != null)
            {
                EventCollection.SomeoneShot += eventHandler.OnEvent;
                if (context.action().NUMBER() != null)
                {
                    resultTrigger.Amount = Parsers.DoubleParseFromNumber(context.action().NUMBER().GetText());
                }
                if (context.action().character() != null)
                {
                    if (context.action().character().PLAYER() != null)
                        resultTrigger.TargetCharacterOption = CharacterOptions.Player;
                    if (context.action().character().MONSTER() != null || (type.Equals(Types.MONSTER) && context.action().character().ME() != null ))
                    {
                        Error += ErrorMessages.EventError.MONSTER_SHOOTING_MONSTER;
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                    if (context.action().character().PARTNER() != null)
                        resultTrigger.TargetCharacterOption = CharacterOptions.Partner;
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
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            if (context.action() == null)
            {
                Error += ErrorMessages.EventError.EVENT_WITHOUT_ACTION;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            if(context.character().TRAP() != null)
                resultTrigger.SourceCharacter = CharacterOptions.Trap;
            if (context.character().ME() != null)
                resultTrigger.SourceCharacter = CharacterOptions.Me;
            if (context.action().place() != null)
            {
                resultTrigger.TargetPlace = Parsers.PlaceParseFromNumbers(context.action().place());
                if (resultTrigger.TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                {
                    Error += ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
            }
            if (context.action().MOVE() != null)
            {
                EventCollection.SomeoneMoved += eventHandler.OnEvent;
                if (context.action().fromPlace() != null)
                {
                    resultTrigger.SourcePlace = Parsers.PlaceParseFromNumbers(context.action().place());
                    if (resultTrigger.SourcePlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                    {
                        Error += ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE;
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                }
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
                    resultTrigger.Amount = Parsers.DoubleParseFromNumber(context.action().NUMBER().GetText());
                }
                if (context.action().character() != null)
                {
                    if (context.action().character().PLAYER() != null)
                        resultTrigger.TargetCharacterOption = CharacterOptions.Player;
                    if (context.action().character().MONSTER() != null || (type.Equals(Types.MONSTER) && context.action().character().ME() != null ))
                    {
                        resultTrigger.TargetCharacterOption = CharacterOptions.Monster;
                        if (context.action().character().ME() != null)
                            resultTrigger.TargetCharacterOption = CharacterOptions.Me;
                    }
                    if (context.action().character().PARTNER() != null)
                        resultTrigger.TargetCharacterOption = CharacterOptions.Partner;
                    if (context.action().character().TRAP() != null || (type.Equals(Types.TRAP) && context.action().character().ME() != null ))
                    {
                        resultTrigger.TargetCharacterOption = CharacterOptions.Trap;
                        if (context.action().character().ME() != null)
                        {
                            Error += ErrorMessages.TeleportError.TRYING_TO_TELEPORT_YOURSELF;
                            Error += context.GetText() + "\n";
                            ErrorFound = true;
                        }
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
                    resultTrigger.Amount = Parsers.DoubleParseFromNumber(context.action().NUMBER().GetText());
                }
                if (context.action().character() != null)
                {
                    if (context.action().character().PLAYER() != null)
                        resultTrigger.TargetCharacterOption = CharacterOptions.Player;
                    if (context.action().character().MONSTER() != null || (type.Equals(Types.MONSTER) && context.action().character().ME() != null ))
                    {
                        resultTrigger.TargetCharacterOption = CharacterOptions.Monster;
                        if (context.action().character().ME() != null)
                            resultTrigger.TargetCharacterOption = CharacterOptions.Me;
                    }
                    if (context.action().character().PARTNER() != null)
                        resultTrigger.TargetCharacterOption = CharacterOptions.Partner;
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
                    resultTrigger.TargetCharacterOption = CharacterOptions.Player;
                if (context.action().character().MONSTER() != null || (type.Equals(Types.MONSTER) && context.action().character().ME() != null ))
                {
                    resultTrigger.TargetCharacterOption = CharacterOptions.Monster;
                    if (context.action().character().ME() != null)
                        resultTrigger.TargetCharacterOption = CharacterOptions.Me;
                }
                if (context.action().character().PARTNER() != null)
                    resultTrigger.TargetCharacterOption = CharacterOptions.Partner;
                if (context.action().character().TRAP() != null || (type.Equals(Types.TRAP) && context.action().character().ME() != null ))
                {
                    Error += ErrorMessages.EventError.TRAP_TELEPORTING_TRAP;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
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
                    resultTrigger.TargetCharacterOption = CharacterOptions.Monster;
                    if (context.action().character().ME() != null)
                        resultTrigger.TargetCharacterOption = CharacterOptions.Me;
                }
                if (context.action().character().PARTNER() != null)
                    resultTrigger.TargetCharacterOption = CharacterOptions.Partner;
                if (context.action().character().TRAP() != null || (type.Equals(Types.TRAP) && context.action().character().ME() != null ))
                {
                    Error += ErrorMessages.EventError.TRAP_SPAWNING_TRAP;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
            }
            Error += ErrorMessages.EventError.UNEXPECTED_ERROR;
            Error += context.GetText() + "\n";
            ErrorFound = true;
            return resultTrigger;
        }

        private TriggerEvent VisitPartnerActionContext(TriggerEventContext context, TriggerEvent resultTrigger, TriggerEventHandler eventHandler)
        {
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            if (context.action() == null)
            {
                Error += ErrorMessages.EventError.EVENT_WITHOUT_ACTION;
                Error += context.GetText() + "\n";
                ErrorFound = true;
            }
            if(context.character().PARTNER() != null)
                resultTrigger.SourceCharacter = CharacterOptions.Partner;
            if (context.action().place() != null)
            {
                resultTrigger.TargetPlace = Parsers.PlaceParseFromNumbers(context.action().place());
                if (resultTrigger.TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                {
                    Error += ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
            }
            if (context.action().MOVE() != null)
            {
                EventCollection.SomeoneMoved += eventHandler.OnEvent;
                if (context.action().fromPlace() != null)
                {
                    resultTrigger.SourcePlace = Parsers.PlaceParseFromNumbers(context.action().place());
                    if (resultTrigger.SourcePlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                    {
                        Error += ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE;
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                }
                return resultTrigger;
            }
            if (context.action().DIE() != null)
            {
                EventCollection.SomeoneDied += eventHandler.OnEvent;
                return resultTrigger;
            }
            if (context.action().SHOOT() != null)
            {
                EventCollection.SomeoneShot += eventHandler.OnEvent;
                if (context.action().NUMBER() != null)
                {
                    resultTrigger.Amount = Parsers.DoubleParseFromNumber(context.action().NUMBER().GetText());
                }
                if (context.action().character() != null)
                {
                    if (context.action().character().PLAYER() != null)
                        resultTrigger.TargetCharacterOption = CharacterOptions.Player;
                    if (context.action().character().MONSTER() != null || (type.Equals(Types.MONSTER) && context.action().character().ME() != null) ||
                            context.action().character().PARTNER() != null)
                    {
                        Error += ErrorMessages.EventError.MONSTER_SHOOTING_MONSTER;
                        Error += context.GetText() + "\n";
                        ErrorFound = true;
                    }
                    if (context.action().character().TRAP() != null || (type.Equals(Types.TRAP) && context.action().character().ME() != null))
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
                EventCollection.TrapDamaged += eventHandler.OnEvent;
                if (context.action().NUMBER() != null)
                {
                    resultTrigger.Amount = Parsers.DoubleParseFromNumber(context.action().NUMBER().GetText());
                }
                if (context.action().character() != null)
                {
                    if (context.action().character().PLAYER() != null)
                        resultTrigger.TargetCharacterOption = CharacterOptions.Player;
                    if (context.action().character().MONSTER() != null || (type.Equals(Types.MONSTER) && context.action().character().ME() != null))
                    {
                        resultTrigger.TargetCharacterOption = CharacterOptions.Monster;
                        if (context.action().character().ME() != null)
                            resultTrigger.TargetCharacterOption = CharacterOptions.Me;
                    }
                    if (context.action().character().TRAP() != null || (type.Equals(Types.TRAP) && context.action().character().ME() != null) ||
                            context.action().character().PARTNER() != null)
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
                    resultTrigger.Amount = Parsers.DoubleParseFromNumber(context.action().NUMBER().GetText());
                }
                if (context.action().character() != null)
                {
                    if (context.action().character().PLAYER() != null)
                        resultTrigger.TargetCharacterOption = CharacterOptions.Player;
                    if (context.action().character().MONSTER() != null || (type.Equals(Types.MONSTER) && context.action().character().ME() != null))
                    {
                        resultTrigger.TargetCharacterOption = CharacterOptions.Monster;
                        if (context.action().character().ME() != null)
                            resultTrigger.TargetCharacterOption = CharacterOptions.Me;
                    }
                    if (context.action().character().TRAP() != null || (type.Equals(Types.TRAP) && context.action().character().ME() != null) ||
                            context.action().character().PARTNER() != null)
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
                    resultTrigger.TargetCharacterOption = CharacterOptions.Player;
                if (context.action().character().MONSTER() != null || (type.Equals(Types.MONSTER) && context.action().character().ME() != null))
                {
                    resultTrigger.TargetCharacterOption = CharacterOptions.Monster;
                    if (context.action().character().ME() != null)
                        resultTrigger.TargetCharacterOption = CharacterOptions.Me;
                }
                if (context.action().character().TRAP() != null || (type.Equals(Types.TRAP) && context.action().character().ME() != null) ||
                        context.action().character().PARTNER() != null)
                {
                    resultTrigger.TargetCharacterOption = CharacterOptions.Trap;
                    if (context.action().character().ME() != null)
                        resultTrigger.TargetCharacterOption = CharacterOptions.Me;
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
                if (context.action().character().MONSTER() != null || (type.Equals(Types.MONSTER) && context.action().character().ME() != null))
                {
                    resultTrigger.TargetCharacterOption = CharacterOptions.Monster;
                    if (context.action().character().ME() != null)
                        resultTrigger.TargetCharacterOption = CharacterOptions.Me;
                }
                if (context.action().character().TRAP() != null || (type.Equals(Types.TRAP) && context.action().character().ME() != null))
                {
                    Error += ErrorMessages.EventError.TRAP_SPAWNING_TRAP;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
            }
            Error += ErrorMessages.EventError.UNEXPECTED_ERROR;
            Error += context.GetText() + "\n";
            ErrorFound = true;
            return resultTrigger;
        }

        public HealthChangerCommand VisitHealthChangeOption([NotNull] HealthChangeOptionContext context, HealthChangerCommand command)
        {
            if (String.IsNullOrEmpty(context.GetText()))
            {
                ErrorFound = true;
            }
            if (context.distanceDeclare() != null)
            {
                command.Distance = Parsers.IntParseFromNumber(context.distanceDeclare().NUMBER().GetText());
                if (command.Distance == StaticStartValues.PLACEHOLDER_INT)
                {
                    Error += ErrorMessages.ParseError.UNABLE_TO_PARSE_INT;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                    command.Distance = StaticStartValues.STARTER_DISTANCE;
                }
                if (command.Distance <= 0)
                {
                    Error += ErrorMessages.MoveError.ZERO_DISTANCE;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                    command.Distance = StaticStartValues.STARTER_DISTANCE;
                }
            }

            if (context.hpChangeAmountDeclaration() != null)
            {
                if (context.hpChangeAmountDeclaration().damageAmountDeclaration() != null)
                {
                    command.HealthChangeAmount = Parsers.DoubleParseFromNumber(context.hpChangeAmountDeclaration().damageAmountDeclaration().NUMBER().GetText());
                }
                else
                {
                    command.HealthChangeAmount = Parsers.DoubleParseFromNumber(context.hpChangeAmountDeclaration().healAmountDeclaration().NUMBER().GetText());
                }
            }

            var direction = context.DIRECTION();
            if (direction != null)
            {
                command.Direction = direction.GetText();
                if (command is ShootCommand)
                {
                    ((ShootCommand)command).ShootDelegate = new ShootDelegate(DynamicEnemyGrammarVisitorDelegates.ShootDirection);
                }
                if (command is DamageCommand)
                {
                    ((DamageCommand)command).DamageDelegate = new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageDirection);
                }
                if (command is HealCommand)
                {
                    ((HealCommand)command).HealDelegate = new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealDirection);
                }
                return command;
            }
            PlaceContext place = context.place();
            if (place != null)
            {
                command.TargetPlace = Parsers.PlaceParseFromNumbers(place);
                if (command.TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                {
                    Error += ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE;
                    Error += context.GetText() + "\n";
                    ErrorFound = true;
                }
                if (command is ShootCommand)
                {
                    ((ShootCommand)command).ShootDelegate = new ShootDelegate(DynamicEnemyGrammarVisitorDelegates.ShootToPlace);
                }
                if (command is DamageCommand)
                {
                    ((DamageCommand)command).DamageDelegate = new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageToPlace);
                }
                if (command is HealCommand)
                {
                    ((HealCommand)command).HealDelegate = new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealToPlace);
                }
                return command; ;
            }

            if (context.character() != null)
            {
                if (context.character().TRAP() != null || context.character().ME() != null || 
                        context.character().PARTNER() != null)
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
                        ((ShootCommand)command).ShootDelegate = new ShootDelegate(DynamicEnemyGrammarVisitorDelegates.ShootToPlayer);
                }
                if (command is DamageCommand)
                {
                    if (context.character().MONSTER() != null)
                        ((DamageCommand)command).DamageDelegate = new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageToMonster);
                    if (context.character().PLAYER() != null)
                        ((DamageCommand)command).DamageDelegate = new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageToPlayer);
                    if(context.character().PARTNER() != null)
                        ((DamageCommand)command).DamageDelegate = new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageToPartner);
                }
                if (command is HealCommand)
                {
                    if (context.character().MONSTER() != null)
                        ((HealCommand)command).HealDelegate = new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealToMonster);
                    if (context.character().PLAYER() != null)
                        ((HealCommand)command).HealDelegate = new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealToPlayer);
                }
                return command;
            }

            var random = context.RANDOM();
            if (random != null)
            {
                if (command is ShootCommand)
                    ((ShootCommand)command).ShootDelegate = new ShootDelegate(DynamicEnemyGrammarVisitorDelegates.ShootRandom);
                if (command is DamageCommand)
                    ((DamageCommand)command).DamageDelegate = new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageRandom);
                if (command is HealCommand)
                    ((HealCommand)command).HealDelegate = new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealRandom);
                return command;
            }
            return command;
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
    }
}
