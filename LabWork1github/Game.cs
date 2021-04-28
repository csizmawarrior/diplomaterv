using Antlr4.Runtime;
using LabWork1github;
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

        private MonsterAI monsterAI;

        private TrapAI trapAI;

        public static PlayerMove move = new PlayerMove();

        private int round = 0;

        public bool wrongMove = false;

        public Drawer drawer;

        public void Start()
        {
            drawer.drawBoard(Board, Player, Monsters, Traps);
            while (Player.Health > 0 && Monsters.Count > 0)
            {

                if(!wrongMove)
                    round++;
                Step();
            }
            drawer.writeCommand("You died!");
        }

        public void Step()
        {

                wrongMove = false;
                drawer.writeCommand("Give a command!");
                string inputLine = Console.ReadLine();
                commandProcess(inputLine);
                switch (move.CommandType)
                {
                    case CommandType.health:
                        drawer.writeCommand("The payer's health is: " + Player.Health);
                        break;
                    case CommandType.move:
                        if (fallingCheck(Player, move))
                        {
                            drawer.writeCommand("Invalid move, falling off the board, try again!");
                            wrongMove = true;
                            break;
                        }
                        for (int i = 0; i < Monsters.Count; i++)
                        {
                            if (Monsters.ElementAt(i).Place.directionTo(Player.Place) == "collision") { 
                                drawer.writeCommand("Invalid move, bumping into Monster, you damaged yourself, try again!");
                                Player.Damage(25);
                                wrongMove = true;
                                break;
                            }
                        }
                        Player.Move(move.Direction);
                        break;
                    case CommandType.shoot:
                        switch (move.Direction)
                        {
                            case "F":
                                for(int i = 0; i < Monsters.Count; i++)
                                {
                                    if (Monsters.ElementAt(i).Place.directionTo(Player.Place) == move.Direction) {
                                        Monsters.ElementAt(i).Damage(50);
                                    }
                                }
                                break;
                        }
                        break;
                    default:
                        drawer.writeCommand("Invalid command! Try again!");
                        wrongMove = true;
                        break;
                }
                if (wrongMove)
                    return;

                trapAI.Step(round, Player, Traps);

                foreach(Monster monster in Monsters)
                {
                    if (monster.Health <= 0)
                        Monsters.Remove(monster);
                }

                monsterAI.Step(round, Player, Monsters);
                if (trapAI.Spawning)
                    Monsters.Add(new Monster(Program.starterHP, Program.monsterTypes.ElementAt(0), trapAI.spawnPoint));

            drawer.drawBoard(Board, Player, Monsters, Traps);

            //TODO: draw things out, and write things out

            }

        public void Init()
            {
                monsterAI = new MonsterAI();
                trapAI = new TrapAI();
                drawer = new Drawer();
                Board = Program.Board;
                Monsters = Board.Monsters;
                Traps = Board.Traps;
                Player = Board.Player;
                foreach (Monster monster in Monsters)
                {
                    if (monster.Place.X > Board.Width || monster.Place.Y > Board.Height)
                        Monsters.Remove(monster);
                }
                foreach (Trap Trap in Traps)
                {
                    if (Trap.Place.X > Board.Width || Trap.Place.Y > Board.Height)
                        Traps.Remove(Trap);
                    if (Trap.Type.EffectPlace != null)
                    {
                        if (Trap.Type.EffectPlace.X > Board.Width || Trap.Type.EffectPlace.Y > Board.Height)
                            Traps.Remove(Trap);
                    }
                }
                if (Player.Place.X > Board.Width || Player.Place.Y > Board.Height)
                    throw new NullReferenceException("Player is not on the board");
            }

            private void commandProcess(string inputCommand)
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
                if (Player.Place.X == 0 && move.Direction == "L")
                    return true;
                if (Player.Place.X == Board.Width - 1 && move.Direction == "R")
                    return true;
                if (Player.Place.Y == 0 && move.Direction == "F")
                    return true;
                if (Player.Place.Y == Board.Height - 1 && move.Direction == "B")
                    return true;
                return false;
        }
    }
}
