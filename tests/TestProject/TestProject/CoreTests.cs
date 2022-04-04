using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;

namespace Core.Tests
{
    [TestClass()]
    public class CoreTests
    {
        [TestMethod()]
        public void gen_chain_word_headTest()
        {
            HashSet<string> words = new HashSet<string> { "abb", "bbc", "ccd", "dde", "wq", "qwe", "ert", "cew" };
            int len = words.Count;
            ArrayList result = new ArrayList();
            //test without head
            char head = '\0';
            char tail = '\0';
            bool enable_loop = false;
            int cnt = Core.gen_chain_word(words, len, result, head, tail, enable_loop);
            List<string> realResultWithoutHead = new List<string> { "abb", "bbc", "cew", "wq", "qwe", "ert" };
            Assert.AreEqual(cnt, realResultWithoutHead.Count);
            Assert.AreEqual(true, validChain(result, enable_loop));

            //test with head
            result = new ArrayList();
            head = 'c';
            cnt = Core.gen_chain_word(words, len, result, head, tail, enable_loop);
            List<string> realResultWithHead = new List<string> { "cew", "wq", "qwe", "ert" };
            Assert.AreEqual(cnt, realResultWithHead.Count);
            Assert.AreEqual(true, validChain(result, enable_loop));
        }

        [TestMethod()]
        public void gen_chain_word_tailTest()
        {
            HashSet<string> words = new HashSet<string> { "abc", "cwq", "qwer", "rtyu", "uii" };
            int len = words.Count;
            ArrayList result = new ArrayList();

            //test without tail
            char head = '\0';
            char tail = '\0';
            bool enable_loop = false;
            int cnt = Core.gen_chain_word(words, len, result, head, tail, enable_loop);
            List<string> realResultWithoutTail = new List<string> { "abc", "cwq", "qwer", "rtyu", "uii" };
            Assert.AreEqual(cnt, realResultWithoutTail.Count);
            Assert.AreEqual(true, validChain(result, enable_loop));

            //test with tail
            result = new ArrayList();
            tail = 'u';
            cnt = Core.gen_chain_word(words, len, result, head, tail, enable_loop);
            List<string> realResultWithTail = new List<string> { "abc", "cwq", "qwer", "rtyu" };
            Assert.AreEqual(cnt, realResultWithTail.Count);
            Assert.AreEqual(true, validChain(result, enable_loop));
        }

        [TestMethod()]
        public void gen_chain_word_loopTest()
        {
            HashSet<string> words = new HashSet<string> { "abc", "cwa", "acd", "cew", "wq", "dwq" };
            int len = words.Count;
            ArrayList result = new ArrayList();

            //test without loop
            char head = '\0';
            char tail = '\0';
            bool enable_loop = true;
            int cnt = Core.gen_chain_word(words, len, result, head, tail, enable_loop);
            List<string> realResultWithoutTail = new List<string> { "abc", "cwa", "acd", "dwq" };
            Assert.AreEqual(cnt, realResultWithoutTail.Count);
            Assert.AreEqual(true, validChain(result, enable_loop));

            //test with loop
            enable_loop = false;
            Assert.ThrowsException<LoopException>(() => cnt = Core.gen_chain_word(words, len, result, head, tail, enable_loop));
        }

        [TestMethod()]
        public void gen_chain_word_blendTest()
        {
            //public int gen_chain_word(List<string> words, int len, ref List<string> result, char head, char tail, bool enable_loop)
            HashSet<string> words = new HashSet<string> { "abb", "bbc", "ccd", "dde", "wq", "qwe", "ert", "cew", "bw" };
            int len = words.Count;
            ArrayList result = new ArrayList();

            //test with head and tail
            char head = 'b';
            char tail = 'd';
            bool enable_loop = false;
            int cnt = Core.gen_chain_word(words, len, result, head, tail, enable_loop);
            List<string> realResultWithHeadAndTail = new List<string> { "bbc", "ccd" };
            Assert.AreEqual(cnt, realResultWithHeadAndTail.Count);
            Assert.AreEqual(true, validChain(result, enable_loop));

            //test with head and loop
            head = 'w';
            tail = '\0';
            enable_loop = true;
            words = new HashSet<string> { "bc", "cw", "wb", "ber", "rw", "wc" };
            len = words.Count;
            ArrayList realResultWithHeadAndLoop = new ArrayList { "wc", "cw", "wb", "ber", "rw" };
            cnt = Core.gen_chain_word(words, len, realResultWithHeadAndLoop, head, tail, enable_loop);
            Assert.AreEqual(cnt, realResultWithHeadAndLoop.Count);
            Assert.AreEqual(true, validChain(result, enable_loop));

            //test with tail and loop
            head = '\0';
            tail = 'd';
            enable_loop = true;
            words = new HashSet<string> { "abc", "cwa", "acd", "dwq", "cde", "efg" };
            len = words.Count;
            ArrayList realResultWithTailAndLoop = new ArrayList { "abc", "cwa", "acd" };
            cnt = Core.gen_chain_word(words, len, realResultWithTailAndLoop, head, tail, enable_loop);
            Assert.AreEqual(cnt, realResultWithTailAndLoop.Count);
            Assert.AreEqual(true, validChain(result, enable_loop));

            //test with head, tail and loop
            head = 'q';
            tail = 'w';
            enable_loop = true;
            words = new HashSet<string> { "asd", "dfg", "qwe", "ewq", "qta", "qw" };
            ArrayList realResultWithHeadTailAndLoop = new ArrayList { "qwe", "ewq", "qw" };
            len = words.Count;
            cnt = Core.gen_chain_word(words, len, realResultWithHeadTailAndLoop, head, tail, enable_loop);
            Assert.AreEqual(cnt, realResultWithHeadTailAndLoop.Count);
            Assert.AreEqual(true, validChain(result, enable_loop));
        }

