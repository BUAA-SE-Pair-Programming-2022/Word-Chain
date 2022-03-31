using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class ResField : MonoBehaviour
    {
        InputField _resField;
        string _value;

        public void Start()
        {
            _resField = GameObject.Find("ResField").GetComponent<InputField>();
        }

        public void Update()
        {
            _resField.text = _value;
        }

        public void SetValue(string v)
        {
            _value = v;
        }
    }
}