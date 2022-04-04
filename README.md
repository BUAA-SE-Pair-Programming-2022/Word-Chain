# Word-Chain
本次作业要求详见[2022北航敏捷软件工程课程博客](https://bbs.csdn.net/topics/605443466)。

## DEV-COMBINE 组间Core核心计算模块交换

### 交换小组

* 郑皓文 19373339 —— 周五班
* 顾晨宇 19373333 —— 周二班

### 问题暴露

在只针对 `Core.dll` 的输入单词数组中，我们之前的程序要求其为已经完全处理好大小写不敏感的单词数组，这在两组交换后暴露出了问题。因为交换对象组的设计是在读入单词组后再进行有关大小写不敏感的处理，这就导致我们的核心模块在载入交换组的项目后没有进行转小写的相关处理，导致程序运行结果不符合预期。

### 相关修改

除了大小写转换相关操作，我们还进行了一些代码冗余上的消除。修改的文件有：

```
modified:   core_src/Core/Processor.cs
modified:   core_src/Core/WordsGen.cs
```

其中，在 `Processor.cs` 中，我们消除了不必要的 `using System;` 和 `catch` 之后的空操作；在 `WordsGen.cs` 中，我们对传入的原单词数组（`HashSet<string>`）统一进行了 `String.ToLower()` 的操作，避免了大小写敏感：

```C#
public WordsGen(IEnumerable<string> words)
{
	foreach (var v in words.Where(word => word.Length > 1))
    {
		try
        {
        	_dict[v[0]].Add(v.ToLower());
        }
        catch (KeyNotFoundException)
        {
        	_dict[v[0]] = new List<string> { v.ToLower() };
        }
        _list.Add(v.ToLower());
	} 
}
```

