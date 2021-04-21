using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class Player
    {
        public Player(Place p, int hp)
        {
            Health = hp;
            Place = p;
        }
        
        public int Health { get; set; }
        public Place Place { get; set; }

        public void Damage(int amount)
        {
            Health -= amount;
        }
        public void Heal(int amount)
        {
            Health += amount;
            if (Health > Program.starterHP)
                Health = Program.starterHP;
        }
        public void Teleport(Place newPlace)
        {
            Place = newPlace;
        }
        public void Move(string direction)
        {
            switch (direction)
            {
                case "F":
                    Place.X -= 1;
                    break;
                case "B":
                    Place.X += 1;
                    break;
                case "L":
                    Place.Y -= 1;
                    break;
                case "R":
                    Place.Y += 1;
                    break;
            }
        }
    }
}
