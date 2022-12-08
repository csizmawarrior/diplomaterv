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
            public const string DUPLICATED_NAME = "The following name were assigned to multiple characters, the former one gets the name only, names are unique:\n";
            public const string ZERO_WIDTH = "The given width is zero at place:\n";
            public const string ZERO_HEIGHT = "The given height is zero at place:\n";
            public const string ZERO_AS_COORDINATE = "The coordinate given is zero at place:\n";
            public const string UNDEFINED_TRAP_TYPE = "The Trap type is undefined at place:\n";
            public const string PARTNER_CANNOT_BE_THE_PLAYER = "The character with the following name tried to adress the player as its partner:\n";
            public const string CANNOT_BE_YOUR_OWN_PARTNER = "The character with the following name tried to adress itself as its partner:\n";
        }

        public static class ConditionError
        {
            public const string CONDITION_CHECK_FAIL = "Condition check failed\n";
            public const string UNRECOGNIZED_NUMBER = "Unrecognized number expression at:\n";
            public const string UNRECOGNIZED_ATTRIBUTE_ERROR = "An attribute caused error!\n" + ErrorMessages.ConditionError.IN_PLACE;
            public const string PLAYER_ATTRIBUTE_ERROR = "A player attribute caused error!\n";
            public const string PLAYER_HAS_NO_NAME = "The player in the game has no name, assign it one in the board creation script!\n";
            public const string CHARACTER_HAS_NO_NAME = "The referenced character has no name, assign it one in the board creation script!\n";
            public const string TRAP_ATTRIBUTE_ERROR = "A trap attribute caused error!\n";
            public const string MONSTER_ATTRIBUTE_ERROR = "A monster attribute caused error!\n";
            public const string CHARACTER_TYPE_HAS_NO_DAMAGE_DEFINED = "The referenced type has no Damage attribute defined!\n";
            public const string CHARACTER_TYPE_HAS_NO_HEAL_DEFINED = "The referenced type has no Heal attribute defined!\n";
            public const string CHARACTER_TYPE_HAS_NO_HEALTH_DEFINED = "The referenced type has no Health attribute defined!\n";
            public const string CHARACTER_TYPE_HAS_NO_TELEPORT_PLACE_DEFINED = "The referenced type has no \"teleport_place\" attribute defined!\n";
            public const string CHARACTER_TYPE_HAS_NO_SPAWN_PLACE_DEFINED = "The referenced type has no \"spawn_place\" attribute defined!\n";
            public const string CHARACTER_TYPE_HAS_NO_SPAWN_TYPE_DEFINED = "The referenced type has no \"spawn_type\" attribute defined!\n";
            public const string TRAP_HAS_NO_HEALTH = "Trap type has no health attribute\n";
            public const string UNRECOGNIZED_EXPRESSION = "Unrecognized number expression:\n";
            public const string ONLY_TRAP_CAN_HEAL = "Only Trap type can heal, no heal attribute for other types!\n";
            public const string SPAWN_TYPE_NOT_FOUND = "The given Trap type's \"spawn_type\" cannot be found, give a valid type.\n";
            public const string IN_PLACE = "In place:\n";
            public const string TYPE_MISMATCH = "The two types aren't both Monster type or Trap type, so incompatible to compare!\n";
            public const string TYPE_MISMATCH_UNEXPECTED = "Unexpected error: The two types aren't compatible to compare!\n";
        }
        public static class CommandAddingError
        {
            public const string UNEXPECTED_ERROR = "Unexpected error occurred while saving commands\n";
        }
        public static class DamageError
        {
            public const string ONLY_TRAP_CAN_DAMAGE = "Only Trap type can damage:\n";
        }
        public static class DistanceError
        {
            public const string ZERO_DISTANCE = "Negative or zero Distance is not supported!\n";
        }
        public static class EventError
        {
            public const string EVENT_WITHOUT_ACTION = "When command doesn't have action at:\n";
            public const string ACTION_WITHOUT_CHARACTER_OR_PLACE = "When command action doesn't have character nor place at:\n";
            public const string ACTION_WITHOUT_CHARACTER = "When command action doesn't have character at:\n";
            public const string PLAYER_SHOOTING_ITSELF = "When command action error, player can't shoot itself:\n";
            public const string PLAYER_SHOOTING_TRAP = "When command action error, player can't shoot trap:\n";
            public const string MONSTER_SHOOTING_MONSTER = "When command action error, monster can't shoot itself or another monster:\n";
            public const string MONSTER_SHOOTING_PARTNER = "When command action error, monster can't shoot its partner:\n";
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
        public static class ExpressionError
        {
            public const string TYPE_COMPARED_WITH_OTHER_ATTRIBUTE = "The attribute \"type\" can only be compared with \"type\" attributes, at:\n";
            public const string PLACE_COMPARED_WITH_OTHER_ATTRIBUTE = "The attribute \"place\" can only be compared with \"place\" attributes, at:\n";
            public const string NAME_COMPARED_WITH_OTHER_ATTRIBUTE = "The attribute \"name\" can only be compared with \"name\" attributes, at:\n";
            public const string NOT_A_NUMBER = "The referenced attribute is not a number, number expected at:\n";
            public const string DIVIDING_WITH_ZERO = "The expression want to divide with zero.\n" + ErrorMessages.ConditionError.IN_PLACE;
            public const string NOT_NUMBER_EXPRESSION_COMPARED_WITH_NUMBER = "The expressions compared don't match type, one is number, the other isn't at:\n";
            public const string NOT_NUMBER_EXPRESSIONS_HANDLED_AS_NUMBER = "At least one of the referenced number exppressions does not calculate to a number at:\n";
            public const string NOT_NUMBER_EXPRESSION_HANDLED_AS_NUMBER = "The referenced number exppression does not calculate to a number at:\n";
            public const string PLAYER_DOES_NOT_HAVE_THIS_ATTRIBUTE = "The Player type doesn't have this kind of attribute at:\n";
            public const string MONSTER_DOES_NOT_HAVE_THIS_ATTRIBUTE = "The Monster type doesn't have this kind of attribute at:\n";
            public const string TRAP_DOES_NOT_HAVE_THIS_ATTRIBUTE = "The Trap type doesn't have this kind of attribtue at:\n";
            public const string PLACE_DOES_NOT_HAVE_THIS_ATTRIBUTE = "The Place attribute doesn't have this kind of attribute at:\n";
            public const string ENEMY_DOES_NOT_HAVE_THIS_ATTRIBUTE = "The Monster type and the Trap type don't have this kind of attribute at:\n";
            public const string NOBODY_HAS_THIS_ATTRIBUTE = "The game's current types don't have this kind of attribute at:\n";
        }
        public static class HealError
        {
            public const string ONLY_TRAP_CAN_HEAL = "Only Trap type can heal:\n";
        }
        public static class HealthChangeError
        {
            public const string CHARACTER_HAS_NO_HEALTH = "The character has no health, or you can't change its health yourself:\n";
        }
        public static class GameError
        {
            public const string CHARACTER_SPAWNED_OUT_OF_BOUNDS = "A character was out of bounds, so it got deleted, with the given name: ";
            public const string CHARACTER_SPAWNED_ON_TRAP = "A character wwith the following name spawns on a trap, give a valid board: ";
            public const string ERRORS_OCCURED_CONTINUE = "Errors occured, while processing the input scrips, if you wish to continue press a button.\n";
            public const string PLAYER_SPAWNED_ON_CHARACTER = "The player and another character collide on the board, give a valid board.\n";
            public const string CHARACTER_SPAWNED_ON_CHARACTER = "A character and another character collide on the board, give a valid board.\n";
            public const string PLAYER_SPAWNED_ON_TRAP = "The player and a trap collide on the board at spawn, give a valid board.\n";
            public const string PLACE_OUT_OF_BOUNDS = "A referenced place is out of bounds.\n";
        }
        public static class ParameterDeclarationError
        {
            public const string TRAP_TYPE_ALREADY_EXISTS = "Trap with this type already exists:\n";
            public const string MONSTER_TYPE_ALREADY_EXISTS = "Monster with this type already exists:\n";
            public const string MONSTER_ZERO_HEALTH = "Monster type cannot have zero health as default at:\n";
            public const string TRAP_HAS_NO_HEALTH = "Trap doesn't have health:\n";
            public const string ONLY_TRAP_CAN_HEAL = "Only Traps have heal attribute:\n";
            public const string ONLY_TRAP_CAN_TELEPORT = "Only Traps have teleport point attribute:\n";
            public const string ONLY_TRAP_CAN_SPAWN_TO_PLACE = "Only Traps have spawn point attribute:\n";
            public const string ONLY_TRAP_CAN_SPAWN_TYPE = "Only Traps have spawn type attribute:\n";
        }
        public static class ParseError
        {
            public const string UNABLE_TO_PARSE_DOUBLE = "The entered number is not recognized as a number.";
            public const string UNABLE_TO_PARSE_INT = "The entered number is not recognized as an integer.";
            public const string UNABLE_TO_PARSE_PLACE = "The entered numbers do not represent a valid place, make sure to use positive integers.";
        }
        public static class PartnerError
        {
            public const string NON_EXISTANT_PARTNER = "The partner of the referenced character is not existing, give a real enemy partner to the character with the name:\n";
            public const string PLAYER_CANNOT_BE_PARTNER = "The partner of the referenced character is the player, give a real enemy partner to the character with the name:\n";
        }
        public static class ShootError
        {
            public const string ONLY_MONSTER_CAN_SHOOT = "Only Monster type and Player can shoot, try damage:\n";
            public const string MONSTER_CANNOT_BE_SHOT = "Monster cannot be shot only by a player at:\n";
        }
        public static class SpawnError
        {
            public const string ONLY_TRAP_CAN_SPAWN = "Only Trap type can spawn:\n";
            public const string SPAWN_WITHOUT_PLACE_GIVEN = "Spawn point not given, but trying to spawn:\n";
            public const string SPAWN_WITHOUT_TYPE_GIVEN = "Spawn type not given, but trying to spawn:\n";
        }
        public static class TeleportError
        {
            public const string ONLY_TRAP_CAN_TELEPORT = "Only Trap type can teleport:\n";
            public const string TELEPORTING_WITHOUT_PLACE_GIVEN = "Teleport point not given, but trying to teleport:\n";
            public const string TRYING_TO_TELEPORT_YOURSELF = "You can't teleport yourself:\n";
        }
        public static class TypeCreationError
        {
            public const string TYPE_DOES_NOT_EXIST = "The type with the following name doesn't exist: ";
        }
    }
}
