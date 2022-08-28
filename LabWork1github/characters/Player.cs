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
            health = hp;
            Place = p;
        }

        public PlayerType Type { get; } = new PlayerType();

        private int health = 0;

        public override void Damage(int amount)
        {
            health -= amount;
        }

        public override int GetHealth()
        {
            return health;
        }

        public override CharacterType GetCharacterType()
        {
            return Type;
        }

        public override void Heal(int amount)
        {
            health += amount;
            if (health > Program.starterHP)
                health = Program.starterHP;
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

        public override Character GetPartner()
        {
            return null;
        }
    }
}
