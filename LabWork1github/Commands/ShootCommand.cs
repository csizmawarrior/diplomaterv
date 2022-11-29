using LabWork1github.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public delegate void ShootDelegate(GameParamProvider provider, ShootCommand command);

    public class ShootCommand : HealthChangerCommand
    {
        public ShootDelegate ShootDelegate { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            ShootDelegate(provider, this);
        }

    }
}
