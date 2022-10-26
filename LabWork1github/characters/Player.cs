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
        public Player(Place p, double hp)
        {
            health = hp;
            Place = p;
        }

        public PlayerType Type { get; } = new PlayerType();

        private double health = 0;

        public override void Damage(double amount)
        {
            health -= amount;
        }

        public override double GetHealth()
        {
            return health;
        }

        public override CharacterType GetCharacterType()
        {
            return Type;
        }

        public override void Heal(double amount)
        {
            health += amount;
            if (health > StaticStartValues.STARTER_PLAYER_HP)
                health = StaticStartValues.STARTER_PLAYER_HP;
        }

        public void Move(string direction)
        {
            TriggerEvent moveEvent = new TriggerEvent
            {
                EventType = EventType.Move,
                SourceCharacter = CharacterOptions.Player,
                SourcePlace = new Place(Place.X, Place.Y)
            };
            switch (direction)
            {
                case Directions.FORWARD:
                    Place.X -= 1;
                    moveEvent.TargetPlace = new Place(Place.X, Place.Y);
                    break;
                case Directions.BACKWARDS:
                    Place.X += 1;
                    moveEvent.TargetPlace = new Place(Place.X, Place.Y);
                    break;
                case Directions.LEFT:
                    Place.Y -= 1;
                    moveEvent.TargetPlace = new Place(Place.X, Place.Y);
                    break;
                case Directions.RIGHT:
                    Place.Y += 1;
                    moveEvent.TargetPlace = new Place(Place.X, Place.Y);
                    break;
            }
            EventCollection.InvokeSomeoneMoved(this, moveEvent);
        }

        public override Character GetPartner()
        {
            return null;
        }
    }
}
