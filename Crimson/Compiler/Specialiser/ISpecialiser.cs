using Compiler.Generaliser;

namespace Compiler.Specialiser
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISpecialiser
    {

        /// <summary>
        /// 
        /// <para>- Flattening member names -</para>
        /// <para>Each member knows its own name.</para>
        /// <para>All of these are singletons, so updating the name of a member in one location will update it in all locations.</para>
        /// <para>When a new member is declared, if the name already exists, it will be given a new name (e.g. 'originalname_1'). </para>
        /// <para>This name will be updated across all locations because the target member is a singleton.</para>
        /// 
        /// </summary>
        /// <param name="compilation"></param>
        /// <returns></returns>
        public AbstractSpecificAssemblyProgram Specialise (GeneralAssemblyProgram general);
    }
}