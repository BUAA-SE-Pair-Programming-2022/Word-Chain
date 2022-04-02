using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Core;

namespace WordList
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // help
            if (args.Length == 1 && HelpRequired(args[0]))
            {
                DisplayHelp();
                return;
            }

            var argsParser = new ArgsParser(args);
            var words = Regex.Split(new FileReader.FileReader(argsParser.GetFile()).ReadFileAsString().ToLower(), @"[^a-zA-Z]+");
            var allArgs = argsParser.GetArgs();

            var generalType = allArgs[0] ? 0 : allArgs[1] ? 1 : allArgs[2] ? 2 : allArgs[3] ? 3 : -1;

            var inWords = new HashSet<string>(words);
            var inLen = words.Length;
            var resultMulti = new ArrayList();
            var resultSingle = new ArrayList();
            char starting = argsParser.H() ? argsParser.StartingChar() : '\0', ending = argsParser.T() ? argsParser.EndingChar() : '\0';
            var loopAllowed = argsParser.R();

            switch (generalType)
            {
                case 0:
                    Core.Core.gen_chains_all(inWords, inLen, resultMulti);
                    break;
                case 1:
                    Core.Core.gen_chain_word(inWords, inLen, resultSingle, starting, ending, loopAllowed);
                    break;
                case 2:
                    Core.Core.gen_chain_word_unique(inWords, inLen, resultSingle);
                    break;
                case 3:
                    Core.Core.gen_chain_char(inWords, inLen, resultSingle, starting, ending, loopAllowed);
                    break;
                default:
                    break;
            }

            if (generalType == 0)
            {
                Console.WriteLine(resultMulti.Count);
                foreach (ArrayList list in resultMulti)
                {
                    foreach (string word in list)
                    {
                        Console.Write(word + " ");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                var output = new FileOutput.FileOutput(resultSingle);
                output.PrintToSolutionTXT();
            }
        }

        private static bool HelpRequired(string param)
        {
            return param.Equals("--help") || param.Equals("--help");
            //return param is "--help" or "help";
        }

        private static void DisplayHelp()
        {
            Console.WriteLine(
                "Word List (1.0.0)\n" +
                "Usage: dotnet run --project <.csproj> <command> <args>\n" +
                "======================================================\n");
        }
    }
}