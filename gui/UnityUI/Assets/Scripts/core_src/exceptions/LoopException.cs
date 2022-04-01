using System;

namespace core_src.exceptions
{
    public class LoopException : Exception
    {
        public LoopException() : base("Loop detected! Please review your input :(")
        {

        }
    }
}