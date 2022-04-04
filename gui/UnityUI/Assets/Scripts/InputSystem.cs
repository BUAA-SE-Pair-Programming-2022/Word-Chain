using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class InputSystem : MonoBehaviour
    {
        private InputField _field, _filePath;
        private Button _button;
        private bool _state;
        private Scripts.FileReader _fileReader;

        public void Start() 
        {
            _state = false;
            _field = GameObject.Find("InputField").GetComponent<InputField>();
            _filePath = GameObject.Find("FilePathInput").GetComponent<InputField>();
            _button = GameObject.Find("Confirm").GetComponent<Button>();
            _button.onClick.AddListener(ChangeState);

            _fileReader = GameObject.Find("InputSystem").GetComponent<FileReader>();
        }

        public void Update() 
        {
            if (_field.text != "")
            {
                _filePath.readOnly = true;
                _filePath.image.color = Color.gray;
            }
            else 
            {
                _filePath.readOnly = false;
                _filePath.image.color = new Color(246, 243, 237, 255);
            }

            if (_filePath.text != "")
            {
                _field.readOnly = true;
                _field.image.color = Color.gray;
            }
            else 
            {
                _field.readOnly = false;
                _field.image.color = new Color(246, 243, 237, 255);
            }
        }

        public string GetContent()
        {
            return _field.text != "" ? _field.text.Replace('\n', ' ') : ReadFile(_filePath.text);
        }

        private void ChangeState()
        {
            _state = !_state;
        }

        public bool GetState()
        {
            return _state;
        }

        private string ReadFile(string path)
        {
            return _fileReader.ReadFileAsString(path);
        }
    }
}