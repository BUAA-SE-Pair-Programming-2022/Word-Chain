using System;
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
            foreach (var v in words.Where(word => word.Length > 1))
            {
                try
                {
                    _dict[v[0]].Add(v.ToLower());
                }
                catch (KeyNotFoundException)
                {
                    _dict[v[0]] = new List<string> { v.ToLower() };
                }
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