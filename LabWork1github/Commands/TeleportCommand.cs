using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public delegate void TeleportDelegate(GameParamProvider provider, TeleportCommand command);

    public class TeleportCommand : Command
    {
        public Place TargetPlace { get; set; } = new Place(-1, -1);

        public int Round { get; set; }

        public bool Random { get; set; } 

        public Character Character { get; set; }

        public TeleportDelegate TeleportDelegate { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            TeleportDelegate(provider, this);
        }
    }
}
