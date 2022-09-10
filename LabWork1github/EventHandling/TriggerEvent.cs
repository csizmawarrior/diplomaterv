using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.EventHandling
{
    public class TriggerEvent: EventArgs
    {
        public EventType EventType { get; set; }
        public Place TargetPlace { get; set; }
        public Place SourcePlace { get; set; }
        public CharacterType SourceCharacter { get; set; }
        public CharacterType TargetCharacter { get; set; }
        public double Amount { get; set; }

    }   
}
