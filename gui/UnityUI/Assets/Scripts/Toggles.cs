using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Scripts 
{
    public class Toggles : MonoBehaviour
    {
        private bool _n, _w, _m, _c, _h, _t, _r;
        private Toggle _tN, _tW, _tM, _tC, _tH, _tT, _tR;

        private Toggle FindToggle(string name) 
        {
            return GameObject.Find(name).GetComponent<Toggle>();
        }

        public void Start() 
        {
            _tN = FindToggle("N");
            _tW = FindToggle("W");
            _tM = FindToggle("M");
            _tC = FindToggle("C");
            _tH = FindToggle("H");
            _tT = FindToggle("T");
            _tR = FindToggle("R");
        }

        public void Update() 
        {
            Maintain();
            _n = _tN.isOn;
            _w = _tW.isOn;
            _m = _tM.isOn;
            _c = _tC.isOn;
            _h = _tH.isOn;
            _t = _tT.isOn;
            _r = _tR.isOn;
        }

        private void ChangeColor(Toggle toggle, bool isEnabled)
        {
            ColorBlock cb = toggle.colors;
            if (isEnabled)
            {
                cb.colorMultiplier = 1f;
            }
            else
            {
                cb.colorMultiplier = 0.4f;
            }
            toggle.colors = cb;
        }

        private void Maintain()
        {
            _tN.enabled = _n || !(_n || _w || _m || _c);
            _tW.enabled = _w || !(_n || _w || _m || _c);
            _tM.enabled = _m || !(_n || _w || _m || _c);
            _tC.enabled = _c || !(_n || _w || _m || _c);
            ChangeColor(_tN, _tN.enabled);
            ChangeColor(_tW, _tW.enabled);
            ChangeColor(_tM, _tM.enabled);
            ChangeColor(_tC, _tC.enabled);
        }

        public bool GetN()
        {
            return _n;
        }

        public bool GetW()
        {
            return _w;
        }

        public bool GetM()
        {
            return _m;
        }

        public bool GetC()
        {
            return _c;
        }

        public bool GetH()
        {
            return _h;
        }

        public bool GetT()
        {
            return _t;
        }

        public bool GetR()
        {
            return _r;
        }
    }
}