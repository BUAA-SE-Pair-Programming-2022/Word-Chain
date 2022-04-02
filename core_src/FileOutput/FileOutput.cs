using System;
using System.Collections;
using System.Text;

namespace FileOutput
{
    public class FileOutput
    {
        private ArrayList _value;
        public FileOutput(ArrayList value)
        {
            _value = value;
        }

        public void PrintToSolutionTXT()
        {
            var filename = "solution.txt";
            var sb = new StringBuilder();

            foreach (var item in _value)
                sb.Append(item + "\n");

            System.IO.File.WriteAllText(filename, sb.ToString());
        }
    }
}
