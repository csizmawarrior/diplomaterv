using LabWork1github.static_constants;
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
                return Directions.COLLISION;
            if (otherPlace.X == X && otherPlace.Y == Y-1)
                return Directions.LEFT;
            if (otherPlace.X == X && otherPlace.Y == Y+1)
                return Directions.RIGHT;
            if (otherPlace.X == X+1 && otherPlace.Y == Y)
                return Directions.BACKWARDS;
            if (otherPlace.X == X-1 && otherPlace.Y == Y)
                return Directions.FORWARD;
            return Directions.AWAY;
        }
        public override bool Equals(object obj)
        {
            return this.DirectionTo((Place)obj) == Directions.COLLISION;
        }
        public override int GetHashCode()
        {
            return X+Y+base.GetHashCode();
        }
    }
}
