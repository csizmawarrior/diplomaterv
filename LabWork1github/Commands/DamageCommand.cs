using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.Commands
{
    public delegate void DamageDelegate(GameParamProvider provider, DamageCommand command);

    public class DamageCommand : HealthChangerCommand
    {
        public DamageDelegate DamageDelegate { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            DamageDelegate(provider, this);
        }
    }
}
