using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class Processor
    {
        private readonly bool _detectLoop;
        private readonly Dictionary<char, List<string>> _wordsDict;
        private readonly List<string> _wordsList;
        private readonly List<ConcatTree> _roots = new List<ConcatTree>();
        private bool _resMade;
        private readonly Dictionary<char, List<WordChain>> _headCharDict = new Dictionary<char, List<WordChain>>();
        private readonly Dictionary<char, List<WordChain>> _tailCharDict = new Dictionary<char, List<WordChain>>();

        private readonly List<WordChain> _res = new List<WordChain>();
        private SortedDictionary<int, List<WordChain>> _resByWordCount = new SortedDictionary<int, List<WordChain>>();
        private SortedDictionary<int, List<WordChain>> _resByCharCount = new SortedDictionary<int, List<WordChain>>();

        public Processor(Dictionary<char, List<string>> dict, List<string> list, bool detectLoop)
        {
            _wordsDict = dict;
            _wordsList = list;
            _detectLoop = detectLoop;
        }

        private static void AddTreeNode(ConcatTree ct, char starting, IReadOnlyDictionary<char, List<string>> dict)
        {
            var visited = new List<string>();
            try
            {
                foreach (var str in dict[starting])
                {
                    if (visited.Contains(str)) continue;
                    var newKid = new ConcatTree(str);
                    var tDict = new Dictionary<char, List<string>>();
                    foreach (var c in dict.Keys)
                        tDict[c] = new List<string>(dict[c].ToArray());
                    tDict[starting].RemoveAt(tDict[starting].LastIndexOf(str));
                    ct.AddKid(newKid);
                    AddTreeNode(newKid, str[str.Length - 1], tDict);
                    visited.Add(str);
                }
            }
            catch (KeyNotFoundException)
            {
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

        private void MakeRes(ConcatTree ct, List<string> pres, HashSet<char> kindsOfStarts)
        {
            var tRes = new List<string>(pres.ToArray()) {ct.GetVal()};
            var res = new WordChain(tRes, tRes.Sum(item => item.Length), kindsOfStarts.Count == tRes.Count);
            char starting = pres[0][0], ending = tRes[tRes.Count - 1][tRes[tRes.Count - 1].Length - 1];
            if (starting == ending && _detectLoop)
                throw new LoopException();

            try
            {
                _headCharDict[starting].Add(res);
            }
            catch (KeyNotFoundException)
            {
                _headCharDict[starting] = new List<WordChain> { res };
            }
            
            try
            {
                _tailCharDict[ending].Add(res);
            }
            catch (KeyNotFoundException)
            {
                _tailCharDict[ending] = new List<WordChain> { res };
            }
                

            _res.Add(res);
            AddToDictionary(ref _resByCharCount, -res.GetLength(), res);
            AddToDictionary(ref _resByWordCount, -res.GetCount(), res);
        }

        private void TraverseRoot(ConcatTree ct, List<string> pres, HashSet<char> kindsOfStart)
        {
            if (ct == null) return;

            if (pres.Count > 0)
                MakeRes(ct, pres, kindsOfStart);

            var tPres = new List<string>(pres.ToArray()) {ct.GetVal()};

            foreach (var tct in ct.GetKids())
            {
                var t = new HashSet<char>(kindsOfStart);
                t.Add(tct.GetVal()[0]);
                TraverseRoot(tct, tPres, t);
            }
        }

        private void MakeResList()
        {
            var hash = new HashSet<char>();
            foreach (var ct in _roots)
            {
                hash.Add(ct.GetVal()[0]);
                TraverseRoot(ct, new List<string>(), hash);
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