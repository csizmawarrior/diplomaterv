using Antlr4.Runtime;
using LabWork1github;
using System;
using System.Collections.Generic;
using static LabWork1github.DynamicEnemyGrammarParser;

namespace LabWork1github
{
    public class Program
    {
        public static List<CharacterType> CharacterTypes = new List<CharacterType>();
        public static List<Character> Characters = new List<Character>();

        public static Board Board = new Board();
        
        public static int starterHP = 300;

        static void Main(string[] args)
        {
            //we hand in a default type for a monster and a trap, so when nothing is given, the trap or monster will get these ones' attributes
            //CharacterTypes.Add(new MonsterType("DefaultMonster"));
            //GetCharacterType("DefaultMonster").Damage = 50;
            //GetCharacterType("DefaultMonster").Health = 200;

            //CharacterTypes.Add(new TrapType("DefaultTrap"));
            //GetCharacterType("DefaultTrap").Damage = 50;


            //  MonsterTypeLoader();
            TrapTypeLoader();
            MonsterTypeLoader();
            BoardLoader();


            Game theGame = new Game();
            theGame.Init();
            theGame.Start();

            Console.ReadKey();

        }

        public static void MonsterTypeLoader()
        {
            string text = System.IO.File.ReadAllText("C:/Users/Dana/antlrworks/DefaultMonster.txt");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            DynamicEnemyGrammarLexer MonsterGrammarLexer_ = new DynamicEnemyGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(MonsterGrammarLexer_);
            DynamicEnemyGrammarParser MonsterGrammarParser = new DynamicEnemyGrammarParser(commonTokenStream);
            DynamicEnemyGrammarParser.DefinitionContext chatContext = MonsterGrammarParser.definition();
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            if (visitor.ErrorFound)
                Console.WriteLine(visitor.Error);
        }

        public static void BoardLoader()
        {
            string text = System.IO.File.ReadAllText("C:/Users/Dana/antlrworks/BoardCreation.txt");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            BoardGrammarVisitor visitor = new BoardGrammarVisitor();
            visitor.Visit(chatContext);
        }

        public static void TrapTypeLoader()
        {
            string text = System.IO.File.ReadAllText("C:/Users/Dana/antlrworks/DefaultTrap.txt");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            DynamicEnemyGrammarLexer DynamicEnemyGrammarLexer_ = new DynamicEnemyGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(DynamicEnemyGrammarLexer_);
            DynamicEnemyGrammarParser DynamicEnemyGrammarParser = new DynamicEnemyGrammarParser(commonTokenStream);
            DynamicEnemyGrammarParser.DefinitionContext chatContext = DynamicEnemyGrammarParser.definition();
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            if(visitor.ErrorFound)
                Console.WriteLine(visitor.Error);
        }

        public static CharacterType GetCharacterType(string name)
        {
            if (CharacterTypes.FindAll(e => e.Name.Equals(name)).Count > 1)
            {
                Console.WriteLine("The type "+name+" does not exist");
                return null;
            }   
            else if (CharacterTypes.FindAll(e => e.Name.Equals(name)).Count < 1)
            {
                Console.WriteLine("The type " + name + " does not exist");
                return null;
            }
            return CharacterTypes.Find(e => e.Name.Equals(name));
                
        }
    }
}
