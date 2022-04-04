using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordList.Tests
{
    [TestClass()]
    public class ArgsParserTests
    {
        [TestMethod()]
        public void basicTest()
        {
            string[] args = new string[] { "-n", "test.txt" };
            ArgsParser argsParser = new ArgsParser(args);
            Assert.AreEqual(true, argsParser.GetArgs()[0]);

            args[0] = "-w";
            argsParser = new ArgsParser(args);
            Assert.AreEqual(true, argsParser.GetArgs()[1]);

            args[0] = "-m";
            argsParser = new ArgsParser(args);
            Assert.AreEqual(true, argsParser.GetArgs()[2]);

            args[0] = "-c";
            argsParser = new ArgsParser(args);
            Assert.AreEqual(true, argsParser.GetArgs()[3]);
            Assert.AreEqual("test.txt", argsParser.GetFile());
        }

        [TestMethod()]
        public void ArgsTypeExceptionTest()
        {
            string[] args = new string[] { "-q", "test.txt" };
            ArgsParser argsParser;
            Assert.ThrowsException<ArgsTypeException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "-n", "notxt" };
            Assert.ThrowsException<ArgsTypeException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "-x", "notxt" };
            Assert.ThrowsException<ArgsTypeException>(() => argsParser = new ArgsParser(args));

        }

        [TestMethod()]
        public void ArgsShouldBeCharExceptionTest()
        {
            string[] args = new string[] { "-w", "-h", "ab", "test.txt" };
            ArgsParser argsParser;
            Assert.ThrowsException<ArgsShouldBeCharException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "-w", "-t", "-r", "test.txt" };
            Assert.ThrowsException<ArgsShouldBeCharException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "-w", "-t", "test.txt" };
            Assert.ThrowsException<ArgsShouldBeCharException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "-w", "-h", "test.txt" };
            Assert.ThrowsException<ArgsShouldBeCharException>(() => argsParser = new ArgsParser(args));
        }

        [TestMethod()]
        public void ArgsMissNecessaryExceptionTest()
        {
            string[] args = new string[] { "test.txt" };
            ArgsParser argsParser;
            Assert.ThrowsException<ArgsMissNecessaryException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-r" };
            Assert.ThrowsException<ArgsMissNecessaryException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-h", "h" };
            Assert.ThrowsException<ArgsMissNecessaryException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-t", "h" };
            Assert.ThrowsException<ArgsMissNecessaryException>(() => argsParser = new ArgsParser(args));
        }


        [TestMethod()]
        public void ArgsMissCharacterExceptionTest()
        {
            string[] args = new string[] { "test.txt", "-w", "-h" };
            ArgsParser argsParser;
            Assert.ThrowsException<ArgsMissCharacterException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-w", "-t" };
            Assert.ThrowsException<ArgsMissCharacterException>(() => argsParser = new ArgsParser(args));
        }

        [TestMethod()]
        public void ArgsConflictExceptionTest()
        {
            string[] args = new string[] { "test.txt", "-n", "-w" };
            ArgsParser argsParser;
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-n", "-w" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-n", "-c" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-n", "-m" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-n", "-w", "-c" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-n", "-w", "-m" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-n", "-c", "-m" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-n", "-w", "-c", "-m" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-w", "-c" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-w", "-m" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-w", "-c", "-m" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-c", "-m" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));



            args = new string[] { "test.txt", "-n", "-h", "a" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-n", "-t", "a" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-n", "-r" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-n", "-r", "-h", "a" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-n", "-r", "-t", "a" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-n", "-r", "-h", "a", "-t", "a" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-n", "-h", "a", "-t", "a" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));


            args = new string[] { "test.txt", "-m", "-h", "a" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-m", "-t", "a" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-m", "-r" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-m", "-r", "-h", "a" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-m", "-r", "-t", "a" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-m", "-r", "-h", "a", "-t", "a" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));

            args = new string[] { "test.txt", "-m", "-h", "a", "-t", "a" };
            Assert.ThrowsException<ArgsConflictException>(() => argsParser = new ArgsParser(args));
        }
    }
}


