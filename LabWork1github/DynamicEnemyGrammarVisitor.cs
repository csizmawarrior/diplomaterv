using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using static LabWork1github.DynamicEnemyGrammarParser;

namespace LabWork1github
{
    public class DynamicEnemyGrammarVisitor : DynamicEnemyGrammarBaseVisitor<object>
    {
        private string typeName = "";
        private string type = null;
        private MyType TypeCheckHelp = new MyType();

        public override object VisitTrapNameDeclaration([NotNull] TrapNameDeclarationContext context)
        {
            type = MyType.TRAP;
            Program.EnemyTypes.Add(new TrapType(context.name().GetText()));
            return base.VisitTrapNameDeclaration(context);
        }
        public override object VisitMonsterNameDeclaration([NotNull] MonsterNameDeclarationContext context)
        {
            type = MyType.MONSTER;
            Program.EnemyTypes.Add(new MonsterType(context.name().GetText()));
            return base.VisitMonsterNameDeclaration(context);
        }
    }
}
