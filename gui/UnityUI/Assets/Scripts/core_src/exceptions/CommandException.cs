using System;

namespace core_src.exceptions
{
    public class CommandException : Exception
    {
        public CommandException(int type) :
            base("Mis-command! Please review your command :( For details, use \"help\".")
        {
            // Exceptions
            // 0 : Missing file
            // 1 : Wrong type 

            switch (type)
            {
                case 0:
                    Console.WriteLine("******* Please specify a file. *******");
                    break;
                case 1:
                    Console.WriteLine("******* It has to be \"file <FILENAME>\" or \"stdin\". *******");
                    break;
                default:
                    return;
            }
        }
    }
}