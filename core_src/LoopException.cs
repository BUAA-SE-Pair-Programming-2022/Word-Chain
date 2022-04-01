using System;

namespace core_src
{
    public class LoopException : Exception
    {
        public LoopException() : base("Loop detected! Please review your input :(")
        {

        }
    }
}