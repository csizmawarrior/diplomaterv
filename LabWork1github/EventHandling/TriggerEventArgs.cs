using LabWork1github.static_constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.EventHandling
{
    public class TriggerEventArgs: EventArgs
    {
        public EventType EventType { get; set; }
        public Place TargetPlace { get; set; } = null;
        public Place SourcePlace { get; set; } = null;
        public CharacterOptions SourceCharacter { get; set; } = CharacterOptions.NULL;
        public CharacterOptions TargetCharacterOption { get; set; } = CharacterOptions.NULL;
        public Character TargetCharacter { get; set; } = null;
        public double Amount { get; set; } = StaticStartValues.PLACEHOLDER_AMOUNT;

    }   
}
