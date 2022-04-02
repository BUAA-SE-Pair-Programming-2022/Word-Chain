using System;

namespace Core
{
    public class LoopException : Exception
    {
        public LoopException() : base("Loop detected! Please review your input :(")
        {

        }
    }
}