using UnityEngine;
using UnityEngine.UI;
using System;

namespace Scripts
{
    public class FileReader : MonoBehaviour
    {
        private bool _fileNotFound;

        public FileReader() 
        {
            _fileNotFound = false;
        }

        public string ReadFileAsString(string _absolutePath)
        {
            string[] tem;
            try
            {
                tem = System.IO.File.ReadAllLines(_absolutePath);
            }
            catch (Exception)
            {
                print("File not found!");   // TODO: Exception Popup.
                _fileNotFound = true;
                return "";
            }
            _fileNotFound = false;
            var res = "";
            foreach (var str in tem)
                res += str + " ";
            return res;
        }

        public bool GetFileNotFound()
        {
            return _fileNotFound;
        }
    }
}