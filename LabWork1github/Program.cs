using Antlr4.Runtime;
using LabWork1github;
using System;
using System.Collections.Generic;

namespace LabWork1
{
    class Program
    {
        //átír grammarek hogy jó legyen a place
        public static Board Board = new Board();
        public static List<MonsterType> monsterTypes = new List<MonsterType>();
        public static List<TrapType> trapTypes = new List<TrapType>();

        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");

            string text = System.IO.File.ReadAllText("C:/Users/Dana/antlrworks/CloseMonster.txt");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            MonsterGrammarLexer MonsterGrammarLexer_ = new MonsterGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(MonsterGrammarLexer_);
            MonsterGrammarParser MonsterGrammarParser = new MonsterGrammarParser(commonTokenStream);
            MonsterGrammarParser.DefinitionContext chatContext = MonsterGrammarParser.definition();
            MonsterGrammarVisitor visitor = new MonsterGrammarVisitor();
            visitor.Visit(chatContext);

            foreach (var monster in monsterTypes)
            {
                Console.WriteLine(monster.Name);
                Console.WriteLine(monster.ShootRound);
                Console.WriteLine(monster.MoveRound);
                Console.WriteLine(monster.Range);
            }

            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
