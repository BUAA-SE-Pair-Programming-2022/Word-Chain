using System;

namespace core_src
{
    public class ArgsException : Exception
    {
        public ArgsException() :
            base("Mis-args! Please review your args :( it has to be one or some of -n, -w, -m, -c, -h, -t, -r. For details, use \"help\".")
        {

        }
    }
}