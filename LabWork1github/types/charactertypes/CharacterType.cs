using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public abstract class CharacterType : MyType
    {
        public int Health { get; set; }
        public int Heal { get; set; }
        public int Damage { get; set; }
        public Place TeleportPlace { get; set; }
        public Place SpawnPlace { get; set; }
        public CharacterType SpawnType { get; set; }
        public string Name { get; set; }
        public List<Command> Commands { get; set; }


        public abstract void Step(GameParamProvider provider);
    }
}
