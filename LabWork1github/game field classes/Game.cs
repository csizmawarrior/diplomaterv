using Antlr4.Runtime;
using LabWork1github;
using LabWork1github.EventHandling;
using LabWork1github.static_constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{

    public class Game
    {
        public Board Board { get; set; }

        public Player Player { get; set; }

        public List<Monster> Monsters { get; set; }

        public List<Trap> Traps { get; set; }

        public static PlayerMove Move { get; set; } = new PlayerMove();

        public static List<Character> Characters { get; set; } = new List<Character>();
        public int Round { get; set; } = 0;
        public bool WrongMove { get; set; } = false;
        public Drawer Drawer { get; set; }
        public bool Spawned { get; set; } = false;

        public Character ActualCharacter { get; set; }

        private GameParamProvider Provider;

        public void Init()
        {
            Drawer = Program.Drawer;
            Provider = new GameParamProvider(this);
            Board = Program.Board;
            Monsters = Board.Monsters;
            Characters = Program.Characters;
            Traps = Board.Traps;
            Move = new PlayerMove();
            Player = Board.Player;
            CheckOutOfBoundCharacters();
            foreach (Character character in Characters)
            {
                if (character.GetCharacterType().EventHandlers.Count > 0)
                {
                    List<TriggerEventHandler> deleteCandidates = new List<TriggerEventHandler>();
                    foreach (TriggerEventHandler eventHandler in character.GetCharacterType().EventHandlers)
                    {
                        if (eventHandler.TriggeringEvent.SourceCharacter == CharacterOptions.Partner ||
                            eventHandler.TriggeringEvent.TargetCharacterOption == CharacterOptions.Partner)
                            if (!IsEventHandlerValid(eventHandler, character))
                                deleteCandidates.Add(eventHandler);
                        eventHandler.GameParamProvider = Provider;
                    }
                    foreach (TriggerEventHandler eventHandler in deleteCandidates)
                    {
                        character.GetCharacterType().EventHandlers.Remove(eventHandler);
                    }
                }
            }
        }


        //hmm these checks might be able to go into the board visitor

        public void Start()
        {
            Drawer.DrawBoard(Board, Player, Monsters, Traps);
            while (Player.GetHealth() > 0 && Monsters.Count > 0)
            {
                Step();
                Round++;
            }
            if (Player.GetHealth() <= 0)
            {
                TriggerEvent dieEvent = new TriggerEvent
                {
                    EventType = EventType.Die,
                    SourceCharacter = CharacterOptions.Player,
                    SourcePlace = new Place(Player.Place.X, Player.Place.Y)
                };
                EventCollection.InvokeSomeoneDied(Player, dieEvent);
                Drawer.WriteCommand(PlayerInteractionMessages.YOU_LOST);
            }
            else
                Drawer.WriteCommand(PlayerInteractionMessages.YOU_WON);
        }

        public void Step()
        {
            WrongMove = false;
            Move = new PlayerMove();
            Spawned = false;

            foreach (Character character in Characters)
            {
                ActualCharacter = character;
                character.GetCharacterType().Step(Provider);
            }

            if (Spawned)
                Characters.Add(Monsters.ElementAt(Monsters.Count - 1));
            if (WrongMove)
                return;


            foreach (Monster monster in Monsters)
            {
                ActualCharacter = monster;
                if (monster.Health <= 0)
                {
                    TriggerEvent dieEvent = new TriggerEvent
                    {
                        EventType = EventType.Die,
                        SourceCharacter = CharacterOptions.Monster,
                        SourcePlace = new Place(monster.Place.X, monster.Place.Y)
                    };
                    Monsters.Remove(monster);
                    Characters.Remove(monster);
                    EventCollection.InvokeSomeoneDied(monster, dieEvent);
                    break;
                }
            }
            Drawer.DrawBoard(Board, Player, Monsters, Traps);
        }

        private void CommandProcess(string inputCommand)
        {
            AntlrInputStream inputStream = new AntlrInputStream(inputCommand);
            PlayerGrammarLexer PlayerGrammarLexer_ = new PlayerGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(PlayerGrammarLexer_);
            PlayerGrammarParser PlayerGrammarParser = new PlayerGrammarParser(commonTokenStream);
            PlayerGrammarParser.StatementContext chatContext = PlayerGrammarParser.statement();
            PlayerGrammarVisitor visitor = new PlayerGrammarVisitor();
            visitor.Visit(chatContext);
        }

        private bool FallingCheck(Player player, PlayerMove move)
        {
            if (Player.Place.Y == 0 && move.Direction == Directions.LEFT)
                return true;
            if (Player.Place.Y == Board.Width - 1 && move.Direction == Directions.RIGHT)
                return true;
            if (Player.Place.X == 0 && move.Direction == Directions.FORWARD)
                return true;
            if (Player.Place.X == Board.Height - 1 && move.Direction == Directions.BACKWARDS)
                return true;
            return false;
        }

        private void CheckOutOfBoundCharacters()
        {
            List<Character> deleteCandidates = new List<Character>();
            foreach (Character character in Characters)
            {
                if (character.Place.X > Board.Height || character.Place.Y > Board.Width)
                {
                    deleteCandidates.Add(character);
                    Drawer.WriteCommand(ErrorMessages.GameError.CHARACTER_SPAWNED_OUT_OF_BOUNDS + character.Name);
                }
            }
            foreach (Character c in deleteCandidates)
            {
                Characters.Remove(c);
            }
        }

        public void PlayerCommand()
        {
            Drawer.WriteProvideCommand();
            string inputLine = Console.ReadLine();
            CommandProcess(inputLine);
            Drawer.LogMessage(StaticStartValues.PLAYER_COMMAND_LOG+inputLine);
            switch (Move.CommandType)
            {
                case CommandType.health:
                    TriggerEvent healthCheckEvent = new TriggerEvent
                    {
                        EventType = EventType.HealthCheck,
                        SourceCharacter = CharacterOptions.Player,
                        SourcePlace = new Place(Player.Place.X, Player.Place.Y)
                    };
                    Drawer.WriteHealths(Monsters);
                    EventCollection.InvokePlayerHealthCheck(Player, healthCheckEvent);
                    break;
                case CommandType.move:
                    if (FallingCheck(Player, Move))
                    {
                        Drawer.WriteCommand(PlayerInteractionMessages.PLAYER_FALLING_OFF_BOARD);
                        WrongMove = true;
                        break;
                    }
                    for (int i = 0; i < Monsters.Count; i++)
                    {
                        if (Player.Place.DirectionTo(Monsters.ElementAt(i).Place) == Move.Direction)
                        {
                            Drawer.WriteCommand(PlayerInteractionMessages.PLAYER_BUMP_INTO_MONSTER);
                            Player.Damage(StaticStartValues.BUMPING_INTO_MONSTER_DAMAGE);
                            WrongMove = true;
                            break;
                        }
                    }
                    int trapCounter = 0;
                    foreach (Trap trap in Traps)
                    {
                        if (Player.Place.DirectionTo(trap.Place) == Move.Direction)
                        {
                            trapCounter++;
                            if (trapCounter >= 2)
                            {
                                Drawer.WriteCommand(PlayerInteractionMessages.PLAYER_BUMP_INTO_DOUBLE_TRAP);
                                Player.Damage(StaticStartValues.BUMPING_INTO_DOUBLE_TRAP_DAMAGE);
                                WrongMove = true;
                                break;
                            }
                        }
                    }
                    if (!WrongMove)
                        Player.Move(Move.Direction);
                    break;
                case CommandType.shoot:
                    TriggerEvent shootEvent = new TriggerEvent
                    {
                        EventType = EventType.Shoot,
                        SourceCharacter = CharacterOptions.Player,
                        SourcePlace = new Place(Player.Place.X, Player.Place.Y)
                    };
                    for (int i = 0; i < Monsters.Count; i++)
                    {
                        if (Player.Place.DirectionTo(Monsters.ElementAt(i).Place) == Move.Direction)
                        {
                            Monsters.ElementAt(i).Damage(Player.Type.Damage);
                            shootEvent.TargetCharacterOption = CharacterOptions.Monster;
                            shootEvent.TargetCharacter = Monsters.ElementAt(i);
                            shootEvent.TargetPlace = new Place(Monsters.ElementAt(i).Place.X, Monsters.ElementAt(i).Place.Y);
                        }
                    }
                    EventCollection.InvokeSomeoneShot(Player, shootEvent);
                    break;
                case CommandType.help:
                    WrongMove = true;
                    Drawer.writeHelp();
                    break;
                default:
                    Drawer.WriteCommand(PlayerInteractionMessages.PLAYER_INVALID_COMMAND);
                    WrongMove = true;
                    break;
            }
        }

        public void SpawnMonster(Monster monster)
        {
            Spawned = true;
            this.Monsters.Add(monster);
            if (!this.Board.Monsters.Contains(monster))
                this.Board.Monsters.Add(monster);
        }
        public bool IsOccupiedOrOutOfBounds(Place p)
        {
            int counter = 0;
            if (p.X < 0 || p.X >= Board.Height || p.Y < 0 || p.Y >= Board.Width)
            {
                Drawer.WriteCommand(ErrorMessages.GameError.PLACE_OUT_OF_BOUNDS);
                return true;
            }
            foreach (Character c in Characters)
            {
                if (c.Place.DirectionTo(p) == Directions.COLLISION)
                {
                    if (c.GetCharacterType() is TrapType)
                    {
                        if (counter >= 1)
                            return true;
                        counter++;
                        continue;
                    }
                    return true;
                }
            }
            if (Spawned)
                if (Monsters.ElementAt(Monsters.Count - 1).Place.DirectionTo(p) == Directions.COLLISION)
                    return true;
            return false;
        }

        public Monster GetClosestMonster()
        {
            int smallestDistance = Board.Height + Board.Width;
            Monster closestMonster = null;
            foreach (Monster m in Monsters)
            {
                if (m.Equals(ActualCharacter))
                    continue;
                int xDistance = Math.Abs(m.Place.X - ActualCharacter.Place.X);
                int yDistance = Math.Abs(m.Place.Y - ActualCharacter.Place.Y);
                if (xDistance + yDistance < smallestDistance)
                {
                    smallestDistance = xDistance + yDistance;
                    closestMonster = m;
                }
            }
            return closestMonster;
        }

        public Trap GetClosestTrap()
        {
            int smallestDistance = Board.Height + Board.Width;
            Trap closestTrap = null;
            foreach (Trap m in Traps)
            {
                if (m.Equals(ActualCharacter))
                    continue;
                int xDistance = Math.Abs(m.Place.X - ActualCharacter.Place.X);
                int yDistance = Math.Abs(m.Place.Y - ActualCharacter.Place.Y);
                if (xDistance + yDistance < smallestDistance)
                {
                    smallestDistance = xDistance + yDistance;
                    closestTrap = m;
                }
            }
            return closestTrap;
        }

        private bool IsEventHandlerValid(TriggerEventHandler eventHandler, Character character)
        {
            if (character.GetPartner() == null)
            {
                Drawer.WriteCommand(ErrorMessages.PartnerError.NON_EXISTANT_PARTNER + character.Name);
                return true;
            }
            if (character.GetPartner() is Monster)
            {
                if (eventHandler.TriggeringEvent.SourceCharacter == CharacterOptions.Partner)
                {
                    if (eventHandler.TriggeringEvent.EventType == EventType.Damage)
                    {
                        Drawer.WriteCommand(ErrorMessages.DamageError.ONLY_TRAP_CAN_DAMAGE);
                        return false;
                    }
                    if (eventHandler.TriggeringEvent.EventType == EventType.Heal)
                    {
                        Drawer.WriteCommand(ErrorMessages.HealError.ONLY_TRAP_CAN_HEAL);
                        return false;
                    }
                    if (eventHandler.TriggeringEvent.EventType == EventType.Teleport)
                    {
                        Drawer.WriteCommand(ErrorMessages.TeleportError.ONLY_TRAP_CAN_TELEPORT);
                        return false;
                    }
                    if (eventHandler.TriggeringEvent.EventType == EventType.Spawn)
                    {
                        Drawer.WriteCommand(ErrorMessages.SpawnError.ONLY_TRAP_CAN_SPAWN);
                        return false;
                    }
                }
                if (eventHandler.TriggeringEvent.TargetCharacterOption == CharacterOptions.Partner)
                {
                    if (eventHandler.TriggeringEvent.EventType == EventType.Shoot &&
                        (eventHandler.TriggeringEvent.SourceCharacter != CharacterOptions.Player))
                    {
                        Drawer.WriteCommand(ErrorMessages.EventError.MONSTER_SHOOTING_MONSTER);
                        return false;
                    }
                }
            }
            if (character.GetPartner() is Trap)
            {
                if (eventHandler.TriggeringEvent.SourceCharacter == CharacterOptions.Partner)
                {
                    if (eventHandler.TriggeringEvent.EventType == EventType.Shoot)
                    {
                        Drawer.WriteCommand(ErrorMessages.EventError.ONLY_MONSTER_CAN_SHOOT);
                        return false;
                    }
                    if (eventHandler.TriggeringEvent.EventType == EventType.Die)
                    {
                        Drawer.WriteCommand(ErrorMessages.EventError.TRAPS_DO_NOT_DIE);
                        return false;
                    }
                }
                if (eventHandler.TriggeringEvent.TargetCharacterOption == CharacterOptions.Partner)
                {
                    if (eventHandler.TriggeringEvent.EventType == EventType.Shoot ||
                        eventHandler.TriggeringEvent.EventType == EventType.Damage ||
                        eventHandler.TriggeringEvent.EventType == EventType.Heal)
                    {
                        Drawer.WriteCommand(ErrorMessages.HealthChangeError.CHARACTER_HAS_NO_HEALTH);
                        return false;
                    }
                    if (eventHandler.TriggeringEvent.EventType == EventType.Spawn)
                    {
                        Drawer.WriteCommand(ErrorMessages.EventError.TRAP_SPAWNING_TRAP);
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
