﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.EventHandling
{
    public class TriggerEventHandler
    {
        public TriggerEvent TriggeringEvent { get; set; }

        public List<Command> Commands { get; set; } = new List<Command>();
        public GameParamProvider GameParamProvider { get; set; }

        public virtual void OnEvent(object sender, TriggerEvent args)
        {
            if (args.EventType.Equals(TriggeringEvent.EventType) && args.TargetCharacter == TriggeringEvent.TargetCharacter) {
                if ((TriggeringEvent.Amount == 0 || TriggeringEvent.Amount == args.Amount)
                    && (TriggeringEvent.SourcePlace == null || (TriggeringEvent.SourcePlace.X.Equals(args.SourcePlace.X) && TriggeringEvent.SourcePlace.Y.Equals(args.SourcePlace.Y)) )
                    && (TriggeringEvent.SourceCharacter == null || TriggeringEvent.SourceCharacter.Equals(args.SourceCharacter)) )
                {
                    foreach (Command command in Commands)
                    {
                        command.Execute(GameParamProvider);
                    }
                }
            }
        }
    }
}
