using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class Enemy
    {
        public EnemyType Type { get; set; }
        public int Health { get; set; }
        public Place Place { get; set; }

        public void Step()
        {
            Type.Step();
        }
    }
}
