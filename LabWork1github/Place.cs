using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class Place
    {
        public Place(uint x, uint y)
        {
            X = x;
            Y = y;
        }
        
        public uint X { get; set; }
        public uint Y { get; set; }
    }
}
