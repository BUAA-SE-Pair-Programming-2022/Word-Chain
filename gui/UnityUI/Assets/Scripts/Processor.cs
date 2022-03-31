using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts
{
    public class Processor
    {
        private bool _detectLoop;
        private readonly Dictionary<char, List<string>> _wordsDict;
        private readonly List<string> _wordsList;
        private readonly List<ConcatTree> _roots = new List<ConcatTree>();
        private bool _resMade;
        private List<List<string>> _res = new List<List<string>>();
        private readonly Dictionary<char, List<List<string>>> _headCharDict = new Dictionary<char, List<List<string>>>();
        private readonly Dictionary<char, List<List<string>>> _tailCharDict = new Dictionary<char, List<List<string>>>();
        private int _currentMaxLen, _currentMaxQuan, _currentMaxQuanWithoutRepe, _currentMaxLenIndex, _currentMaxQuanIndex, _currentMaxQuanWithoutRepeIndex;
        private List<string> _resForMaxQuanWithoutRepe = new List<string>();

        public Processor(Dictionary<char, List<string>> dict, List<string> list, bool detectLoop)
        {
            _wordsDict = dict;
            _wordsList = list;
            _detectLoop = detectLoop;
            for (var c = 'a'; c < 'z'; ++c)
            {
                _headCharDict[c] = new List<List<string>>();
                _tailCharDict[c] = new List<List<string>>();
            }
        }

        public void SetDetectLoop(bool detect)
        {
            _detectLoop = detect;
            _resMade = false;
            _res = new List<List<string>>();
            for (var c = 'a'; c < 'z'; ++c)
            {
                _headCharDict[c] = new List<List<string>>();
                _tailCharDict[c] = new List<List<string>>();
            }
            _currentMaxLen = 0;
            _currentMaxQuan = 0;
            _currentMaxLenIndex = 0;
            _currentMaxQuanIndex = 0;
        }

        private static void AddTreeNode(ConcatTree ct, char starting, IReadOnlyDictionary<char, List<string>> dict)
        {
            var visited = new List<string>();
            foreach (var str in dict[starting])
            {
                if (visited.Contains(str)) continue;
                var newKid = new ConcatTree(str);
                var tDict = new Dictionary<char, List<string>>();
                for (var c = 'a'; c <= 'z'; ++c)
                    tDict[c] = new List<string>(dict[c].ToArray());
                tDict[starting].RemoveAt(tDict[starting].LastIndexOf(str));
                ct.AddKid(newKid);
                AddTreeNode(newKid, str[str.Length - 1], tDict);
                visited.Add(str);
            }
        }

        public void BuildConcatTree()
        {
            var wordsCnt = _wordsList.Count;
            for (var a = 0; a < wordsCnt; ++a)
            {
                var tRoot = new ConcatTree(_wordsList[a]);
                _roots.Add(tRoot);
                _wordsDict[_wordsList[a][0]].RemoveAt(_wordsDict[_wordsList[a][0]].LastIndexOf(_wordsList[a]));
                AddTreeNode(tRoot, _wordsList[a][_wordsList[a].Length - 1], _wordsDict);
                _wordsDict[_wordsList[a][0]].Add(_wordsList[a]);
            }
        }

        private bool MakeRes(ConcatTree ct, List<string> pres, HashSet<char> preFirstLetters, bool repeated)
        {
            if (pres.Count + 1 > _currentMaxQuan)
            {
                _currentMaxQuan = pres.Count + 1;
                _currentMaxQuanIndex = _res.Count;
            }

            var tLen = pres.Sum(str => str.Length);
            tLen += ct.GetVal().Length;
            if (tLen > _currentMaxLen)
            {
                _currentMaxLen = tLen;
                _currentMaxLenIndex = _res.Count;
            }

            var tRepeated = repeated;
            var tRes = new List<string>(pres.ToArray()) { ct.GetVal() };
            char starting = pres[0][0], ending = tRes[tRes.Count - 1][tRes[tRes.Count - 1].Length - 1];
            if (starting == ending && _detectLoop) 
            {
                Console.WriteLine("LoopException");
                // throw new LoopException();
            }

            if (preFirstLetters.Contains(ct.GetVal()[0]) || repeated)
            {
                tRepeated = true;
            }
            else if (pres.Count + 1 > _currentMaxQuanWithoutRepe)
            {
                _currentMaxQuanWithoutRepe = pres.Count + 1;
                _currentMaxQuanWithoutRepeIndex = _res.Count;
                _resForMaxQuanWithoutRepe = new List<string>(tRes);
            }

            if (_res.Contains(tRes)) return tRepeated;
            _headCharDict[starting].Add(tRes);
            _tailCharDict[ending].Add(tRes);
            _res.Add(tRes);
            return tRepeated;
        }

        private void TraverseRoot(ConcatTree ct, List<string> pres, HashSet<char> preFirstLetters, bool repeated)
        {
            if (ct == null) return;

            bool tRepeated = repeated;
            if (pres.Count > 0)
                tRepeated = MakeRes(ct, pres, preFirstLetters, repeated);

            var tPres = new List<string>(pres.ToArray()) { ct.GetVal() };
            var tPreFirstLetters = new HashSet<char>(preFirstLetters) {ct.GetVal()[0]};
            foreach (var tct in ct.GetKids())
                TraverseRoot(tct, tPres, tPreFirstLetters, tRepeated);
        }

        private void MakeResList()
        {
            foreach (var ct in _roots)
                TraverseRoot(ct, new List<string>(), new HashSet<char>(), false);

            _resMade = true;
        }

        public void GenAll()
        {
            if (!_resMade) MakeResList();
            foreach (var i in _res)
            {
                foreach (var ii in i)
                    Console.Write(ii + " ");
                Console.WriteLine();
            }
        }

        public void GenMaxQuan()
        {
            if (!_resMade) MakeResList();
            foreach (var str in _res[_currentMaxQuanIndex])
                Console.Write(str + " ");
            Console.WriteLine();
        }

        public void GenMaxLen()
        {
            if (!_resMade) MakeResList();
            foreach (var str in _res[_currentMaxLenIndex])
                Console.Write(str + " ");
            Console.WriteLine();
        }

        public void GenSpecificHeadOrTail(char c, bool head)
        {
            if (!_resMade) MakeResList();
            foreach (var list in head ? _headCharDict[c] : _tailCharDict[c])
            {
                foreach (var str in list)
                    Console.Write(str + " ");
                Console.WriteLine();
            }
        }

        public void GenMaxQuanWithoutRepeatedHead()
        {
            if (!_resMade) MakeResList();
            foreach (var str in _resForMaxQuanWithoutRepe)
                Console.Write(str + " ");
        }
    }
}