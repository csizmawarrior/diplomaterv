using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Antlr4.Runtime;
using LabWork1github;
using LabWork1github.static_constants;
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

        //Parameter declaring errors
        [TestMethod]
        public void AssigningTrapsHealth()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; health=20;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.TRAP_HAS_NO_HEALTH + "health=20" + "\n");
        }
        [TestMethod]
        public void AssigningTrapsHealthAsCommand()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands: health=20;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.TRAP_HAS_NO_HEALTH + "health=20" + "\n");
        }
        [TestMethod]
        public void AssigningMonsterHeal()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; heal=20;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_HEAL + "heal=20" + "\n");
        }
        [TestMethod]
        public void AssigningMonsterHealAsCommand()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; commands: heal=20;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_HEAL + "heal=20" + "\n");
        }
        [TestMethod]
        public void AssigningMonsterTeleportPoint()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; teleport_place=1,1;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_TELEPORT + "teleport_place=1,1" + "\n");
        }
        [TestMethod]
        public void AssigningMonsterTeleportPointAsCommand()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; commands: teleport_place=1,1;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_TELEPORT + "teleport_place=1,1" + "\n");
        }
        [TestMethod]
        public void AssigningMonsterSpawnPoint()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; spawn_place=1,1;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_SPAWN_TO_PLACE + "spawn_place=1,1" + "\n");
        }
        [TestMethod]
        public void AssigningMonsterSpawnPointAsCommand()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; commands: spawn_place=1,1;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_SPAWN_TO_PLACE + "spawn_place=1,1" + "\n");
        }
        [TestMethod]
        public void AssigningMonsterSpawnType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; spawn_type=DefaultMonster;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_SPAWN_TYPE + "spawn_type=DefaultMonster" + "\n");
        }
        [TestMethod]
        public void AssigningMonsterSpawnTypeAsCommand()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; commands: spawn_type=DefaultMonster;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_SPAWN_TYPE + "spawn_type=DefaultMonster" + "\n");
        }


        //MoveCommand errors
        [TestMethod]
        public void AssignMonMoveCommandToPlayer()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; commands: move to player;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveToPlayer))) != null);
        }
        [TestMethod]
        public void AssignMonMoveCommandDirectionOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; commands: move B;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveDirection)) &&
                 ((MoveCommand)x).Direction.Equals(Directions.BACKWARDS) && ((MoveCommand)x).Distance == 1 ) != null);
        }
        [TestMethod]
        public void AssignMonMoveCommandDirectionAndDistance()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; commands: move R distance = 5;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveDirection)) &&
                ((MoveCommand)x).Direction.Equals(Directions.RIGHT) && ((MoveCommand)x).Distance == 5) != null);
        }
        [TestMethod]
        public void AssignMonMoveCommandRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; commands: move to random;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(visitor.MoveRandom))) != null);
        }
        [TestMethod]
        public void AssignMonMoveCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; commands: move to 1,2;");
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
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; commands: shoot F;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(visitor.ShootDirection)) &&
                 ((ShootCommand)x).Direction.Equals(Directions.FORWARD) && ((ShootCommand)x).Distance == 1) != null);
        }
        [TestMethod]
        public void AssignMonShootCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; commands: shoot to 1,2;");
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
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; damage = 50; health = 50; commands: shoot L distance = 4;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(visitor.ShootDirection)) &&
                 ((ShootCommand)x).Direction.Equals(Directions.LEFT) && ((ShootCommand)x).Distance == 4) != null);
        }
        [TestMethod]
        public void AssignMonShootDirAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; commands: shoot L damage = 40;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(visitor.ShootDirection)) &&
                 ((ShootCommand)x).Direction.Equals(Directions.LEFT) && ((ShootCommand)x).HealthChangeAmount == 40) != null);
        }
        [TestMethod]
        public void AssignMonShootCommandRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; commands: shoot random;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                 .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(visitor.ShootRandom))) != null);
        }
        [TestMethod]
        public void AssignMonShootCommandToPlayer()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; commands: shoot to player;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(visitor.ShootToPlayer))) != null);
        }
        [TestMethod]
        public void AssignMonShootCommandToPlayerAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; commands: shoot to player damage = 55;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(visitor.ShootToPlayer)) &&
                ((ShootCommand)x).HealthChangeAmount == 55 ) != null);
        }
        [TestMethod]
        public void AssignMonShootCommandDirAndDistAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; commands: shoot F distance = 5 damage = 70;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(visitor.ShootDirection)) &&
                 ((ShootCommand)x).Direction.Equals(Directions.FORWARD) && ((ShootCommand)x).HealthChangeAmount == 70 && ((ShootCommand)x).Distance == 5 ) != null);
        }
        [TestMethod]
        public void AssignMonShootCommandToPlaceAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = tesztmonster ; health = 50; commands: shoot to 3,1 damage = 100;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("tesztmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(visitor.ShootToPlace)) &&
                 ((ShootCommand)x).TargetPlace.X == 3 && ((ShootCommand)x).TargetPlace.Y == 1 && ((ShootCommand)x).HealthChangeAmount == 100 ) != null);
        }
        [TestMethod]
        public void AssignTrapDamageCommandDirectionOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands:  damage F;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageDirection)) &&
                 ((DamageCommand)x).Direction.Equals(Directions.FORWARD) && ((DamageCommand)x).Distance == 1) != null);
        }
        [TestMethod]
        public void AssignTrapDamageCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands:  damage to 1,2;");
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
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands:  damage L distance = 0;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageDirection)) &&
                 ((DamageCommand)x).Direction.Equals(Directions.LEFT) && ((DamageCommand)x).Distance == 0) != null);
        }
        [TestMethod]
        public void AssignTrapDamageDirAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands:  damage L damage = 40;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageDirection)) &&
                 ((DamageCommand)x).Direction.Equals(Directions.LEFT) && ((DamageCommand)x).HealthChangeAmount == 40) != null);
        }
        [TestMethod]
        public void AssignTrapDamageCommandRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands:  damage random;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                 .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageRandom))) != null);
        }
        [TestMethod]
        public void AssignTrapDamageCommandToPlayer()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;  commands: damage to player;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageToPlayer))) != null);
        }
        [TestMethod]
        public void AssignTrapDamageCommandToMonster()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands:  damage to monster;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageToMonster))) != null);
        }
        [TestMethod]
        public void AssignTrapDamageCommandToPlayerAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands:  damage to player damage = 55;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageToPlayer)) &&
                ((DamageCommand)x).HealthChangeAmount == 55) != null);
        }
        [TestMethod]
        public void AssignTrapDamageCommandDirAndDistAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands:  damage F distance = 0 damage = 70;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageDirection)) &&
                 ((DamageCommand)x).Direction.Equals(Directions.FORWARD) && ((DamageCommand)x).HealthChangeAmount == 70 && ((DamageCommand)x).Distance == 0) != null);
        }
        [TestMethod]
        public void AssignTrapDamageCommandToPlaceAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands:  damage to 3,1 damage = 100;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(visitor.DamageToPlace)) &&
                 ((DamageCommand)x).TargetPlace.X == 3 && ((DamageCommand)x).TargetPlace.Y == 1 && ((DamageCommand)x).HealthChangeAmount == 100) != null);
        }
        [TestMethod]
        public void AssignTrapHealCommandDirectionOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands:  heal F;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealDirection)) &&
                 ((HealCommand)x).Direction.Equals(Directions.FORWARD) && ((HealCommand)x).Distance == 1) != null);
        }
        [TestMethod]
        public void AssignTrapHealCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands:  heal to 1,2;");
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
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands:  heal L distance = 0;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealDirection)) &&
                 ((HealCommand)x).Direction.Equals(Directions.LEFT) && ((HealCommand)x).Distance == 0) != null);
        }
        [TestMethod]
        public void AssignTrapHealDirAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands:  heal L heal = 40;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealDirection)) &&
                 ((HealCommand)x).Direction.Equals(Directions.LEFT) && ((HealCommand)x).HealthChangeAmount == 40) != null);
        }
        [TestMethod]
        public void AssignTrapHealCommandRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands:  heal random;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                 .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealRandom))) != null);
        }
        [TestMethod]
        public void AssignTrapHealCommandToPlayer()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands:  heal to player;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealToPlayer))) != null);
        }
        [TestMethod]
        public void AssignTrapHealCommandToMonster()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands:  heal to monster;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealToMonster))) != null);
        }
        [TestMethod]
        public void AssignTrapHealCommandToPlayerAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands:  heal to player heal = 55;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealToPlayer)) &&
                ((HealCommand)x).HealthChangeAmount == 55) != null);
        }
        [TestMethod]
        public void AssignTrapHealCommandDirAndDistAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ;  commands: heal F distance = 2 heal = 70;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealDirection)) &&
                 ((HealCommand)x).Direction.Equals(Directions.FORWARD) && ((HealCommand)x).HealthChangeAmount == 70 && ((HealCommand)x).Distance == 2) != null);
        }
        [TestMethod]
        public void AssignTrapHealCommandToPlaceAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands:  heal to 3,1 heal = 100;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(visitor.HealToPlace)) &&
                 ((HealCommand)x).TargetPlace.X == 3 && ((HealCommand)x).TargetPlace.Y == 1 && ((HealCommand)x).HealthChangeAmount == 100) != null);
        }
        [TestMethod]
        public void AssignTrapTeleportCommandPlayerOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; teleport_place = 3,1; commands: teleport player;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(visitor.TeleportPlayer)) &&
                 ((TeleportCommand)x).TargetPlace.X == 3 && ((TeleportCommand)x).TargetPlace.Y == 1) != null);
        }
        [TestMethod]
        public void AssignTrapTeleportCommandMonsterOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; teleport_place = 2,4; commands: teleport monster;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(visitor.TeleportMonster)) &&
                 ((TeleportCommand)x).TargetPlace.X == 2 && ((TeleportCommand)x).TargetPlace.Y == 4) != null);
        }
        [TestMethod]
        public void AssignTrapTeleportCommandTrapOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; teleport_place = 3,1; commands: teleport trap;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(visitor.TeleportTrap)) &&
                 ((TeleportCommand)x).TargetPlace.X == 3 && ((TeleportCommand)x).TargetPlace.Y == 1) != null);
        }
        [TestMethod]
        public void AssignTrapTeleportCommandPlayerRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands: teleport player random;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(visitor.TeleportPlayer)) &&
                 ((TeleportCommand)x).Random) != null);
        }
        [TestMethod]
        public void AssignTrapTeleportCommandMonsterRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands: teleport monster random;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(visitor.TeleportMonster)) &&
                 ((TeleportCommand)x).Random) != null);
        }
        [TestMethod]
        public void AssignTrapTeleportCommandTrapRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands: teleport trap random;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(visitor.TeleportTrap)) &&
                 ((TeleportCommand)x).Random) != null);
        }
        [TestMethod]
        public void AssignTrapTeleportCommandPlayerToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands: teleport player to 3,1;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(visitor.TeleportPlayer)) &&
                 ((TeleportCommand)x).TargetPlace.X == 3 && ((TeleportCommand)x).TargetPlace.Y == 1) != null);
        }
        [TestMethod]
        public void AssignTrapTeleportCommandMonsterToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands: teleport monster to 1,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(visitor.TeleportMonster)) &&
                 ((TeleportCommand)x).TargetPlace.X == 1 && ((TeleportCommand)x).TargetPlace.Y == 2) != null);
        }
        [TestMethod]
        public void AssignTrapTeleportCommandTrapToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands: teleport trap to 3,3;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(visitor.TeleportTrap)) &&
                 ((TeleportCommand)x).TargetPlace.X == 3 && ((TeleportCommand)x).TargetPlace.Y == 3) != null);
        }
        [TestMethod]
        public void AssignTrapSpawnCommandTypeToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands: spawn monster DefaultMonster to 2,3;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is SpawnCommand && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(visitor.Spawn)) &&
                 ((SpawnCommand)x).TargetPlace.X == 2 && ((SpawnCommand)x).TargetPlace.Y == 3 && 
                 ((SpawnCommand)x).TargetCharacterType.Name.Equals(Types.DEFAULT_MONSTER)) != null);
        }
        [TestMethod]
        public void AssignTrapSpawnCommandRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; commands: spawn random;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is SpawnCommand && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(visitor.SpawnRandom)) ) != null);
        }
        [TestMethod]
        public void AssignTrapSpawnCommandOnlyType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; spawn_place = 1,2 ; commands: spawn monster FutureMonster;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is SpawnCommand && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(visitor.Spawn)) &&
                 ((SpawnCommand)x).TargetPlace.X == 1 && ((SpawnCommand)x).TargetPlace.Y == 2 &&
                 ((SpawnCommand)x).TargetCharacterType.Name.Equals("FutureMonster")) != null);
        }
        [TestMethod]
        public void AssignTrapSpawnCommandOnlyPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; spawn_type = UnrealMonster ; commands: spawn to 2,2;");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is SpawnCommand && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(visitor.Spawn)) &&
                 ((SpawnCommand)x).TargetPlace.X == 2 && ((SpawnCommand)x).TargetPlace.Y == 2 &&
                 ((SpawnCommand)x).TargetCharacterType.Name.Equals("UnrealMonster")) != null);
        }
        [TestMethod]
        public void AssignTrapSpawnCommandOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = teszttrap ; spawn_place = 4,1; spawn_type = UnrealMonster ; commands: spawn");
            LabWork1github.DynamicEnemyGrammarVisitor visitor = new LabWork1github.DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("teszttrap").Commands
                .Find(x => x is SpawnCommand && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(visitor.Spawn)) &&
                 ((SpawnCommand)x).TargetPlace.X == 4 && ((SpawnCommand)x).TargetPlace.Y == 1 &&
                 ((SpawnCommand)x).TargetCharacterType.Name.Equals("UnrealMonster")) != null);
        }




    }
    //TODO: pozitív tesztek, condition visitor, think about the ways to bring it forward
    //TODO: test case bonyolult eseménykezelő: "feliratkozás szörny mozgásra, pl. ha ő mozgott én is"    !!!!!!!!!!!
    //TODO: példa leírása hogyan lehetne leírni hogyan érdemes megoldani
    //TODO: ID szörnyeknek és referálni rá hogy legyen társa
    //TODO: tartalomjegyzék egyes fejezetekhez leírni hogy mi lesz benne
}
