using LabWork1github.static_constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.MathsCount
{
    public class MultiplySymbol : MathSymbol
    {
        public override double CountResult(double param1, double param2)
        {
            return param1 * param2;
        }

        public override MathSymbol EndSymbol()
        {
            return null;
        }

        public override bool IsStrongerThan(MathSymbol symbol)
        {
            return (symbol is AddSymbol || symbol is SubstractSymbol);
        }

        public override MathSymbol StartSymbol()
        {
            return null;
        }
    }
}
