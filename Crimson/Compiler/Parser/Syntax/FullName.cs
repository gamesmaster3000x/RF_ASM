using Compiler.Common.Exceptions;
using Compiler.Mapper;

namespace Compiler.Parser.Syntax
{
    public class FullName : IMappable
    {
        public string? LibraryName { get; protected set; }
        public string MemberName { get; protected set; }

        public FullName (string? libraryName, string memberName)
        {
            LibraryName = libraryName;
            MemberName = memberName;
        }

        public FullName (string fullName)
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

        public override string ToString ()
        {
            if (HasLibrary() && HasMember()) return $"{LibraryName}.{MemberName}";
            if (HasLibrary()) return LibraryName;
            if (HasMember()) return MemberName;
            return "";
        }

        public void Map (MappingContext context)
        {
            /*
             * Before:
             *  (#using "utils.crm" as u)
             *  u.help
             * 
             * After:
             *  ${utils.crm}.help
             */
            if (HasMember() && HasLibrary())
            {
                string alias = LibraryName!;

                Scope unit = context.GetScope(alias);
                string call = MemberName;

                LibraryName = $"{{{unit}}}";
                MemberName = call;
            }

            // Is a short local name with no library, so no linking needed
            if (HasMember())
                return;

            // Somehow only has a library name?
            throw new LinkingException($"This idenifier {this} with no member name somehow got through the parsing process?");
        }
    }
}