using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.Commands
{
    public delegate void HealDelegate(GameParamProvider provider, HealCommand command);

    public class HealCommand : HealthChangerCommand
    {

        public HealDelegate HealDelegate { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            HealDelegate(provider, this);
        }
    }
}
