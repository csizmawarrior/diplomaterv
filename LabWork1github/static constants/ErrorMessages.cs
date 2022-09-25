using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.static_constants
{
    public static class ErrorMessages
    {
        public static class ParameterDeclarationError
        {
            public const string TRAP_TYPE_ALREADY_EXISTS = "Trap with this type already exists:\n";
            public const string MONSTER_TYPE_ALREADY_EXISTS = "Monster with this type already exists:\n";
            public const string TRAP_HAS_NO_HEALTH = "Trap doesn't have health:\n";
            public const string ONLY_TRAP_CAN_HEAL = "Only Traps have heal parameter:\n";
            public const string ONLY_TRAP_CAN_TELEPORT = "Only Traps have teleport point parameter:\n";
            public const string ONLY_TRAP_CAN_SPAWN_TO_PLACE = "Only Traps have spawn point parameter:\n";
            public const string ONLY_TRAP_CAN_SPAWN_TYPE = "Only Traps have spawn type parameter:\n";
        }
        public static class MoveError
        {
            public const string WRONG_DIRECTION = "Wrong direction used:\n";
        }
        public static class ShootError
        {

        }
    }
}
