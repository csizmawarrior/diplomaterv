using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.Commands
{
    public delegate void PlaceParameterDeclareDelegate(GameParamProvider provider, PlaceParameterDeclareCommand command);

    public class PlaceParameterDeclareCommand : HealthChangerCommand
    {
        public Place Place { get; set; }
        public PlaceParameterDeclareDelegate PlaceParameterDeclareDelegate { get; set; }
        public override void Execute(GameParamProvider provider)
        {
            PlaceParameterDeclareDelegate(provider, this);
        }
    }
}
