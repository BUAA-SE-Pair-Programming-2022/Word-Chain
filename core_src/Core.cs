using System.Collections.Generic;
using System.Linq;

namespace core_src
{
    public class Core
    {
        public int gen_chains_all(List<string> words, int len, out List<List<string>> result, char head, char tail,
            bool enable_loop)
        {
            var wg = new WordsGen(words);
            var processor = new Processor(wg.GetDict(), wg.GetList(), !enable_loop);
            processor.BuildConcatTree();
            processor.GenAll();
            var res = processor.GetRes();
            var chains = res.Where(item => head == '\0' || head == item.GetHead())
                .Where(item => tail == '\0' || tail == item.GetTail()).ToList();
            result = new List<List<string>>();
            foreach (var c in chains) result.Add(c.GetChain());
            return result.Count;
        }

        public int gen_chain_word(List<string> words, int len, out List<string> result, char head, char tail,
            bool enable_loop)
        {
            var wg = new WordsGen(words);
            var processor = new Processor(wg.GetDict(), wg.GetList(), !enable_loop);
            processor.BuildConcatTree();
            processor.GenAll();
            var resByWordCount = processor.GetResByWordCount();

            foreach (var item in resByWordCount.Keys.SelectMany(key =>
                         resByWordCount[key].Where(item => head == '\0' || head == item.GetHead())
                             .Where(item => tail == '\0' || tail == item.GetTail())))
            {
                result = item.GetChain();
                return result.Count;
            }

            result = new List<string>();
            return 0;
        }

        public int gen_chain_word_unique(List<string> words, int len, out List<string> result, char head, char tail,
            bool enable_loop)
        {
            var wg = new WordsGen(words);
            var processor = new Processor(wg.GetDict(), wg.GetList(), !enable_loop);
            processor.BuildConcatTree();
            processor.GenAll();
            var resByWordCount = processor.GetResByWordCount();

            foreach (var item in resByWordCount.Keys.SelectMany(key => from item in resByWordCount[key]
                         where head == '\0' || head == item.GetHead()
                         where tail == '\0' || tail == item.GetTail()
                         where item.GetAllDifferentStart()
                         select item))
            {
                result = item.GetChain();
                return result.Count;
            }

            result = new List<string>();
            return 0;
        }

        public int gen_chain_char(List<string> words, int len, out List<string> result, char head, char tail,
            bool enable_loop)
        {
            var wg = new WordsGen(words);
            var processor = new Processor(wg.GetDict(), wg.GetList(), !enable_loop);
            processor.BuildConcatTree();
            processor.GenAll();
            var resByCharCount = processor.GetResByCharCount();

            foreach (var item in resByCharCount.Keys.SelectMany(key =>
                         resByCharCount[key].Where(item => head == '\0' || head == item.GetHead())
                             .Where(item => tail == '\0' || tail == item.GetTail())))
            {
                result = item.GetChain();
                return result.Count;
            }

            result = new List<string>();
            return 0;
        }
    }
}