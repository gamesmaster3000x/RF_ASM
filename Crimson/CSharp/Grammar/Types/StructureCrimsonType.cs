using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Statements;
using Crimson.CSharp.Grammar.Tokens;

namespace Crimson.CSharp.Grammar.Types
{
    internal class StructureCrimsonType : CrimsonTypeCToken
    {

        public StructureCStatement? Structure { get; set; }

        public StructureCrimsonType (FullNameCToken name) : base(name)
        {
        }

        public override int GetSize ()
        {
            throw new NotImplementedException();
        }

        public override void Link (LinkingContext ctx)
        {
            Scope scope;

            if (Name.HasLibrary())
            {
                if (!ctx.HasScope(Name.LibraryName!))
                    throw new LinkingException($"Error linking type {Name}! " +
                        $"The given scope alias {Name.LibraryName} could not be found in the current linking context: {ctx}. " +
                        $"Available scope links: [{String.Join(',', ctx.Links.Select(pair => pair.Key))}].");
                scope = ctx.GetScope(Name.LibraryName!);
            }
            else
            {
                scope = ctx.CurrentScope;
            }

            Structure = scope.FindStructure(Name.MemberName);

            if (Structure == null)
                throw new LinkingException($"Error linking type {Name}! " +
                    $"The given structure name {Name.MemberName} could not be found in the given scope {Name.LibraryName}: {scope}. " +
                    $"Available structures: [{String.Join(',', scope.Structures.Select(pair => pair.Value.Name))}].");
        }
    }
}