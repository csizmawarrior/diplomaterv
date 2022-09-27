using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.static_constants
{
    public static class ErrorMessages
    {
        public static class BoardError
        {
            public const string UNDEFINED_MONSTER_TYPE = "The Monster type is undefined at place:\n";
            public const string UNDEFINED_TRAP_TYPE = "The Trap type is undefined at place:\n";
        }

        public static class HealthChangeError
        {
            public const string WRONG_DIRECTION = "Wrong direction used:\n";
            public const string CHARACTER_HAS_NO_HEALTH = "The character has no health, or you can't change its health yourself:\n";
        }

        public static class ParameterDeclarationError
        {
            public const string TRAP_TYPE_ALREADY_EXISTS = "Trap with this type already exists:\n";
            public const string MONSTER_TYPE_ALREADY_EXISTS = "Monster with this type already exists:\n";
            public const string TRAP_HAS_NO_HEALTH = "Trap doesn't have health:\n";
            public const string ONLY_TRAP_CAN_HEAL = "Only Traps have heal attribute:\n";
            public const string ONLY_TRAP_CAN_TELEPORT = "Only Traps have teleport point attribute:\n";
            public const string ONLY_TRAP_CAN_SPAWN_TO_PLACE = "Only Traps have spawn point attribute:\n";
            public const string ONLY_TRAP_CAN_SPAWN_TYPE = "Only Traps have spawn type attribute:\n";
        }
        public static class MoveError
        {
            public const string WRONG_DIRECTION = "Wrong direction used:\n";
        }
        public static class ShootError
        {
            public const string ONLY_MONSTER_CAN_SHOOT = "Only Monster type and Player can shoot, try damage:\n";
            public const string MONSTER_CANNOT_BE_SHOT = "Monster cannot be shot only by a player at:\n";
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
        public static class GameError
        {
            public const string CHARACTER_SPAWNED_OUT_OF_BOUNDS = "A character was out of bounds, so it got deleted";
        }
        public static class ExpressionError
        {
            public const string TYPE_COMPARED_WITH_OTHER_ATTRIBUTE = "The attribute type can only be compared with type attributes, at:\n";
            public const string PLACE_COMPARED_WITH_OTHER_ATTRIBUTE = "The attribute place can only be compared with place attributes, at:\n";
            public const string NOT_A_NUMBER = "The referenced attribute is not a number, number expected at:\n";
            public const string PLAYER_DOES_NOT_HAVE_THIS_ATTRIBUTE = "The Player type doesn't have this kind of attribute at:\n";
            public const string MONSTER_DOES_NOT_HAVE_THIS_ATTRIBUTE = "The Monster type doesn't have this kind of attribute at:\n";
            public const string TRAP_DOES_NOT_HAVE_THIS_ATTRIBUTE = "The Trap type doesn't have this kind of attribtue at:\n";
            public const string PLACE_DOES_NOT_HAVE_THIS_ATTRIBUTE = "The Place attribute doesn't have this kind of attribute at:\n";
            public const string ENEMY_DOES_NOT_HAVE_THIS_ATTRIBUTE = "The Monster type and the Trap type don't have this kind of attribute at:\n";
        }
        public static class ConditionError
        {
            public const string CONDITION_CHECK_FAIL = "Condition check failed\n";
            public const string UNRECOGNIZED_NUMBER = "Unrecognized number expression at:\n";
            public const string UNRECOGNIZED_ATTRIBUTE_ERROR = "An attribute caused error!\n" + ErrorMessages.ConditionError.IN_PLACE;
            public const string PLAYER_ATTRIBUTE_ERROR = "A player attribute caused error!\n";
            public const string TRAP_ATTRIBUTE_ERROR = "A trap attribute caused error!\n";
            public const string MONSTER_ATTRIBUTE_ERROR = "A monster attribute caused error!\n";
            public const string MONSTER_TYPE_HAS_NO_DAMAGE_DEFINED = "The referenced Monster type has no Damage attribute defined!\n";
            public const string TRAP_HAS_NO_HEALTH = "Trap type has no health attribute\n";
            public const string ONLY_TRAP_CAN_HEAL = "Only Trap type can heal, no heal attribute for other types!\n";
            public const string IN_PLACE = "In place:\n";
            public const string TYPE_MISMATCH = "The two types aren't both Monster type or Trap type, so incompatible to compare!\n";
            public const string TYPE_MISMATCH_UNEXPECTED = "Unexpected error: The two types aren't compatible to compare!\n";
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
            public const string ONLY_TRAP_CAN_HEAL = "When command action error, only Trap type can heal, try heal to monster/player instead at:\n";
            public const string ONLY_TRAP_CAN_TELEPORT = "When command action error, only Trap type can teleport, try teleport monster/player to place instead at:\n";
            public const string ONLY_TRAP_CAN_SPAWN = "When command action error, only Trap type can spawn:\n";
            public const string ONLY_MONSTER_CAN_SHOOT = "When command action error, only Monster type and player can shoot, try damage instead:\n";
            public const string TRAPS_DO_NOT_DIE = "When command error, Traps can't die, error at:\n";
            public const string TRAP_DAMAGING_TRAP = "When command error, Trap can't damage trap, error at:\n";
            public const string TRAP_HEALING_TRAP = "When command error, Trap can't heal trap, error at:\n";
            public const string TRAP_TELEPORTING_TRAP = "When command error, Trap can't teleport trap, error at:\n";
            public const string TRAP_SPAWNING_PLAYER = "When command error, Trap can't spawn player, error at:\n";
            public const string TRAP_SPAWNING_TRAP = "When command error, Trap can't spawn trap or itself, error at:\n";
            public const string UNEXPECTED_ERROR = "When command error, unexpected error at:\n";
        }
    }
}
