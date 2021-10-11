using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabWork1github.DynamicMonsterParser;
using static LabWork1github.DynamicTrapParser;

namespace LabWork1github
{
    public class BoolsConnectedVisitor
    {
        private readonly DynamicMonsterParser.BoolexpressionContext ContextMonster;
        private readonly DynamicTrapParser.BoolexpressionContext ContextTrap;

        public BoolsConnectedVisitor(DynamicMonsterParser.BoolexpressionContext monsterContext, DynamicTrapParser.BoolexpressionContext trapContext)
        {
            ContextMonster = monsterContext;
            ContextTrap = trapContext;
        }

        public bool CheckExpression()
        {
            return false;
        }



    }
}
