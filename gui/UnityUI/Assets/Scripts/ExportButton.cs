using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class ExportButton : MonoBehaviour
    {
        private Button _export;

        public void Start()
        {
            _export = GameObject.Find("Export").GetComponent<Button>();
            _export.onClick.AddListener(Export);
        }

        private void Export()
        {
            string filename = "export.txt";
            System.IO.File.WriteAllText(filename, GameObject.Find("ResField").GetComponent<InputField>().text);
            print("Export Succeeded!");
        }
    }
}