        [TestMethod()]
        public void gen_chains_allTest()
        {
            HashSet<string> words = new HashSet<string> { "woo", "oom", "moon", "noox" };
            int len = words.Count;
            ArrayList result = new ArrayList();

            bool enable_loop = false;
            int cnt = Core.gen_chains_all(words, len, result);
            Assert.AreEqual(cnt, 6);
            foreach (var v in result)
            {
                Assert.AreEqual(true, validChain((ArrayList)v, enable_loop));
            }
        }

        [TestMethod()]
        public void gen_chain_word_unique_Test()
        {
            HashSet<string> words = new HashSet<string> { "algea", "apple", "zoo", "elephant", "ant", "under", "fox", "dog", "moon", "leaf", "trick" };
            int len = words.Count;
            ArrayList result = new ArrayList();

            int cnt = Core.gen_chain_word_unique(words, len, result);
            Assert.AreEqual(cnt, 3);
            Assert.AreEqual(true, validChain(result, true));
        }

        [TestMethod()]
        public void gen_chain_charHead_Test()
        {
            HashSet<string> words = new HashSet<string> { "asdsdsdsdsdsdsdssdsd", "dz", "qwe", "efr", "rty", "eertr" };
            int len = words.Count;
            ArrayList result = new ArrayList();

            //test without head
            char head = '\0';
            char tail = '\0';
            bool enable_loop = false;
            int cnt = Core.gen_chain_char(words, len, result, head, tail, enable_loop);

            ArrayList realResultWithoutHead = new ArrayList { "asdsdsdsdsdsdsdssdsd", "dz" };
            Assert.AreEqual(cnt, realResultWithoutHead.Count);
            Assert.AreEqual(true, validChain(result, enable_loop));

            //test with head
            result = new ArrayList();
            head = 'e';
            cnt = Core.gen_chain_char(words, len, result, head, tail, enable_loop);
            ArrayList realResultWithHead = new ArrayList { "eertr", "rty" };
            Assert.AreEqual(cnt, realResultWithHead.Count);
            Assert.AreEqual(true, validChain(result, enable_loop));
        }

        [TestMethod]
        public void gen_chain_charTail_Test()
        {
            HashSet<string> words = new HashSet<string> { "asdsdsdsdsdsdsdssdsd", "dz", "omq", "qwe", "efr", "rty", "eertr" };
            int len = words.Count;
            ArrayList result = new ArrayList();

            //test without tail
            char head = '\0';
            char tail = '\0';
            bool enable_loop = false;
            int cnt = Core.gen_chain_char(words, len, result, head, tail, enable_loop);

            List<string> realResultWithoutTail = new List<string> { "asdsdsdsdsdsdsdssdsd", "dz" };
            Assert.AreEqual(cnt, realResultWithoutTail.Count);
            Assert.AreEqual(true, validChain(result, enable_loop));

            //test with tail
            result = new ArrayList();
            tail = 'r';
            cnt = Core.gen_chain_char(words, len, result, head, tail, enable_loop);
            List<string> realResultWithTail = new List<string> { "omq", "qwe ", "eertr" };
            Assert.AreEqual(cnt, realResultWithTail.Count);
            Assert.AreEqual(true, validChain(result, enable_loop));
        }

        [TestMethod]
        public void gen_chain_charLoop_Test()
        {
            HashSet<string> words = new HashSet<string> { "adadadd", "dqw", "qwe", "ewq", "qop", "poi" };
            int len = words.Count;
            ArrayList result = new ArrayList();

            //test without loop
            char head = '\0';
            char tail = '\0';
            bool enable_loop = false;
            int cnt;
            Assert.ThrowsException<LoopException>(() => cnt = Core.gen_chain_char(words, len, result, head, tail, enable_loop));

            //test with loop
            result = new ArrayList();
            enable_loop = true;
            cnt = Core.gen_chain_char(words, len, result, head, tail, enable_loop);
            List<string> realResultWithLoop = new List<string> { "qwe", "ewq", "qop", "poi" };
            Assert.AreEqual(cnt, realResultWithLoop.Count);
            Assert.AreEqual(true, validChain(result, enable_loop));
        }

        [TestMethod()]
        public void zero_Test()
        {
            HashSet<string> words = new HashSet<string> { };
            int len = words.Count;
            ArrayList result = new ArrayList();

            int cnt = Core.gen_chain_word_unique(words, len, result);
            Assert.AreEqual(cnt, 0);

            cnt = Core.gen_chain_char(words, len, result, '\0', '\0', false);
            Assert.AreEqual(cnt, 0);

            cnt = Core.gen_chain_word(words, len, result, '\0', '\0', false);
            Assert.AreEqual(cnt, 0);

            cnt = Core.gen_chains_all(words, len, result);
            Assert.AreEqual(cnt, 0);
        }

        public bool validChain(ArrayList result, bool enable_loop)
        {
            if (result.Count == 0) return true;

            Dictionary<char, bool> visited = new Dictionary<char, bool>();
            for (var i = 'a'; i <= 'z'; ++i)
                visited[i] = false;

            char preEnd = ((string)result[0])[((string)result[0]).Length - 1], curBegin = ((string)result[0])[0];
            visited[curBegin] = true;

            for (var i = 1; i < result.Count; ++i)
            {
                string cur = (string)result[i];
                curBegin = cur[0];
                if (curBegin != preEnd)
                {
                    return false;
                }
                if (!enable_loop && visited[curBegin])
                {
                    return false;
                }

                preEnd = cur[cur.Length - 1];
                visited[cur[0]] = true;
            }
            return true;
        }
    }
}
