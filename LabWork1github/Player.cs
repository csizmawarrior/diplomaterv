using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class Player : Character
    {
        public Player(Place p, int hp)
        {
            Health = hp;
            Place = p;
        }

        public PlayerType Type { get; } = new PlayerType();

        public int Health { get; set; }

        public override void Damage(int amount)
        {
            Health -= amount;
        }

        public override int GetHealth()
        {
            throw new NotImplementedException();
        }

        public override CharacterType GetType()
        {
            return Type;
        }

        public override void Heal(int amount)
        {
            Health += amount;
            if (Health > Program.starterHP)
                Health = Program.starterHP;
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
