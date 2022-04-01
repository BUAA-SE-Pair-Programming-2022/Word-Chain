using System.Collections.Generic;
using System.Linq;

namespace core_src
{
    public class Processor
    {
        private bool _detectLoop;
        private readonly Dictionary<char, List<string>> _wordsDict;
        private readonly List<string> _wordsList;
        private readonly List<ConcatTree> _roots = new();
        private bool _resMade;
        private readonly Dictionary<char, List<WordChain>> _headCharDict = new();
        private readonly Dictionary<char, List<WordChain>> _tailCharDict = new();

        private List<WordChain> _res = new();
        private SortedDictionary<int, List<WordChain>> _resByWordCount = new();
        private SortedDictionary<int, List<WordChain>> _resByCharCount = new();

        public Processor(Dictionary<char, List<string>> dict, List<string> list, bool detectLoop)
        {
            _wordsDict = dict;
            _wordsList = list;
            _detectLoop = detectLoop;
            for (var c = 'a'; c <= 'z'; ++c)
            {
                _headCharDict[c] = new List<WordChain>();
                _tailCharDict[c] = new List<WordChain>();
            }
        }

        public void SetDetectLoop(bool detect)
        {
            _detectLoop = detect;
            _resMade = false;
            _res = new List<WordChain>();
            for (var c = 'a'; c <= 'z'; ++c)
            {
                _headCharDict[c] = new List<WordChain>();
                _tailCharDict[c] = new List<WordChain>();
            }
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

        private void AddToDictionary(ref SortedDictionary<int, List<WordChain>> sortedDict, int key, WordChain value)
        {
            try
            {
                sortedDict[key].Add(value);
            }
            catch (KeyNotFoundException)
            {
                sortedDict[key] = new List<WordChain> {value};
            }
        }

        private void MakeRes(ConcatTree ct, List<string> pres, Dictionary<char, int> kindsOfStarts)
        {
            var tRes = new List<string>(pres.ToArray()) {ct.GetVal()};
            var variety = 0;
            for (var a = 'a'; a <= 'z'; ++a)
                variety += kindsOfStarts[a] == 0 ? 0 : 1;
            var res = new WordChain(tRes, tRes.Sum(item => item.Length), variety == tRes.Count);
            char starting = pres[0][0], ending = tRes[tRes.Count - 1][tRes[tRes.Count - 1].Length - 1];
            if (starting == ending && _detectLoop)
                throw new LoopException();

            _headCharDict[starting].Add(res);
            _tailCharDict[ending].Add(res);

            _res.Add(res);
            AddToDictionary(ref _resByCharCount, -res.GetLength(), res);
            AddToDictionary(ref _resByWordCount, -res.GetCount(), res);
        }

        private void TraverseRoot(ConcatTree ct, List<string> pres, Dictionary<char, int> kindsOfStart)
        {
            if (ct == null) return;

            if (pres.Count > 0)
                MakeRes(ct, pres, kindsOfStart);

            var tPres = new List<string>(pres.ToArray()) {ct.GetVal()};

            foreach (var tct in ct.GetKids())
            {
                var t = new Dictionary<char, int>(kindsOfStart);
                ++t[tct.GetVal()[0]];
                TraverseRoot(tct, tPres, t);
            }
        }

        private void MakeResList()
        {
            var dict = new Dictionary<char, int>();
            foreach (var ct in _roots)
            {
                for (var c = 'a'; c <= 'z'; ++c)
                    dict[c] = 0;
                dict[ct.GetVal()[0]] = 1;
                TraverseRoot(ct, new List<string>(), dict);
            }

            _resMade = true;
        }

        public void GenAll()
        {
            if (!_resMade)
                MakeResList();
        }

        public List<WordChain> GetRes()
        {
            return _res;
        }

        public SortedDictionary<int, List<WordChain>> GetResByWordCount()
        {
            return _resByWordCount;
        }

        public SortedDictionary<int, List<WordChain>> GetResByCharCount()
        {
            return _resByCharCount;
        }
    }
}