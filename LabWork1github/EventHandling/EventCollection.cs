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

        public static event EventDelegate PlayerHealthCheck = new EventDelegate(DefaultDelegate);
        public static event EventDelegate SomeoneMoved = new EventDelegate(DefaultDelegate);
        public static event EventDelegate SomeoneShot = new EventDelegate(DefaultDelegate);
        public static event EventDelegate SomeoneDied = new EventDelegate(DefaultDelegate);
        public static event EventDelegate TrapDamaged = new EventDelegate(DefaultDelegate);
        public static event EventDelegate TrapHealed = new EventDelegate(DefaultDelegate);
        public static event EventDelegate TrapTeleported = new EventDelegate(DefaultDelegate);
        public static event EventDelegate TrapSpawned = new EventDelegate(DefaultDelegate);

        private static void DefaultDelegate(object sender, TriggerEvent triggerEvent)
        {
            return;
        }
        public static void InvokeSomeoneMoved(object sender, TriggerEvent triggerEvent)
        {
            SomeoneMoved.Invoke(sender, triggerEvent);
        }
        public static void InvokeSomeoneShot(object sender, TriggerEvent triggerEvent)
        {
            SomeoneShot.Invoke(sender, triggerEvent);
        }
        public static void InvokeSomeoneDied(object sender, TriggerEvent triggerEvent)
        {
            SomeoneDied.Invoke(sender, triggerEvent);
        }
        public static void InvokePlayerHealthCheck(object sender, TriggerEvent triggerEvent)
        {
            PlayerHealthCheck.Invoke(sender, triggerEvent);
        }
        public static void InvokeTrapDamaged(object sender, TriggerEvent triggerEvent)
        {
            TrapDamaged.Invoke(sender, triggerEvent);
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
