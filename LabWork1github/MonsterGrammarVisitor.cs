using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using LabWork1github;
using LabWork1github;
using static LabWork1github.MonsterGrammarParser;

namespace LabWork1github
{
    class MonsterGrammarVisitor : MonsterGrammarBaseVisitor<object>
    {
        private string typeName = "";

        public override object VisitNameDeclaration([NotNull] NameDeclarationContext context)
        {
            typeName = context.name().GetText();
            Program.monsterTypes.Add(new MonsterType());
            return base.VisitNameDeclaration(context);
        }
        public override object VisitMoveRoundDeclaration([NotNull] MoveRoundDeclarationContext context)
        {
            for (int i = 0; i < Program.monsterTypes.Count; i++)
            {
                if (Program.monsterTypes.ElementAt(i).Name.Equals(typeName))
                    Program.monsterTypes.ElementAt(i).MoveRound = int.Parse(context.NUMBER().GetText());
            }
            return base.VisitMoveRoundDeclaration(context);
        }
        
        public override object VisitShootRoundDeclaration([NotNull] ShootRoundDeclarationContext context)
        {
            for (int i = 0; i < Program.monsterTypes.Count; i++)
            {
                if (Program.monsterTypes.ElementAt(i).Name.Equals(typeName))
                    Program.monsterTypes.ElementAt(i).ShootRound = int.Parse(context.NUMBER().GetText());
            }
            return base.VisitShootRoundDeclaration(context);
        }
    }
}
