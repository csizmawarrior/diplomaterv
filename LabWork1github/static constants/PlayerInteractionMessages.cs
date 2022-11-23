using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.static_constants
{
    public static class PlayerInteractionMessages
    {
        public const string YOU_WON = "Congratulations! You won!";
        public const string YOU_LOST = "GAME OVER! You died!";
        public const string COMMAND_SEPARATOR = "--------------------------------\n";
        public const string PLAYER_FALLING_OFF_BOARD = "Invalid move, falling off the board, try again next turn!";
        public const string PLAYER_BUMP_INTO_MONSTER = "Invalid move, bumping into Monster, you damaged yourself, try again next turn!";
        public const string PLAYER_INVALID_COMMAND = "Invalid command! Try again! Try the help command";
        public const string PROVIDE_A_COMMAND = "Give a command!";
        public const string PLAYER_HEALTH_MESSAGE = "The player's health is: ";
        public const string PLAYER_DAMAGE_MESSAGE = "The player's damage is: ";
        public const string PLAYER_BUMP_INTO_DOUBLE_TRAP = "Invalid move, bumping into two Traps standing on each other, you damaged yourself, try again next turn!";
    }
}
