using Crimson.CSharp.Core;
using Crimson.CSharp.Exception;
using Crimson.CSharp.Grammar.Tokens;
using CrimsonBasic.CSharp.Core;
using CrimsonBasic.CSharp.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimson.CSharp.Grammar.Statements
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

        public override Fragment GetCrimsonBasic ()
        {
            Fragment statements = new Fragment(0);

            // int i = (6 + 5);
            if (Assignment.Complex != null)
            {
                Fragment valueStatements = Assignment.Complex.GetBasicFragment();
                statements.Add(valueStatements);
                statements.Add(new SetBStatement(Assignment.Name.ToString(), -1, valueStatements.ResultHolder!));

            }
            else if (Assignment.Simple != null)
            {
                statements.Add(new SetBStatement(Assignment.Name.ToString(), -1, Assignment.Simple.GetText()));
            }
            else
            {
                throw new FlatteningException("Unable to flatten internal variable with no simple or complex value");
            }

            return statements;
        }
    }
}
