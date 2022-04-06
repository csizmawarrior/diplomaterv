using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LabWork1github;
using Antlr4.Runtime;

namespace UnitTestLabWork
{
    [TestClass]
    public class LabWorkTests
    {
        [TestMethod]
        public void DoubleDeclareCheckSameAmountHeal()
        {
            string text = System.IO.File.ReadAllText("trap name = teszttrap ; heal=20;heal=20;");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Heal amount was already declared:" + "\n" + "heal=20;" + "\n");
        }
        [TestMethod]
        public void DoubleCheckDifferentAmountHeal()
        {
            string text = System.IO.File.ReadAllText("trap name = teszttrap ; heal=20;heal=50;");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Heal amount was already declared:" + "\n" + "heal=50;" + "\n");
        }
        public void DoubleDeclareCheckSameAmountHealth()
        {
            string text = System.IO.File.ReadAllText("monster name = teszttrap ; health=20;health=20;");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Health amount was already declared:" + "\n" + "health=20;" + "\n");
        }
        [TestMethod]
        public void DoubleCheckDifferentAmountHealth()
        {
            string text = System.IO.File.ReadAllText("monster name = teszttrap ; health=20;health=50;");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Health amount was already declared:" + "\n" + "health=50;" + "\n");
        }
        public void DoubleDeclareCheckSameAmountDamage()
        {
            string text = System.IO.File.ReadAllText("trap name = teszttrap ; damage=20;damage=20;");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Damage amount was already declared:" + "\n" + "damage=20;" + "\n");
        }
        [TestMethod]
        public void DoubleCheckDifferentAmountDamage()
        {
            string text = System.IO.File.ReadAllText("trap name = teszttrap ; damage=20;damage=50;");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Damage amount was already declared:" + "\n" + "damage=50;" + "\n");
        }
        public void DoubleDeclareCheckSameAmountTeleportPoint()
        {
            string text = System.IO.File.ReadAllText("trap name = teszttrap ; teleport_place=1,1;teleport_place=1,1;");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Teleport destination was already declared:" + "\n" + "teleport_place=1,1;" + "\n");
        }
        [TestMethod]
        public void DoubleCheckDifferentAmountTeleportPoint()
        {
            string text = System.IO.File.ReadAllText("trap name = teszttrap ; teleport_place=1,1;teleport_place=2,2;");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Teleport destination was already declared:" + "\n" + "teleport_place=2,2;" + "\n");
        }
        public void DoubleDeclareCheckSameAmountSpawnPoint()
        {
            string text = System.IO.File.ReadAllText("trap name = teszttrap ; spawn_place=1,1;spawn_place=1,1;");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Spawn destination was already declared:" + "\n" + "spawn_place=1,1;" + "\n");
        }
        [TestMethod]
        public void DoubleCheckDifferentAmountSpawnPoint()
        {
            string text = System.IO.File.ReadAllText("trap name = teszttrap ; spawn_place=1,1;spawn_place=2,2;");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Spawn destination was already declared:" + "\n" + "spawn_place=2,2;" + "\n");
        }
        public void DoubleDeclareCheckSameAmountSpawnType()
        {
            string text = System.IO.File.ReadAllText("trap name = teszttrap ; spawn_type=DefaultMonster;spawn_type=DefaultMonster;");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Spawning enemy type has been already declared:" + "\n" + "spawn_type=DefaultMonster;" + "\n");
        }
        [TestMethod]
        public void DoubleCheckDifferentAmountSpawnType()
        {
            string text = System.IO.File.ReadAllText("trap name = teszttrap ; spawn_type=DefaultMonster;spawn_type=TestType;");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Spawning enemy type has been already declared:" + "\n" + "spawn_place=2,2;" + "\n");
        }
        public void AssigningTrapsHealth()
        {
            string text = System.IO.File.ReadAllText("trap name = teszttrap ; health=20;");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Trap doesn't have health:" + "\n" + "health=20;" + "\n");
        }
        public void AssigningMonsterHeal()
        {
            string text = System.IO.File.ReadAllText("monster name = tesztmonster ; heal=20;");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "A non Trap wants to heal:" + "\n" + "heal=20;" + "\n");
        }
        public void AssigningMonsterTeleportPoint()
        {
            string text = System.IO.File.ReadAllText("monster name = tesztmonster ; teleport_place=1,1;");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "A non Trap wants to teleport:" + "\n" + "teleport_place=1,1;" + "\n");
        }
        public void AssigningMonsterSpawnPoint()
        {
            string text = System.IO.File.ReadAllText("monster name = tesztmonster ; spawn_place=1,1;");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "A non Trap wants to spawn:" + "\n" + "spawn_place=1,1;" + "\n");
        }
        public void AssigningMonsterSpawnType()
        {
            string text = System.IO.File.ReadAllText("monster name = tesztmonster ; spawn_type=DefaultMonster;");
            AntlrInputStream inputStream = new AntlrInputStream(text);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(chatContext);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "A non Trap wants to spawn:" + "\n" + "spawn_type=DefaultMonster;" + "\n");
        }
    }
    //TODO: pozitív tesztek, condition visitor, think about the ways to bring it forward, move tesztek
}
