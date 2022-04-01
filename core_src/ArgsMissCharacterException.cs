using System;

namespace core_src
{
    public class ArgsMissCharacterException : Exception
    {
        public ArgsMissCharacterException() :
            base("-h and -t must be followed with an alphabetical character! For details, use \"help\".")
        {

        }
    }
}