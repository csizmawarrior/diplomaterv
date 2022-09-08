using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabWork1github.BoardGrammarParser;

namespace LabWork1github
{
    class BoardGrammarVisitor : BoardGrammarBaseVisitor<object>
    {
        public bool ErrorFound { get; set; }

        public string ErrorList { get; set; } = "";

        public override object VisitProgram([NotNull] ProgramContext context)
        {
            return base.VisitProgram(context);
        }

        public override object VisitStatementList(StatementListContext context)
        {
            return base.VisitStatementList(context);
        }
        public override object VisitStatement([NotNull] StatementContext context)
        {
            return base.VisitStatement(context);
        }
        public override object VisitBoardCreation([NotNull] BoardCreationContext context)
        {
            PlaceContext place = context.place();
            Program.Board.Height = int.Parse(place.x().GetText());
            Program.Board.Width = int.Parse(place.y().GetText());

            return base.VisitBoardCreation(context);
        }

        public override object VisitPlayerPlacement([NotNull] PlayerPlacementContext context)
        {
            PlaceContext place = context.place();
            int xPos = int.Parse(place.x().GetText());
            int yPos = int.Parse(place.y().GetText());
            Program.Board.Player = new Player(new Place(xPos-1, yPos-1), Program.starterHP);
            Program.Characters.Add(Program.Board.Player);
            return base.VisitPlayerPlacement(context);
        }
        public override object VisitMonsterPlacement([NotNull] MonsterPlacementContext context)
        {
            PlaceContext place = context.place();
            int xPos = int.Parse(place.x().GetText());
            int yPos = int.Parse(place.y().GetText());
            string typeName = context.typeName().GetText();
            if (Program.GetCharacterType(typeName) != null && Program.GetCharacterType(typeName) is MonsterType) {
                Monster m = new Monster(Program.starterHP, (MonsterType)Program.GetCharacterType(typeName), new Place(xPos - 1, yPos - 1));

                if(context.nameDeclaration() != null)
                {
                    m.Name = context.nameDeclaration().ID().GetText();
                }
                if (context.partnerDeclaration() != null)
                {
                    m.PartnerName = context.partnerDeclaration().ID().GetText();
                }

                Program.Board.Monsters.Add(m);
                Program.Characters.Add(m);
                return base.VisitMonsterPlacement(context);
            }

            ErrorFound = true;
            ErrorList += "The monster type is incorrect at place:\n";
            ErrorList += context.GetText() + "\n";
           
            
            return base.VisitMonsterPlacement(context);
        }
        public override object VisitTrapPlacement([NotNull] TrapPlacementContext context)
        {
            PlaceContext place = context.place();
            int xPos = int.Parse(place.x().GetText());
            int yPos = int.Parse(place.y().GetText());
            string typeName = context.typeName().GetText();
            if (Program.GetCharacterType(typeName) != null && Program.GetCharacterType(typeName) is TrapType)
            {
                Trap t = new Trap((TrapType)Program.GetCharacterType(typeName), new Place(xPos - 1, yPos - 1));

                if (context.nameDeclaration() != null)
                {
                    t.Name = context.nameDeclaration().ID().GetText();
                }
                if (context.partnerDeclaration() != null)
                {
                    t.PartnerName = context.partnerDeclaration().ID().GetText();
                }
                
                Program.Board.Traps.Add(t);
                Program.Characters.Add(t);
                return base.VisitTrapPlacement(context);
            }
            ErrorFound = true;
            ErrorList += "The trap type is incorrect at place:\n";
            ErrorList += context.GetText() + "\n";

            return base.VisitTrapPlacement(context);
        }
    }
}
