using Antlr4.Runtime;
using LabWork1github;
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

        public Character ActualCharacter { get; set; }

        public Monster ActualMonster { get; set; }

        public Trap ActualTrap { get; set; }

        public int NoExecution { get; set; } = 0;

        public int ImmediateExecution { get; set; } = 0;

        public int Repeat { get; set; } = 0;

        private GameParamProvider Provider;

        public void Init()
        {
            Drawer = new Drawer();
            Provider = new GameParamProvider(this);
            Board = Program.Board;
            Monsters = Board.Monsters;
            Characters = Program.Characters;
            Traps = Board.Traps;
            move = new PlayerMove();
            Player = Board.Player;
            foreach (Character character in Characters)
            {
                if (character.Place.X > Board.Height || character.Place.Y > Board.Width)
                {
                    Characters.Remove(character);
                    Drawer.WriteCommand("A character was out of bounds, so it got deleted");
                }
                if (!character.PartnerName.Equals(""))
                {
                    foreach(Character m in Characters)
                    {
                        if (m.Name.Equals(character.PartnerName))
                        {
                            character.Partner = m;
                            break;
                        }
                    }
                    if (character.Partner == null)
                        Drawer.WriteCommand($"Character named {character.Name}'s partner doesn't exist, give an existing name.");
                }
            
            foreach (Trap Trap in Traps)
            {
                {
                    if (character.Place.DirectionTo(Player.Place) == Directions.COLLISION && !(character is Player))
                        throw new NullReferenceException("Player is on the same spot as a character.");
                    if (character.Place.DirectionTo(Trap.Place) == Directions.COLLISION && character != Trap)
                        throw new NullReferenceException("Character named " +character.Name +" spawned on a trap");
                }
                if (Trap.Place.DirectionTo(Player.Place) == Directions.COLLISION)
                    throw new NullReferenceException("Player spawned on a trap");
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
                Drawer.WriteCommand("You died!");
            else
                Drawer.WriteCommand("You WON!");
        }

        public void Step()
        {
            wrongMove = false;
            move = new PlayerMove();

            foreach (Character character in Characters)
            {
                ActualCharacter = character;
                character.GetCharacterType().Step(Provider);
            }

            if (wrongMove)
                return;


            foreach (Monster monster in Monsters)
            {
                if (monster.Health <= 0)
                {
                    Monsters.Remove(monster);
                    break;
                }
            }
            Drawer.DrawBoard(Board, Player, Monsters, Traps);
        }

        private bool CheckSpawn(Place spawnPoint)
        {
            foreach(Trap trap in Traps)
            {
                if (trap.Place.DirectionTo(spawnPoint) == Directions.COLLISION)
                    return false;
            }
            foreach (Monster monster in Monsters)
            {
                if (monster.Place.DirectionTo(spawnPoint) == Directions.COLLISION)
                    return false;
            }
            if (Player.Place.DirectionTo(spawnPoint) == Directions.COLLISION)
                return false;
            return true;
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

        private bool fallingCheck(Player player, PlayerMove move)
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
            Drawer.WriteCommand("Give a command!");
            string inputLine = Console.ReadLine();
            CommandProcess(inputLine);
            switch (move.CommandType)
            {
                case CommandType.health:
                    Drawer.WriteCommand("The payer's health is: " + Player.GetHealth());
                    break;
                case CommandType.move:
                    if (fallingCheck(Player, move))
                    {
                        Drawer.WriteCommand("Invalid move, falling off the board, try again next turn!");
                        wrongMove = true;
                        break;
                    }
                    for (int i = 0; i < Monsters.Count; i++)
                    {
                        if (Player.Place.DirectionTo(Monsters.ElementAt(i).Place) == move.Direction)
                        {
                            Drawer.WriteCommand("Invalid move, bumping into Monster, you damaged yourself, try again next turn!");
                            Player.Damage(25);
                            wrongMove = true;
                            break;
                        }
                    }
                    if (!wrongMove)
                        //TODO: ask if this is good, or should the game move the player
                        Player.Move(move.Direction);
                    break;
                case CommandType.shoot:
                    for (int i = 0; i < Monsters.Count; i++)
                    {
                        if (Player.Place.DirectionTo(Monsters.ElementAt(i).Place) == move.Direction)
                        {
                            Monsters.ElementAt(i).Damage(Player.Type.Damage);
                        }
                    }
                    break;
                case CommandType.help:
                    wrongMove = true;
                    Drawer.writeHelp();
                    break;
                default:
                    Drawer.WriteCommand("Invalid command! Try again! Try the help command");
                    wrongMove = true;
                    break;
            }
        }

        public bool IsOccupied(Place p)
        {
            foreach(Monster m in Monsters)
            {
                if (m.Place.DirectionTo(p) == Directions.COLLISION)
                    return true;
            }
            foreach (Trap t in Traps)
            {
                if (t.Place.DirectionTo(p) == Directions.COLLISION)
                    return true;
            }
            if (Player.Place.DirectionTo(p) == Directions.COLLISION)
                return true;
            return false;
        }
    }
}
