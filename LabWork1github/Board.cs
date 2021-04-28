using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class Board
    {
        private int width = 0;
        private int height = 0;

        public Player Player { get; set; }

        public List<Monster> Monsters { get; set; } = new List<Monster>();
        

        public List<Trap> Traps { get; set; } = new List<Trap>();

        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                if (width == 0)
                    width = value;
                if (value < 0)
                    throw new NullReferenceException("negative width");
            }
        }
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                if (height == 0)
                    height = value;
                if (value < 0)
                    throw new NullReferenceException("negative width");
            }
        }
    }
}
