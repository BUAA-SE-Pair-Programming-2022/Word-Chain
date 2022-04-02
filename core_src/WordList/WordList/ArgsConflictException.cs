using System;

namespace WordList
{
    public class ArgsConflictException : Exception
    {
        public ArgsConflictException() :
            base("Only one of -n, -w, -m can be called, and -n, -m must stand alone! For details, use \"help\".")
        {

        }
    }
}