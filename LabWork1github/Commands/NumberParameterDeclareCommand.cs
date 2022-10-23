using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.Commands
{
    public delegate void NumberParameterDeclareDelegate(GameParamProvider provider, NumberParameterDeclareCommand command);

    public class NumberParameterDeclareCommand : HealthChangerCommand
    {
        public double Number { get; set; }

        public NumberParameterDeclareDelegate NumberParameterDeclareDelegate { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            NumberParameterDeclareDelegate(provider, this);
        }
    }
}
