using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using static LabWork1github.DynamicEnemyGrammarParser;

namespace LabWork1github
{
    public class DynamicEnemyGrammarVisitor : DynamicEnemyGrammarBaseVisitor<object>
    {
        private string typeName = "";
        private string type = null;

        public override object VisitTrapNameDeclaration([NotNull] TrapNameDeclarationContext context)
        {
            type = Types.TRAP;
            Program.EnemyTypes.Add(new TrapType(context.name().GetText()));
            typeName = context.name().GetText();
            return base.VisitTrapNameDeclaration(context);
        }
        public override object VisitMonsterNameDeclaration([NotNull] MonsterNameDeclarationContext context)
        {
            type = Types.MONSTER;
            Program.EnemyTypes.Add(new MonsterType(context.name().GetText()));
            typeName = context.name().GetText();
            return base.VisitMonsterNameDeclaration(context);
        }
        public override object VisitHealthDeclaration([NotNull] HealthDeclarationContext context)
        {
            if (type == Types.TRAP)
                throw new ArrayTypeMismatchException("Traps don't have Health");
            Program.GetEnemyType(typeName).Health = int.Parse(context.NUMBER().GetText());
            return base.VisitHealthDeclaration(context);
        }
        public override object VisitHealAmountDeclaration([NotNull] HealAmountDeclarationContext context)
        {
            if (type != Types.TRAP)
                throw new ArrayTypeMismatchException("Traps don't have Health");
            Program.GetEnemyType(typeName).Heal = int.Parse(context.NUMBER().GetText());
            return base.VisitHealAmountDeclaration(context);
        }
        public override object VisitDamageAmountDeclaration([NotNull] DamageAmountDeclarationContext context)
        {
            Program.GetEnemyType(typeName).Damage = int.Parse(context.NUMBER().GetText());
            return base.VisitDamageAmountDeclaration(context);
        }
        public override object VisitTeleportPointDeclaration([NotNull] TeleportPointDeclarationContext context)
        {
            if (type != Types.TRAP)
                throw new ArrayTypeMismatchException("Traps don't have Health");
            Program.GetEnemyType(typeName).TeleportPlace = new Place(uint.Parse(context.place().x().GetText()), uint.Parse(context.place().y().GetText()));
            return base.VisitTeleportPointDeclaration(context);
        }
        public override object VisitSpawnPointDeclaration([NotNull] SpawnPointDeclarationContext context)
        {
            if (type != Types.TRAP)
                throw new ArrayTypeMismatchException("Traps don't have Health");
            Program.GetEnemyType(typeName).SpawnPlace = new Place(uint.Parse(context.place().x().GetText()), uint.Parse(context.place().y().GetText()));
            return base.VisitSpawnPointDeclaration(context);
        }
        public override object VisitSpawnTypeDeclaration([NotNull] SpawnTypeDeclarationContext context)
        {
            if (type != Types.TRAP)
                throw new ArrayTypeMismatchException("Traps don't have Health");
            Program.GetEnemyType(typeName).SpawnType = Program.GetEnemyType(context.name().GetText());
            return base.VisitSpawnTypeDeclaration(context);
        }

    }
}
