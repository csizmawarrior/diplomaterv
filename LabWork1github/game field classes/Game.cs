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
        public static int CharacterNameCount = 0;

        public Board Board { get; set; }

        public Player Player { get; set; }

        public List<Monster> Monsters { get; set; }

        public List<Trap> Traps { get; set; }

        public static PlayerMove move = new PlayerMove();

        public static List<Character> Characters = new List<Character>();

        public int Round { get; set; } = 0;

        public bool wrongMove = false;

        public Drawer Drawer;

        public bool spawned = false;

        public Character ActualCharacter { get; set; }

        public int NoExecution { get; set; } = 0;

        public int ImmediateExecution { get; set; } = 0;

        public int Repeat { get; set; } = 0;

        private GameParamProvider Provider;

        public void Init()
        {
            Drawer = Program.Drawer;
            Provider = new GameParamProvider(this);
            Board = Program.Board;
            Monsters = Board.Monsters;
            Characters = Program.Characters;
            Traps = Board.Traps;
            move = new PlayerMove();
            Player = Board.Player;
            foreach (Character character in Characters)
            {
                if(character.GetCharacterType().EventHandlers.Count > 0)
                {
                    foreach (TriggerEventHandler eventHandler in character.GetCharacterType().EventHandlers)
                        eventHandler.GameParamProvider = Provider;
                }
            }
        }
        //hmm these checks might be able to go into the board visitor

        public void Start()
        {
            Drawer.DrawBoard(Board, Player, Monsters, Traps);
            while (Player.GetHealth() > 0 && Monsters.Count > 0)
            {
                Round++;
                Step();
            }
            if (Player.GetHealth() <= 0)
            {
                TriggerEvent dieEvent = new TriggerEvent
                {
                    EventType = EventType.Die,
                    SourceCharacter = new PlayerType(),
                    SourcePlace = Player.Place
                };
                EventCollection.InvokePlayerDied(Player, dieEvent);
                Drawer.WriteCommand(PlayerInteractionMessages.YOU_LOST);
            }
            else
                Drawer.WriteCommand(PlayerInteractionMessages.YOU_WON);
        }

        public void Step()
        {
            wrongMove = false;
            move = new PlayerMove();
            spawned = false;

            foreach (Character character in Characters)
            {
                ActualCharacter = character;
                character.GetCharacterType().Step(Provider);
            }

            if (spawned)
                Characters.Add(Monsters.ElementAt(Monsters.Count - 1));
            if (wrongMove)
                return;


            foreach (Monster monster in Monsters)
            {
                if (monster.Health <= 0)
                {
                    TriggerEvent dieEvent = new TriggerEvent
                    {
                        EventType = EventType.Die,
                        SourceCharacter = new MonsterType(),
                        SourcePlace = monster.Place
                    };
                    Monsters.Remove(monster);
                    Characters.Remove(monster);
                    EventCollection.InvokeMonsterDied(monster, dieEvent);
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

        public void PlayerCommand()
        {
            Drawer.WriteCommand(PlayerInteractionMessages.PROVIDE_A_COMMAND);
            string inputLine = Console.ReadLine();
            CommandProcess(inputLine);
            switch (move.CommandType)
            {
                case CommandType.health:
                    TriggerEvent healthCheckEvent = new TriggerEvent
                    {
                        EventType = EventType.HealthCheck,
                        SourceCharacter = new PlayerType(),
                        SourcePlace = Player.Place
                    };
                    Drawer.WriteHealths(Monsters);
                    EventCollection.InvokePlayerHealthCheck(Player, healthCheckEvent);
                    break;
                case CommandType.move:
                    if (FallingCheck(Player, move))
                    {
                        Drawer.WriteCommand(PlayerInteractionMessages.PLAYER_FALLING_OFF_BOARD);
                        wrongMove = true;
                        break;
                    }
                    for (int i = 0; i < Monsters.Count; i++)
                    {
                        if (Player.Place.DirectionTo(Monsters.ElementAt(i).Place) == move.Direction)
                        {
                            Drawer.WriteCommand(PlayerInteractionMessages.PLAYER_BUMP_INTO_MONSTER);
                            Player.Damage(25);
                            wrongMove = true;
                            break;
                        }
                    }
                    if (!wrongMove)
                        Player.Move(move.Direction);
                    break;
                case CommandType.shoot:
                    TriggerEvent shootEvent = new TriggerEvent
                    {
                        EventType = EventType.Shoot,
                        SourceCharacter = new PlayerType(),
                        SourcePlace = Player.Place
                    };
                    for (int i = 0; i < Monsters.Count; i++)
                    {
                        if (Player.Place.DirectionTo(Monsters.ElementAt(i).Place) == move.Direction)
                        {
                            Monsters.ElementAt(i).Damage(Player.Type.Damage);
                            shootEvent.TargetCharacter = new MonsterType();
                            shootEvent.TargetPlace = Monsters.ElementAt(i).Place;
                        }
                    }
                    EventCollection.InvokePlayerShot(Player, shootEvent);
                    break;
                case CommandType.help:
                    wrongMove = true;
                    Drawer.writeHelp();
                    break;
                default:
                    Drawer.WriteCommand(PlayerInteractionMessages.PLAYER_INVALID_COMMAND);
                    wrongMove = true;
                    break;
            }
        }

        public void SpawnMonster(Monster monster)
        {
            if (monster.Place.X >= 0 && monster.Place.X < Board.Height && monster.Place.Y >= 0 && monster.Place.Y < Board.Width)
            {
                spawned = true;
                this.Monsters.Add(monster);
                if(!this.Board.Monsters.Contains(monster))
                    this.Board.Monsters.Add(monster);
            }
            else
                Drawer.WriteCommand(ErrorMessages.GameError.CHARACTER_SPAWNED_OUT_OF_BOUNDS);
        }
        public bool IsOccupiedOrOutOfBounds(Place p)
        {
            int counter = 0;
            if(p.X < 0 || p.X >= Board.Height || p.Y < 0 || p.Y >= Board.Width)
            {
                Drawer.WriteCommand(ErrorMessages.GameError.PLACE_OUT_OF_BOUNDS);
                return true;
            }
            foreach(Character c in Characters)
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
            if (spawned)
                if (Monsters.ElementAt(Monsters.Count - 1).Place.DirectionTo(p) == Directions.COLLISION)
                    return true;
            return false;
        }

        public Monster GetClosestMonster()
        {
            int smallestDistance = Board.Height+Board.Width;
            Monster closestMonster = null;
            int xDistance = smallestDistance;
            int yDistance = smallestDistance;
            foreach(Monster m in Monsters)
            {
                if (m.Equals(ActualCharacter))
                    continue;
                if (m.Place.X > ActualCharacter.Place.X)
                    xDistance = m.Place.X - ActualCharacter.Place.X;
                else
                    xDistance = ActualCharacter.Place.X - m.Place.X;
                if (m.Place.Y > ActualCharacter.Place.Y)
                    yDistance = m.Place.Y - ActualCharacter.Place.Y;
                else
                    yDistance = ActualCharacter.Place.Y - m.Place.Y;
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
            int xDistance = smallestDistance;
            int yDistance = smallestDistance;
            foreach (Trap m in Traps)
            {
                if (m.Equals(ActualCharacter))
                    continue;
                if (m.Place.X > ActualCharacter.Place.X)
                    xDistance = m.Place.X - ActualCharacter.Place.X;
                else
                    xDistance = ActualCharacter.Place.X - m.Place.X;
                if (m.Place.Y > ActualCharacter.Place.Y)
                    yDistance = m.Place.Y - ActualCharacter.Place.Y;
                else
                    yDistance = ActualCharacter.Place.Y - m.Place.Y;
                if (xDistance + yDistance < smallestDistance)
                {
                    smallestDistance = xDistance + yDistance;
                    closestTrap = m;
                }
            }
            return closestTrap;
        }
    }
}
