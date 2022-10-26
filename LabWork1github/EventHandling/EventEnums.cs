using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.EventHandling
{
    public enum EventType
    { 
        Move,
        Shoot,
        Damage,
        Heal,
        Teleport,
        Spawn,
        HealthCheck,
        Die
    }
    public enum CharacterOptions
    {
        NULL,
        Me,
        Monster,
        Partner,
        Player,
        Trap
    }
}
