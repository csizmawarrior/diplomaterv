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

        public static event EventDelegate PlayerMoved = new EventDelegate(DefaultDelegate);
        public static event EventDelegate PlayerShot = new EventDelegate(DefaultDelegate);
        public static event EventDelegate PlayerStayed = new EventDelegate(DefaultDelegate);
        public static event EventDelegate PlayerDied = new EventDelegate(DefaultDelegate);
        public static event EventDelegate PlayerHealthCheck = new EventDelegate(DefaultDelegate);
        public static event EventDelegate MonsterMoved = new EventDelegate(DefaultDelegate);
        public static event EventDelegate MonsterShot = new EventDelegate(DefaultDelegate);
        public static event EventDelegate MonsterStayed = new EventDelegate(DefaultDelegate);
        public static event EventDelegate MonsterDied = new EventDelegate(DefaultDelegate);
        public static event EventDelegate TrapMoved = new EventDelegate(DefaultDelegate);
        public static event EventDelegate TrapDamaged = new EventDelegate(DefaultDelegate);
        public static event EventDelegate TrapStayed = new EventDelegate(DefaultDelegate);
        public static event EventDelegate TrapHealed = new EventDelegate(DefaultDelegate);
        public static event EventDelegate TrapTeleported = new EventDelegate(DefaultDelegate);
        public static event EventDelegate TrapSpawned = new EventDelegate(DefaultDelegate);

        private static void DefaultDelegate(object sender, TriggerEvent triggerEvent)
        {
            return;
        }
        public static void InvokePlayerMoved(object sender, TriggerEvent triggerEvent)
        {
            PlayerMoved.Invoke(sender, triggerEvent);
        }
        public static void InvokePlayerShot(object sender, TriggerEvent triggerEvent)
        {
            PlayerShot.Invoke(sender, triggerEvent);
        }
        public static void InvokePlayerStayed(object sender, TriggerEvent triggerEvent)
        {
            PlayerStayed.Invoke(sender, triggerEvent);
        }
        public static void InvokePlayerDied(object sender, TriggerEvent triggerEvent)
        {
            PlayerDied.Invoke(sender, triggerEvent);
        }
        public static void InvokePlayerHealthCheck(object sender, TriggerEvent triggerEvent)
        {
            PlayerHealthCheck.Invoke(sender, triggerEvent);
        }
        public static void InvokeMonsterMoved(object sender, TriggerEvent triggerEvent)
        {
            MonsterMoved.Invoke(sender, triggerEvent);
        }
        public static void InvokeMonsterShot(object sender, TriggerEvent triggerEvent)
        {
            MonsterShot.Invoke(sender, triggerEvent);
        }
        public static void InvokeMonsterStayed(object sender, TriggerEvent triggerEvent)
        {
            MonsterStayed.Invoke(sender, triggerEvent);
        }
        public static void InvokeMonsterDied(object sender, TriggerEvent triggerEvent)
        {
            MonsterDied.Invoke(sender, triggerEvent);
        }
        public static void InvokeTrapMoved(object sender, TriggerEvent triggerEvent)
        {
            TrapMoved.Invoke(sender, triggerEvent);
        }
        public static void InvokeTrapDamaged(object sender, TriggerEvent triggerEvent)
        {
            TrapDamaged.Invoke(sender, triggerEvent);
        }
        public static void InvokeTrapStayed(object sender, TriggerEvent triggerEvent)
        {
            TrapStayed.Invoke(sender, triggerEvent);
        }
        public static void InvokeTrapHealed(object sender, TriggerEvent triggerEvent)
        {
            TrapHealed.Invoke(sender, triggerEvent);
        }
        public static void InvokeTrapTeleported(object sender, TriggerEvent triggerEvent)
        {
            TrapTeleported.Invoke(sender, triggerEvent);
        }
        public static void InvokeTrapSpawned(object sender, TriggerEvent triggerEvent)
        {
            TrapSpawned.Invoke(sender, triggerEvent);
        }
    }
}
