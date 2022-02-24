using Antlr4.Runtime;
using LabWork1github;
using System;
using System.Collections.Generic;

namespace LabWork1github
{
    class Program
    {
        public static List<CharacterType> CharacterTypes = new List<CharacterType>();


        public static Board Board = new Board();
        public static List<MonsterType> monsterTypes = new List<MonsterType>();
        public static List<TrapType> trapTypes = new List<TrapType>();
        public static int starterHP = 300;

        static void Main(string[] args)
        {

            CharacterTypes.Add(new MonsterType("Default Monster"));
            GetCharacterType("Default Monster").Damage = 50;
            GetCharacterType("Default Monster").Health = 200;


            //  MonsterTypeLoader();
            BoardLoader();
            TrapTypeLoader();
            BoardLoader();

            Game theGame = new Game();
            theGame.Init();
            theGame.Start();

            Console.ReadKey();

        }

        private static void MonsterTypeLoader()
        {
            string text = System.IO.File.ReadAllText("C:/Users/Dana/antlrworks/MonsterTypes.txt");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            MonsterGrammarLexer MonsterGrammarLexer_ = new MonsterGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(MonsterGrammarLexer_);
            MonsterGrammarParser MonsterGrammarParser = new MonsterGrammarParser(commonTokenStream);
            MonsterGrammarParser.DefinitionContext chatContext = MonsterGrammarParser.definition();
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
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
            string text = System.IO.File.ReadAllText("C:/Users/Dana/antlrworks/TestTypes.txt");
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
                throw new ArgumentException("Not a valid CharacterType name");
            else if (CharacterTypes.FindAll(e => e.Name.Equals(name)).Count < 1)
                    throw new ArgumentException("Not a valid CharacterType name");
            return CharacterTypes.Find(e => e.Name.Equals(name));
                
        }
    }
}
