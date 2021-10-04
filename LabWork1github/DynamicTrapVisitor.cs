using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabWork1github.DynamicTrapParser;
using LabWork1github;

namespace LabWork1github
{
    class DynamicTrapVisitor : DynamicTrapBaseVisitor<object>
    {
        private string typeName = "";
        private int round = 0;

        public override object VisitNameDeclaration([NotNull] NameDeclarationContext context)
        {
            typeName = context.name().GetText();
            Program.trapTypes.Add(new TrapType(typeName));
            return base.VisitNameDeclaration(context);
        }
        public override object VisitDistanceDeclare([NotNull] DistanceDeclareContext context)
        {
            
            return base.VisitDistanceDeclare(context);
        }
    }
}
