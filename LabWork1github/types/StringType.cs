using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.types
{
    public class StringType : MyType
    {
        public string Value { get; set; }

        public override bool CompatibleAbsolue(object param2)
        {
            return false;
        }

        public override bool CompatibleAlive(object param2)
        {
            return false;
        }

        public override bool CompatibleAttribue(object param2)
        {
            return false;
        }

        public override bool CompatibleBoolConnecter(object param2)
        {
            return false;
        }

        public override bool CompatibleCompare(object param2)
        {
            if (param2 is StringType)
                return true;
            return false;
        }

        public override bool CompatibleIsNear(object param2)
        {
            return false;
        }

        public override bool CompatibleNegate(object param2)
        {
            return false;
        }

        public override bool CompatibleNumCompare(object param2)
        {
            return false;
        }

        public override bool CompatibleNumConnecter(object param2)
        {
            return false;
        }
    }
}
