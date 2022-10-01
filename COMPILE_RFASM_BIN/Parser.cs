using RF_ASM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace COMPILE_RFASM_BIN
{
    /// <summary>
    /// Class responsible for the pre-processing of non-compiled data. This inlcludes:
    /// <list type="number">
    ///     <item>Resolving compiler directives (.val, .jump)</item>
    ///     <item>Removing comments</item>
    /// </list>
    /// </summary>
    internal class Parser
    {
        public static readonly Regex WHITESPACE = new Regex(@"\s+");
        public static readonly Regex NON_WHITESPACE = new Regex(@"[^\s]");
        public static readonly Regex COMMENT = new Regex(@"//.+");
        public static readonly Regex LABEL_NAME = new Regex(@"(?<=::)(.*?)(?= )"); // Capture everything between "::" and " " // "::LABEL something" -> "LABEL"
        public static readonly Regex LABEL = new Regex(@"::(.*?)(?= )"); // Capture everything between "::" (inclusive) and " " (exclusive) // "::LABEL something" -> "::LABEL"
        public static readonly Regex WIDTH_VALUE = new Regex(@"(?<=\.width )(.*?)(?= )"); // Capture everything between ".width " (inclusive) and " " (exclusive) // "::LABEL something" -> "::LABEL"

        // Name, value
        public static Dictionary<string, string> constants = new Dictionary<string, string>();
        // Name, line index
        public static Dictionary<string, int> labels = new Dictionary<string, int>();

        private Metadata meta;

        public Parser(Metadata meta)
        {
            this.meta = meta;
        }

        /// <summary>
        /// Takes a list of lines and converts it to individual tokens.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns>A list of lists of the parsed output.</returns>
        public List<string> Parse(List<string> lines)
        {
            lines = RemoveWhiteSpace(lines);
            lines = RemoveBlankLines(lines);
            lines = RemoveComments(lines);
            lines = ReadMetadata(lines, meta);
            lines = FindConstants(lines);
            lines = FindLabels(lines);
            lines = ReplaceConstants(lines);
            lines = ReplaceLabels(lines);

            return lines;
        }

        // -
        // Parsing stages
        // -

        /// <summary>
        /// Removes unnecessary white space from the line. (replaces large gaps with a single space)
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static List<string> RemoveWhiteSpace(List<string> lines)
        {
            List<string> strings = new List<string>();
            foreach (string str in lines)
            {
                string swap = WHITESPACE.Replace(str, " ");
                strings.Add(swap);
            }
            return strings;
        }

        /// <summary>
        /// Removes blank lines from the compilation
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static List<string> RemoveBlankLines(List<string> lines)
        {
            List<string> newLines = new List<string>();
            foreach (string str in lines)
            {
                if (NON_WHITESPACE.IsMatch(str))
                {
                    newLines.Add(str);
                }
            }
            return newLines;
        }

        /// <summary>
        /// Remove comments from the fragments of the given line.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static List<string> RemoveComments(List<string> lines)
        {
            List<string> newLines = new List<string>();
            foreach (string line in lines)
            {
                string str = COMMENT.Replace(line, "");
                if (NON_WHITESPACE.IsMatch(str))
                {
                    newLines.Add(str);
                }
            }
            return newLines;
        }

        private List<string> ReadMetadata(List<string> lines, Metadata meta)
        {

            bool widthIsNotSet = true;

            List<string> newLines = new List<string>();
            foreach(string line in lines)
            {
                    string[] args = line.Split(' ');
                    if (args.Length > 0 && args[0].Equals(".width"))
                    {
                        if (args.Length > 1)
                        {
                            meta.DATA_WIDTH = (int) Byte.Parse(args[1].Replace("0x", ""));
                            widthIsNotSet = false;

                            if (args.Length > 2)
                            {
                                throw new ParsingException(line, ".width must have exactly 3 arguments (" + args.Length + " provided)");
                            }
                        }
                    }
                    continue; // Don't add line

                if (!widthIsNotSet)
                {
                    break;
                }
                
                // Nothing has triggered and some values are not set. We must be missing data!!
                if (widthIsNotSet)
                {
                    throw new ParsingException("Missing required metadata (need .width)");
                } else
                {
                    newLines.Add(line);
                }
                
            }

            return newLines;
        }

        /// <summary>
        /// Remove comments from the fragments of the given line.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static List<string> FindConstants(List<string> lines)
        {
            List<string> newLines = new List<string>();
            foreach (string line in lines)
            {
                string[] args = line.Split(' ');
                if (args.Length > 0 && args[0].Equals(".val"))
                {
                    if (args.Length > 1)
                    {
                        string name = args[1];
                        if (args.Length > 2)
                        {
                            string value = args[2];
                            if(args.Length > 3)
                            {
                                throw new ParsingException(line, ".val must have exactly 3 arguments (" + args.Length + " provided)");
                            }

                            constants.Add(name, value);
                            continue; // Don't add anything to the list of lines! 

                        } else
                        {
                            throw new ParsingException(line, ".val must have exactly 3 arguments (" + args.Length + " provided)");
                        }
                    }
                    else
                    {
                        throw new ParsingException(line, ".val must have exactly 3 arguments (" + args.Length + " provided)");
                    }
                }
            }
            return newLines;
        }

        /// <summary>
        /// Remove comments from the fragments of the given line.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static List<string> FindLabels(List<string> lines)
        {
            int numberOfLabelsFound = 0;

            List<string> newLines = new List<string>();
            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                string[] split = line.Split(" ");
                if (split.Length > 0 && LABEL_NAME.IsMatch(split[0])) // Check the first fragment only
                {
                    string name = LABEL_NAME.Matches(line)[0].Value;
                    labels.Add(name, i - numberOfLabelsFound);
                }
            }
            return newLines;
        }

        /// <summary>
        /// Remove comments from the fragments of the given line.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static List<string> ReplaceConstants(List<string> lines)
        {
            List<string> newLines = new List<string>();
            foreach (string line in lines)
            {
                string[] args = line.Split(" ");
                for (int i = 0; i < args.Length; i++)
                {
                    string arg = args[i];
                    if (constants.ContainsKey(arg))
                    {
                        // Replace the key with the value
                        constants.TryGetValue(arg, out args[i]);
                    }
                }

                StringBuilder builder = new StringBuilder();
                foreach(string arg in args)
                {
                    builder.Append(" " + arg);
                }
                newLines.Add(builder.ToString());
            }
            return newLines;
        }

        /// <summary>
        /// Remove comments from the fragments of the given line.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static List<string> ReplaceLabels(List<string> lines)
        {
            List<string> newLines = new List<string>();
            foreach (string line in lines)
            {
                string[] args = line.Split(" ");
                for(int i = 0; i < args.Length; i++)
                {
                    if (labels.ContainsKey(args[i]))
                    {
                        labels.TryGetValue(args[0], out int iAddr);
                        string addr = iAddr.ToString();
                        args[i] = addr;
                    }
                }

                StringBuilder builder = new StringBuilder();
                foreach (string arg in args)
                {
                    builder.Append(" " + arg);
                }
                newLines.Add(builder.ToString());
            }
            return newLines;
        }
    }
}
