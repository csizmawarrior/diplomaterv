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
            Program.Characters.Clear();
            Program.CharacterTypes.Clear();
            Program.Board = new Board();
            Program.TrapTypeLoader();
            Program.MonsterTypeLoader();

            AntlrInputStream inputStream = new AntlrInputStream(fileText);
            DynamicEnemyGrammarLexer DynamicEnemyGrammarLexer_ = new DynamicEnemyGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(DynamicEnemyGrammarLexer_);
            DynamicEnemyGrammarParser DynamicEnemyGrammarParser = new DynamicEnemyGrammarParser(commonTokenStream);
            DynamicEnemyGrammarParser.DefinitionContext chatContext = DynamicEnemyGrammarParser.definition();
            return chatContext;
        }
        public BoardGrammarParser.ProgramContext PreparingBoardGrammar(string fileText)
        {
            Program.Characters.Clear();
            Program.CharacterTypes.Clear();
            Program.Board = new Board();
            Program.TrapTypeLoader();
            Program.MonsterTypeLoader();

            AntlrInputStream inputStream = new AntlrInputStream(fileText);
            BoardGrammarLexer BoardGrammarLexer_ = new BoardGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(BoardGrammarLexer_);
            BoardGrammarParser BoardGrammarParser = new BoardGrammarParser(commonTokenStream);
            BoardGrammarParser.ProgramContext chatContext = BoardGrammarParser.program();
            return chatContext;
        }
        public PlayerGrammarParser.StatementContext PreparingPlayerGrammar(string fileText)
        {
            LabWork1github.Program program = new Program();

            AntlrInputStream inputStream = new AntlrInputStream(fileText);
            PlayerGrammarLexer PlayerGrammarLexer_ = new PlayerGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(PlayerGrammarLexer_);
            PlayerGrammarParser PlayerGrammarParser = new PlayerGrammarParser(commonTokenStream);
            PlayerGrammarParser.StatementContext chatContext = PlayerGrammarParser.statement();
            return chatContext;
        }

        //Declaration tests: To see that declaring works as they should

        [TestMethod]
        public void DoubleDeclareCheckSameAmountHeal()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; heal=20;heal=20;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Heal amount was already declared:" + "\n" + "heal=20" + "\n");
        }
        [TestMethod]
        public void DoubleCheckDifferentAmountHeal()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; heal=20;heal=50;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Heal amount was already declared:" + "\n" + "heal=50" + "\n");
        }
        [TestMethod]
        public void DoubleDeclareCheckSameAmountHealth()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = teszttrap ; health=20;health=20;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Health amount was already declared:" + "\n" + "health=20" + "\n");
        }
        [TestMethod]
        public void DoubleCheckDifferentAmountHealth()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = teszttrap ; health=20;health=50;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Health amount was already declared:" + "\n" + "health=50" + "\n");
        }
        [TestMethod]
        public void DoubleDeclareCheckSameAmountDamage()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; damage=20;damage=20;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Damage amount was already declared:" + "\n" + "damage=20" + "\n");
        }
        [TestMethod]
        public void DoubleCheckDifferentAmountDamage()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; damage=20;damage=50;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Damage amount was already declared:" + "\n" + "damage=50" + "\n");
        }
        [TestMethod]
        public void DoubleDeclareCheckSameAmountTeleportPoint()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; teleport_place=1,1;teleport_place=1,1;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Teleport destination was already declared:" + "\n" + "teleport_place=1,1" + "\n");
        }
        [TestMethod]
        public void DoubleCheckDifferentAmountTeleportPoint()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; teleport_place=1,1;teleport_place=2,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Teleport destination was already declared:" + "\n" + "teleport_place=2,2" + "\n");
        }
        [TestMethod]
        public void DoubleDeclareCheckSameAmountSpawnPoint()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; spawn_place=1,1;spawn_place=1,1;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Spawn destination was already declared:" + "\n" + "spawn_place=1,1" + "\n");
        }
        [TestMethod]
        public void DoubleCheckDifferentAmountSpawnPoint()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; spawn_place=1,1;spawn_place=2,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Spawn destination was already declared:" + "\n" + "spawn_place=2,2" + "\n");
        }
        [TestMethod]
        public void DoubleDeclareCheckSameAmountSpawnType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; spawn_type=DefaultMonster;spawn_type=DefaultMonster;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Spawning enemy type has been already declared:" + "\n" + "spawn_type=DefaultMonster" + "\n");
        }
        [TestMethod]
        public void DoubleCheckDifferentAmountSpawnType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; spawn_type=DefaultMonster;spawn_type=TestType;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Spawning enemy type has been already declared:" + "\n" + "spawn_type=TestType" + "\n" +
                                        "Spawning enemy type doesn't exist at place:" + "\n" + "spawn_type=TestType" + "\n");
        }
        [TestMethod]
        public void AssigningTrapsHealth()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; health=20;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "Trap doesn't have health:" + "\n" + "health=20" + "\n");
        }
        [TestMethod]
        public void AssigningMonsterHeal()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; heal=20;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "A non Trap wants to heal:" + "\n" + "heal=20" + "\n");
        }
        [TestMethod]
        public void AssigningMonsterTeleportPoint()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; teleport_place=1,1;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "A non Trap wants to teleport:" + "\n" + "teleport_place=1,1" + "\n");
        }
        [TestMethod]
        public void AssigningMonsterSpawnPoint()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; spawn_place=1,1;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "A non Trap wants to spawn:" + "\n" + "spawn_place=1,1" + "\n");
        }
        [TestMethod]
        public void AssigningMonsterSpawnType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; spawn_type=DefaultMonster;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, "A non Trap wants to spawn:" + "\n" + "spawn_type=DefaultMonster" + "\n");
        }
        [TestMethod]
        public void AssignMonsterMoveCommandToPlayer()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; move to player;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveToPlayer))) != null);
        }
        [TestMethod]
        public void AssignMonsterMoveCommandDirectionOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; move F;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveDirection)) &&
                 ((MoveCommand)x).Distance == 1 ) != null);
        }
        [TestMethod]
        public void AssignMonsterMoveCommandDirectionAndDistance()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; move F distance = 5;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveDirection)) &&
                 ((MoveCommand)x).Distance == 5) != null);
        }
        [TestMethod]
        public void AssignMonsterMoveCommandRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; move to random;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveRandom))) != null);
        }
        [TestMethod]
        public void AssignMonsterMoveCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; move to 1,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveToPlace)) &&
                 ((MoveCommand)x).TargetPlace.X == 1 && ((MoveCommand)x).TargetPlace.Y == 2 ) != null);
        }
        [TestMethod]
        public void AssignMonsterMoveCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; move to 1,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveToPlace)) &&
                 ((MoveCommand)x).TargetPlace.X == 1 && ((MoveCommand)x).TargetPlace.Y == 2) != null);
        }
        [TestMethod]
        public void AssignMonsterMoveCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; move to 1,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveToPlace)) &&
                 ((MoveCommand)x).TargetPlace.X == 1 && ((MoveCommand)x).TargetPlace.Y == 2) != null);
        }
        [TestMethod]
        public void AssignMonsterMoveCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; move to 1,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveToPlace)) &&
                 ((MoveCommand)x).TargetPlace.X == 1 && ((MoveCommand)x).TargetPlace.Y == 2) != null);
        }
        [TestMethod]
        public void AssignMonsterMoveCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; move to 1,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveToPlace)) &&
                 ((MoveCommand)x).TargetPlace.X == 1 && ((MoveCommand)x).TargetPlace.Y == 2) != null);
        }
        [TestMethod]
        public void AssignMonsterMoveCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; move to 1,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveToPlace)) &&
                 ((MoveCommand)x).TargetPlace.X == 1 && ((MoveCommand)x).TargetPlace.Y == 2) != null);
        }
        [TestMethod]
        public void AssignMonsterMoveCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; move to 1,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveToPlace)) &&
                 ((MoveCommand)x).TargetPlace.X == 1 && ((MoveCommand)x).TargetPlace.Y == 2) != null);
        }
        [TestMethod]
        public void AssignMonsterMoveCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; move to 1,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveToPlace)) &&
                 ((MoveCommand)x).TargetPlace.X == 1 && ((MoveCommand)x).TargetPlace.Y == 2) != null);
        }
        [TestMethod]
        public void AssignMonsterMoveCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; move to 1,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveToPlace)) &&
                 ((MoveCommand)x).TargetPlace.X == 1 && ((MoveCommand)x).TargetPlace.Y == 2) != null);
        }
        [TestMethod]
        public void AssignMonsterMoveCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; move to 1,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveToPlace)) &&
                 ((MoveCommand)x).TargetPlace.X == 1 && ((MoveCommand)x).TargetPlace.Y == 2) != null);
        }
    }
    //TODO: pozitív tesztek, condition visitor, think about the ways to bring it forward
    //TODO: test case bonyolult eseménykezelő: "feliratkozás szörny mozgásra, pl. ha ő mozgott én is"
    //TODO: ID szörnyeknek és referálni rá hogy legyen társa
}
