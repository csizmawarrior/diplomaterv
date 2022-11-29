using LabWork1github.static_constants;
using System;
using System.Collections.Generic;
using System.IO;
namespace LabWork1github
{
    public class Drawer
    {
        bool Occupied { get; set; } = false;
        bool TrapOccupied { get; set; } = false;
        bool found = false;
        public static void LogMessage(string message)
        {
            try
            {
                TextWriter tsw = new StreamWriter(FileNames.ERROR_LOG_FILE_ADDRESS, true);
                tsw.WriteLine(message);
                tsw.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void WriteCommand(string message)
        {
            LogMessage(message);
            Console.WriteLine(PlayerInteractionMessages.COMMAND_SEPARATOR);
            Console.WriteLine(message+"\n");
            Console.WriteLine(PlayerInteractionMessages.COMMAND_SEPARATOR);
        }

        public void WriteHealths(List<Monster> monsters)
        {
            foreach(Monster monster in monsters)
            {
                Console.WriteLine($"Health of Monster at {monster.Place.X+1},{monster.Place.Y+1}: {monster.Health}");
            }
        }
        public void DrawBoard(Board board, Player player, List<Monster> monsters, List<Trap> traps)
        {
            Console.WriteLine(PlayerInteractionMessages.COMMAND_SEPARATOR);

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
                    Occupied = false;
                    TrapOccupied = false;
                    found = false;

                    foreach (Trap trap in traps)
                    {
                        if (trap.Place.DirectionTo(new Place(i, j)) == Directions.COLLISION)
                        {
                            Console.Write(" TTT");
                            TrapOccupied = true;
                            break;
                        }
                    }
                    if(!TrapOccupied)
                        Console.Write(" |||");
                    if(j == board.Width-1)
                        Console.Write("\n");

                }
                //Middle line

                for (int j = 0; j < board.Width; j++)
                {
                    Occupied = false;
                    TrapOccupied = false;
                    found = false;

                    foreach (Trap trap in traps)
                    {
                        if (trap.Place.DirectionTo(new Place(i, j)) == Directions.COLLISION)
                        {
                            TrapOccupied = true;
                            break;
                        }
                    }

                    if (TrapOccupied)
                    {
                        Console.Write(" T");
                    }
                    else
                        Console.Write(" |");

                    foreach (Place place in places)
                    {
                        if (place.DirectionTo(new Place(i, j)) == Directions.COLLISION)
                        {
                            Occupied = true;
                            break;
                        }
                    }

                    if (!Occupied)
                    {
                        Console.Write("||");
                        if (j == board.Width - 1)
                            Console.Write("\n");
                        continue;
                    }
                    if (player.Place.DirectionTo(new Place(i, j)) == Directions.COLLISION)
                        Console.Write("P");
                    else
                    {
                        foreach(Monster monster in monsters)
                        {
                            if (monster.Place.DirectionTo(new Place(i, j)) == Directions.COLLISION)
                            {
                                Console.Write("M");
                                found = true;
                                break;
                            }
                        }
                        int counter = 0;
                        foreach (Trap trap in traps)
                        {
                            if (trap.Place.DirectionTo(new Place(i, j)) == Directions.COLLISION)
                            {
                                counter++;
                                if (counter == 2)
                                {
                                    Console.Write("T");
                                    found = true;
                                    break;
                                }
                            }
                        }
                        if (!found)
                            Console.Write(" ");
                    }

                    if(TrapOccupied)
                        Console.Write("T");
                    else
                        Console.Write("|");
                    if(j==board.Width-1)
                        Console.Write("\n");
                }
                //Third line

                for(int j = 0; j < board.Width; j++)
                {
                    TrapOccupied = false;

                    foreach (Trap trap in traps)
                    {
                        if (trap.Place.DirectionTo(new Place(i, j)) == Directions.COLLISION)
                        {
                            TrapOccupied = true;
                            break;
                        }
                    }

                    if (TrapOccupied)
                        Console.Write(" TTT");
                    else
                        Console.Write(" |||");

                    if (j == board.Width - 1)
                        Console.Write("\n");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
            Console.WriteLine(PlayerInteractionMessages.PLAYER_HEALTH_MESSAGE+player.GetHealth());
            Console.WriteLine(PlayerInteractionMessages.PLAYER_DAMAGE_MESSAGE+player.GetCharacterType().Damage);
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

        internal void WriteProvideCommand()
        {
            Console.WriteLine(PlayerInteractionMessages.COMMAND_SEPARATOR);
            Console.WriteLine(PlayerInteractionMessages.PROVIDE_A_COMMAND);
            Console.WriteLine(PlayerInteractionMessages.COMMAND_SEPARATOR);
        }
    }
}
