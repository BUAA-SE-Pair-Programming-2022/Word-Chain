using System;
using System.Collections.Generic;
using System.Linq;

namespace core_src
{
    public class ResGen
    {
        private readonly ArgsParser _argsParser;
        private List<WordChain> _resultAll;
        private WordChain _result;

        private readonly SortedDictionary<int, List<WordChain>> _resByWordCount;
        private readonly SortedDictionary<int, List<WordChain>> _resByCharCount;
        private readonly List<WordChain> _res;

        /*public ResGen(Core core, ArgsParser argsParser)
        {
            _core = core;
            _argsParser = argsParser;
            _resultAll = new List<WordChain>();
        }*/

        public ResGen(Processor processor, ArgsParser argsParser)
        {
            var _processor = processor;
            _processor.GenAll();
            _resByWordCount = _processor.GetResByWordCount();
            _resByCharCount = _processor.GetResByCharCount();
            _res = _processor.GetRes();
            _argsParser = argsParser;
            _resultAll = new List<WordChain>();
        }

        public void Gen()
        {
            var args = _argsParser.GetArgs();
            int resInt;
            var generalType = args[0] ? 0 : args[1] ? 1 : args[2] ? 2 : args[3] ? 3 : -1;

            if (generalType != -1)
                resInt = generalType switch
                {
                    1 => gen_chain_word(out _result, args[4] ? _argsParser.StartingChar() : '\0',
                        args[5] ? _argsParser.EndingChar() : '\0'),
                    2 => gen_chain_word_unique(out _result, args[4] ? _argsParser.StartingChar() : '\0',
                        args[5] ? _argsParser.EndingChar() : '\0'),
                    3 => gen_chain_char(out _result, args[4] ? _argsParser.StartingChar() : '\0',
                        args[5] ? _argsParser.EndingChar() : '\0'),
                    _ => gen_chains_all(out _resultAll, args[4] ? _argsParser.StartingChar() : '\0',
                        args[5] ? _argsParser.EndingChar() : '\0')
                };
            else
                throw new ArgsMissNecessaryException();

            if (generalType == 0)
            {
                Console.WriteLine(resInt);
                foreach (var list in _resultAll)
                {
                    list.Print(' ');
                    Console.WriteLine();
                }
            }
            else
            {
                _result.Print('\n');
            }
        }

        public int gen_chains_all(out List<WordChain> result, char head, char tail)
        {
            result = _res.Where(item => head == '\0' || head == item.GetHead())
                .Where(item => tail == '\0' || tail == item.GetTail()).ToList();
            return result.Count;
        }

        public int gen_chain_word(out WordChain result, char head, char tail)
        {
            foreach (var item in _resByWordCount.Keys.SelectMany(key =>
                         _resByWordCount[key].Where(item => head == '\0' || head == item.GetHead())
                             .Where(item => tail == '\0' || tail == item.GetTail())))
            {
                result = item;
                return result.GetCount();
            }

            result = new WordChain(new List<string>(), 0, true);
            return 0;
        }

        public int gen_chain_word_unique(out WordChain result, char head, char tail)
        {
            foreach (var item in _resByWordCount.Keys.SelectMany(key => from item in _resByWordCount[key]
                         where head == '\0' || head == item.GetHead()
                         where tail == '\0' || tail == item.GetTail()
                         where item.GetAllDifferentStart()
                         select item))
            {
                result = item;
                return result.GetCount();
            }

            result = new WordChain(new List<string>(), 0, true);
            return 0;
        }

        public int gen_chain_char(out WordChain result, char head, char tail)
        {
            foreach (var item in _resByCharCount.Keys.SelectMany(key =>
                         _resByCharCount[key].Where(item => head == '\0' || head == item.GetHead())
                             .Where(item => tail == '\0' || tail == item.GetTail())))
            {
                result = item;
                return result.GetCount();
            }

            result = new WordChain(new List<string>(), 0, true);
            return 0;
        }
    }
}