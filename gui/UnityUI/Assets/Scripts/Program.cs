using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Core;

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

                var argsParser = new ArgsParser(new List<bool>(
                    TogglesScript().GetArgs()), 
                    TogglesScript().GetHeadChar(), 
                    TogglesScript().GetTailChar());

                try 
                {
                    srcStr = InputSystemScript().GetContent();
                }
                catch (FileNotFoundException)
                {
                    ResFieldScript().SetValue("文件不存在！请检查绝对路径或手动输入。ʕ•́ᴥ•̀ʔ");
                    return;
                }
                var words = Regex.Split(srcStr.ToLower(), @"[^a-zA-Z]+");
                var inWords = new HashSet<string>(words);
                var inLen = inWords.Count;
                var resultMulti = new ArrayList();
                var resultSingle = new ArrayList();
                var starting = argsParser.StartingChar();
                var ending = argsParser.EndingChar();
                var loopAllowed = argsParser.R();

                try 
                {
                    switch (argsParser.GetGeneralType())
                    {
                        case 0:
                            Core.Core.gen_chains_all(inWords, inLen, resultMulti);
                            break;
                        case 1:
                            Core.Core.gen_chain_word(inWords, inLen, resultSingle, starting, ending, loopAllowed);
                            break;
                        case 2:
                            Core.Core.gen_chain_word_unique(inWords, inLen, resultSingle);
                            break;
                        case 3:
                            Core.Core.gen_chain_char(inWords, inLen, resultSingle, starting, ending, loopAllowed);
                            break;
                        default:
                            ResFieldScript().SetValue("请在左侧至少选择一个生成规则。ʕ•́ᴥ•̀ʔ");
                            break;
                    }
                }
                catch (LoopException)
                {
                    ResFieldScript().SetValue("检测到单词环但是没有被允许！请检查输入或允许单词环存在。ʕ•́ᴥ•̀ʔ");
                    return;
                }
                

                var res = new StringBuilder();
                if (argsParser.GetGeneralType() == 0)
                {
                    res.Append(resultMulti.Count.ToString() + '\n');
                    foreach (ArrayList chain in resultMulti)
                    {
                        foreach (var item in chain) 
                        {
                            res.Append(item + " ");
                        }
                        res.AppendLine();
                    }
                }
                else 
                {
                    foreach (var item in resultSingle)
                    {
                        res.Append(item + "\n");
                    }
                }

                ResFieldScript().SetValue(res.ToString());
                
                // var wg = new WordsGen(srcStr.ToLower());
                // var processor = new Processor(wg.GetDict(), wg.GetList(), !argsParser.R());
                // processor.BuildConcatTree();
                
                // if (TogglesScript().GetAllFalse())
                // {
                //     ResFieldScript().SetValue("请在确认前至少选择一项功能。");
                // } 
                // else if (!Char.IsLetter(TogglesScript().GetHeadChar()) && TogglesScript().GetHeadChar() != '\0' || 
                //          !Char.IsLetter(TogglesScript().GetTailChar()) && TogglesScript().GetTailChar() != '\0')
                // {
                //     ResFieldScript().SetValue("指定的首尾字符必须为英文字母。");    
                // }
                // else 
                // {
                //     var resGen = new ResGen(new Core(processor), argsParser);
                //     if (processor.GetPopupError()) 
                //     {
                //         processor.SetPopupError(false);
                //         ResFieldScript().SetValue("输入中存在单词环但是没有被允许！请检查输入或调整指令。");
                //     } else {
                //         string newValue = resGen.Gen();
                //         ResFieldScript().SetValue(InputSystemScript().FileNotFound() && InputSystemScript().GetFromFile() ? "输入内容为空或者文件不存在！请指定输入或检查文件绝对路径。" : newValue);
                //     }
                // }
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
