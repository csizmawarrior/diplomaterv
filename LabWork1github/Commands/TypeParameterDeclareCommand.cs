using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.Commands
{
    public delegate void TypeParameterDeclareDelegate(GameParamProvider provider, TypeParameterDeclareCommand command);

    public class TypeParameterDeclareCommand : HealthChangerCommand
    {
        public CharacterType CharacterType { get; set; }

        public TypeParameterDeclareDelegate TypeParameterDeclareDelegate { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            TypeParameterDeclareDelegate(provider, this);
        }
    }
}
