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
        public string directionTo(Place otherPlace)
        {
            if (otherPlace.X == X && otherPlace.Y == Y)
                return "collision";
            if (otherPlace.X == X && otherPlace.Y == Y-1)
                return "L";
            if (otherPlace.X == X && otherPlace.Y == Y+1)
                return "R";
            if (otherPlace.X == X-1 && otherPlace.Y == Y)
                return "B";
            if (otherPlace.X == X+1 && otherPlace.Y == Y)
                return "F";
            return "away";
        }
    }
}
