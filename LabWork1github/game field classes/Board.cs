using LabWork1github.static_constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class Board
    {
        private int width = StaticStartValues.PLACEHOLDER_WIDTH;
        private int height = StaticStartValues.PLACEHOLDER_HEIGHT;

        public Player Player { get; set; }

        public string Name { get; set; }

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
                if (value == 0)
                    throw new NullReferenceException(ErrorMessages.BoardError.ZERO_WIDTH);
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
                    throw new NullReferenceException(ErrorMessages.BoardError.ZERO_HEIGHT);
            }
        }
    }
}
