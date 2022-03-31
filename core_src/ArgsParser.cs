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

        public ArgsParser(IReadOnlyList<string> args)
        {
            _readFromFile = false;
            _starting = '0';
            _ending = '0';

            for (var i = 0; i < args.Count; ++i)
            {
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
                            _starting = args[i][0];
                        }
                        catch (Exception)
                        {
                            throw new ArgsMissCharacterException();
                        }

                        if (args[i].Length > 1)
                            throw new ArgsShouldBeCharException();

                        _h = true;
                        break;
                    case "-t":
                        ++i;
                        try
                        {
                            _ending = args[i][0];
                        }
                        catch (Exception)
                        {
                            throw new ArgsMissCharacterException();
                        }

                        if (args[i].Length > 1)
                            throw new ArgsShouldBeCharException();

                        _t = true;
                        break;
                    case "-r":
                        _r = true;
                        break;
                    default:
                        if (args[i].Substring(args[i].Length - 4, 4) == ".txt")
                        {
                            _file = args[i];
                        } 
                        else 
                            throw new ArgsTypeException();
                        break;
                }
            }

            _args = new List<bool>{_n, _w, _m, _c, _h, _t};
            var throwConflict = false;
            for (var k = 0; k < 4; ++k)
            {
                if (!_args[k]) continue;
                if (throwConflict)
                    throw new ArgsConflictException();
                throwConflict = true;
            }
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