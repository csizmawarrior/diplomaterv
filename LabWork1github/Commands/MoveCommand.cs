using LabWork1github.static_constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public delegate void MoveDelegate(GameParamProvider provider, MoveCommand command);

    public class MoveCommand : Command
    {

        public int Distance { get; set; }

        public int Round { get; set; }

        public Place TargetPlace { get; set; }
        public string Direction { get; set; }

        public MoveDelegate MoveDelegate { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            MoveDelegate(provider, this);
        }


    }
}
