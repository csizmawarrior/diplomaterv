using LabWork1github.static_constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public delegate void MoveDelegate(GameParamProvider provider, MoveCommand command);

    public class MoveCommand : Command
    {
        private int _distance = StaticStartValues.STARTER_DISTANCE ;

        public int Distance
        {
            get { return _distance; }
            set
            {
                //TODO: Filter this in the Visitor
                if (value <= 0)
                    throw new NotSupportedException(ErrorMessages.MoveError.NEGATIVE_DIRECTION);
                _distance = value;
            }
        }

        public int Round { get; set; }

        public Place TargetPlace { get; set; }
        public string Direction { get; set; }

        public MoveDelegate MoveDelegate { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            MoveDelegate(provider, this);
        }


    }
}
