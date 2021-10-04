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
        private int _distance =1 ;

        public int Distance
        {
            get { return _distance; }
            set
            {
                if (value <= 0)
                    throw new NotSupportedException("Negative or zero Distance is not supported!");
                _distance = value;
            }
        }

        public int Round { get; set; }
        private int _damage = 10;

        public int Damage
        {
            get { return _damage; }
            set {
                if(value<=0)
                    throw new NotSupportedException("Negative or zero Distance is not supported!");
                _damage = value;
            }
        }

        public Place targetPlace { get; set; }
        public string Direction { get; set; }

        public MoveDelegate MoveDelegate { get; set; }

        public override void Execute(GameParamProvider provider)
        {
            MoveDelegate(provider, this);
        }


    }
}
