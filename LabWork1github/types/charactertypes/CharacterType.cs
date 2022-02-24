using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public abstract class CharacterType : MyType
    {
        public int Health { get; set; } = -1;
        public int Heal { get; set; } = -1;
        public int Damage { get; set; } = -1;
        public Place TeleportPlace { get; set; } = new Place(-1, -1);
        public Place SpawnPlace { get; set; } = new Place(-1, -1);
        public CharacterType SpawnType { get; set; }
        public string Name { get; set; }
        public List<Command> Commands { get; set; } = new List<Command>();


        public abstract void Step(GameParamProvider provider);
    }
}
