using Antlr4.Runtime;
using CrimsonCore.Exceptions;
using CrimsonCore.Linking;

namespace Compiler.Parsing.Tokens
{
    public class FullNameCToken : ICrimsonToken
    {
        public string? LibraryName { get; }
        public string MemberName { get; }

        public FullNameCToken (string? libraryName, string memberName)
        {
            LibraryName = libraryName;
            MemberName = memberName;
        }

        public FullNameCToken (string fullName)
        {
            string[] strings = fullName.Split('.');
            if (strings.Length == 1)
            {
                LibraryName = null;
                MemberName = strings[0];
            }
            else if (strings.Length == 2)
            {
                LibraryName = strings[0];
                MemberName = strings[1];
            }
            else
                throw new ArgumentException($"Cannot construct FullNameCToken from {strings.Length} parts (must have 1 or 2 parts)");
        }

        public bool HasMember ()
        {
            return !string.IsNullOrWhiteSpace(MemberName);
        }

        public bool HasLibrary ()
        {
            return !string.IsNullOrWhiteSpace(LibraryName);
        }

        public void Link (LinkingContext ctx)
        {
            // Not dealt with here because this should result in a Function/FunctionCall/etc...
            // Use LinkerHelper for this purpose
            return;
        }

        public override string ToString ()
        {
            if (HasLibrary() && HasMember()) return $"{LibraryName}.{MemberName}";
            if (HasLibrary()) return LibraryName;
            if (HasMember()) return MemberName;
            return "";
        }
    }
}