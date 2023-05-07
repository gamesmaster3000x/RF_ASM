using Compiler.Generalising.Structures;
using Compiler.Mapping;
using Compiler.Generalising;

namespace Compiler.Parsing.Syntax.Variables
{
    /// <summary>
    /// A uhm... global variable... Is a member of a package, rather than a function.
    /// </summary>
    public class GlobalVariable : IAssemblable, INamed, IMappable
    {
        public VariableAssignment Assignment { get; }

        public GlobalVariable (VariableAssignment assignment) => Assignment = assignment;

        public void Map (MappingContext ctx)
        {
            Assignment.Name = ctx.GetUniqueGlobalVariableName(Assignment.Name);
        }

        public FullName GetName ()
        {
            return Assignment.Name;
        }

        public void SetName (FullName name)
        {
            Assignment.Name = name;
        }

        public IGeneralAssemblyStructure Generalise (GeneralisationContext context)
        {
            // Can evaluate size at compile time
            if (Assignment.Size.CanEvaluateDuringCompile())
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
