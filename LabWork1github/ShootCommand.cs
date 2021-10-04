using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public delegate void ShootDelegate(GameParamProvider provider, ShootCommand command);

    public class ShootCommand : Command
    {
        public int Distance { get; set; } = 1;
        public string Direction { get; set; }
        public int Round { get; set; }
        public Place targetPlace { get; set; }
        public int Damage { get; set; } = 50;

        public ShootDelegate ShootDelegate { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            ShootDelegate(provider, this);
        }

    }
}
