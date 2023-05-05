namespace Linker.Specialising
{
    public abstract class AbstractSpecificAssemblyProgram
    {

        public abstract IEnumerable<Fragment> GetFragments ();
        public abstract string GetExtension ();
        public abstract void Write (string path);
    }
}
