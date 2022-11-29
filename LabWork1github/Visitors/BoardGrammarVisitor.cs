using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using LabWork1github.static_constants;
using LabWork1github.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabWork1github.BoardGrammarParser;

namespace LabWork1github
{
    public class BoardGrammarVisitor : BoardGrammarBaseVisitor<object>
    {
        public bool ErrorFound { get; set; }

        public string ErrorList { get; set; } = "";

        public override object VisitProgram([NotNull] ProgramContext context)
        {
            VisitStatementList(context.statementList());
            AfterBoardCreationCheck();
            return null;
        }
        public override object VisitBoardCreation([NotNull] BoardCreationContext context)
        {
            PlaceContext place = context.place();
            Program.Board.Height = Parsers.IntParseFromNumber(place.x().GetText());
            if(Program.Board.Height == 0)
            {
                ErrorFound = true;
                ErrorList += ErrorMessages.BoardError.ZERO_HEIGHT;
                ErrorList += context.GetText() + "\n";
            }

            Program.Board.Width = Parsers.IntParseFromNumber(place.y().GetText());
            if (Program.Board.Width == 0)
            {
                ErrorFound = true;
                ErrorList += ErrorMessages.BoardError.ZERO_WIDTH;
                ErrorList += context.GetText() + "\n";
            }

            if (context.nameDeclaration() != null)
            {
                Program.Board.Name = context.nameDeclaration().ID().GetText();
            }
            return base.VisitBoardCreation(context);
        }

        public override object VisitPlayerPlacement([NotNull] PlayerPlacementContext context)
        {
            PlaceContext place = context.place();
            int xPos = Parsers.IntParseFromNumber(place.x().GetText());
            int yPos = Parsers.IntParseFromNumber(place.y().GetText());
            if(xPos == 0 || yPos == 0)
            {
                ErrorFound = true;
                ErrorList += ErrorMessages.BoardError.ZERO_AS_COORDINATE;
                ErrorList += context.GetText() + "\n";
            }
            Program.Board.Player = new Player(new Place(xPos -1, yPos -1), StaticStartValues.STARTER_PLAYER_HP);
            if (context.nameDeclaration() != null)
                Program.Board.Player.Name = context.nameDeclaration().ID().GetText();
            Program.Characters.Add(Program.Board.Player);
            Program.CharacterTypes.Add(new PlayerType());
            return base.VisitPlayerPlacement(context);
        }
        public override object VisitMonsterPlacement([NotNull] MonsterPlacementContext context)
        {
            PlaceContext place = context.place();
            int xPos = Parsers.IntParseFromNumber(place.x().GetText());
            int yPos = Parsers.IntParseFromNumber(place.y().GetText());
            if (xPos == 0 || yPos == 0)
            {
                ErrorFound = true;
                ErrorList += ErrorMessages.BoardError.ZERO_AS_COORDINATE;
                ErrorList += context.GetText() + "\n";
            }
            string typeName = context.typeName().GetText();
            if (Program.GetCharacterType(typeName) is MonsterType type) {
                Monster m = new Monster(StaticStartValues.STARTER_MONSTER_HP, 
                                    type, new Place(xPos - 1, yPos - 1));

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
            ErrorList += ErrorMessages.BoardError.UNDEFINED_MONSTER_TYPE;
            ErrorList += context.GetText() + "\n";
           
            
            return base.VisitMonsterPlacement(context);
        }
        public override object VisitTrapPlacement([NotNull] TrapPlacementContext context)
        {
            PlaceContext place = context.place();
            int xPos = Parsers.IntParseFromNumber(place.x().GetText());
            int yPos = Parsers.IntParseFromNumber(place.y().GetText());
            if (xPos == 0 || yPos == 0)
            {
                ErrorFound = true;
                ErrorList += ErrorMessages.BoardError.ZERO_AS_COORDINATE;
                ErrorList += context.GetText() + "\n";
            }
            string typeName = context.typeName().GetText();
            if (Program.GetCharacterType(typeName) is TrapType type)
            {
                Trap t = new Trap(type, new Place(xPos - 1, yPos - 1));

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
            ErrorList += ErrorMessages.BoardError.UNDEFINED_TRAP_TYPE;
            ErrorList += context.GetText() + "\n";

            return base.VisitTrapPlacement(context);
        }

        private void AfterBoardCreationCheck()
        {
            foreach (Character character in Program.Characters)
            {
                if (!character.PartnerName.Equals(StaticStartValues.PLACEHOLDER_PARTNER_NAME))
                {
                    foreach(var c in Program.Characters.Where(c => c.Name.Equals(character.Name) && !c.Equals(character)))
                    {
                        c.Name = "";
                    }
                    foreach (var m in Program.Characters.Where(m => m.Name.Equals(character.PartnerName)))
                    {
                        if (m is Player)
                        {
                            ErrorFound = true;
                            ErrorList += $"{ErrorMessages.BoardError.PARTNER_CANNOT_BE_THE_PLAYER}{character.Name}\n";
                        }

                        if (character.Equals(m))
                        {
                            ErrorFound = true;
                            ErrorList += $"{ErrorMessages.BoardError.CANNOT_BE_YOUR_OWN_PARTNER}{character.Name}\n";
                        }

                        character.Partner = m;
                    }

                    if (character.Partner == null)
                    {
                        ErrorFound = true;
                        ErrorList += ErrorMessages.PartnerError.NON_EXISTANT_PARTNER + character.Name + "\n";
                    }
                }
                foreach (Character c in Program.Characters)
                {
                    if (c.Place.DirectionTo(character.Place) == Directions.COLLISION && !(c is Player))
                    {
                        if (character is Player)
                        {
                            ErrorFound = true;
                            ErrorList += ErrorMessages.GameError.PLAYER_SPAWNED_ON_CHARACTER + "\n";
                        }
                            if ((c is Trap))
                        {
                            ErrorFound = true;
                            ErrorList += ErrorMessages.GameError.CHARACTER_SPAWNED_ON_TRAP + "\n";
                            if (character is Player)
                                ErrorList += ErrorMessages.GameError.CHARACTER_SPAWNED_ON_TRAP + "\n";
                        }
                    }
                }
            }
        }
    }
}
