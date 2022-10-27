using LabWork1github.static_constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabWork1github.BoardGrammarParser;

namespace LabWork1github.Visitors
{
    public static class Parsers
    {
        public static double DoubleParseFromNumber(string numberText)
        {
            double outputDouble;
            if (double.TryParse(numberText, out outputDouble))
            {
                return outputDouble;
            }
            else
            {
                return StaticStartValues.PLACEHOLDER_DOUBLE;
            }
        }
        public static int IntParseFromNumber(string numberText)
        {
            int outputInt;
            if (int.TryParse(numberText, out outputInt))
            {
                return outputInt;
            }
            else
            {
                return StaticStartValues.PLACEHOLDER_INT;
            }
        }
        //TODO: return sth else for place instead of throwing an error
        public static Place PlaceParseFromNumbers(DynamicEnemyGrammarParser.PlaceContext context)
        {
            int xCoordinate;
            int yCoordinate;
            if (int.TryParse(context.x().GetText(), out xCoordinate))
            {
                if (int.TryParse(context.y().GetText(), out yCoordinate))
                {
                    return new Place(xCoordinate-1, yCoordinate-1);
                }
                else
                {
                    return StaticStartValues.PLACEHOLDER_PLACE;
                }
            }
            else
            {
                return StaticStartValues.PLACEHOLDER_PLACE;
            }
        }
    }
}
