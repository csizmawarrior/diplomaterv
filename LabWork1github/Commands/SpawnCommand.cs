using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public delegate void SpawnDelegate(GameParamProvider provider, SpawnCommand command);

    public class SpawnCommand : Command
    {
        public Place TargetPlace { get; set; } = new Place(-1, -1);

        public int Round { get; set; }

        public EnemyType SpawnType { get; set; }

        public SpawnDelegate SpawnDelegate { get; set; }

        public CharacterType TargetCharacterType { get; set; } = null;

        public override void Execute(GameParamProvider provider)
        {
            SpawnDelegate(provider, this);
        }
    }
}
