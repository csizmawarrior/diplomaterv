using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabWork1github.BoardGrammarParser;
using LabWork1github;
using LabWork1github;

namespace LabWork1github
{
    class BoardGrammarVisitor : BoardGrammarBaseVisitor<object>
    {
        public override object Visit([NotNull] IParseTree tree)
        {
            return base.Visit(tree);
        }

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
            uint xPos = uint.Parse(place.x().GetText());
            uint yPos = uint.Parse(place.y().GetText());
            Program.Board.Player = new Player(new Place(xPos-1, yPos-1), Program.starterHP);
            return base.VisitPlayerPlacement(context);
        }
        public override object VisitMonsterPlacement([NotNull] MonsterPlacementContext context)
        {
            PlaceContext place = context.place();
            uint xPos = uint.Parse(place.x().GetText());
            uint yPos = uint.Parse(place.y().GetText());
            string typeName = context.typeName().GetText();
            for(int i = 0; i < Program.monsterTypes.Count; i++)
            {
                if(typeName == Program.monsterTypes.ElementAt(i).Name)
                {
                    Program.Board.Monsters.Add(new Monster(Program.starterHP, Program.monsterTypes.ElementAt(i), new Place(xPos-1, yPos-1)));
                    return base.VisitMonsterPlacement(context);
                }

            }
            
            return base.VisitMonsterPlacement(context);
        }
        public override object VisitTrapPlacement([NotNull] TrapPlacementContext context)
        {
            PlaceContext place = context.place();
            uint xPos = uint.Parse(place.x().GetText());
            uint yPos = uint.Parse(place.y().GetText());
            string typeName = context.typeName().GetText();
            for (int i = 0; i < Program.trapTypes.Count; i++)
            {
                if (typeName == Program.trapTypes.ElementAt(i).Name)
                {
                    Program.Board.Traps.Add(new Trap(Program.trapTypes.ElementAt(i), new Place(xPos-1, yPos-1)));
                    return base.VisitTrapPlacement(context);
                }

            }

            return base.VisitTrapPlacement(context);
        }
    }
}
