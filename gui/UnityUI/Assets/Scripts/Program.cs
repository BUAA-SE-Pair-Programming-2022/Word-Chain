using UnityEngine;
using System;
using System.Collections.Generic;
using core_src;

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

                srcStr = InputSystemScript().GetContent();

                var argsParser = new ArgsParser(new List<bool>(
                    TogglesScript().GetArgs()), 
                    TogglesScript().GetHeadChar(), 
                    TogglesScript().GetTailChar());
                srcStr = InputSystemScript().GetContent();
                
                var wg = new WordsGen(srcStr.ToLower());
                var processor = new Processor(wg.GetDict(), wg.GetList(), !argsParser.R());
                processor.BuildConcatTree();
                
                var resGen = new ResGen(new Core(processor), argsParser);
                if (processor.GetPopupError()) 
                {
                    processor.SetPopupError(false);
                    print("Loop not allowed but detected! Please review your input!");
                } else {
                    string newValue = resGen.Gen();
                    ResFieldScript().SetValue(newValue);
                }
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
