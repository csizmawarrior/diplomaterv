using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using LabWork1github.static_constants;
using static LabWork1github.DynamicEnemyGrammarParser;

namespace LabWork1github
{
    public class ExpressionVisitor : DynamicEnemyGrammarBaseVisitor<object>
    {

        public BoolExpressionContext BoolExpressionContext { get; set; }

        public string ErrorList { get; set; } = "";
        public bool CheckFailed { get; set; } = false;
        public bool NumberCheckFailed { get; set; } = false;
        public string characterType { get; set; }

        public ExpressionVisitor(BoolExpressionContext context, string myType)
        {
            BoolExpressionContext = context;
            characterType = myType;
        }

        public void CheckTypes(BoolExpressionContext Excontext)
        {
            
        }
        
        public void CheckBool(BoolExpressionContext context)
        {
            this.Visit(context);
        }

        public override object VisitBoolExpression([NotNull] BoolExpressionContext context)
        {
            if(context.attribute().Length > 1)
            {
                //this way they can only be compared if both of them are a charactertype
                if((context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("type") ||
                    context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("spawn_type")) 
                    && context.attribute().ElementAt(0).possibleAttributes().possibleAttributes().Length == 0)

                    if (!((context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("type") ||
                    context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_type"))
                    && context.attribute().ElementAt(1).possibleAttributes().possibleAttributes().Length == 0))
                    {
                        ErrorList += ErrorMessages.ExpressionError.TYPE_COMPARED_WITH_OTHER_ATTRIBUTE;
                        ErrorList += context.GetText() + "\n";
                        CheckFailed = true;
                    }
                //this way they can only be compared if both of them are a place
                if ((context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("place") ||
                    context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("teleport_place") ||
                    context.attribute().ElementAt(0).possibleAttributes().GetText().Equals("spawn_place"))
                    && context.attribute().ElementAt(0).possibleAttributes().possibleAttributes().Length == 0)
                    if (!((context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("place") ||
                        context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("teleport_type") ||
                    context.attribute().ElementAt(1).possibleAttributes().GetText().Equals("spawn_place"))
                    && context.attribute().ElementAt(1).possibleAttributes().possibleAttributes().Length == 0))
                    {
                        ErrorList += ErrorMessages.ExpressionError.PLACE_COMPARED_WITH_OTHER_ATTRIBUTE;
                        ErrorList += context.GetText() + "\n";
                        CheckFailed = true;
                    }
            }
            return base.VisitBoolExpression(context);
        }

        public override object VisitNumberFirstExpression([NotNull] NumberFirstExpressionContext context)
        {
            if(context.something() == null || context.something().attribute() == null)
                return base.VisitNumberFirstExpression(context);
            if (context.something().attribute().possibleAttributes().GetText().Equals("type") ||
                context.something().attribute().possibleAttributes().GetText().Equals("spwan_type") ||
                context.something().attribute().possibleAttributes().GetText().Equals("place") ||
                context.something().attribute().possibleAttributes().GetText().Equals("spawn_place") ||
                context.something().attribute().possibleAttributes().GetText().Equals("teleport_place"))
                    if(context.something().attribute().possibleAttributes().possibleAttributes().Length < 2)
                {
                    ErrorList += ErrorMessages.ExpressionError.NOT_A_NUMBER;
                    ErrorList += context.GetText() + "\n";
                    CheckFailed = true;
                }
            return base.VisitNumberFirstExpression(context);
        }

        public override object VisitAttribute([NotNull] AttributeContext context)
        {
            if (context.character().GetText().Equals(Types.MONSTER) || characterType.Equals(Types.MONSTER))
            {
                if (!(context.possibleAttributes().GetText().Equals("place") || context.possibleAttributes().GetText().Equals("health") ||
                    context.possibleAttributes().GetText().Equals("damage") || context.possibleAttributes().GetText().Equals("type")))
                {
                    ErrorList += ErrorMessages.ExpressionError.MONSTER_DOES_NOT_HAVE_THIS_ATTRIBUTE;
                    ErrorList += context.GetText() + "\n";
                    CheckFailed = true;
                }
            }
            if (context.character().GetText().Equals(Types.PLAYER))
            {
                if (!(context.possibleAttributes().GetText().Equals("place") || context.possibleAttributes().GetText().Equals("health") ||
                    context.possibleAttributes().GetText().Equals("damage")))
                {
                    ErrorList += ErrorMessages.ExpressionError.PLAYER_DOES_NOT_HAVE_THIS_ATTRIBUTE;
                    ErrorList += context.GetText() + "\n";
                    CheckFailed = true;
                }
            }
            if (context.character().GetText().Equals(Types.TRAP) || characterType.Equals(Types.TRAP))
            {
                if (!(context.possibleAttributes().GetText().Equals("place") || context.possibleAttributes().GetText().Equals("damage") || 
                    context.possibleAttributes().GetText().Equals("type") || context.possibleAttributes().GetText().Equals("teleport_place") || 
                    context.possibleAttributes().GetText().Equals("spawn_place") ||  context.possibleAttributes().GetText().Equals("spawn_type") ||
                    context.possibleAttributes().GetText().Equals("heal")))
                {
                    ErrorList += ErrorMessages.ExpressionError.TRAP_DOES_NOT_HAVE_THIS_ATTRIBUTE;
                    ErrorList += context.GetText() + "\n";
                    CheckFailed = true;
                }
            }
            if (context.possibleAttributes() != null)
                if (context.possibleAttributes().possibleAttributes().Length > 0)
                {
                    if (context.possibleAttributes().GetText().Equals("place") || context.possibleAttributes().GetText().Equals("teleport_place") ||
                        context.possibleAttributes().GetText().Equals("spawn_place"))
                    {
                        if (!(context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("x") ||
                                    context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("y")))
                        {
                                ErrorList += ErrorMessages.ExpressionError.PLACE_DOES_NOT_HAVE_THIS_ATTRIBUTE;
                                ErrorList += context.GetText() + "\n";
                                CheckFailed = true;
                        }
                    }
                    else
        //by this we rerstrict the deepness of the reference for types, no need for further levels, the same things can be represented like this as well
        //and it'd look weird to have e.g. type.type.type, or spawn_type.type they return the same type as the first one
        //we can't ensure now that it will be a trap or a monster referred to under type, or a player, so the error for this can only be provided in runtime
                    if (context.possibleAttributes().GetText().Equals("spawn_type") || context.possibleAttributes().GetText().Equals("type"))
                    {
                        if (!(context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("health") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("heal") ||
                            context.possibleAttributes().possibleAttributes().ElementAt(1).GetText().Equals("damage")))
                        {
                            ErrorList += ErrorMessages.ExpressionError.ENEMY_DOES_NOT_HAVE_THIS_ATTRIBUTE;
                            ErrorList += context.GetText() + "\n";
                            CheckFailed = true;
                        }
                    }
        
                }
                return base.VisitAttribute(context);
        }
    }
}