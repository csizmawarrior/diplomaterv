using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabWork1github.DynamicMonsterParser;
using LabWork1github;

namespace LabWork1github
{
    class MonsterExpressionVisitor : DynamicMonsterBaseVisitor<object>
    {
        public Dictionary<int, Symbol> SymbolTable { get; set; }

        public override object Visit([NotNull] IParseTree tree)
        {
            return base.Visit(tree);
        }
    }
}
