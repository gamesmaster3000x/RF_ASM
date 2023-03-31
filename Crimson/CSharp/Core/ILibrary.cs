using Crimson.CSharp.Parsing;

namespace Crimson.CSharp.Core
{
    internal interface ILibrary
    {
        Scope? GetScope (Uri uri);
        IEnumerable<Task<Scope>> GetUnits ();
    }
}
