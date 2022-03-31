using UnityEngine;
using System;

namespace Scripts
{
    public class Program : MonoBehaviour
    {
        private string srcStr;
        private GameObject toggles, confirmButton, inputSystem, resField;
        private bool lastConfirmState;

        public void Start() 
        {
            toggles = GameObject.Find("Toggles");
            confirmButton = GameObject.Find("Confirm");
            inputSystem = GameObject.Find("InputSystem");
            resField = GameObject.Find("ResField");

            lastConfirmState = InputSystemScript().GetState();
        }

        public void Update() 
        {
            if (lastConfirmState ^ InputSystemScript().GetState())
            {
                lastConfirmState = InputSystemScript().GetState();

                srcStr = InputSystemScript().GetContent();      //////////////////////// CORE!!!!!!!!!!!!!!!!!!
                string newValue = srcStr;               // TODO: make actual res //////////////////////////////

                ResFieldScript().SetValue(newValue);
            }
        }

        private Scripts.InputSystem InputSystemScript()
        {
            return inputSystem.GetComponent<InputSystem>();
        }

        private Scripts.ResField ResFieldScript()
        {
            return resField.GetComponent<ResField>();
        }

        private Scripts.Toggles TogglesScript()
        {
            return toggles.GetComponent<Toggles>();
        }

        public void Method()
        {
            var wg = new WordsGen((srcStr ?? "").ToLower());        // To separate string into a list of words and make a dictionary. E.g. "abc cde" => ["abc", "cde"].
            var processor = new Processor(wg.GetDict(), wg.GetList(), false);
            processor.BuildConcatTree();                            // Very essential!!! This method organizes word chains as trees. (* Can be optimized)

            ///////////////////// core methods ///////////////////////
            // processor.GenAll();                                  // To generate all applicable chains.
            // processor.GenMaxQuan();                              // To generate chains with maximum word count.
            // processor.GenMaxLen();                               // To generate chains with maximum sum of words' lengths.
            // processor.GenSpecificHeadOrTail('a', true);          // To generate chains with specific starting character.
            // processor.GenSpecificHeadOrTail('t', false);         // To generate chains with specific ending character.
            // processor.SetDetectLoop(true);                       // To enable loop detection.
            // processor.GenAll();                                  // This time, LoopException would be induced.
            // processor.GenMaxQuanWithoutRepeatedHead();           // To generate chains with maximum word count without repeated starting letter.
            //////////////////////////////////////////////////////////
        }
    }
}
