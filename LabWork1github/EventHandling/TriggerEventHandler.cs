using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.EventHandling
{
    public class TriggerEventHandler
    {
        public TriggerEvent TriggeringEvent { get; set; }

        public List<Command> Commands { get; set; }
        public GameParamProvider GameParamProvider { get; set; }

        public virtual void OnEvent(object sender, TriggerEvent args)
        {
            if(args.Equals(TriggeringEvent))
                foreach(Command command in Commands)
                {
                    command.Execute(GameParamProvider);
                }
        }
    }
}
