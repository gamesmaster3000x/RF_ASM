namespace Compiler.Parsing.Syntax.ControlFlow
{
    public class Comparator
    {
        public enum Values
        {
            LESS,
            LESS_EQUAL,
            GREATER,
            GREATER_EQUAL,
            EQUAL_TO
        }

        public static Values Get (string value)
        {
            switch (value)
            {
                case "<": return Values.LESS;
                case "<=": return Values.LESS_EQUAL;
                case ">": return Values.GREATER;
                case ">=": return Values.GREATER_EQUAL;
                case "==": return Values.EQUAL_TO;
            }

            throw new ArgumentException($"{value} cannot be parsed to a Comparator");
        }

        public static string ToString (Values comparator)
        {
            switch (comparator)
            {
                case Values.LESS: return "<";
                case Values.LESS_EQUAL: return "<=";
                case Values.GREATER: return ">";
                case Values.GREATER_EQUAL: return ">=";
                case Values.EQUAL_TO: return "==";
            }

            throw new ArgumentException($"Cannot parse comparator {comparator} ToString");
        }
    }
}