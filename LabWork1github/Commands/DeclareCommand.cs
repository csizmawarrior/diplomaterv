using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.Commands
{
    public delegate void DeclareDelegate(GameParamProvider provider, MoveCommand command);

    class DeclareCommand : Command
    {
        public DeclareDelegate DeclareDelegate { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            throw new NotImplementedException();
        }
    }
}
