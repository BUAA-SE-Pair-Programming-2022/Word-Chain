using System.Collections.Generic;
using System.Linq;

namespace core_src
{
    public class Core
    {
        private readonly SortedDictionary<int, List<WordChain>> _resByWordCount;
        private readonly SortedDictionary<int, List<WordChain>> _resByCharCount;
        private readonly List<WordChain> _res;

        public Core(Processor processor)
        {
            var _processor = processor;
            _processor.GenAll();
            _resByWordCount = _processor.GetResByWordCount();
            _resByCharCount = _processor.GetResByCharCount();
            _res = _processor.GetRes();
        }
        
        public int gen_chains_all(out List<WordChain> result, char head, char tail)
        {
            result = _res.Where(item => head == '\0' || head == item.GetHead()).Where(item => tail == '\0' || tail == item.GetTail()).ToList();
            return result.Count;
        }
        
        public int gen_chain_word(out WordChain result, char head, char tail)
        {
            foreach (var item in _resByWordCount.Keys.SelectMany(key => _resByWordCount[key].Where(item => head == '\0' || head == item.GetHead()).Where(item => tail == '\0' || tail == item.GetTail())))
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
                         where item.GetAllDifferentStart() select item))
            {
                result = item;
                return result.GetCount();
            }

            result = new WordChain(new List<string>(), 0, true);
            return 0;
        }
        
        public int gen_chain_char(out WordChain result, char head, char tail)
        {
            foreach (var item in _resByCharCount.Keys.SelectMany(key => _resByCharCount[key].Where(item => head == '\0' || head == item.GetHead()).Where(item => tail == '\0' || tail == item.GetTail())))
            {
                result = item;
                return result.GetCount();
            }

            result = new WordChain(new List<string>(), 0, true);
            return 0;
        }
    }
}