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
            public const string ONLY_MONSTER_CAN_SHOOT = "Only Monster type can shoot:\n";
        }
        public static class TeleportError
        {
            public const string ONLY_TRAP_CAN_TELEPORT = "Only Trap type can teleport:\n";
            public const string TELEPORTING_WITHOUT_PLACE_GIVEN = "Teleport point not given, but trying to teleport:\n";
            public const string TRYING_TO_TELEPORT_YOURSELF = "You can't teleport yourself:\n";
        }
        public static class SpawnError
        {
            public const string ONLY_TRAP_CAN_SPAWN = "Only Trap type can spawn:\n";
            public const string SPAWN_WITHOUT_PLACE_GIVEN = "Spawn point not given, but trying to spawn:\n";
            public const string SPAWN_WITHOUT_TYPE_GIVEN = "Spawn type not given, but trying to spawn:\n";
        }
        public static class DamageError
        {
            public const string ONLY_TRAP_CAN_DAMAGE = "Only Trap type can damage:\n";
        }
        public static class HealError
        {
            public const string ONLY_TRAP_CAN_HEAL = "Only Trap type can heal:\n";
        }
        public static class ConditionError
        {
            public const string CONDITION_CHECK_FAIL = "Condition check failed\n";
        }
        public static class EventError
        {
            public const string EVENT_WITHOUT_ACTION = "When command doesn't have action at:\n";
            public const string ACTION_WITHOUT_CHARACTER_OR_PLACE = "When command action doesn't have character nor place at:\n";
            public const string ACTION_WITHOUT_CHARACTER = "When command action doesn't have character at:\n";
            public const string PLAYER_SHOOTING_ITSELF = "When command action error, player can't shoot itself:\n";
            public const string MONSTER_SHOOTING_MONSTER = "When command action error, monster can't shoot itself or another monster:\n";
            public const string MONSTER_SHOOTING_TRAP = "When command action error, monster can't shoot a trap:\n";
            public const string ONLY_TRAP_CAN_DAMAGE = "When command action error, only Trap type can damage, try shoot instead at:\n";
            public const string ONLY_TRAP_CAN_HEAL = "When command action error, only Trap type can heal, try heal to character instead at:\n";
            public const string ONLY_TRAP_CAN_TELEPORT = "When command action error, only Trap type can teleport, try teleport character instead at:\n";
            public const string ONLY_TRAP_CAN_SPAWN = "When command action error, only Trap type can spawn:\n";
        }
    }
}
