
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class PlayerType : CharacterType
    {
        public PlayerType()
        {
            this.Damage = 50;
        }

        public override void Step(GameParamProvider provider)
        {
            provider.PlayerCommand();
        }
    }
}
