using System;

// 1. Wordlist.exe -n absolute_path_of_word_list
// 2. Wordlist.exe -w absolute_path_of_word_list 程序将从路径中读取单词文本，并将最长单词链输出至与Wordlist.exe同目录的solution.txt中，每次生成的txt文件需要覆盖上次生成的txt文件。
// 3. Wordlist.exe -m absolute_path_of_word_list 程序将从路径中读取单词文本，并将最长单词链输出至与Wordlist.exe同目录的solution.txt中，每次生成的txt文件需要覆盖上次生成的txt文件。
// 4. Wordlist.exe -c absolute_path_of_word_list
// 5. Wordlist.exe -h e -w absolute_path_of_word_list
// 6. Wordlist.exe -t t -w absolute_path_of_word_list
// 7. Wordlist.exe -r -w absolute_path_of_word_list

namespace core_src
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

            WordsGen wg;
            var argsParser = new ArgsParser(args);
            if (argsParser.GetFile() == null)
            {
                var words = Console.ReadLine()?.Split(' ');
                argsParser = new ArgsParser(words);
                wg = new WordsGen(argsParser.GetWords());
            }
            else
            {
                wg = new WordsGen(new FileReader(argsParser.GetFile()).ReadFileAsString().ToLower());
            }
            //var srcStr = argsParser.ReadFromFile() ? new FileReader(argsParser.GetFile()).ReadFileAsString() : argsParser.GetWords();

            //var wg = new WordsGen(srcStr.ToLower());


            var processor = new Processor(wg.GetDict(), wg.GetList(), !argsParser.R());
            processor.BuildConcatTree();

            //var resGen = new ResGen(new Core(processor), argsParser);
            var resGen = new ResGen(processor, argsParser);
            resGen.Gen();
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
                "======================================================\n" +
                "<.csproj> is to specify a .csproj project. \n" +
                "<command> is to choose a way to input, can be: \n" +
                "   file <FILENAME>\n" +
                "   stdin\n" +
                "<args> is to ask for special results, can be:\n" +
                "   -n: all word chains and the number of them;\n" +
                "   -w: word chains with most words;\n" +
                "   -m: word chains with most words that consist of words with different starting character;\n" +
                "   -c: word chains with most characters;\n" +
                "   -h <starting>: word chains with specific starting character, can be used with -t;\n" +
                "   -t <ending>: word chains with specific ending character, can be used with -h;\n" +
                "   -r: potential circles are allowed (normally they are not), e.g. [\"abc\", \"cba\"] is allowed when given -r.\n");
        }
    }
}