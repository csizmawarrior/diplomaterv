using LabWork1github.static_constants;
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
            this.Damage = StaticStartValues.STARTER_PLAYER_DAMAGE;
            this.Name = StaticStartValues.PLAYER_TYPE_NAME;
        }

        public override void Step(GameParamProvider provider)
        {
            provider.PlayerCommand();
        }
    }
}
