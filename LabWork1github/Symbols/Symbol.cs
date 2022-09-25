using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.Symbols
{
    public class Symbol
    {
        public Symbol(string name, Command value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public Command Value { get; set; }
    }
}
