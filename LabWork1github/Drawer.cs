using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class Drawer
    {
        bool occupied = false;
        bool trapOccupied = false;
        bool found = false;

        public void writeCommand(string message)
        {
            Console.WriteLine(message);
        }
        public void drawBoard(Board board, Player player, List<Monster> monsters, List<Trap> traps)
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
                for(int j = 0; j < board.Width; j++)
                {
                    occupied = false;
                    trapOccupied = false;
                    found = false;

                    foreach (Trap trap in traps)
                    {
                        if (trap.Place.directionTo(new Place((uint)i, (uint)j)) == "collision")
                        {
                            Console.WriteLine(" TTT");
                            trapOccupied = true;
                            break;
                        }
                    }
                    if(!trapOccupied)
                        Console.WriteLine(" ---");
                    if(j == board.Width-1)
                        Console.WriteLine("\n");

                }
                for (int j = 0; j < board.Width; j++)
                {
                    if (trapOccupied)
                    {
                        Console.WriteLine(" T");
                    }
                    else
                        Console.WriteLine(" |");

                    foreach (Place place in places)
                    {
                        if (place.directionTo(new Place((uint)i, (uint)j)) == "collision")
                        {
                            occupied = true;
                            break;
                        }
                    }

                    if (!occupied)
                    {
                        Console.WriteLine("||");
                        if (j == board.Width - 1)
                            Console.WriteLine("\n");
                        continue;
                    }
                    if (player.Place.directionTo(new Place((uint)i, (uint)j)) == "collision")
                        Console.WriteLine("P");
                    else
                    {
                        foreach(Monster monster in monsters)
                        {
                            if (monster.Place.directionTo(new Place((uint)i, (uint)j)) == "collision")
                            {
                                Console.WriteLine("M");
                                found = true;
                                break;
                            }
                        }
                        if(!found)
                            Console.WriteLine("T");
                    }

                    if(trapOccupied)
                        Console.WriteLine("T");
                    else
                        Console.WriteLine("|");
                }

                for(int j = 0; j < board.Width; j++)
                {
                    if (trapOccupied)
                        Console.WriteLine(" TTT");
                    else
                        Console.WriteLine(" |||");

                    if (j == board.Width - 1)
                        Console.WriteLine("\n");
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n");
        }
    }
}
