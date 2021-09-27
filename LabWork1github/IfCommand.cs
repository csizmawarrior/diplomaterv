using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class IfCommand
    {
        public List<IfCommand> InnerConditions { get; set; }
        public List<MoveCommand> InnerMoves { get; set; }
        public List<ShootCommand> InnerShoots { get; set; }

        //TODO: need to find what is required so every condition can be decided
        //can be done with config file? or a new type?
        public bool ConditionEvaluation()
        {
            return true;
        }
        public void Execute()
        {

        }

    }
}
