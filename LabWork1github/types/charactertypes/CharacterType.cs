using LabWork1github.EventHandling;
using LabWork1github.static_constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public abstract class CharacterType
    {

        public double Health { get; set; } = StaticStartValues.PLACEHOLDER_HEALTH;
        public double Heal { get; set; } = StaticStartValues.PLACEHOLDER_HEAL;
        public double Damage { get; set; } = StaticStartValues.PLACEHOLDER_DAMAGE;
        public Place TeleportPlace { get; set; } = StaticStartValues.PLACEHOLDER_PLACE;
        public Place SpawnPlace { get; set; } = StaticStartValues.PLACEHOLDER_PLACE;
        public CharacterType SpawnType { get; set; } = null;
        public string Name { get; set; } = StaticStartValues.PLACEHOLDER_NAME;
        public List<Command> Commands { get; set; } = new List<Command>();
        public List<TriggerEventHandler> EventHandlers { get; set; } = new List<TriggerEventHandler>();


        public abstract void Step(GameParamProvider provider);
    }
}
