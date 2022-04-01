using System;

namespace core_src
{
    public class ArgsShouldBeCharException : Exception
    {
        public ArgsShouldBeCharException() :
            base("-h and -t must be follow by an alphabetical character but not a string! For details, use \"help\".")
        {

        }
    }
}