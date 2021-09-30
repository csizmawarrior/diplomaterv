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
            for (int i = 0; i < Program.trapTypes.Count; i++)
            {
                if (Program.trapTypes.ElementAt(i).Name.Equals(typeName))
                {
                    foreach (var command in Program.trapTypes.ElementAt(i).Moves)
                    {
                        if (command.Round == round)
                        {
                            string dist = context.ID().GetText();
                            if (dist.Equals("F") || dist.Equals("L") || dist.Equals("B") || dist.Equals("R"))
                                command.Distance = dist;
                        }
                    }
                }
            }
            return base.VisitDistanceDeclare(context);
        }
    }
}
