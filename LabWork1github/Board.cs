using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1
{
    public class Board
    {
        private int width = 0;
        private int length = 0;

        public Player player { get; set; }

        public List<Monster> Monsters { get; set; }

        public List<Trap> Traps { get; set; }


        public int With
        {
            get
            {
                return width;
            }
            set
            {
                if (width == 0)
                    width = value;
            }
        }
        public int Length
        {
            get
            {
                return length;
            }
            set
            {
                if (length == 0)
                    length = value;
            }
        }
    }
}
