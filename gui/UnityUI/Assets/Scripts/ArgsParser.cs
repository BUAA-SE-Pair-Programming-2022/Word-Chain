using System;
using System.Collections.Generic;

namespace Scripts
{
    public class ArgsParser
    {
        private readonly List<bool> _args;
        private readonly char _starting, _ending;

        public ArgsParser(List<bool> args, char head, char tail)
        {
            _starting = head;
            _ending = tail;

            _args = args;
        }

        public List<bool> GetArgs()
        {
            return _args;
        }

        public bool R()
        {
            return _args[6];
        }

        public char StartingChar()
        {
            return _starting;
        }

        public char EndingChar()
        {
            return _ending;
        }

        public int GetGeneralType()
        {
            return _args[0] ? 0 : _args[1] ? 1 : _args[2] ? 2 : _args[3] ? 3 : -1;
        }
    }
}