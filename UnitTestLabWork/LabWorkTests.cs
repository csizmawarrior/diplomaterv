using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LabWork1github;
using Antlr4.Runtime;
using LabWork1github.Commands;

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
        public void AssignMonMoveCommandToPlayer()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; move to player;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveToPlayer))) != null);
        }
        [TestMethod]
        public void AssignMonMoveCommandDirectionOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; move B;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveDirection)) &&
                 ((MoveCommand)x).Direction.Equals("B") && ((MoveCommand)x).Distance == 1 ) != null);
        }
        [TestMethod]
        public void AssignMonMoveCommandDirectionAndDistance()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; move R distance = 5;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveDirection)) &&
                ((MoveCommand)x).Direction.Equals("R") && ((MoveCommand)x).Distance == 5) != null);
        }
        [TestMethod]
        public void AssignMonMoveCommandRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; move to random;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveRandom))) != null);
        }
        [TestMethod]
        public void AssignMonMoveCommandToPlace()
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
        public void AssignMonShootCommandDirectionOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; shoot F;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(visitor.ShootDirection)) &&
                 ((ShootCommand)x).Direction.Equals("F") && ((ShootCommand)x).Distance == 1) != null);
        }
        [TestMethod]
        public void AssignMonShootCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; shoot to 1,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(visitor.ShootToPlace)) &&
                 ((ShootCommand)x).TargetPlace.X == 1 && ((ShootCommand)x).TargetPlace.Y == 2) != null);
        }
        [TestMethod]
        public void AssignMonShootCommandDirAndDist()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; shoot L distance = 4;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(visitor.ShootDirection)) &&
                 ((ShootCommand)x).Direction.Equals("L") && ((ShootCommand)x).Distance == 4) != null);
        }
        [TestMethod]
        public void AssignMonShootDirAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; shoot L damage = 40;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(visitor.ShootDirection)) &&
                 ((ShootCommand)x).Direction.Equals("L") && ((ShootCommand)x).Damage == 40) != null);
        }
        [TestMethod]
        public void AssignMonShootCommandRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; shoot random;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                 .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(visitor.ShootRandom))) != null);
        }
        [TestMethod]
        public void AssignMonShootCommandToPlayer()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; shoot to player;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(visitor.ShootToPlayer))) != null);
        }
        [TestMethod]
        public void AssignMonShootCommandToPlayerAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; shoot to player damage = 55;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(visitor.ShootToPlayer)) &&
                ((ShootCommand)x).Damage == 55 ) != null);
        }
        [TestMethod]
        public void AssignMonShootCommandDirAndDistAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; shoot F distance = 5 damage = 70;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(visitor.ShootDirection)) &&
                 ((ShootCommand)x).Direction.Equals("F") && ((ShootCommand)x).Damage == 70 && ((ShootCommand)x).Distance == 5 ) != null);
        }
        [TestMethod]
        public void AssignMonShootCommandToPlaceAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; shoot to 3,1 damage = 100;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(visitor.ShootToPlace)) &&
                 ((ShootCommand)x).TargetPlace.X == 3 && ((ShootCommand)x).TargetPlace.Y == 1 && ((ShootCommand)x).Damage == 100 ) != null);
        }
        [TestMethod]
        public void AssignTrapDamageCommandDirectionOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   damage F;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageDirection)) &&
                 ((DamageCommand)x).Direction.Equals("F") && ((DamageCommand)x).Distance == 1) != null);
        }
        [TestMethod]
        public void AssignTrapDamageCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   damage to 1,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageToPlace)) &&
                 ((DamageCommand)x).TargetPlace.X == 1 && ((DamageCommand)x).TargetPlace.Y == 2) != null);
        }
        [TestMethod]
        public void AssignTrapDamageCommandDirAndDist()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   damage L distance = 0;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageDirection)) &&
                 ((DamageCommand)x).Direction.Equals("L") && ((DamageCommand)x).Distance == 0) != null);
        }
        [TestMethod]
        public void AssignTrapDamageDirAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   damage L damage = 40;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageDirection)) &&
                 ((DamageCommand)x).Direction.Equals("L") && ((DamageCommand)x).Damage == 40) != null);
        }
        [TestMethod]
        public void AssignTrapDamageCommandRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   damage random;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                 .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageRandom))) != null);
        }
        [TestMethod]
        public void AssignTrapDamageCommandToPlayer()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   damage to player;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageToPlayer))) != null);
        }
        [TestMethod]
        public void AssignTrapDamageCommandToMonster()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   damage to monster;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageToMonster))) != null);
        }
        [TestMethod]
        public void AssignTrapDamageCommandToPlayerAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   damage to player damage = 55;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageToPlayer)) &&
                ((DamageCommand)x).Damage == 55) != null);
        }
        [TestMethod]
        public void AssignTrapDamageCommandDirAndDistAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   damage F distance = 0 damage = 70;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageDirection)) &&
                 ((DamageCommand)x).Direction.Equals("F") && ((DamageCommand)x).Damage == 70 && ((DamageCommand)x).Distance == 0) != null);
        }
        [TestMethod]
        public void AssignTrapDamageCommandToPlaceAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   damage to 3,1 damage = 100;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageToPlace)) &&
                 ((DamageCommand)x).TargetPlace.X == 3 && ((DamageCommand)x).TargetPlace.Y == 1 && ((DamageCommand)x).Damage == 100) != null);
        }
        [TestMethod]
        public void AssignTrapHealCommandDirectionOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   heal F;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealDirection)) &&
                 ((HealCommand)x).Direction.Equals("F") && ((HealCommand)x).Distance == 1) != null);
        }
        [TestMethod]
        public void AssignTrapHealCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   heal to 1,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealToPlace)) &&
                 ((HealCommand)x).TargetPlace.X == 1 && ((HealCommand)x).TargetPlace.Y == 2) != null);
        }
        [TestMethod]
        public void AssignTrapHealCommandDirAndDist()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   heal L distance = 0;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealDirection)) &&
                 ((HealCommand)x).Direction.Equals("L") && ((HealCommand)x).Distance == 0) != null);
        }
        [TestMethod]
        public void AssignTrapHealDirAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   heal L heal = 40;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealDirection)) &&
                 ((HealCommand)x).Direction.Equals("L") && ((HealCommand)x).HealAmount == 40) != null);
        }
        [TestMethod]
        public void AssignTrapHealCommandRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   heal random;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                 .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealRandom))) != null);
        }
        [TestMethod]
        public void AssignTrapHealCommandToPlayer()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   heal to player;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealToPlayer))) != null);
        }
        [TestMethod]
        public void AssignTrapHealCommandToMonster()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   heal to monster;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealToMonster))) != null);
        }
        [TestMethod]
        public void AssignTrapHealCommandToPlayerAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   heal to player heal = 55;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealToPlayer)) &&
                ((HealCommand)x).HealAmount == 55) != null);
        }
        [TestMethod]
        public void AssignTrapHealCommandDirAndDistAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   heal F distance = 2 heal = 70;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealDirection)) &&
                 ((HealCommand)x).Direction.Equals("F") && ((HealCommand)x).HealAmount == 70 && ((HealCommand)x).Distance == 2) != null);
        }
        [TestMethod]
        public void AssignTrapHealCommandToPlaceAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;   heal to 3,1 heal = 100;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealToPlace)) &&
                 ((HealCommand)x).TargetPlace.X == 3 && ((HealCommand)x).TargetPlace.Y == 1 && ((HealCommand)x).HealAmount == 100) != null);
        }



    }
    //TODO: pozitív tesztek, condition visitor, think about the ways to bring it forward
    //TODO: test case bonyolult eseménykezelő: "feliratkozás szörny mozgásra, pl. ha ő mozgott én is"    !!!!!!!!!!!
    //TODO: példa leírása hogyan lehetne leírni hogyan érdemes megoldani !!!!!!!!!
    //TODO: ID szörnyeknek és referálni rá hogy legyen társa
    //TODO: tartalomjegyzék egyes fejezetekhez leírni hogy mi lesz benne
}
