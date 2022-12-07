using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.static_constants
{
    public static class StaticStartValues
    {
        public static readonly double PLACEHOLDER_HEALTH = -1;
        public static readonly double PLACEHOLDER_HEAL = -1;
        public static readonly double PLACEHOLDER_DAMAGE = -1;
        public static readonly double PLACEHOLDER_DOUBLE = -1;
        public static readonly int PLACEHOLDER_INT = -1;
        public static readonly double PLACEHOLDER_AMOUNT = 0;
        public static readonly int PLACEHOLDER_HEIGHT = 0;
        public static readonly int PLACEHOLDER_WIDTH = 0;
        public static readonly string PLACEHOLDER_NAME = "";
        public static readonly string PLACEHOLDER_PARTNER_NAME = "";
        public static readonly Place PLACEHOLDER_PLACE = new Place(-1, -1);
        public static readonly double BASE_HEALTH_CHANGE = 50;
        public static readonly double RANDOM_HEALTH_CHANGE_PLAYER_HEALTH_PARTITION = 2;
        public static readonly double STARTER_PLAYER_DAMAGE = 50;
        public static readonly double STARTER_TRAP_DAMAGE = 50.5;
        public static readonly double STARTER_MONSTER_DAMAGE = 50;
        public static readonly double STARTER_MONSTER_HP = 200;
        public static readonly int STARTER_DISTANCE = 1;
        public static readonly int STARTER_NEAR = 2;
        public static readonly double STARTER_PLAYER_HP = 500;
        public static readonly double BUMPING_INTO_MONSTER_DAMAGE = 20;
        public static readonly double BUMPING_INTO_DOUBLE_TRAP_DAMAGE = 20;
        public static readonly double TRAP_HEALTH = -1;
        public static readonly string PLAYER_TYPE_NAME = "PlayerType";
        public static readonly string PLAYER_COMMAND_LOG = "Player command: ";
    }
}
