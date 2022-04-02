namespace FileReader
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
            try
            {
                return System.IO.File.ReadAllText(_absolutePath);
            }
            catch (System.Exception)
            {
                throw new FileNotFoundException(_absolutePath);
            }
        }
    }
}