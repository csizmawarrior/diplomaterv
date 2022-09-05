using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.EventHandling
{
    public class Event: EventArgs
    {
        public EventType EventType { get; set; }
        public Place TargetPlace { get; set; }
        public Place SourcePlace { get; set; }
        public Character SourceCharacter { get; set; }
        public Character TargetCharacter { get; set; }
        public int Amount { get; set; }

    }   
}
