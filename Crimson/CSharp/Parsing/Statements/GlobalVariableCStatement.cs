using Crimson.CSharp.Exceptions;
using Crimson.CSharp.Generalising;
using Crimson.CSharp.Generalising.Structures;
using Crimson.CSharp.Linking;
using Crimson.CSharp.Parsing.Tokens;

namespace Crimson.CSharp.Parsing.Statements
{
    /// <summary>
    /// A uhm... global variable... Is a member of a package, rather than a function.
    /// </summary>
    public class GlobalVariableCStatement : AbstractCrimsonStatement, INamed
    {
        public VariableAssignmentCStatement Assignment { get; }

        public GlobalVariableCStatement (VariableAssignmentCStatement assignment) => Assignment = assignment;

        public override void Link (LinkingContext ctx)
        {
            Linked = true;
        }

        public FullNameCToken GetName ()
        {
            return Assignment.Name;
        }

        public void SetName (FullNameCToken name)
        {
            Assignment.Name = name;
        }

        public override IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            ScopeAssemblyStructure scope = new ScopeAssemblyStructure();

            // int i = (6 + 5);
            if (Assignment.Complex != null)
            {
                // Fragment valueStatements = Assignment.Complex.GetBasicFragment();
                scope.AddSubStructure(new CommentAssemblyStructure($"Set {Assignment.Name.ToString()}=RESULT"));
            }
            else if (Assignment.Simple != null)
                scope.AddSubStructure(new CommentAssemblyStructure($"Assign {Assignment.Name.ToString()}={Assignment.Simple.GetText()}"));
            else
                throw new FlatteningException("Unable to flatten internal variable with no simple or complex value");

            return scope;
        }
    }
}
