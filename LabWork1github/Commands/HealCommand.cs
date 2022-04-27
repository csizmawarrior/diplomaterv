using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.Commands
{
    public delegate void HealDelegate(GameParamProvider provider, HealCommand command);

    public class HealCommand : Command
    {
        public int Distance { get; set; } = 1;
        public string Direction { get; set; }
        public Place TargetPlace { get; set; }
        public int Heal { get; set; } = 50;

        public HealDelegate HealDelegate { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            HealDelegate(provider, this);
        }
    }
}
