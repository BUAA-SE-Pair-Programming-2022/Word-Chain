using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class WordsGen
    {
        private readonly Dictionary<char, List<string>> _dict = new Dictionary<char, List<string>>();
        private readonly List<string> _list = new List<string>();

        public WordsGen(IEnumerable<string> words)
        {
            for (var c = 'a'; c <= 'z'; ++c)
                _dict[c] = new List<string>();
            foreach (var v in words.Where(word => word.Length > 1))
                if (v.Length > 1)
                {
                    _dict[v[0]].Add(v.ToLower());
                    _list.Add(v.ToLower());
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