using System;
using System.Collections;
using System.Collections.Generic;

namespace Core
{
    public class WordChain
    {
        private readonly List<string> _itself;
        private readonly int _length;
        private readonly bool _allDifferentStart;
        private readonly char _head;
        private readonly char _tail;

        public WordChain(List<string> itself, int length, bool allDifferentStart)
        {
            _itself = itself;
            _length = length;
            _allDifferentStart = allDifferentStart;
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

        public ArrayList GetChain()
        {
            return new ArrayList(_itself);
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
    }
}