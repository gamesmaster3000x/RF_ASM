
namespace Crimson.CSharp.Grammar.Tokens
{
    public class RawValueCToken : ResolvableValueCToken
    {
        public RawValueCToken(string v) : base(v, ValueType.RAW)
        {
            V = v;
        }

        public RawValueCToken(int v) : base(v.ToString(), ValueType.RAW)
        {
            V = v.ToString();
        }

        public string V { get; }
    }
}