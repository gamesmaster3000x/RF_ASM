using Crimson.CSharp.Statements;
using CrimsonBasic.CSharp.Core;

namespace Crimson.CSharp.Core
{
    public class Flattener
    {
        internal BasicProgram Flatten(Compilation dependentTranslation, CrimsonOptions options)
        {
            BasicProgram program = new BasicProgram();

            // Gather all statements
            IList<CrimsonStatement> crimsonStatements = new List<CrimsonStatement>();

            return program;
        }
    }
}