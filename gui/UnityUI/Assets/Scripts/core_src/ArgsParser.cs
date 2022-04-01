using System;
using System.Collections.Generic;
using core_src.exceptions;

namespace core_src
{
    public class ArgsParser
    {
        private readonly bool _readFromFile, _n, _w, _m, _c, _h, _t, _r;
        private readonly List<bool> _args;
        private readonly string _file;
        private readonly char _starting, _ending;

        public ArgsParser(List<bool> args, char head, char tail)
        {
            _readFromFile = false;
            _starting = head;
            _ending = tail;

            _args = args;
        }
        
        public bool ReadFromFile()
        {
            return _readFromFile;
        }

        public string GetFile()
        {
            return _file;
        }

        public List<bool> GetArgs()
        {
            return _args;
        }

        public bool R()
        {
            return _r;
        }

        public char StartingChar()
        {
            return _starting;
        }

        public char EndingChar()
        {
            return _ending;
        }
    }
}