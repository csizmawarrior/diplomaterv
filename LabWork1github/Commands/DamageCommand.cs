using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.Commands
{
    public delegate void DamageDelegate(GameParamProvider provider, DamageCommand command);

    public class DamageCommand : Command
    {
        public int Distance { get; set; } = 1;
        public string Direction { get; set; }
        public Place TargetPlace { get; set; }
        public int Damage { get; set; } = 50;

        public DamageDelegate DamageDelegate { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            DamageDelegate(provider, this);
        }
    }
}
