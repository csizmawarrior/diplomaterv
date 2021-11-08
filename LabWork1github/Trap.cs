using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class Trap : Character
    {
        public Trap(TrapType type, Place place)
        {
            Type = type;
            Place = place;
        }
        public TrapType Type { get; private set; }
        public Place Place { get; set; }
    }
}
