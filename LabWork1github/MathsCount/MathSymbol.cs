using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.static_constants
{
    public abstract class MathSymbol
    {
        public abstract bool IsStrongerThan(MathSymbol symbol);
        public abstract double CountResult(double param1, double param2);
    }
}
