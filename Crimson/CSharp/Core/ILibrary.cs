using Crimson.CSharp.Parsing;

namespace Crimson.CSharp.Core
{
    internal interface ILibrary
    {
        Task<Scope> GetScope(string path);
        Task<Scope> LoadScopeAsync(string path, bool root);
        IEnumerable<Task<Scope>> GetUnits();
    }
}
