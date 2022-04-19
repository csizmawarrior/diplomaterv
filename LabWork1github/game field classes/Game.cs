﻿using Antlr4.Runtime;
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

        public static PlayerMove move = new PlayerMove();

        public static List<Character> Characters = new List<Character>();

        public int Round { get; set; } = 0;

        public bool wrongMove = false;

        public Drawer Drawer;

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
            Traps = Board.Traps;
            move = new PlayerMove();
            Player = Board.Player;
            foreach (Monster monster in Monsters)
            {
                if (monster.Place.X > Board.Height || monster.Place.Y > Board.Width)
                {
                    Monsters.Remove(monster);
                    Drawer.WriteCommand("A monster was out of bounds, so it got deleted");
                }
            }
            foreach (Trap Trap in Traps)
            {
                if (Trap.Place.X > Board.Height || Trap.Place.Y > Board.Width)
                {
                    Traps.Remove(Trap);
                    Drawer.WriteCommand("A trap was out of bounds, so it got deleted");
                }

                foreach (Monster monster in Monsters)
                {
                    if (monster.Place.DirectionTo(Player.Place) == "collision")
                        throw new NullReferenceException("Player is on the same spot as a monster.");
                    if (monster.Place.DirectionTo(Trap.Place) == "collision")
                        throw new NullReferenceException("Monster spawned on a trap");
                }
                if (Trap.Place.DirectionTo(Player.Place) == "collision")
                    throw new NullReferenceException("Player spawned on a trap");
            }
            if (Player.Place.X > Board.Height || Player.Place.Y > Board.Width)
                throw new NullReferenceException("Player is not on the board");
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
                if (trap.Place.DirectionTo(spawnPoint) == "collision")
                    return false;
            }
            foreach (Monster monster in Monsters)
            {
                if (monster.Place.DirectionTo(spawnPoint) == "collision")
                    return false;
            }
            if (Player.Place.DirectionTo(spawnPoint) == "collision")
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
                if (Player.Place.Y == 0 && move.Direction == "L")
                    return true;
                if (Player.Place.Y == Board.Width - 1 && move.Direction == "R")
                    return true;
                if (Player.Place.X == 0 && move.Direction == "F")
                    return true;
                if (Player.Place.X == Board.Height - 1 && move.Direction == "B")
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
                if (m.Place.DirectionTo(p) == "collision")
                    return true;
            }
            foreach (Trap t in Traps)
            {
                if (t.Place.DirectionTo(p) == "collision")
                    return true;
            }
            if (Player.Place.DirectionTo(p) == "collision")
                return true;
            return false;
        }
    }
}
