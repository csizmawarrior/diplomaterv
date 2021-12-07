using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public abstract class Command
    {
        public List<Command> CommandList { get; set; } = new List<Command>();
        public abstract void Execute(GameParamProvider provider);
    }
}
