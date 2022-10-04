using LabWork1github.static_constants;
using System;
using System.Collections.Generic;
using System.IO;
namespace LabWork1github
{
    public class Drawer
    {
        bool occupied = false;
        bool trapOccupied = false;
        bool found = false;
        public static void LogMessage(string message)
        {
            try
            {
                File.WriteAllText("error_log.txt", message + "\n");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void WriteCommand(string message)
        {
            LogMessage(message);
            Console.WriteLine("--------------------------------\n");
            Console.WriteLine(message+"\n");
            Console.WriteLine("--------------------------------\n");
        }
        public void DrawBoard(Board board, Player player, List<Monster> monsters, List<Trap> traps)
        {
            List<Place> places = new List<Place>();
            places.Add(player.Place);
            foreach(Monster monster in monsters)
            {
                places.Add(monster.Place);
            }
            foreach(Trap trap in traps)
            {
                places.Add(trap.Place);
            }
            for(int i = 0; i < board.Height; i++)
            {
                //First Line

                for(int j = 0; j < board.Width; j++)
                {
                    occupied = false;
                    trapOccupied = false;
                    found = false;

                    foreach (Trap trap in traps)
                    {
                        if (trap.Place.DirectionTo(new Place((int)i, (int)j)) == Directions.COLLISION)
                        {
                            Console.Write(" TTT");
                            trapOccupied = true;
                            break;
                        }
                    }
                    if(!trapOccupied)
                        Console.Write(" |||");
                    if(j == board.Width-1)
                        Console.Write("\n");

                }
                //Middle line

                for (int j = 0; j < board.Width; j++)
                {
                    occupied = false;
                    trapOccupied = false;
                    found = false;

                    foreach (Trap trap in traps)
                    {
                        if (trap.Place.DirectionTo(new Place((int)i, (int)j)) == Directions.COLLISION)
                        {
                            trapOccupied = true;
                            break;
                        }
                    }

                    if (trapOccupied)
                    {
                        Console.Write(" T");
                    }
                    else
                        Console.Write(" |");

                    foreach (Place place in places)
                    {
                        if (place.DirectionTo(new Place((int)i, (int)j)) == Directions.COLLISION)
                        {
                            occupied = true;
                            break;
                        }
                    }

                    if (!occupied)
                    {
                        Console.Write("||");
                        if (j == board.Width - 1)
                            Console.Write("\n");
                        continue;
                    }
                    if (player.Place.DirectionTo(new Place((int)i, (int)j)) == Directions.COLLISION)
                        Console.Write("P");
                    else
                    {
                        foreach(Monster monster in monsters)
                        {
                            if (monster.Place.DirectionTo(new Place((int)i, (int)j)) == Directions.COLLISION)
                            {
                                Console.Write("M");
                                found = true;
                                break;
                            }
                        }
                        if(!found)
                            Console.Write("T");
                    }

                    if(trapOccupied)
                        Console.Write("T");
                    else
                        Console.Write("|");
                    if(j==board.Width-1)
                        Console.Write("\n");
                }
                //Third line

                for(int j = 0; j < board.Width; j++)
                {
                    trapOccupied = false;

                    foreach (Trap trap in traps)
                    {
                        if (trap.Place.DirectionTo(new Place((int)i, (int)j)) == Directions.COLLISION)
                        {
                            trapOccupied = true;
                            break;
                        }
                    }

                    if (trapOccupied)
                        Console.Write(" TTT");
                    else
                        Console.Write(" |||");

                    if (j == board.Width - 1)
                        Console.Write("\n");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        internal void writeHelp()
        {
            Console.WriteLine("Guide for the game: ");
            Console.WriteLine("The available commands as a player: ");
            Console.WriteLine("shoot <Direction>: shoots 1 block into that direction, damaging the monster on that block, if it stands there.");
            Console.WriteLine("move <Direction>: moves 1 block into that direction, if a monster blocks the way, the move damages you.");
            Console.WriteLine("health: the game writes our your health to the screen.");
            Console.WriteLine("help: The guide for the game gets displayed to the screen.");
            Console.WriteLine("Available Directions: F B L R as forward, backwards, left and right");
            Console.WriteLine("Kill all monsters to win. If you die you lose.");
        }
    }
}
