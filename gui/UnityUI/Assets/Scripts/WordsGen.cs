using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Scripts
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