# Word-Chain
本次作业要求详见[2022北航敏捷软件工程课程博客](https://bbs.csdn.net/topics/605443466)。

## [Step 1](https://github.com/BUAA-SE-Pair-Programming-2022/Word-Chain/tree/step1)

### Tasks:

* 针对CLI（命令行界面）完成基本功能
* 部分异常处理
* GUI（图形界面）

### Tests:

见[tests/step1.txt](https://github.com/BUAA-SE-Pair-Programming-2022/Word-Chain/blob/master/tests/step1.txt)。

### 使用方法：

在`core_src`中进入终端，运行：

```bash
dotnet bin/Debug/net5.0/core_src.dll --help
```

以查看详细使用方法。

------

#### 一个例子

文本文件`/Users/springs/Desktop/test.txt`内容如下：

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

运行`dotnet bin/Debug/net5.0/core_src.dll -n -h a -t k /Users/springs/Desktop/test.txt`，可以看到输出为：

```
2
algebra apple elephant trick 
apple elephant trick 
```

