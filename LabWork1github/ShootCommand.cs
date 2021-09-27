using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class ShootCommand
    {
        public int Distance { get; set; } = 1;
        private string _direction;
        public int Round { get; set; }

        public string Direction
        {
            get { return _direction; }
            set
            {
                if (!value.Equals("F") && !value.Equals("L") && value.Equals("B") && value.Equals("R"))
                    throw new ArgumentException("wrong direction given");
                _direction = value;
            }
        }
        public Place targetPlace { get; set; }
        private int _damage;

        public int Damage
        {
            get { return _damage; }
            set {
                if(value < 0)
                    throw new ArgumentException("Negative damage given!");
                _damage = value;
            }
        }


        public void Shoot() { }
    }
}
