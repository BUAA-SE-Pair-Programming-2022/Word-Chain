using UnityEngine;
using UnityEngine.UI;
using System;

namespace Scripts
{
    public class FileReader : MonoBehaviour
    {
        public FileReader() {}

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
                return "";
            }
            var res = "";
            foreach (var str in tem)
                res += str + " ";
            return res;
        }
    }
}