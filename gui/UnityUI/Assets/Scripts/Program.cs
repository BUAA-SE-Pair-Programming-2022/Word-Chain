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
                catch (Core.LoopException)
                {
                    ResFieldScript().SetValue("检测到单词环但是没有被允许！请检查输入或允许单词环存在。ʕ•́ᴥ•̀ʔ");
                    return;
                }
                catch (Core.OverflowException e)
                {
                    ResFieldScript().SetValue("结果总长度为：" + e.V().ToString() + "，超过了最大限制（20000）！请适当缩减数据规模。ʕ•́ᴥ•̀ʔ");
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
    }
}
