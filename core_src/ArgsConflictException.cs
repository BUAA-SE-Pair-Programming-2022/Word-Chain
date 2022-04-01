using System;

namespace core_src
{
    public class ArgsConflictException : Exception
    {
        public ArgsConflictException() :
            base("Only one of -n, -w, -m can be called! For details, use \"help\".")
        {

        }
    }
}