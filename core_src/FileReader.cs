namespace core_src
{
    public class FileReader
    {
        private readonly string _absolutePath;

        public FileReader(string absolutePath)
        {
            _absolutePath = absolutePath;
        }

        public string ReadFileAsString()
        {
            return System.IO.File.ReadAllText(_absolutePath);
        }
    }
}