using Antlr4.Runtime;
using LabWork1github;
using System;
using System.Collections.Generic;

namespace LabWork1github
{
    class Program
    {


        //átír grammarek hogy jó legyen a place
        public static Board Board = new Board();
        public static List<MonsterType> monsterTypes = new List<MonsterType>();
        public static List<TrapType> trapTypes = new List<TrapType>();
        public static int starterHP = 300;

        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");


            MonsterTypeLoader();
            TrapTypeLoader();
            BoardLoader();

            //TODO: You shouldn't be able to put 2 of the same kind of things on top of each other in board loader
            //      or put a player on a trap by default

            Game theGame = new Game();
            theGame.Init();
            theGame.Start();
            Console.WriteLine("Hello World!");

            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }

        private static void MonsterTypeLoader()
        {
            string text = System.IO.File.ReadAllText("C:/Users/Dana/antlrworks/CloseMonster.txt");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            MonsterGrammarLexer MonsterGrammarLexer_ = new MonsterGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(MonsterGrammarLexer_);
            MonsterGrammarParser MonsterGrammarParser = new MonsterGrammarParser(commonTokenStream);
            MonsterGrammarParser.DefinitionContext chatContext = MonsterGrammarParser.definition();
            MonsterGrammarVisitor visitor = new MonsterGrammarVisitor();
            visitor.Visit(chatContext);
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
            string text = System.IO.File.ReadAllText("C:/Users/Dana/antlrworks/TrapTypes.txt");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            TrapGrammarLexer TrapGrammarLexer_ = new TrapGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(TrapGrammarLexer_);
            TrapGrammarParser TrapGrammarParser = new TrapGrammarParser(commonTokenStream);
            TrapGrammarParser.DefinitionContext chatContext = TrapGrammarParser.definition();
            TrapGrammarVisitor visitor = new TrapGrammarVisitor();
            visitor.Visit(chatContext);
        }
    }
}
