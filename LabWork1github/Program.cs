using Antlr4.Runtime;
using LabWork1github;
using LabWork1github.static_constants;
using System;
using System.Collections.Generic;
using static LabWork1github.DynamicEnemyGrammarParser;

namespace LabWork1github
{
    public class Program
    {
        public static List<CharacterType> CharacterTypes = new List<CharacterType>();
        public static List<Character> Characters = new List<Character>();
        public static List<EventHandler> EventHandlers = new List<EventHandler>();
        public static Drawer Drawer = new Drawer();
        public static Board Board = new Board();

        static void Main(string[] args)
        {
            //we hand in a default type for a monster and a trap, so when nothing is given, the trap or monster will get these ones' attributes
            //CharacterTypes.Add(new MonsterType(Types.DEFAULT_MONSTER));
            //GetCharacterType(Types.DEFAULT_MONSTER).Damage = 50;
            //GetCharacterType(Types.DEFAULT_MONSTER).Health = 200;

            //CharacterTypes.Add(new TrapType(Types.DEFAULT_TRAP));
            //GetCharacterType(Types.DEFAULT_TRAP).Damage = 50;


            //  MonsterTypeLoader();
            bool trapInit = TrapTypeLoader(FileNames.EXAMPLE_TRAPS_FILE_ADDRESS);
            bool monsterInit = MonsterTypeLoader(FileNames.EXAMPLE_MONSTERS_FILE_ADDRESS);
            bool boardInit = BoardLoader(FileNames.EXAMPLE_BOARD_FILE_ADDRESS);

            if (trapInit || monsterInit || boardInit)
            {
                Drawer.WriteCommand(ErrorMessages.GameError.ERRORS_OCCURED_CONTINUE);
                Console.ReadKey();
            }


            Game theGame = new Game();
            theGame.Init();
            theGame.Start();

            Console.ReadKey();

        }

        public static bool MonsterTypeLoader(string fileName)
        {
            string text = System.IO.File.ReadAllText(fileName);
            AntlrInputStream inputStream = new AntlrInputStream(text);
            DynamicEnemyGrammarLexer MonsterGrammarLexer_ = new DynamicEnemyGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(MonsterGrammarLexer_);
            DynamicEnemyGrammarParser DynamicEnemyGrammarParser = new DynamicEnemyGrammarParser(commonTokenStream);
            DynamicEnemyGrammarParser.DefinitionContext chatContext = DynamicEnemyGrammarParser.definition();
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            if (visitor.ErrorFound)
                Drawer.WriteCommand(visitor.Error);
            return visitor.ErrorFound;
        }

        public static bool BoardLoader(string fileName)
        {
            string text = System.IO.File.ReadAllText(fileName);
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            BoardGrammarVisitor visitor = new BoardGrammarVisitor();
            visitor.Visit(chatContext);
            if (visitor.ErrorFound)
                Drawer.WriteCommand(visitor.ErrorList);
            return visitor.ErrorFound;
        }

        public static bool TrapTypeLoader(string fileName)
        {
            string text = System.IO.File.ReadAllText(fileName);
            AntlrInputStream inputStream = new AntlrInputStream(text);
            DynamicEnemyGrammarLexer DynamicEnemyGrammarLexer_ = new DynamicEnemyGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(DynamicEnemyGrammarLexer_);
            DynamicEnemyGrammarParser DynamicEnemyGrammarParser = new DynamicEnemyGrammarParser(commonTokenStream);
            DynamicEnemyGrammarParser.DefinitionContext chatContext = DynamicEnemyGrammarParser.definition();
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            if(visitor.ErrorFound)
                Drawer.WriteCommand(visitor.Error);
            return visitor.ErrorFound;
        }

        public static CharacterType GetCharacterType(string name)
        {
            if (String.IsNullOrEmpty(name))
                return null;
            if (CharacterTypes.FindAll(e => e.Name.Equals(name)).Count > 1)
            {
                return null;
            }   
            else if (CharacterTypes.FindAll(e => e.Name.Equals(name)).Count < 1)
            {
                return null;
            }
            return CharacterTypes.Find(e => e.Name.Equals(name));
                
        }
    }
}
