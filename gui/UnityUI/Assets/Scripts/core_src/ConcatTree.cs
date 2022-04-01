using System.Collections.Generic;

namespace core_src
{
    public class ConcatTree
    {
        private readonly List<ConcatTree> _kids = new List<ConcatTree>();
        private string _val;

        public ConcatTree() { }

        public ConcatTree(string str)
        {
            this._val = str;
        }

        public ConcatTree(string str, List<ConcatTree> listOfNodes)
        {
            this._val = str;
            this._kids = listOfNodes;
        }

        public void AddKid(ConcatTree newKid)
        {
            this._kids.Add(newKid);
        }

        public void SetVal(string str)
        {
            this._val = str;
        }

        public string GetVal()
        {
            return this._val;
        }

        public List<ConcatTree> GetKids()
        {
            return this._kids;
        }

        public int KidsCount()
        {
            return this._kids.Count;
        }
    }
}
