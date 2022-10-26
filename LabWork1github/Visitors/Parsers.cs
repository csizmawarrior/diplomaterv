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
                throw new InvalidCastException(ErrorMessages.ParseError.UNABLE_TO_PARSE_DOUBLE);
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
                throw new InvalidCastException(ErrorMessages.ParseError.UNABLE_TO_PARSE_INT);
            }
        }
        public static Place PlaceParseFromNumbers(PlaceContext context)
        {
            int xCoordinate;
            int yCoordinate;
            if (int.TryParse(context.x().GetText(), out xCoordinate))
            {
                if (int.TryParse(context.x().GetText(), out yCoordinate))
                {
                    return new Place(xCoordinate, yCoordinate);
                }
                else
                {
                    throw new InvalidCastException(ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE);
                }
            }
            else
            {
                throw new InvalidCastException(ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE);
            }
        }
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
                    throw new InvalidCastException(ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE);
                }
            }
            else
            {
                throw new InvalidCastException(ErrorMessages.ParseError.UNABLE_TO_PARSE_PLACE);
            }
        }
    }
}
