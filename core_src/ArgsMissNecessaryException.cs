using System;

namespace core_src
{
    public class ArgsMissNecessaryException : Exception
    {
        public ArgsMissNecessaryException() :
            base("Arguments must contain one of -n, -w, -m, -c! For details, use \"help\".")
        {
        }
    }
}