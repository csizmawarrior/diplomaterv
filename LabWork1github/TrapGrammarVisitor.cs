using Antlr4.Runtime.Misc;
using LabWork1github;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabWork1github.TrapGrammarParser;

namespace LabWork1github
{
    class TrapGrammarVisitor : TrapGrammarBaseVisitor<object>
    {
        private string typeName = "";
        public override object VisitNameDeclaration([NotNull] NameDeclarationContext context)
        {
            typeName = context.name().GetText();
            Program.trapTypes.Add(new TrapType(typeName));
            return base.VisitNameDeclaration(context);
        }
       
        public override object VisitMoveRoundDeclaration([NotNull] MoveRoundDeclarationContext context)
        {
            for (int i = 0; i < Program.trapTypes.Count; i++)
            {
                if (Program.trapTypes.ElementAt(i).Name.Equals(typeName))
                    Program.trapTypes.ElementAt(i).MoveRound = int.Parse(context.NUMBER().GetText());
            }
            return base.VisitMoveRoundDeclaration(context);
        }
        public override object VisitDamage([NotNull] DamageContext context)
        {
            for (int i = 0; i < Program.trapTypes.Count; i++)
            {
                if (Program.trapTypes.ElementAt(i).Name.Equals(typeName))
                {
                    Program.trapTypes.ElementAt(i).EffectType = TrapEffect.Damage;
                    Program.trapTypes.ElementAt(i).EffectNumber = int.Parse(context.NUMBER().GetText());
                }
            }
            return base.VisitDamage(context);
        }
        public override object VisitHeal([NotNull] HealContext context)
        {
            for (int i = 0; i < Program.trapTypes.Count; i++)
            {
                if (Program.trapTypes.ElementAt(i).Name.Equals(typeName))
                {
                    Program.trapTypes.ElementAt(i).EffectType = TrapEffect.Heal;
                    Program.trapTypes.ElementAt(i).EffectNumber = int.Parse(context.NUMBER().GetText());
                }
            }
            return base.VisitHeal(context);
        }
        public override object VisitMonsterSpawn([NotNull] MonsterSpawnContext context)
        {
            for (int i = 0; i < Program.trapTypes.Count; i++)
            {
                if (Program.trapTypes.ElementAt(i).Name.Equals(typeName))
                {
                    Program.trapTypes.ElementAt(i).EffectType = TrapEffect.Spawner;
                    Program.trapTypes.ElementAt(i).EffectPlace = new Place(
                                                                            uint.Parse(context.place().x().GetText())-1,
                                                                            uint.Parse(context.place().y().GetText())-1
                                                                            );
                }
            }
            return base.VisitMonsterSpawn(context);
        }
        public override object VisitTeleport([NotNull] TeleportContext context)
        {
            for (int i = 0; i < Program.trapTypes.Count; i++)
            {
                if (Program.trapTypes.ElementAt(i).Name.Equals(typeName))
                {
                    Program.trapTypes.ElementAt(i).EffectType = TrapEffect.Teleport;
                    Program.trapTypes.ElementAt(i).EffectPlace = new Place(
                                                                            uint.Parse(context.place().x().GetText())-1,
                                                                            uint.Parse(context.place().y().GetText())-1
                                                                            );
                }
            }
            return base.VisitTeleport(context);
        }
    }
}
