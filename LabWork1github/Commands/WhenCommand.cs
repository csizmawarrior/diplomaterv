using LabWork1github.EventHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.Commands
{
    public delegate void WhenDelegate(GameParamProvider provider, WhenCommand command);

    public class WhenCommand : Command
    {
        public WhenDelegate WhenDelegate { get; set; }
        public TriggerEventHandler TriggerEventHandler { get; set; }
        public override void Execute(GameParamProvider provider)
        {
            foreach (Command command in CommandList)
                TriggerEventHandler.Commands.Add(command);
        }
    }
}
