using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class Place
    {
        public Place(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public int X { get; set; }
        public int Y { get; set; }
        public string DirectionTo(Place otherPlace)
        {
            if (otherPlace.X == X && otherPlace.Y == Y)
                return "collision";
            if (otherPlace.X == X && otherPlace.Y == Y-1)
                return "L";
            if (otherPlace.X == X && otherPlace.Y == Y+1)
                return "R";
            if (otherPlace.X == X+1 && otherPlace.Y == Y)
                return "B";
            if (otherPlace.X == X-1 && otherPlace.Y == Y)
                return "F";
            return "away";
        }
    }
}
