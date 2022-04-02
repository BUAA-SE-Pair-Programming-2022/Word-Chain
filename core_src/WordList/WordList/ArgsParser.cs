using System;
using System.Collections.Generic;

namespace WordList
{
    public class ArgsParser
    {
        private readonly bool _n, _w, _m, _c, _h, _t, _r;
        private readonly List<bool> _args;
        private readonly string _file;
        private readonly char _starting, _ending;

        public ArgsParser(IReadOnlyList<string> args)
        {
            _starting = '\0';
            _ending = '\0';

            for (var i = 0; i < args.Count; ++i)
                switch (args[i])
                {
                    case "-n":
                        _n = true;
                        break;
                    case "-w":
                        _w = true;
                        break;
                    case "-m":
                        _m = true;
                        break;
                    case "-c":
                        _c = true;
                        break;
                    case "-h":
                        ++i;
                        try
                        {
                            _starting = args[i].ToLower()[0];
                        }
                        catch (Exception)
                        {
                            throw new ArgsMissCharacterException();
                        }

                        if (args[i].Length > 1)
                            throw new ArgsShouldBeCharException();

                        if (!Char.IsLetter(_starting))
                            throw new ArgsMissCharacterException();

                        _h = true;
                        break;
                    case "-t":
                        ++i;
                        try
                        {
                            _ending = args[i].ToLower()[0];
                        }
                        catch (Exception)
                        {
                            throw new ArgsMissCharacterException();
                        }

                        if (args[i].Length > 1)
                            throw new ArgsShouldBeCharException();

                        if (!Char.IsLetter(_ending))
                            throw new ArgsMissCharacterException();

                        _t = true;
                        break;
                    case "-r":
                        _r = true;
                        break;
                    default:
                        if (args[i].Length >= 4 && args[i].Substring(args[i].Length - 4, 4) == ".txt")
                            _file = args[i];
                        else
                            throw new ArgsTypeException();
                        break;
                }

            _args = new List<bool> {_n, _w, _m, _c, _h, _t};

            var throwConflict = false;
            var generalType = -1;
            for (var k = 0; k < 4; ++k)
            {
                if (!_args[k]) continue;
                if (throwConflict)
                    throw new ArgsConflictException();
                throwConflict = true;
                generalType = k;
            }

            if ((generalType == 0 || generalType == 2) && (_h || _t || _r))
                    throw new ArgsConflictException();

            if (!throwConflict) 
                throw new ArgsMissNecessaryException();
        }

        public string GetFile()
        {
            return _file;
        }

        public List<bool> GetArgs()
        {
            return _args;
        }

        public bool H()
        {
            return _h;
        }

        public bool T()
        {
            return _t;
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