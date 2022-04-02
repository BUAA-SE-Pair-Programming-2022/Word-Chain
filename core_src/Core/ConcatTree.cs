using System.Collections.Generic;

namespace Core
{
    public class ConcatTree
    {
        private readonly List<ConcatTree> _kids = new List<ConcatTree>();
        private readonly string _val;

        public ConcatTree(string str)
        {
            this._val = str;
        }

        public void AddKid(ConcatTree newKid)
        {
            this._kids.Add(newKid);
        }

        public string GetVal()
        {
            return this._val;
        }

        public List<ConcatTree> GetKids()
        {
            return this._kids;
        }
    }
}