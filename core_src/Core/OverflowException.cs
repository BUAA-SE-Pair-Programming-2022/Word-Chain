using System;

namespace Core
{
    public class OverflowException
    {
        public OverflowException()
        {
            Console.WriteLine("Results has exceeded 20,000 which is not allowed :(");
        }
    }
}