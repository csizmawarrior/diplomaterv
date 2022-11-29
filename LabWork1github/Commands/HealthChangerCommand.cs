using LabWork1github.static_constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.Commands
{

    public abstract class HealthChangerCommand : Command
    {
        public int Distance { get; set; } = 1;
        public string Direction { get; set; }
        public Place TargetPlace { get; set; }
        public double HealthChangeAmount { get; set; } = StaticStartValues.BASE_HEALTH_CHANGE;

        public override abstract void Execute(GameParamProvider provider);
    }
}
