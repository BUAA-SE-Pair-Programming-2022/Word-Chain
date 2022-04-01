using System;
using System.Collections.Generic;

namespace core_src
{
    public class WordChain
    {
        private readonly List<string> _itself;
        private int _length;
        private readonly Dictionary<char, List<string>> _headCharDictionary;
        private readonly bool _allDifferentStart;
        private char _head, _tail;

        public WordChain(List<string> itself, int length, bool allDifferentStart)
        {
            _itself = itself;
            _length = length;
            _headCharDictionary = new Dictionary<char, List<string>>();
            _allDifferentStart = allDifferentStart;
            for (var a = 'a'; a <= 'z'; ++a)
                _headCharDictionary[a] = new List<string>();
            try
            {
                _head = itself[0][0];
                _tail = itself[itself.Count - 1][itself[itself.Count - 1].Length - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                _head = '\0';
                _tail = '\0';
            }
        }

        public List<string> GetChain()
        {
            return _itself;
        }

        public void Add(string str)
        {
            _itself.Add(str);
            _length += str.Length;
            _headCharDictionary[str[0]].Add(str);
        }

        public int GetLength()
        {
            return _length;
        }

        public int GetCount()
        {
            return _itself.Count;
        }

        public char GetHead()
        {
            return _head;
        }

        public char GetTail()
        {
            return _tail;
        }

        public bool GetAllDifferentStart()
        {
            return _allDifferentStart;
        }

        public void Print(char end)
        {
            foreach (var item in _itself)
                Console.Write(item + end);
        }
    }
}