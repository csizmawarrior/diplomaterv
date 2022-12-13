using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.EventHandling
{
    public static class EventCollection
    {
        public delegate void EventDelegate(object sender, TriggerEventArgs args);

        public static event EventDelegate PlayerHealthCheck = new EventDelegate(DefaultDelegate);
        public static event EventDelegate SomeoneMoved = new EventDelegate(DefaultDelegate);
        public static event EventDelegate SomeoneShot = new EventDelegate(DefaultDelegate);
        public static event EventDelegate SomeoneDied = new EventDelegate(DefaultDelegate);
        public static event EventDelegate TrapDamaged = new EventDelegate(DefaultDelegate);
        public static event EventDelegate TrapHealed = new EventDelegate(DefaultDelegate);
        public static event EventDelegate TrapTeleported = new EventDelegate(DefaultDelegate);
        public static event EventDelegate TrapSpawned = new EventDelegate(DefaultDelegate);

        private static void DefaultDelegate(object sender, TriggerEventArgs triggerEvent)
        {
            // Method intentionally left empty. It is just a placeholder,
            // until a method subscribes to the event.
        }
        public static void InvokeSomeoneMoved(object sender, TriggerEventArgs triggerEvent)
        {
            SomeoneMoved.Invoke(sender, triggerEvent);
        }
        public static void InvokeSomeoneShot(object sender, TriggerEventArgs triggerEvent)
        {
            SomeoneShot.Invoke(sender, triggerEvent);
        }
        public static void InvokeSomeoneDied(object sender, TriggerEventArgs triggerEvent)
        {
            SomeoneDied.Invoke(sender, triggerEvent);
        }
        public static void InvokePlayerHealthCheck(object sender, TriggerEventArgs triggerEvent)
        {
            PlayerHealthCheck.Invoke(sender, triggerEvent);
        }
        public static void InvokeTrapDamaged(object sender, TriggerEventArgs triggerEvent)
        {
            TrapDamaged.Invoke(sender, triggerEvent);
        }
        public static void InvokeTrapHealed(object sender, TriggerEventArgs triggerEvent)
        {
            TrapHealed.Invoke(sender, triggerEvent);
        }
        public static void InvokeTrapTeleported(object sender, TriggerEventArgs triggerEvent)
        {
            TrapTeleported.Invoke(sender, triggerEvent);
        }
        public static void InvokeTrapSpawned(object sender, TriggerEventArgs triggerEvent)
        {
            TrapSpawned.Invoke(sender, triggerEvent);
        }
    }
}
