using System;

namespace core_src.exceptions
{
    public class FileNotFoundException : Exception
    {
        public FileNotFoundException(string filePath) :
            base("No such file! Please check your file's absolute path :(")
        {
            Console.WriteLine("Are you sure the file's absolute path is: \"" + filePath + "\"?");
        }
    }
}