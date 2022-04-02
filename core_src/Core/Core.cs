using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System;

namespace Core
{
    public class Core
    {
        static public int gen_chains_all(HashSet<string> words, int len, ArrayList result)
        {
            char head = '\0', tail = '\0';
            bool enable_loop = true;

            var wg = new WordsGen(new List<string>(words));
            var processor = new Processor(wg.GetDict(), wg.GetList(), !enable_loop);
            processor.BuildConcatTree();
            processor.GenAll();
            var res = processor.GetRes();

            var chains = res.Where(item => head == '\0' || head == item.GetHead())
                .Where(item => tail == '\0' || tail == item.GetTail()).ToList();
            foreach (var c in chains) result.Add(c.GetChain());

            return result.Count;
        }

        static public int gen_chain_word(HashSet<string> words, int len, ArrayList result, char head, char tail,
            bool enable_loop)
        {
            var wg = new WordsGen(new List<string>(words));
            var processor = new Processor(wg.GetDict(), wg.GetList(), !enable_loop);
            processor.BuildConcatTree();
            processor.GenAll();
            var resByWordCount = processor.GetResByWordCount();

            foreach (var item in resByWordCount.Keys.SelectMany(key =>
                         resByWordCount[key].Where(item => head == '\0' || head == item.GetHead())
                             .Where(item => tail == '\0' || tail == item.GetTail())))
            {
                result.AddRange(item.GetChain());
                return result.Count;
            }

            result = new ArrayList();
            return 0;
        }

        static public int gen_chain_word_unique(HashSet<string> words, int len, ArrayList result)
        {
            char head = '\0', tail = '\0';
            bool enable_loop = true;

            var wg = new WordsGen(new List<string>(words));
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
                result.AddRange(item.GetChain());
                return result.Count;
            }

            result = new ArrayList();
            return 0;
        }

        static public int gen_chain_char(HashSet<string> words, int len, ArrayList result, char head, char tail,
            bool enable_loop)
        {
            var wg = new WordsGen(new List<string>(words));
            var processor = new Processor(wg.GetDict(), wg.GetList(), !enable_loop);
            processor.BuildConcatTree();
            processor.GenAll();
            var resByCharCount = processor.GetResByCharCount();

            foreach (var item in resByCharCount.Keys.SelectMany(key =>
                         resByCharCount[key].Where(item => head == '\0' || head == item.GetHead())
                             .Where(item => tail == '\0' || tail == item.GetTail())))
            {
                result.AddRange(item.GetChain());
                return result.Count;
            }

            result = new ArrayList();
            return 0;
        }
    }
}