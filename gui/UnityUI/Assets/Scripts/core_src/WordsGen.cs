using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace core_src
{
    public class WordsGen
    {
        private readonly Dictionary<char, List<string>> _dict = new Dictionary<char, List<string>>();
        private readonly List<string> _list = new List<string>();

        public WordsGen(string str)
        {
            var tem = Regex.Split(str, @"[^a-zA-Z]+");
            for (var c = 'a'; c <= 'z'; ++c)
                _dict[c] = new List<string>();
            foreach (var v in tem)
                if (v.Length > 1)
                {
                    _dict[v[0]].Add(v);
                    _list.Add(v);
                }
        }

        public WordsGen(List<string> words)
        {
            for (var c = 'a'; c <= 'z'; ++c)
                _dict[c] = new List<string>();
            foreach (var word in words.Where(word => word.Length > 1))
            {
                _dict[word[0]].Add(word.ToLower());
                _list.Add(word.ToLower());
            }
        }

        public Dictionary<char, List<string>> GetDict()
        {
            return _dict;
        }

        public List<string> GetList()
        {
            return _list;
        }
    }
}