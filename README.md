# Word-Chain
本次作业要求详见[2022北航敏捷软件工程课程博客](https://bbs.csdn.net/topics/605443466)。

> 语言：C# + .NET 4.0
>
> 开发环境：Windows 10 x64 + macOS Monterey 12.3.1
>
> IDE：Visual Studio Community 2022 + JetBrains Rider

## [Step 1](https://github.com/BUAA-SE-Pair-Programming-2022/Word-Chain/tree/step1)

### Tasks:

* 针对CLI（命令行界面）完成基本功能
* 部分异常处理
* GUI（图形界面）

### Tests:

见[tests/step1.txt](https://github.com/BUAA-SE-Pair-Programming-2022/Word-Chain/blob/master/tests/step1.txt)。

### CLI 使用方法

在终端切至 ` .\bin\` 或者从 `Releases` 中下载 `CLI`，运行：

```bash
./WordList.exe --help
```

以查看详细使用方法。

------

#### 一个例子

文本文件`C:\Users\VR\Desktop\core_src test.txt`内容如下：

```
Algebra
Apple
Zoo
Elephant
Under
Fox
Dog
Moon
Leaf
Trick
Pseudopseudohypoparathyroidism
```

运行 ` ./WordList.exe -w -h a -t t "C:\Users\VR\Desktop\core_src test.txt"` ，可以看到输出文件 `solution.txt` 中的结果为：

```
algebra
apple
elephant
```

### GUI 使用方法

基于CLI的图形化界面。

- macOS用户：下载解压 `WordList.app.zip` 后，直接运行即可。
- Windows用户：下载解压 `WordList.zip` 后，进入文件夹运行 `WordList.exe`。

使用说明详见[此处](https://github.com/BUAA-SE-Pair-Programming-2022/Word-Chain/blob/master/guibin/README.md)。

- 该图形化界面支持**直接手动输入**单词文本，也可以通过**输入文件的绝对路径**来导入文件内容。
- 在点击“确认”**之前**，请先选择单词链生成选项。特定选项之间允许组合。要指定单词链首尾字母，先选择对应选项后，在选项后的输入框内输入一个**英文字母**，然后点击确认。
- 要**导出**结果为`.txt`文件的话，则点击导出按钮后，生成的文件会默认保存在同`WordList`的目录下。
- 如果有**异常情况**出现，如文件不存在或操作不规范等，在**结果框内**会给出相应报错提示。

### 异常处理

详见[Step3](## Step 3)

## [Step 2](https://github.com/BUAA-SE-Pair-Programming-2022/Word-Chain/tree/step2)

### 封装

在Step 1的基础上，我们将核心计算模块、文件读入和输出模块封装成相互独立的部分。现在整个项目的文件结构为：

```
core_src
├── Core
├── FileOutput
├── FileReader
└── WordList
```

三个独立模块和主项目文件平行。

在与交换测试小组商讨完毕后，我们决定将我们的接口设计为：

```C#
// -n
public static int gen_chains_all(HashSet<string> words, int len, ArrayList result) { ... }
// -w
public static int gen_chain_word(HashSet<string> words, int len, ArrayList result, char head, char tail, bool enable_loop) { ... }
// -m
public static int gen_chain_word_unique(HashSet<string> words, int len, ArrayList result) { ... }
// -c
public static int gen_chain_char(HashSet<string> words, int len, ArrayList result, char head, char tail, bool enable_loop) { ... }
```

`.dll` 文件可见于 [`Word-Chain/core_src/WordList/WordList/bin/Debug/net5.0/`](https://github.com/BUAA-SE-Pair-Programming-2022/Word-Chain/tree/master/core_src/WordList/WordList/bin/Debug/net5.0)

## [Step 3](https://github.com/BUAA-SE-Pair-Programming-2022/Word-Chain/tree/step3)

### 异常处理

1. 单词文本隐含单词环（`LoopException`）

   > 给定的单词组中存在成环可能性，如`ab -ba`。

2. 结果超长（`OverflowException`）

   > 结果长度超过20,000。

3. 单词源文件不存在（`FileNotFoundException`）

   > 给定的文件绝对路径不存在。

4. 规则冲突（`ArgsConflictException`）

   > 传入多个关键规则违法，如 `-n` 和 `-m` 不可同时出现。

5. 关键规则缺失（`ArgsMissNecessaryException`）

   > 缺少一个关键规则（`-n, -m, -w, -c`）。

6. 首尾字母约束不合法（`ArgsShoudBeCharException`）

   > 规定 `-h `或 `-t` 时跟进的字符不是单个英文字母。

7. 非法规则（`ArgsTypeException`）

   > 传入不存在的规则，即不包含在 `-n, -m, -w, -c, -h, -t, -r` 和 `help, --help` 中的其他任何规则。

## [Step 4](https://github.com/BUAA-SE-Pair-Programming-2022/Word-Chain/tree/step4)

### 交换小组

- 郑皓文 19373339 —— 周五班
- 顾晨宇 19373333 —— 周二班

### 相关代码修改

有关该部分详见branch [`dev-combine`](https://github.com/BUAA-SE-Pair-Programming-2022/Word-Chain/tree/dev-combine)。
