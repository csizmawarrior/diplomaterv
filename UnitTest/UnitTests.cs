using Antlr4.Runtime;
using LabWork1github;
using LabWork1github.Commands;
using LabWork1github.static_constants;
using LabWork1github.Visitors;
using NUnit.Framework;
using System;

namespace UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Program.Characters.Clear();
            Program.CharacterTypes.Clear();
            Program.Board = new Board();
            Program.TrapTypeLoader("C:/Users/Dana/antlrworks/DefaultTrap.txt");
            Program.MonsterTypeLoader("C:/Users/Dana/antlrworks/DefaultMonster.txt");
            Program.BoardLoader("C:/Users/Dana/antlrworks/BoardCreation.txt");
        }

        public DynamicEnemyGrammarParser.DefinitionContext PreparingEnemyGrammar(string fileText)
        {
            AntlrInputStream inputStream = new AntlrInputStream(fileText);
            DynamicEnemyGrammarLexer DynamicEnemyGrammarLexer_ = new DynamicEnemyGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(DynamicEnemyGrammarLexer_);
            DynamicEnemyGrammarParser DynamicEnemyGrammarParser = new DynamicEnemyGrammarParser(commonTokenStream);
            DynamicEnemyGrammarParser.DefinitionContext chatContext = DynamicEnemyGrammarParser.definition();
            return chatContext;
        }
        public BoardGrammarParser.ProgramContext PreparingBoardGrammar(string fileText)
        {
            AntlrInputStream inputStream = new AntlrInputStream(fileText);
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

        //Parameter set error tests
        [Test]
        public void CreateExistingTrapType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = DefaultTrap ; health=20;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.TRAP_TYPE_ALREADY_EXISTS + "trapname=DefaultTrap;" + "\n");
        }
        [Test]
        public void CreateExistingMonsterType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = DefaultMonster ; health=20;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.MONSTER_TYPE_ALREADY_EXISTS + "monstername=DefaultMonster;" + "\n");
        }
        [Test]
        public void AssigningTrapsHealth()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; health=20;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.TRAP_HAS_NO_HEALTH + "health=20" + "\n");
        }
        [Test]
        public void AssigningMonsterZeroHealth()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health=0;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.MONSTER_ZERO_HEALTH + "health=0" + "\n");
            Assert.AreEqual(StaticStartValues.PLACEHOLDER_HEALTH, Program.GetCharacterType("testmonster").Health);

        }
        [Test]
        public void AssigningTrapsHealthAsCommand()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: health=20;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.TRAP_HAS_NO_HEALTH + "health=20" + "\n");
        }
        [Test]
        public void AssigningMonsterHeal()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; heal=20;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_HEAL + "heal=20" + "\n");
        }
        [Test]
        public void AssigningMonsterHealAsCommand()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; commands: heal=20;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_HEAL + "heal=20" + "\n");
        }
        [Test]
        public void AssigningMonsterTeleportPoint()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; teleport_place=1,1;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_TELEPORT + "teleport_place=1,1" + "\n");
        }
        [Test]
        public void AssigningTrapTeleportPointDoubleNumbers()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; teleport_place=1.3,1.2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE + "teleport_place=1.3,1.2" + "\n", visitor.Error);
        }
        [Test]
        public void AssigningTrapTeleportPointAsCommandWithDoubleNumbers()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: teleport_place=1.3,1.2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE + "teleport_place=1.3,1.2" + "\n", visitor.Error);
        }
        [Test]
        public void AssigningMonsterTeleportPointAsCommand()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; commands: teleport_place=1,1;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_TELEPORT + "teleport_place=1,1" + "\n");
        }
        [Test]
        public void AssigningMonsterSpawnPoint()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; spawn_place=1,1;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_SPAWN_TO_PLACE + "spawn_place=1,1" + "\n");
        }
        [Test]
        public void AssigningTrapSpawnPointDoubleNumbers()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; spawn_place=1.3,1.2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE + "spawn_place=1.3,1.2" + "\n", visitor.Error);
        }
        [Test]
        public void AssigningTrapSpawnPointAsCommandWithDoubleNumbers()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: spawn_place=1.3,1.2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE + "spawn_place=1.3,1.2" + "\n", visitor.Error);
        }
        [Test]
        public void AssigningMonsterSpawnPointAsCommand()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; commands: spawn_place=1,1;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_SPAWN_TO_PLACE + "spawn_place=1,1" + "\n");
        }
        [Test]
        public void AssigningMonsterSpawnType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; spawn_type=DefaultMonster;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_SPAWN_TYPE + "spawn_type=DefaultMonster" + "\n");
        }
        [Test]
        public void AssigningMonsterSpawnTypeAsCommand()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; commands: spawn_type=DefaultMonster;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(visitor.Error, ErrorMessages.ParameterDeclarationError.ONLY_TRAP_CAN_SPAWN_TYPE + "spawn_type=DefaultMonster" + "\n");
        }

        //Parameter set happy paths
        [Test]
        public void CreateDefaultValueTrap()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreNotEqual(null, Program.GetCharacterType("testtrap"));
            Assert.IsTrue(Program.GetCharacterType("testtrap") is TrapType);
            Assert.AreEqual(StaticStartValues.STARTER_TRAP_DAMAGE, Program.GetCharacterType("testtrap").Damage);
        }
        [Test]
        public void CreateDefaultValueMonster()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; commands:");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreNotEqual(null, Program.GetCharacterType("testmonster"));
            Assert.IsTrue(Program.GetCharacterType("testmonster") is MonsterType);
            Assert.AreEqual(StaticStartValues.STARTER_MONSTER_DAMAGE, Program.GetCharacterType("testmonster").Damage);
            Assert.AreEqual(StaticStartValues.STARTER_MONSTER_HP, Program.GetCharacterType("testmonster").Health);
        }
        [Test]
        public void AssignMonsterHealthOnce()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 5.50; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreEqual(5.50, Program.GetCharacterType("testmonster").Health);
        }
        [Test]
        public void AssignMonsterHealthTwice()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; health = 30; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreEqual(30, Program.GetCharacterType("testmonster").Health);
        }
        [Test]
        public void AssignMonsterHealthAsCommand()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; commands: health = 30; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is NumberParameterDeclareCommand && ((NumberParameterDeclareCommand)x).Number == 30
                && ((NumberParameterDeclareCommand)x).NumberParameterDeclareDelegate.Equals(
                    new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.HealthChange))) != null);
        }
        [Test]
        public void AssignMonsterHealthAsCommandAndParameter()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 30; commands: health = 3.33; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreEqual(30, Program.GetCharacterType("testmonster").Health);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is NumberParameterDeclareCommand && ((NumberParameterDeclareCommand)x).Number == 3.33
                && ((NumberParameterDeclareCommand)x).NumberParameterDeclareDelegate.Equals(
                    new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.HealthChange))) != null);
        }
        [Test]
        public void AssignMonsterHealthAsCommandTwice()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; commands: health = 30; health = 50; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is NumberParameterDeclareCommand && ((NumberParameterDeclareCommand)x).Number == 30
                && ((NumberParameterDeclareCommand)x).NumberParameterDeclareDelegate.Equals(
                    new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.HealthChange))) != null);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is NumberParameterDeclareCommand && ((NumberParameterDeclareCommand)x).Number == 50
                && ((NumberParameterDeclareCommand)x).NumberParameterDeclareDelegate.Equals(
                    new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.HealthChange))) != null);
            Assert.AreEqual(2, Program.GetCharacterType("testmonster").Commands.Count);
        }
        [Test]
        public void AssignMonsterDamageOnce()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; damage = 5.50; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreEqual(5.50, Program.GetCharacterType("testmonster").Damage);
        }
        [Test]
        public void AssignMonsterDamageTwice()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; damage = 50; damage = 30; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreEqual(30, Program.GetCharacterType("testmonster").Damage);
        }
        [Test]
        public void AssignMonsterDamageAsCommand()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; commands: damage = 30; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is NumberParameterDeclareCommand && ((NumberParameterDeclareCommand)x).Number == 30
                && ((NumberParameterDeclareCommand)x).NumberParameterDeclareDelegate.Equals(
                    new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.DamageChange))) != null);
        }
        [Test]
        public void AssignMonsterDamageAsCommandAndParameter()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; damage = 30; commands: damage = 3.33; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreEqual(30, Program.GetCharacterType("testmonster").Damage);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is NumberParameterDeclareCommand && ((NumberParameterDeclareCommand)x).Number == 3.33
                && ((NumberParameterDeclareCommand)x).NumberParameterDeclareDelegate.Equals(
                    new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.DamageChange))) != null);
        }
        [Test]
        public void AssignMonsterDamageAsCommandTwice()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; commands: damage = 30; damage = 50; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is NumberParameterDeclareCommand && ((NumberParameterDeclareCommand)x).Number == 30
                && ((NumberParameterDeclareCommand)x).NumberParameterDeclareDelegate.Equals(
                    new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.DamageChange))) != null);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is NumberParameterDeclareCommand && ((NumberParameterDeclareCommand)x).Number == 50
                && ((NumberParameterDeclareCommand)x).NumberParameterDeclareDelegate.Equals(
                    new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.DamageChange))) != null);
            Assert.AreEqual(2, Program.GetCharacterType("testmonster").Commands.Count);
        }
        [Test]
        public void AssignTrapHealOnce()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 50; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreEqual(50, Program.GetCharacterType("testtrap").Heal);
        }
        [Test]
        public void AssignTrapHealTwice()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 5.50; heal = 30; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreEqual(30, Program.GetCharacterType("testtrap").Heal);
        }
        [Test]
        public void AssignTrapHealAsCommand()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: heal = 3.33; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is NumberParameterDeclareCommand && ((NumberParameterDeclareCommand)x).Number == 3.33
                && ((NumberParameterDeclareCommand)x).NumberParameterDeclareDelegate.Equals(
                    new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.HealChange))) != null);
        }
        [Test]
        public void AssignTrapHealAsCommandAndParameter()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30; commands: heal = 30; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreEqual(30, Program.GetCharacterType("testtrap").Heal);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is NumberParameterDeclareCommand && ((NumberParameterDeclareCommand)x).Number == 30
                && ((NumberParameterDeclareCommand)x).NumberParameterDeclareDelegate.Equals(
                    new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.HealChange))) != null);
        }
        [Test]
        public void AssignTrapHealAsCommandTwice()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: heal = 30; heal = 50; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is NumberParameterDeclareCommand && ((NumberParameterDeclareCommand)x).Number == 30
                && ((NumberParameterDeclareCommand)x).NumberParameterDeclareDelegate.Equals(
                    new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.HealChange))) != null);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is NumberParameterDeclareCommand && ((NumberParameterDeclareCommand)x).Number == 50
                && ((NumberParameterDeclareCommand)x).NumberParameterDeclareDelegate.Equals(
                    new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.HealChange))) != null);
            Assert.AreEqual(2, Program.GetCharacterType("testtrap").Commands.Count);
        }
        [Test]
        public void AssignTrapDamageOnce()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; damage = 50; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreEqual(50, Program.GetCharacterType("testtrap").Damage);
        }
        [Test]
        public void AssignTrapDamageTwice()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; damage = 5.50; damage = 30; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreEqual(30, Program.GetCharacterType("testtrap").Damage);
        }
        [Test]
        public void AssignTrapDamageAsCommand()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: damage = 3.33; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is NumberParameterDeclareCommand && ((NumberParameterDeclareCommand)x).Number == 3.33
                && ((NumberParameterDeclareCommand)x).NumberParameterDeclareDelegate.Equals(
                    new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.DamageChange))) != null);
        }
        [Test]
        public void AssignTrapDamageAsCommandAndParameter()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; damage = 30; commands: damage = 30; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreEqual(30, Program.GetCharacterType("testtrap").Damage);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is NumberParameterDeclareCommand && ((NumberParameterDeclareCommand)x).Number == 30
                && ((NumberParameterDeclareCommand)x).NumberParameterDeclareDelegate.Equals(
                    new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.DamageChange))) != null);
        }
        [Test]
        public void AssignTrapDamageAsCommandTwice()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: damage = 30; damage = 50; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is NumberParameterDeclareCommand && ((NumberParameterDeclareCommand)x).Number == 30
                && ((NumberParameterDeclareCommand)x).NumberParameterDeclareDelegate.Equals(
                    new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.DamageChange))) != null);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is NumberParameterDeclareCommand && ((NumberParameterDeclareCommand)x).Number == 50
                && ((NumberParameterDeclareCommand)x).NumberParameterDeclareDelegate.Equals(
                    new NumberParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.DamageChange))) != null);
            Assert.AreEqual(2, Program.GetCharacterType("testtrap").Commands.Count);
        }
        [Test]
        public void AssignTrapTeleportPlaceOnce()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; teleport_place = 2,2; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").TeleportPlace.X);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").TeleportPlace.Y);
        }
        [Test]
        public void AssignTrapTeleportPlaceTwice()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; teleport_place = 3,4 ; teleport_place = 2,2; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").TeleportPlace.X);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").TeleportPlace.Y);
        }
        [Test]
        public void AssignTrapTeleportPlaceAsCommand()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: teleport_place = 3, 1 ; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is PlaceParameterDeclareCommand && ((PlaceParameterDeclareCommand)x).Place.X == 2
                && ((PlaceParameterDeclareCommand)x).Place.Y == 0
                && ((PlaceParameterDeclareCommand)x).PlaceParameterDeclareDelegate.Equals(
                    new PlaceParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportPlaceChange))) != null);
        }
        [Test]
        public void AssignTrapTeleportPlaceAsCommandAndParameter()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; teleport_place = 1 , 1 ; commands: teleport_place = 3, 1 ; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is PlaceParameterDeclareCommand && ((PlaceParameterDeclareCommand)x).Place.X == 2
                && ((PlaceParameterDeclareCommand)x).Place.Y == 0
                && ((PlaceParameterDeclareCommand)x).PlaceParameterDeclareDelegate.Equals(
                    new PlaceParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportPlaceChange))) != null);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").TeleportPlace.X);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").TeleportPlace.Y);
        }
        [Test]
        public void AssignTrapTeleportPlaceAsCommandTwice()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: teleport_place = 3, 1 ; teleport_place = 2, 2 ; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is PlaceParameterDeclareCommand && ((PlaceParameterDeclareCommand)x).Place.X == 2
                && ((PlaceParameterDeclareCommand)x).Place.Y == 0
                && ((PlaceParameterDeclareCommand)x).PlaceParameterDeclareDelegate.Equals(
                    new PlaceParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportPlaceChange))) != null);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is PlaceParameterDeclareCommand && ((PlaceParameterDeclareCommand)x).Place.X == 1
                && ((PlaceParameterDeclareCommand)x).Place.Y == 1
                && ((PlaceParameterDeclareCommand)x).PlaceParameterDeclareDelegate.Equals(
                    new PlaceParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportPlaceChange))) != null);
            Assert.AreEqual(2, Program.GetCharacterType("testtrap").Commands.Count);
        }
        [Test]
        public void AssignTrapSpawnPlaceOnce()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; spawn_place = 2,2; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").SpawnPlace.X);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").SpawnPlace.Y);
        }
        [Test]
        public void AssignTrapSpawnPlaceTwice()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; spawn_place = 3,4 ; spawn_place = 2,2; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").SpawnPlace.X);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").SpawnPlace.Y);
        }
        [Test]
        public void AssignTrapSpawnPlaceAsCommand()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: spawn_place = 3, 1 ; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is PlaceParameterDeclareCommand && ((PlaceParameterDeclareCommand)x).Place.X == 2
                && ((PlaceParameterDeclareCommand)x).Place.Y == 0
                && ((PlaceParameterDeclareCommand)x).PlaceParameterDeclareDelegate.Equals(
                    new PlaceParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.SpawnPlaceChange))) != null);
        }
        [Test]
        public void AssignTrapSpawnPlaceAsCommandAndParameter()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; spawn_place = 1 , 1 ; commands: spawn_place = 3, 1 ; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is PlaceParameterDeclareCommand && ((PlaceParameterDeclareCommand)x).Place.X == 2
                && ((PlaceParameterDeclareCommand)x).Place.Y == 0
                && ((PlaceParameterDeclareCommand)x).PlaceParameterDeclareDelegate.Equals(
                    new PlaceParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.SpawnPlaceChange))) != null);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").SpawnPlace.X);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").SpawnPlace.Y);
        }
        [Test]
        public void AssignTrapSpawnPlaceAsCommandTwice()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: spawn_place = 3, 1 ; spawn_place = 2, 2 ; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is PlaceParameterDeclareCommand && ((PlaceParameterDeclareCommand)x).Place.X == 2
                && ((PlaceParameterDeclareCommand)x).Place.Y == 0
                && ((PlaceParameterDeclareCommand)x).PlaceParameterDeclareDelegate.Equals(
                    new PlaceParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.SpawnPlaceChange))) != null);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is PlaceParameterDeclareCommand && ((PlaceParameterDeclareCommand)x).Place.X == 1
                && ((PlaceParameterDeclareCommand)x).Place.Y == 1
                && ((PlaceParameterDeclareCommand)x).PlaceParameterDeclareDelegate.Equals(
                    new PlaceParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.SpawnPlaceChange))) != null);
            Assert.AreEqual(2, Program.GetCharacterType("testtrap").Commands.Count);
        }
        [Test]
        public void AssignTrapSpawnTypeOnce()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; spawn_type = DefaultMonster; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreEqual("DefaultMonster", Program.GetCharacterType("testtrap").SpawnType.Name);
        }
        [Test]
        public void AssignTrapSpawnTypeTwice()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; spawn_type = DefaultMonsterOnce; spawn_type = DefaultMonsterTwice;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.AreEqual("DefaultMonsterTwice", Program.GetCharacterType("testtrap").SpawnType.Name);
        }
        [Test]
        public void AssignTrapSpawnTypeAsCommand()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: spawn_type = testmonster ; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TypeParameterDeclareCommand 
                && ((TypeParameterDeclareCommand)x).CharacterType.Name.Equals("testmonster")
                && ((TypeParameterDeclareCommand)x).TypeParameterDeclareDelegate.Equals(
                    new TypeParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.SpawnTypeChange))) != null);
        }
        [Test]
        public void AssignTrapSpawnTypeAsCommandAndParameter()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; spawn_type = DefaultMonster; commands: spawn_type = testmonster ; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TypeParameterDeclareCommand
                && ((TypeParameterDeclareCommand)x).CharacterType.Name.Equals("testmonster")
                && ((TypeParameterDeclareCommand)x).TypeParameterDeclareDelegate.Equals(
                    new TypeParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.SpawnTypeChange))) != null);
            Assert.AreEqual("DefaultMonster", Program.GetCharacterType("testtrap").SpawnType.Name);
        }
        [Test]
        public void AssignTrapSpawnTypeAsCommandTwice()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: spawn_type = testmonster ; spawn_type = testmonstertwice ; ");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TypeParameterDeclareCommand
                && ((TypeParameterDeclareCommand)x).CharacterType.Name.Equals("testmonster")
                && ((TypeParameterDeclareCommand)x).TypeParameterDeclareDelegate.Equals(
                    new TypeParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.SpawnTypeChange))) != null);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TypeParameterDeclareCommand
                && ((TypeParameterDeclareCommand)x).CharacterType.Name.Equals("testmonstertwice")
                && ((TypeParameterDeclareCommand)x).TypeParameterDeclareDelegate.Equals(
                    new TypeParameterDeclareDelegate(DynamicEnemyGrammarVisitorDelegates.SpawnTypeChange))) != null);
            Assert.AreEqual(2, Program.GetCharacterType("testtrap").Commands.Count);
        }

        //Command error tests
        [Test]
        public void AssignMonsterMoveCommandDoubleDistance()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: move F distance = 3.1;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(DynamicEnemyGrammarVisitorDelegates.MoveDirection)) 
                    && ((MoveCommand)x).Distance == StaticStartValues.STARTER_DISTANCE) != null);
            Assert.AreEqual(ErrorMessages.ParseError.UNABLE_TO_PARSE_INT +"moveFdistance=3.1\n",visitor.Error);
        }
        [Test]
        public void AssignMonsterMoveCommandZeroDistance()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: move F distance = 0;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(DynamicEnemyGrammarVisitorDelegates.MoveDirection))
                    && ((MoveCommand)x).Distance == StaticStartValues.STARTER_DISTANCE) != null);
            Assert.AreEqual(ErrorMessages.DistanceError.ZERO_DISTANCE + "moveFdistance=0\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterMoveCommandWrongDirection()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: move J distance = 2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testmonster").Commands.Count);
        }
        [Test]
        public void AssignMonsterMoveCommandDoublePlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: move to 1.3,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(DynamicEnemyGrammarVisitorDelegates.MoveToPlace))
                    && ((MoveCommand)x).TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE)) != null);
            Assert.AreEqual(ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE + "moveto1.3,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapShootCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: shoot to 1,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ShootError.ONLY_MONSTER_CAN_SHOOT + "shootto1,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapShootCommandToPlayer()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: shoot to player;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ShootError.ONLY_MONSTER_CAN_SHOOT + "shoottoplayer" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterShootCommandDoubleDistance()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: shoot F distance = 3.1;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is ShootCommand command && command.ShootDelegate.Equals(new ShootDelegate(DynamicEnemyGrammarVisitorDelegates.ShootDirection))
                    && command.Distance == StaticStartValues.STARTER_DISTANCE) != null);
            Assert.AreEqual(ErrorMessages.ParseError.UNABLE_TO_PARSE_INT + "Fdistance=3.1\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterShootCommandZeroDistance()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: shoot F distance = 0;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is ShootCommand command && command.ShootDelegate.Equals(new ShootDelegate(DynamicEnemyGrammarVisitorDelegates.ShootDirection))
                    && command.Distance == StaticStartValues.STARTER_DISTANCE) != null);
            Assert.AreEqual(ErrorMessages.DistanceError.ZERO_DISTANCE + "Fdistance=0\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterShootCommandDoublePlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: shoot to 1.3,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is ShootCommand command && command.ShootDelegate.Equals(new ShootDelegate(DynamicEnemyGrammarVisitorDelegates.ShootToPlace))
                    && command.TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE)) != null);
            Assert.AreEqual(ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE + "to1.3,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterShootCommandToTrap()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: shoot to trap;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is ShootCommand command && ((ShootCommand)x).ShootDelegate == null) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.HealthChangeError.CHARACTER_HAS_NO_HEALTH + "totrap" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterShootCommandToPartner()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: shoot to partner;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is ShootCommand command && ((ShootCommand)x).ShootDelegate == null) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.HealthChangeError.CHARACTER_HAS_NO_HEALTH + "topartner" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterShootCommandToMe()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: shoot to me;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is ShootCommand command && ((ShootCommand)x).ShootDelegate == null) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.HealthChangeError.CHARACTER_HAS_NO_HEALTH + "tome" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterShootCommandToMonster()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: shoot to monster;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is ShootCommand command && ((ShootCommand)x).ShootDelegate == null) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.ShootError.MONSTER_CANNOT_BE_SHOT + "tomonster" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterTeleportCommandMonster()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: teleport monster;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is TeleportCommand command && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportMonster))) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.TeleportError.ONLY_TRAP_CAN_TELEPORT + "teleportmonster" + "\n" +
                                ErrorMessages.TeleportError.TELEPORTING_WITHOUT_PLACE_GIVEN + "teleportmonster" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterTeleportCommandMonsterToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: teleport monster to 1,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is TeleportCommand command && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportMonster))
                            && ((TeleportCommand)x).TargetPlace.X == 0 && ((TeleportCommand)x).TargetPlace.Y == 1) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.TeleportError.ONLY_TRAP_CAN_TELEPORT + "teleportmonsterto1,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterTeleportCommandMonsterToRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: teleport monster random;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is TeleportCommand command && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportMonster))
                            && ((TeleportCommand)x).Random) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.TeleportError.ONLY_TRAP_CAN_TELEPORT + "teleportmonsterrandom" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterTeleportCommandMonsterToDoublePlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: teleport monster to 1.3,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is TeleportCommand command && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportMonster))
                            && ((TeleportCommand)x).TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE)) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.TeleportError.ONLY_TRAP_CAN_TELEPORT + "teleportmonsterto1.3,2" + "\n" +
                                ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE + "teleportmonsterto1.3,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterTeleportCommandMe()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: teleport me;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is TeleportCommand command && ((TeleportCommand)x).TeleportDelegate == null) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.TeleportError.ONLY_TRAP_CAN_TELEPORT + "teleportme" + "\n" +
                                ErrorMessages.TeleportError.TELEPORTING_WITHOUT_PLACE_GIVEN + "teleportme" + "\n" +
                                ErrorMessages.TeleportError.TRYING_TO_TELEPORT_YOURSELF + "teleportme" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterTeleportCommandMeToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: teleport me to 1,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is TeleportCommand command && ((TeleportCommand)x).TeleportDelegate == null
                            && ((TeleportCommand)x).TargetPlace.X == 0 && ((TeleportCommand)x).TargetPlace.Y == 1) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.TeleportError.ONLY_TRAP_CAN_TELEPORT + "teleportmeto1,2" + "\n" +
                                ErrorMessages.TeleportError.TRYING_TO_TELEPORT_YOURSELF + "teleportmeto1,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterTeleportCommandMeToDoublePlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: teleport me to 1.3,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is TeleportCommand command && ((TeleportCommand)x).TeleportDelegate == null
                            && ((TeleportCommand)x).TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE)) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.TeleportError.ONLY_TRAP_CAN_TELEPORT + "teleportmeto1.3,2" + "\n" +
                                ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE + "teleportmeto1.3,2" + "\n" +
                                ErrorMessages.TeleportError.TRYING_TO_TELEPORT_YOURSELF + "teleportmeto1.3,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapTeleportCommandMonsterNoPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: teleport monster;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TeleportCommand command && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportMonster))) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.TeleportError.TELEPORTING_WITHOUT_PLACE_GIVEN + "teleportmonster" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapTeleportCommandMonsterDoublePlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: teleport monster to 1.3,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TeleportCommand command && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportMonster))) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE + "teleportmonsterto1.3,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapTeleportCommandMeToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: teleport me to 1,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TeleportCommand command && ((TeleportCommand)x).TeleportDelegate == null &&
                        ((TeleportCommand)x).TargetPlace.X == 0 && ((TeleportCommand)x).TargetPlace.Y == 1) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.TeleportError.TRYING_TO_TELEPORT_YOURSELF + "teleportmeto1,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapTeleportCommandMeDoublePlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: teleport me to 1.3,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TeleportCommand command && ((TeleportCommand)x).TeleportDelegate == null) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE + "teleportmeto1.3,2" + "\n" +
                                ErrorMessages.TeleportError.TRYING_TO_TELEPORT_YOURSELF + "teleportmeto1.3,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapTeleportCommandMeNoPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: teleport me;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TeleportCommand command && ((TeleportCommand)x).TeleportDelegate == null) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.TeleportError.TELEPORTING_WITHOUT_PLACE_GIVEN + "teleportme" + "\n" +
                                ErrorMessages.TeleportError.TRYING_TO_TELEPORT_YOURSELF + "teleportme" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterSpawnCommandMonster()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: spawn;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is SpawnCommand command && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn))) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.SpawnError.ONLY_TRAP_CAN_SPAWN + "spawn" + "\n" +
                                ErrorMessages.SpawnError.SPAWN_WITHOUT_PLACE_GIVEN + "spawn" + "\n" +
                                ErrorMessages.SpawnError.SPAWN_WITHOUT_TYPE_GIVEN + "spawn" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterSpawnCommandRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: spawn random;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is SpawnCommand command && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.SpawnRandom))) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.SpawnError.ONLY_TRAP_CAN_SPAWN + "spawnrandom" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterSpawnCommandMonsterWithType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: spawn monster DefaultMonster;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is SpawnCommand command && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn))
                    && ((SpawnCommand)x).TargetCharacterType.Name.Equals("DefaultMonster")) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.SpawnError.ONLY_TRAP_CAN_SPAWN + "spawnmonsterDefaultMonster" + "\n" +
                                ErrorMessages.SpawnError.SPAWN_WITHOUT_PLACE_GIVEN + "spawnmonsterDefaultMonster" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterSpawnCommandMonsterWithTypeToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: spawn monster DefaultMonster to 1,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is SpawnCommand command && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn))
                    && ((SpawnCommand)x).TargetCharacterType.Name.Equals("DefaultMonster") &&
                    ((SpawnCommand)x).TargetPlace.Equals(new Place(0,1))) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.SpawnError.ONLY_TRAP_CAN_SPAWN + "spawnmonsterDefaultMonsterto1,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterSpawnCommandMonsterToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: spawn to 1,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is SpawnCommand command && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn))
                    && ((SpawnCommand)x).TargetPlace.Equals(new Place(0, 1))) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.SpawnError.ONLY_TRAP_CAN_SPAWN + "spawnto1,2" + "\n" +
                        ErrorMessages.SpawnError.SPAWN_WITHOUT_TYPE_GIVEN + "spawnto1,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterSpawnCommandMonsterToDoublePlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: spawn to 1.3,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is SpawnCommand command && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn))
                    && ((SpawnCommand)x).TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE)) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.SpawnError.ONLY_TRAP_CAN_SPAWN + "spawnto1.3,2" + "\n" +
                        ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE + "spawnto1.3,2" + "\n" +
                        ErrorMessages.SpawnError.SPAWN_WITHOUT_PLACE_GIVEN + "spawnto1.3,2" + "\n" +
                        ErrorMessages.SpawnError.SPAWN_WITHOUT_TYPE_GIVEN + "spawnto1.3,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapSpawnCommandWithNothing()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: spawn;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is SpawnCommand command && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn))) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.SpawnError.SPAWN_WITHOUT_PLACE_GIVEN + "spawn" + "\n" +
                                ErrorMessages.SpawnError.SPAWN_WITHOUT_TYPE_GIVEN + "spawn" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapSpawnCommandWithOnlyPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: spawn to 1,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is SpawnCommand command && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn))
                        && ((SpawnCommand)x).TargetPlace.Equals(new Place(0,1))) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.SpawnError.SPAWN_WITHOUT_TYPE_GIVEN + "spawnto1,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapSpawnCommandWithOnlyPlaceAsAttribute()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; spawn_place = 1,2 ; commands: spawn;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is SpawnCommand command && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn))
                        && ((SpawnCommand)x).TargetPlace.Equals(new Place(0, 1))) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.SpawnError.SPAWN_WITHOUT_TYPE_GIVEN + "spawn" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapSpawnCommandWithOnlyDoublePlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; ; commands: spawn to 1.3,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is SpawnCommand command && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn))
                        && ((SpawnCommand)x).TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE)) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE + "spawnto1.3,2" + "\n" +
                        ErrorMessages.SpawnError.SPAWN_WITHOUT_PLACE_GIVEN + "spawnto1.3,2" + "\n" +
                        ErrorMessages.SpawnError.SPAWN_WITHOUT_TYPE_GIVEN + "spawnto1.3,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapSpawnCommandWithOnlyType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: spawn monster SpawnMonster;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is SpawnCommand command && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn))
                        && ((SpawnCommand)x).TargetCharacterType.Name.Equals("SpawnMonster")) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.SpawnError.SPAWN_WITHOUT_PLACE_GIVEN + "spawnmonsterSpawnMonster" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapSpawnCommandWithOnlyTypeAsAttribute()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; spawn_type = SpawnTypeMonster ; commands: spawn;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is SpawnCommand command && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn))
                        && ((SpawnCommand)x).TargetCharacterType.Name.Equals("SpawnTypeMonster")) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.SpawnError.SPAWN_WITHOUT_PLACE_GIVEN + "spawn" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterDamageCommandToDoublePlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: damage to 1.3,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is DamageCommand command && command.DamageDelegate.Equals(new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageToPlace))
                    && command.TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE)) != null);
            Assert.AreEqual(ErrorMessages.DamageError.ONLY_TRAP_CAN_DAMAGE + "damageto1.3,2" + "\n" +
                        ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE + "to1.3,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterDamageCommandToPlayer()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: damage to player;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is DamageCommand command && command.DamageDelegate.Equals(new DamageDelegate(
                            DynamicEnemyGrammarVisitorDelegates.DamageToPlayer))) != null);
            Assert.AreEqual(ErrorMessages.DamageError.ONLY_TRAP_CAN_DAMAGE + "damagetoplayer" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterDamageCommandRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: damage random;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is DamageCommand command && command.DamageDelegate.Equals(new DamageDelegate(
                            DynamicEnemyGrammarVisitorDelegates.DamageRandom))) != null);
            Assert.AreEqual(ErrorMessages.DamageError.ONLY_TRAP_CAN_DAMAGE + "damagerandom" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapDamageDirectionDoubleDistance()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: damage F distance = 3.1;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is DamageCommand command && ((DamageCommand)x).DamageDelegate.Equals(
                        new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageDirection))
                        && ((DamageCommand)x).Distance == StaticStartValues.STARTER_DISTANCE
                        && ((DamageCommand)x).Direction.Equals("F")) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ParseError.UNABLE_TO_PARSE_INT + "Fdistance=3.1" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapDamageDirectionZeroDistance()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: damage F distance = 0 damage = 2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is DamageCommand command && ((DamageCommand)x).DamageDelegate.Equals(
                        new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageDirection))
                        && ((DamageCommand)x).Distance == StaticStartValues.STARTER_DISTANCE
                        && ((DamageCommand)x).Direction.Equals("F")) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.DistanceError.ZERO_DISTANCE + "Fdistance=0damage=2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapDamageToDoublePlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: damage to 1.3,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is DamageCommand command && ((DamageCommand)x).DamageDelegate.Equals(
                        new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageToPlace))
                        && ((DamageCommand)x).TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE)) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE + "to1.3,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapDamageToDoublePlaceDamageGiven()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: damage to 1.3,2 damage = 30;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is DamageCommand command && ((DamageCommand)x).DamageDelegate.Equals(
                        new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageToPlace))
                        && ((DamageCommand)x).TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE)
                        && ((DamageCommand)x).HealthChangeAmount == 30) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE + "to1.3,2damage=30" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapDamageToTrap()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: damage to trap;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is DamageCommand command) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.HealthChangeError.CHARACTER_HAS_NO_HEALTH + "totrap" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapDamageToMeTypeDefinedDamage()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; damage = 30 ; commands: damage to me;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is DamageCommand command && ((DamageCommand)x).HealthChangeAmount == 30) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.HealthChangeError.CHARACTER_HAS_NO_HEALTH + "tome" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterHeaCommandToDoublePlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: heal to 1.3,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is HealCommand command && command.HealDelegate.Equals(new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealToPlace))
                    && command.TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE)) != null);
            Assert.AreEqual(ErrorMessages.HealError.ONLY_TRAP_CAN_HEAL + "healto1.3,2" + "\n" +
                        ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE + "to1.3,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterHealCommandToPlayer()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: heal to player;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is HealCommand command && command.HealDelegate.Equals(new HealDelegate(
                            DynamicEnemyGrammarVisitorDelegates.HealToPlayer))) != null);
            Assert.AreEqual(ErrorMessages.HealError.ONLY_TRAP_CAN_HEAL + "healtoplayer" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterHealCommandRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: heal random;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is HealCommand command && command.HealDelegate.Equals(new HealDelegate(
                            DynamicEnemyGrammarVisitorDelegates.HealRandom))) != null);
            Assert.AreEqual(ErrorMessages.HealError.ONLY_TRAP_CAN_HEAL + "healrandom" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapHealDirectionDoubleDistance()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: heal F distance = 3.1;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is HealCommand command && ((HealCommand)x).HealDelegate.Equals(
                        new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealDirection))
                        && ((HealCommand)x).Distance == StaticStartValues.STARTER_DISTANCE
                        && ((HealCommand)x).Direction.Equals("F")) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ParseError.UNABLE_TO_PARSE_INT + "Fdistance=3.1" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapHealDirectionZeroDistance()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: heal F distance = 0 heal = 2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is HealCommand command && ((HealCommand)x).HealDelegate.Equals(
                        new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealDirection))
                        && ((HealCommand)x).Distance == StaticStartValues.STARTER_DISTANCE
                        && ((HealCommand)x).Direction.Equals("F")) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.DistanceError.ZERO_DISTANCE + "Fdistance=0heal=2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapHealToDoublePlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: heal to 1.3,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is HealCommand command && ((HealCommand)x).HealDelegate.Equals(
                        new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealToPlace))
                        && ((HealCommand)x).TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE)) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE + "to1.3,2" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapHealToDoublePlaceHealGiven()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: heal to 1.3,2 heal = 30;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is HealCommand command && ((HealCommand)x).HealDelegate.Equals(
                        new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealToPlace))
                        && ((HealCommand)x).TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE)
                        && ((HealCommand)x).HealthChangeAmount == 30) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE + "to1.3,2heal=30" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapHealToTrap()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: heal to trap;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is HealCommand command) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.HealthChangeError.CHARACTER_HAS_NO_HEALTH + "totrap" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapHealToMeTypeDefinedHeal()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: heal to me;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is HealCommand command && ((HealCommand)x).HealthChangeAmount == 30) != null);
            Assert.AreEqual(1, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.HealthChangeError.CHARACTER_HAS_NO_HEALTH + "tome" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapIfCommandCompareNumberToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: if(3 == me.place){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSION_COMPARED_WITH_NUMBER
                                + "3==me.place" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapIfCommandNumCompareNumberToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: if(3 < me.place){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSION_HANDLED_AS_NUMBER
                                + "3<me.place" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapIfCommandCompareNumberToABbsoluteType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: if( 1 != Abs(monster.type) ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSION_HANDLED_AS_NUMBER
                            + "Abs(monster.type)" + "\n" + ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSIONS_HANDLED_AS_NUMBER
                                + "1!=Abs(monster.type)" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapIfCommandCompareSpawnTypeToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: if( me.spawn_type != monster.place ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.TYPE_COMPARED_WITH_OTHER_ATTRIBUTE
                            + "me.spawn_type!=monster.place" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapIfCommandCompareTypeToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: if( me.type != monster.place ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.TYPE_COMPARED_WITH_OTHER_ATTRIBUTE
                            + "me.type!=monster.place" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapIfCommandComparePlaceToName()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: if( me.place != monster.name ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.PLACE_COMPARED_WITH_OTHER_ATTRIBUTE
                            + "me.place!=monster.name" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapIfCommandCompareTeleportPlaceToName()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: if( me.teleport_place != monster.name ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.PLACE_COMPARED_WITH_OTHER_ATTRIBUTE
                            + "me.teleport_place!=monster.name" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterIfCommandCompareSpawnPlaceToName()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 30 ; commands: if( me.spawn_place != monster.name ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.PLACE_COMPARED_WITH_OTHER_ATTRIBUTE
                            + "me.spawn_place!=monster.name" + "\n" + ErrorMessages.ExpressionError.MONSTER_DOES_NOT_HAVE_THIS_ATTRIBUTE
                            + "me.spawn_place" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterIfCommandCompareNameToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 30 ; commands: if( me.name != monster.place ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.NAME_COMPARED_WITH_OTHER_ATTRIBUTE
                            + "me.name!=monster.place" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapIfCommandCompareSpawnTypeNameToType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: if( me.spawn_type.name != monster.type ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.NAME_COMPARED_WITH_OTHER_ATTRIBUTE
                            + "me.spawn_type.name!=monster.type" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapIfCommandCompareTypeNameToSpawnType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: if( me.type.name != monster.spawn_type ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.NAME_COMPARED_WITH_OTHER_ATTRIBUTE
                            + "me.type.name!=monster.spawn_type" + "\n" + ErrorMessages.ExpressionError.MONSTER_DOES_NOT_HAVE_THIS_ATTRIBUTE
                            + "monster.spawn_type" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapIfCommandCompareNonExistantNameToType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: if( me.dog.name != monster.type ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.NAME_COMPARED_WITH_OTHER_ATTRIBUTE
                            + "me.dog.name!=monster.type" + "\n" + ErrorMessages.ExpressionError.TRAP_DOES_NOT_HAVE_THIS_ATTRIBUTE
                            + "me.dog.name" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapIfCommandCompareNonExistantNameToTypeName()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: if( me.dog.name != monster.type.name ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.TRAP_DOES_NOT_HAVE_THIS_ATTRIBUTE
                            + "me.dog.name" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapWhileCommandAddAndMultiplyNameAndPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: if( me.name+monster.name == monster.place*me.place ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSIONS_HANDLED_AS_NUMBER
                            + "me.name+monster.name" + "\n" + ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSIONS_HANDLED_AS_NUMBER
                            + "monster.place*me.place" + "\n" + ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSIONS_HANDLED_AS_NUMBER
                            + "me.name+monster.name==monster.place*me.place" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapWhileCommandSubstractTypeToNumber()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: if( me.type-5 == 10 ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSIONS_HANDLED_AS_NUMBER
                            + "me.type-5" + "\n" + ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSIONS_HANDLED_AS_NUMBER
                            + "me.type-5==10" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterIfCommandCompareSpawnPlaceXToNumber()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 30 ; commands: if( me.spawn_place.x < 4 ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.MONSTER_DOES_NOT_HAVE_THIS_ATTRIBUTE
                            + "me.spawn_place.x" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterIfCommandComparePlayerTypeNameToNumber()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 30 ; commands: if( player.type.name == 4 ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSION_COMPARED_WITH_NUMBER
                            + "player.type.name==4" + "\n" + ErrorMessages.ExpressionError.PLAYER_DOES_NOT_HAVE_THIS_ATTRIBUTE
                            + "player.type.name" + "\n", visitor.Error);
        }
        [Test]
        public void AssignMonsterIfCommandComparePlayerHandCountToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 30 ; commands: if( player.hand_count == me.place ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testmonster").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.PLAYER_DOES_NOT_HAVE_THIS_ATTRIBUTE
                            + "player.hand_count" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapIfCommandCompareNonExistantAttributeToTypeName()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: if( trap.spikes.name != monster.type.name ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.TRAP_DOES_NOT_HAVE_THIS_ATTRIBUTE
                            + "trap.spikes.name" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapIfCommandCompareHealthToNumber()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: if( trap.health + 12 > 3 ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.TRAP_DOES_NOT_HAVE_THIS_ATTRIBUTE
                            + "trap.health" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapIfCommandComparePartnerTypoToNumber()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: if( partner.typo != me.heal ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSION_COMPARED_WITH_NUMBER
                        + "partner.typo!=me.heal" + "\n" + ErrorMessages.ExpressionError.NOBODY_HAS_THIS_ATTRIBUTE
                            + "partner.typo" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapWhileCommandComparePartnerTypeOfTypoToType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: while( partner.typo.type != me.type ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.NOBODY_HAS_THIS_ATTRIBUTE
                            + "partner.typo.type" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapWhileCommandComparePlaceTypeToType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: while( partner.place.type != me.type ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.PLACE_DOES_NOT_HAVE_THIS_ATTRIBUTE
                            + "partner.place.type" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapWhileCommandCompareTeleportPlaceZToType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: while( partner.teleport_place.Z != me.type ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.PLACE_DOES_NOT_HAVE_THIS_ATTRIBUTE
                            + "partner.teleport_place.Z" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapWhileCommandCompareSpawnPlaceDogToNumber()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: while( partner.spawn_place.dog < me.damage ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSION_HANDLED_AS_NUMBER
                        + "partner.spawn_place.dog<me.damage" + "\n" + ErrorMessages.ExpressionError.PLACE_DOES_NOT_HAVE_THIS_ATTRIBUTE
                            + "partner.spawn_place.dog" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapWhileCommandCompareTypeDogToNumber()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: while( partner.type.dog == me.damage ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.NOT_NUMBER_EXPRESSION_COMPARED_WITH_NUMBER
                        + "partner.type.dog==me.damage" + "\n" + ErrorMessages.ExpressionError.ENEMY_DOES_NOT_HAVE_THIS_ATTRIBUTE
                            + "partner.type.dog" + "\n", visitor.Error);
        }
        [Test]
        public void AssignTrapWhileCommandCompareSpawnTypeHealToNumber()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; heal = 30 ; commands: while( partner.spawn_type.heal == me.damage ){ move F; }");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsTrue(visitor.ErrorFound);
            Assert.AreEqual(0, Program.GetCharacterType("testtrap").Commands.Count);
            Assert.AreEqual(ErrorMessages.ConditionError.CONDITION_CHECK_FAIL + ErrorMessages.ExpressionError.MONSTER_DOES_NOT_HAVE_THIS_ATTRIBUTE
                            + "partner.spawn_type.heal" + "\n", visitor.Error);
        }





        //Command tests happy path
        [Test]
        public void AssignMonMoveToPlayerCommand()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: move to player;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(DynamicEnemyGrammarVisitorDelegates.MoveToPlayer))) != null);
        }
        [Test]
        public void AssignMonMoveCommandDirectionOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: move B;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(DynamicEnemyGrammarVisitorDelegates.MoveDirection)) &&
                 ((MoveCommand)x).Direction.Equals(Directions.BACKWARDS) && ((MoveCommand)x).Distance == 1) != null);
        }
        [Test]
        public void AssignMonMoveCommandDirectionAndDistance()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: move R distance = 5;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(DynamicEnemyGrammarVisitorDelegates.MoveDirection)) &&
                ((MoveCommand)x).Direction.Equals(Directions.RIGHT) && ((MoveCommand)x).Distance == 5) != null);
        }
        [Test]
        public void AssignMonMoveCommandRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: move to random;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(DynamicEnemyGrammarVisitorDelegates.MoveRandom))) != null);
        }
        [Test]
        public void AssignMonMoveCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: move to 1,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is MoveCommand && ((MoveCommand)x).MoveDelegate.Equals(new MoveDelegate(DynamicEnemyGrammarVisitorDelegates.MoveToPlace)) &&
                 ((MoveCommand)x).TargetPlace.X == 0 && ((MoveCommand)x).TargetPlace.Y == 1) != null);
        }
        [Test]
        public void AssignMonShootCommandDirectionOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: shoot F;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(DynamicEnemyGrammarVisitorDelegates.ShootDirection)) &&
                 ((ShootCommand)x).Direction.Equals(Directions.FORWARD) && ((ShootCommand)x).Distance == 1) != null);
        }
        [Test]
        public void AssignMonShootCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: shoot to 1,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(DynamicEnemyGrammarVisitorDelegates.ShootToPlace)) &&
                 ((ShootCommand)x).TargetPlace.X == 0 && ((ShootCommand)x).TargetPlace.Y == 1) != null);
        }
        [Test]
        public void AssignMonShootCommandDirAndDist()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; damage = 50; health = 50; commands: shoot L distance = 4;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(DynamicEnemyGrammarVisitorDelegates.ShootDirection)) &&
                 ((ShootCommand)x).Direction.Equals(Directions.LEFT) && ((ShootCommand)x).Distance == 4) != null);
        }
        [Test]
        public void AssignMonShootDirAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: shoot L damage = 40;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(DynamicEnemyGrammarVisitorDelegates.ShootDirection)) &&
                 ((ShootCommand)x).Direction.Equals(Directions.LEFT) && ((ShootCommand)x).HealthChangeAmount == 40) != null);
        }
        [Test]
        public void AssignMonShootCommandRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: shoot random;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                 .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(DynamicEnemyGrammarVisitorDelegates.ShootRandom))) != null);
        }
        [Test]
        public void AssignMonShootCommandToPlayer()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: shoot to player;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(DynamicEnemyGrammarVisitorDelegates.ShootToPlayer))) != null);
        }
        [Test]
        public void AssignMonShootCommandToPlayerAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: shoot to player damage = 55;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(DynamicEnemyGrammarVisitorDelegates.ShootToPlayer)) &&
                ((ShootCommand)x).HealthChangeAmount == 55) != null);
        }
        [Test]
        public void AssignMonShootCommandDirAndDistAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: shoot F distance = 5 damage = 70;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(DynamicEnemyGrammarVisitorDelegates.ShootDirection)) &&
                 ((ShootCommand)x).Direction.Equals(Directions.FORWARD) && ((ShootCommand)x).HealthChangeAmount == 70 && ((ShootCommand)x).Distance == 5) != null);
        }
        [Test]
        public void AssignMonShootCommandToPlaceAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("monster name = testmonster ; health = 50; commands: shoot to 3,1 damage = 100;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testmonster").Commands
                .Find(x => x is ShootCommand && ((ShootCommand)x).ShootDelegate.Equals(new ShootDelegate(DynamicEnemyGrammarVisitorDelegates.ShootToPlace)) &&
                 ((ShootCommand)x).TargetPlace.X == 2 && ((ShootCommand)x).TargetPlace.Y == 0 && ((ShootCommand)x).HealthChangeAmount == 100) != null);
        }
        [Test]
        public void AssignTrapDamageCommandDirectionOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  damage F;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageDirection)) &&
                 ((DamageCommand)x).Direction.Equals(Directions.FORWARD) && ((DamageCommand)x).Distance == 1) != null);
        }
        [Test]
        public void AssignTrapDamageCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  damage to 1,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageToPlace)) &&
                 ((DamageCommand)x).TargetPlace.X == 0 && ((DamageCommand)x).TargetPlace.Y == 1) != null);
        }
        [Test]
        public void AssignTrapDamageCommandDirAndDist()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  damage L distance = 3;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageDirection)) &&
                 ((DamageCommand)x).Direction.Equals(Directions.LEFT) && ((DamageCommand)x).Distance == 3) != null);
        }
        [Test]
        public void AssignTrapDamageDirAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  damage L damage = 40;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageDirection)) &&
                 ((DamageCommand)x).Direction.Equals(Directions.LEFT) && ((DamageCommand)x).HealthChangeAmount == 40) != null);
        }
        [Test]
        public void AssignTrapDamageCommandRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  damage random;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                 .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageRandom))) != null);
        }
        [Test]
        public void AssignTrapDamageCommandToPlayer()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ;  commands: damage to player;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageToPlayer))) != null);
        }
        [Test]
        public void AssignTrapDamageCommandToMonster()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  damage to monster;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageToMonster))) != null);
        }
        [Test]
        public void AssignTrapDamageCommandToPlayerAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  damage to player damage = 55;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageToPlayer)) &&
                ((DamageCommand)x).HealthChangeAmount == 55) != null);
        }
        [Test]
        public void AssignTrapDamageCommandToPartnerAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  damage to partner damage = 55;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageToPartner)) &&
                ((DamageCommand)x).HealthChangeAmount == 55) != null);
        }
        [Test]
        public void AssignTrapDamageCommandDirAndDistAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  damage F distance = 3 damage = 70;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageDirection)) &&
                 ((DamageCommand)x).Direction.Equals(Directions.FORWARD) && ((DamageCommand)x).HealthChangeAmount == 70 && ((DamageCommand)x).Distance == 3) != null);
        }
        [Test]
        public void AssignTrapDamageCommandToPlaceAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  damage to 3,1 damage = 100;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is DamageCommand && ((DamageCommand)x).DamageDelegate.Equals(new DamageDelegate(DynamicEnemyGrammarVisitorDelegates.DamageToPlace)) &&
                 ((DamageCommand)x).TargetPlace.X == 2 && ((DamageCommand)x).TargetPlace.Y == 0 && ((DamageCommand)x).HealthChangeAmount == 100) != null);
        }
        [Test]
        public void AssignTrapHealCommandDirectionOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  heal F;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealDirection)) &&
                 ((HealCommand)x).Direction.Equals(Directions.FORWARD) && ((HealCommand)x).Distance == 1) != null);
        }
        [Test]
        public void AssignTrapHealCommandToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  heal to 1,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealToPlace)) &&
                 ((HealCommand)x).TargetPlace.X == 0 && ((HealCommand)x).TargetPlace.Y == 1) != null);
        }
        [Test]
        public void AssignTrapHealCommandDirAndDist()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  heal L distance = 3;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealDirection)) &&
                 ((HealCommand)x).Direction.Equals(Directions.LEFT) && ((HealCommand)x).Distance == 3) != null);
        }
        [Test]
        public void AssignTrapHealDirAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  heal L heal = 40;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealDirection)) &&
                 ((HealCommand)x).Direction.Equals(Directions.LEFT) && ((HealCommand)x).HealthChangeAmount == 40) != null);
        }
        [Test]
        public void AssignTrapHealCommandRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  heal random;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                 .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealRandom))) != null);
        }
        [Test]
        public void AssignTrapHealCommandToPlayer()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  heal to player;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealToPlayer))) != null);
        }
        [Test]
        public void AssignTrapHealCommandToPartner()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  heal to partner;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealToPartner))) != null);
        }
        [Test]
        public void AssignTrapHealCommandToMonster()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  heal to monster;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealToMonster))) != null);
        }
        [Test]
        public void AssignTrapHealCommandToPlayerAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  heal to player heal = 55;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealToPlayer)) &&
                ((HealCommand)x).HealthChangeAmount == 55) != null);
        }
        [Test]
        public void AssignTrapHealCommandDirAndDistAndHeal()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ;  commands: heal F distance = 2 heal = 70;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealDirection)) &&
                 ((HealCommand)x).Direction.Equals(Directions.FORWARD) && ((HealCommand)x).HealthChangeAmount == 70 && ((HealCommand)x).Distance == 2) != null);
        }
        [Test]
        public void AssignTrapHealCommandToPlaceAndDam()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands:  heal to 3,1 heal = 100;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is HealCommand && ((HealCommand)x).HealDelegate.Equals(new HealDelegate(DynamicEnemyGrammarVisitorDelegates.HealToPlace)) &&
                 ((HealCommand)x).TargetPlace.X == 2 && ((HealCommand)x).TargetPlace.Y == 0 && ((HealCommand)x).HealthChangeAmount == 100) != null);
        }
        [Test]
        public void AssignTrapTeleportCommandPlayerOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; teleport_place = 3,1; commands: teleport player;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportPlayer)) &&
                 ((TeleportCommand)x).TargetPlace.X == 2 && ((TeleportCommand)x).TargetPlace.Y == 0) != null);
        }
        [Test]
        public void AssignTrapTeleportCommandPlayerAndPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; teleport_place = 3,1; commands: teleport player to 2,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TeleportCommand command && command.TeleportDelegate.Equals(new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportPlayer)) &&
                 command.TargetPlace.X == 1 && command.TargetPlace.Y == 1) != null);
        }
        [Test]
        public void AssignTrapTeleportCommandMonsterOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; teleport_place = 2,4; commands: teleport monster;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportMonster)) &&
                 ((TeleportCommand)x).TargetPlace.X == 1 && ((TeleportCommand)x).TargetPlace.Y == 3) != null);
        }
        [Test]
        public void AssignTrapTeleportCommandTrapOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; teleport_place = 3,1; commands: teleport trap;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportTrap)) &&
                 ((TeleportCommand)x).TargetPlace.X == 2 && ((TeleportCommand)x).TargetPlace.Y == 0) != null);
        }
        [Test]
        public void AssignTrapTeleportCommandPartnerOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; teleport_place = 3,1; commands: teleport partner;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportPartner)) &&
                 ((TeleportCommand)x).TargetPlace.X == 2 && ((TeleportCommand)x).TargetPlace.Y == 0) != null);
        }
        [Test]
        public void AssignTrapTeleportCommandPlayerRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: teleport player random;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportPlayer)) &&
                 ((TeleportCommand)x).Random) != null);
        }
        [Test]
        public void AssignTrapTeleportCommandMonsterRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: teleport monster random;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportMonster)) &&
                 ((TeleportCommand)x).Random) != null);
        }
        [Test]
        public void AssignTrapTeleportCommandTrapRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: teleport trap random;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportTrap)) &&
                 ((TeleportCommand)x).Random) != null);
        }
        [Test]
        public void AssignTrapTeleportCommandPlayerToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: teleport player to 3,1;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportPlayer)) &&
                 ((TeleportCommand)x).TargetPlace.X == 2 && ((TeleportCommand)x).TargetPlace.Y == 0) != null);
        }
        [Test]
        public void AssignTrapTeleportCommandMonsterToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: teleport monster to 1,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportMonster)) &&
                 ((TeleportCommand)x).TargetPlace.X == 0 && ((TeleportCommand)x).TargetPlace.Y == 1) != null);
        }
        [Test]
        public void AssignTrapTeleportCommandTrapToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: teleport trap to 3,3;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is TeleportCommand && ((TeleportCommand)x).TeleportDelegate.Equals(new TeleportDelegate(DynamicEnemyGrammarVisitorDelegates.TeleportTrap)) &&
                 ((TeleportCommand)x).TargetPlace.X == 2 && ((TeleportCommand)x).TargetPlace.Y == 2) != null);
        }
        [Test]
        public void AssignTrapSpawnCommandTypeToPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: spawn monster DefaultMonster to 2,3;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is SpawnCommand && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn)) &&
                 ((SpawnCommand)x).TargetPlace.X == 1 && ((SpawnCommand)x).TargetPlace.Y == 2 &&
                 ((SpawnCommand)x).TargetCharacterType.Name.Equals(Types.DEFAULT_MONSTER)) != null);
        }
        [Test]
        public void AssignTrapSpawnCommandRandom()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; commands: spawn random;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is SpawnCommand && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.SpawnRandom))) != null);
        }
        [Test]
        public void AssignTrapSpawnCommandOnlyType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; spawn_place = 1,2 ; commands: spawn monster FutureMonster;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is SpawnCommand && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn)) &&
                 ((SpawnCommand)x).TargetPlace.X == 0 && ((SpawnCommand)x).TargetPlace.Y == 1 &&
                 ((SpawnCommand)x).TargetCharacterType.Name.Equals("FutureMonster")) != null);
        }
        [Test]
        public void AssignTrapSpawnCommandOnlyPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; spawn_type = UnrealMonster ; commands: spawn to 2,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is SpawnCommand && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn)) &&
                 ((SpawnCommand)x).TargetPlace.X == 1 && ((SpawnCommand)x).TargetPlace.Y == 1 &&
                 ((SpawnCommand)x).TargetCharacterType.Name.Equals("UnrealMonster")) != null);
        }
        [Test]
        public void AssignTrapSpawnCommandOnly()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; spawn_place = 4,1; spawn_type = UnrealMonster ; commands: spawn ;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is SpawnCommand && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn)) &&
                 ((SpawnCommand)x).TargetPlace.X == 3 && ((SpawnCommand)x).TargetPlace.Y == 0 &&
                 ((SpawnCommand)x).TargetCharacterType.Name.Equals("UnrealMonster")) != null);
        }
        [Test]
        public void AssignTrapSpawnCommandWithPlace()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; spawn_place = 4,1; spawn_type = UnrealMonster ; commands: spawn to 2,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is SpawnCommand && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn)) &&
                 ((SpawnCommand)x).TargetPlace.X == 1 && ((SpawnCommand)x).TargetPlace.Y == 1 &&
                 ((SpawnCommand)x).TargetCharacterType.Name.Equals("UnrealMonster")) != null);
        }
        [Test]
        public void AssignTrapSpawnCommandWithPlaceAndType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; spawn_place = 4,1; spawn_type = UnrealMonster ; commands: spawn monster SpawnMonsterType to 2,2;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is SpawnCommand && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn)) &&
                 ((SpawnCommand)x).TargetPlace.X == 1 && ((SpawnCommand)x).TargetPlace.Y == 1 &&
                 ((SpawnCommand)x).TargetCharacterType.Name.Equals("SpawnMonsterType")) != null);
        }
        [Test]
        public void AssignTrapSpawnCommandWithType()
        {
            DynamicEnemyGrammarParser.DefinitionContext context = PreparingEnemyGrammar("trap name = testtrap ; spawn_place = 4,1; spawn_type = UnrealMonster ; commands: spawn monster SpawnMonsterType;");
            DynamicEnemyGrammarVisitor visitor = new DynamicEnemyGrammarVisitor();
            visitor.Visit(context);
            Assert.IsFalse(visitor.ErrorFound);
            Assert.IsTrue(Program.GetCharacterType("testtrap").Commands
                .Find(x => x is SpawnCommand && ((SpawnCommand)x).SpawnDelegate.Equals(new SpawnDelegate(DynamicEnemyGrammarVisitorDelegates.Spawn)) &&
                 ((SpawnCommand)x).TargetPlace.X == 3 && ((SpawnCommand)x).TargetPlace.Y == 0 &&
                 ((SpawnCommand)x).TargetCharacterType.Name.Equals("SpawnMonsterType")) != null);
        }

    }

    public class InActionTests
    {
        public static Game testGame;

        [SetUp]
        public void Setup()
        {
            Program.Characters.Clear();
            Program.CharacterTypes.Clear();
            Program.Board = new Board();
            Program.TrapTypeLoader("C:/Users/Dana/antlrworks/DefaultTrap.txt");
            Program.MonsterTypeLoader("C:/Users/Dana/antlrworks/DefaultMonster.txt");
            Program.BoardLoader("C:/Users/Dana/antlrworks/BoardCreation.txt");
            testGame = new Game();
        }

        public DynamicEnemyGrammarParser.DefinitionContext PreparingEnemyGrammar(string fileText)
        {
            AntlrInputStream inputStream = new AntlrInputStream(fileText);
            DynamicEnemyGrammarLexer DynamicEnemyGrammarLexer_ = new DynamicEnemyGrammarLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(DynamicEnemyGrammarLexer_);
            DynamicEnemyGrammarParser DynamicEnemyGrammarParser = new DynamicEnemyGrammarParser(commonTokenStream);
            DynamicEnemyGrammarParser.DefinitionContext chatContext = DynamicEnemyGrammarParser.definition();
            return chatContext;
        }
        public BoardGrammarParser.ProgramContext PreparingBoardGrammar(string fileText)
        {
            AntlrInputStream inputStream = new AntlrInputStream(fileText);
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

        //Move tests

    }
}