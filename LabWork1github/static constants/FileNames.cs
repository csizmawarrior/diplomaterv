using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.static_constants
{
    public static class FileNames
    {
        static readonly string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string ERROR_LOG_FILE_ADDRESS = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\files\error_log.txt");
        public static readonly string DEFAULT_MONSTER_FILE_ADDRESS = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\files\DefaultMonster.txt");
        public static readonly string DEFAULT_TRAP_FILE_ADDRESS = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\files\DefaultTrap.txt");
        public static readonly string DEFAULT_BOARD_FILE_ADDRESS = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\files\BoardCreation.txt");
        public static readonly string EXAMPLE_MONSTERS_FILE_ADDRESS = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\files\ExampleMonsters.txt");
        public static readonly string EXAMPLE_TRAPS_FILE_ADDRESS = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\files\ExampleTraps.txt");
        public static readonly string EXAMPLE_BOARD_FILE_ADDRESS = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\files\ExampleBoard.txt");
    }
}
