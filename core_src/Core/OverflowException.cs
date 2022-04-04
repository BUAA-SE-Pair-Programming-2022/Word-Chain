using System;

namespace Core
{
    public class OverflowException
    {
        public OverflowException(int current)
        {
            Console.WriteLine(current.ToString() + "results has exceeded 20,000 which is not allowed :(");
        }
    }
}