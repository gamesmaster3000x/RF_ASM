using CrimsonCore.Exceptions;
using CrimsonCore.Generalising.Structures;
using CrimsonCore.Linking;
using Compiler.Parsing.Tokens;
using Compiler.Generalising;

namespace Compiler.Parsing.Statements
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
            Assignment.Name = ctx.GetUniqueGlobalVariableName(Assignment.Name);
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
            // Can evaluate size at compile time
            if (Assignment.Size.CanEvaluate())
            {

                // Desperately try to parse to an integer
                object? eval = Assignment.Size.Evaluate(context);
                if (eval == null)
                    throw new NullReferenceException($"Compile-time evaluation of a the global variable {GetName()}'s size may not return null.");
                if (eval is not int size)
                    if (!int.TryParse(eval.ToString(), out size))
                        throw new InvalidCastException($"The size '{eval}' of a of the global variable {GetName()} could not be parsed to an integer: found type {eval.GetType()}.");

                int addrOffset = context.AllocGlobal(size);
                return new CommentAssemblyStructure($"GVAR: {GetName()}, addrOffset={addrOffset}");
            }

            // Cannot evaluate size at compile time
            else
                return new CommentAssemblyStructure($"GVAR: {GetName()}=?");
        }
    }
}
