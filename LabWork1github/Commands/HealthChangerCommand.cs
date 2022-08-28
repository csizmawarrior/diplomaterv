using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.Commands
{

    public abstract class HealthChangerCommand : Command
    {

        public static int BASE_HEALTH_CHANGE = 50;

        public int Distance { get; set; } = 1;
        public string Direction { get; set; }
        public Place TargetPlace { get; set; }
        public int HealthChangeAmount { get; set; } = BASE_HEALTH_CHANGE;

        public override abstract void Execute(GameParamProvider provider);
    }
}
