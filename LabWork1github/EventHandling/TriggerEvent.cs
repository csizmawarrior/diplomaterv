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
        public Place TargetPlace { get; set; } = null;
        public Place SourcePlace { get; set; } = null;
        public CharacterType SourceCharacter { get; set; } = null;
        public CharacterType TargetCharacter { get; set; } = null;
        public double Amount { get; set; } = 0;

    }   
}
