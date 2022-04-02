using System;

namespace WordList
{
    public class ArgsTypeException : Exception
    {
        public ArgsTypeException() :
            base("Mis-args! Please review your args :( it has to be one or some of -n, -w, -m, -c, -h, -t, -r, along with text files ending with \".txt\". For details, use \"help\".")
        {

        }
    }
}