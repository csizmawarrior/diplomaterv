using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.EventHandling
{
    public static class EventCollection
    {
        public delegate void EventDelegate(object sender, TriggerEvent args);

        public static event EventDelegate PlayerMoved;
        public static event EventDelegate PlayerShot;
        public static event EventDelegate PlayerStayed;
        public static event EventDelegate PlayerDied;
        public static event EventDelegate PlayerHealthCheck;
        public static event EventDelegate MonsterMoved;
        public static event EventDelegate MonsterShot;
        public static event EventDelegate MonsterStayed;
        public static event EventDelegate MonsterDied;
        public static event EventDelegate TrapMoved;
        public static event EventDelegate TrapDamaged;
        public static event EventDelegate TrapStayed;
        public static event EventDelegate TrapHealed;
        public static event EventDelegate TrapTeleported;
        public static event EventDelegate TrapSpawned;
    }
}
