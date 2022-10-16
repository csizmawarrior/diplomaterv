using LabWork1github.EventHandling;
using LabWork1github.static_constants;
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
            TriggerEvent moveEvent = new TriggerEvent
            {
                EventType = EventType.Move,
                SourceCharacter = new PlayerType(),
                SourcePlace = Place
            };
            switch (direction)
            {
                case Directions.FORWARD:
                    Place.X -= 1;
                    moveEvent.TargetPlace = Place;
                    break;
                case Directions.BACKWARDS:
                    Place.X += 1;
                    moveEvent.TargetPlace = Place;
                    break;
                case Directions.LEFT:
                    Place.Y -= 1;
                    moveEvent.TargetPlace = Place;
                    break;
                case Directions.RIGHT:
                    Place.Y += 1;
                    moveEvent.TargetPlace = Place;
                    break;
            }
            EventCollection.InvokePlayerMoved(this, moveEvent);
        }

        public override Character GetPartner()
        {
            return null;
        }
    }
}
