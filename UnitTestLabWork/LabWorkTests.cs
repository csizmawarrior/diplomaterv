using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LabWork1github;
using Antlr4.Runtime;

namespace UnitTestLabWork
{
    [TestClass]
    public class LabWorkTests
    {
        public DynamicEnemyGrammarParser.DefinitionContext PreparingEnemyGrammar(string fileText)
        {
            string text = System.IO.File.ReadAllText(fileText);
            AntlrInputStream inputStream = new AntlrInputStream(text);
            DynamicEnemyGrammarLexer DynamicEnemyGrammarLexer_ = new DynamicEnemyGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(DynamicEnemyGrammarLexer_);
            DynamicEnemyGrammarParser DynamicEnemyGrammarParser = new DynamicEnemyGrammarParser(commonTokenStream);
            DynamicEnemyGrammarParser.DefinitionContext chatContext = DynamicEnemyGrammarParser.definition();
            return chatContext;
        }
        public BoardGrammarParser.ProgramContext PreparingBoardGrammar(string fileText)
        {
            string text = System.IO.File.ReadAllText(fileText);
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            return chatContext;
        }
        public PlayerGrammarParser.StatementContext PreparingPlayerGrammar(string fileText)
        {
            AntlrInputStream inputStream = new AntlrInputStream(fileText);
            PlayerGrammarLexer PlayerGrammarLexer_ = new PlayerGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(PlayerGrammarLexer_);
            PlayerGrammarParser PlayerGrammarParser = new PlayerGrammarParser(commonTokenStream);
            PlayerGrammarParser.StatementContext chatContext = PlayerGrammarParser.statement();
            return chatContext;
        }

        [TestMethod]
        public void DoubleDeclareCheckSameAmountHeal()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; heal=20;heal=20;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Heal amount was already declared:" + "\n" + "heal=20;" + "\n");
        }
        [TestMethod]
        public void DoubleCheckDifferentAmountHeal()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; heal=20;heal=50;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Heal amount was already declared:" + "\n" + "heal=50;" + "\n");
        }
        public void DoubleDeclareCheckSameAmountHealth()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = teszttrap ; health=20;health=20;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Health amount was already declared:" + "\n" + "health=20;" + "\n");
        }
        [TestMethod]
        public void DoubleCheckDifferentAmountHealth()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = teszttrap ; health=20;health=50;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Health amount was already declared:" + "\n" + "health=50;" + "\n");
        }
        public void DoubleDeclareCheckSameAmountDamage()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; damage=20;damage=20;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Damage amount was already declared:" + "\n" + "damage=20;" + "\n");
        }
        [TestMethod]
        public void DoubleCheckDifferentAmountDamage()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; damage=20;damage=50;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Damage amount was already declared:" + "\n" + "damage=50;" + "\n");
        }
        public void DoubleDeclareCheckSameAmountTeleportPoint()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; teleport_place=1,1;teleport_place=1,1;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Teleport destination was already declared:" + "\n" + "teleport_place=1,1;" + "\n");
        }
        [TestMethod]
        public void DoubleCheckDifferentAmountTeleportPoint()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; teleport_place=1,1;teleport_place=2,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Teleport destination was already declared:" + "\n" + "teleport_place=2,2;" + "\n");
        }
        public void DoubleDeclareCheckSameAmountSpawnPoint()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; spawn_place=1,1;spawn_place=1,1;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Spawn destination was already declared:" + "\n" + "spawn_place=1,1;" + "\n");
        }
        [TestMethod]
        public void DoubleCheckDifferentAmountSpawnPoint()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; spawn_place=1,1;spawn_place=2,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Spawn destination was already declared:" + "\n" + "spawn_place=2,2;" + "\n");
        }
        public void DoubleDeclareCheckSameAmountSpawnType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; spawn_type=DefaultMonster;spawn_type=DefaultMonster;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Spawning enemy type has been already declared:" + "\n" + "spawn_type=DefaultMonster;" + "\n");
        }
        [TestMethod]
        public void DoubleCheckDifferentAmountSpawnType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; spawn_type=DefaultMonster;spawn_type=TestType;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Spawning enemy type has been already declared:" + "\n" + "spawn_place=2,2;" + "\n");
        }
        public void AssigningTrapsHealth()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; health=20;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Trap doesn't have health:" + "\n" + "health=20;" + "\n");
        }
        public void AssigningMonsterHeal()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; heal=20;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "A non Trap wants to heal:" + "\n" + "heal=20;" + "\n");
        }
        public void AssigningMonsterTeleportPoint()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; teleport_place=1,1;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "A non Trap wants to teleport:" + "\n" + "teleport_place=1,1;" + "\n");
        }
        public void AssigningMonsterSpawnPoint()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; spawn_place=1,1;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "A non Trap wants to spawn:" + "\n" + "spawn_place=1,1;" + "\n");
        }
        public void AssigningMonsterSpawnType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; spawn_type=DefaultMonster;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "A non Trap wants to spawn:" + "\n" + "spawn_type=DefaultMonster;" + "\n");
        }
    }
    //TODO: pozitív tesztek, condition visitor, think about the ways to bring it forward, move tesztek
}
