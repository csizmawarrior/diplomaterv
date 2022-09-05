using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public enum CommandType
    {
        move,
        shoot,
        health,
        help
    }

    public class PlayerMove
    {
        public CommandType CommandType { get; set; }
        public string Direction { get; set; }
    }
}
