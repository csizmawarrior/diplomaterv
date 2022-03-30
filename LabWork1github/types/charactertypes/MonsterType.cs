using LabWork1github.types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{

    public class MonsterType : CharacterType
    {
        

        public MonsterType(string name)
        {
            this.Name = name;
        }

        public override void Step(GameParamProvider provider)
        {
            Commands.ElementAt(provider.GetRound()).Execute(provider);
        }

        public override bool CompatibleCompare(object param2)
        {
            if (param2 is MonsterType)
                return true;
            return false;
        }

        public override bool CompatibleNumCompare(object param2)
        {
            return false;
        }

        public override bool CompatibleAttribue(object param2)
        {
            if (param2 is StringType)
                return true;
            return false;
        }

        public override bool CompatibleNumConnecter(object param2)
        {
            return false;
        }

        public override bool CompatibleBoolConnecter(object param2)
        {
            return false;
        }

        public override bool CompatibleAlive(object param2)
        {
            if (param2 == null)
                return true;
            throw new ArgumentException("not null parameter for absolute typecheck");
        }

        public override bool CompatibleIsNear(object param2)
        {
            if (param2 == null)
                return true;
            throw new ArgumentException("not null parameter for absolute typecheck");
        }

        public override bool CompatibleAbsolue(object param2)
        {
            return false;
        }

        public override bool CompatibleNegate(object param2)
        {
            return false;
        }


        //TODO: get out, now only for old code
        public int MoveRound { get; set; }
        public int ShootRound { get; set; }
    }
}
