
namespace Crimson.CSharp.Grammar.Tokens
{
    public class IdentiferValueCToken : ResolvableValueCToken
    {
        public IdentiferValueCToken(string v) : base(v, ValueType.IDENTIFIER)
        {
            V = v;
        }

        public IdentiferValueCToken(int v) : base(v.ToString(), ValueType.IDENTIFIER)
        {
            V = v.ToString();
        }

        public string V { get; }
    }
}