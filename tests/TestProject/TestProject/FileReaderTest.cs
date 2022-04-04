using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileReader.Tests
{
    [TestClass()]
    public class FileReaderTest
    {
        [TestMethod()]
        public void ReadFileAsStringExceptionTest()
        {
            string path = "C;\\no_exit.txt";
            FileReader fileReader = new FileReader(path);
            Assert.ThrowsException<FileNotFoundException>(() => fileReader.ReadFileAsString());

            path = null;
            fileReader = new FileReader(path);
            Assert.ThrowsException<FileNotFoundException>(() => fileReader.ReadFileAsString());
        }
    }
}
