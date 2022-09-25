using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LabWork1github.Symbols
{
    public class Scope
    {
        public Scope Parent { get; set; }
        private Dictionary<string, Symbol> Symbols = new Dictionary<string, Symbol>();
        public Symbol ParentSymbol { get; set; } = null;

        public Scope(Scope parentScope)
        {
            Parent = parentScope;
        }

        public void AddSymbol(Symbol newSymbol)
        {
            int counter = 1;
            while (Symbols.ContainsKey(newSymbol.Name)) {
                if (!Symbols.ContainsKey(newSymbol.Name + counter))
                {
                    newSymbol.Name += counter;
                    break;
                }
                counter++;
            }            
            Symbols.Add(newSymbol.Name, newSymbol);
        }

        public bool IsSymbolInScope(string symbolName)
        {
            string name = Regex.Replace(symbolName, @"[\d-]", string.Empty);
            return Symbols.ContainsKey(name);
        }

        public Symbol FindSymbol(string symbolName)
        {
            if (!Symbols.ContainsKey(symbolName))
                return Parent.FindSymbol(symbolName);
            return Symbols[symbolName];
        }
    }
}
