using System;
using System.Collections.Generic;
using System.Text;
using core_src.exceptions;

namespace core_src
{
    public class ResGen
    {
        private readonly Core _core;
        private readonly ArgsParser _argsParser;
        private List<WordChain> _resultAll;
        private WordChain _result;

        public ResGen(Core core, ArgsParser argsParser)
        {
            _core = core;
            _argsParser = argsParser;
            _resultAll = new List<WordChain>();
        }

        public string Gen()
        {
            var args = _argsParser.GetArgs();
            int resInt = 0;
            var generalType = args[0] ? 0 : args[1] ? 1 : args[2] ? 2 : args[3] ? 3 : -1;

            if (generalType != -1)
            {
                resInt = generalType switch
                {
                    1 => _core.gen_chain_word(out _result, args[4] ? _argsParser.StartingChar() : '\0',
                        args[5] ? _argsParser.EndingChar() : '\0'),
                    2 => _core.gen_chain_word_unique(out _result, args[4] ? _argsParser.StartingChar() : '\0',
                        args[5] ? _argsParser.EndingChar() : '\0'),
                    3 => _core.gen_chain_char(out _result, args[4] ? _argsParser.StartingChar() : '\0',
                        args[5] ? _argsParser.EndingChar() : '\0'),
                    _ => _core.gen_chains_all(out _resultAll, args[4] ? _argsParser.StartingChar() : '\0',
                        args[5] ? _argsParser.EndingChar() : '\0')
                };
            }

            StringBuilder sb = new StringBuilder();
            if (generalType == 0)
            {
                sb.Append(resInt.ToString() + '\n');
                foreach (var list in _resultAll) 
                {
                    sb.Append(list.GenString(' '));
                    sb.Append('\n');
                }
                // Console.WriteLine(resInt);
                // foreach (var list in _resultAll)
                // {
                //     list.Print(' ');
                //     Console.WriteLine();
                // }
            }
            else 
            {
                sb.Append((_result ?? new WordChain(new List<string>(), 0, true)).GenString('\n'));
                // _result.Print('\n');
            }
            return sb.ToString();
        }
    }
}