

# Libraries
* [NLog](https://github.com/NLog/NLog)记录日志
* [Configration](https://github.com/aspnet/Configuration)读取配置文件
* [CommandLineParser](https://github.com/gsscoder/commandline)命令行参数辅助工具 


# Blogs
- [C#内存模型-The C# Memory Model in Theory and Practice](https://msdn.microsoft.com/en-us/magazine/jj863136.aspx)
- [Volatile keyword in C#](http://igoro.com/archive/volatile-keyword-in-c-memory-model-explained/)
- BranchPrediction
  - [fast-and-slow-if-statements-branch-prediction-in-modern-processors](http://igoro.com/archive/fast-and-slow-if-statements-branch-prediction-in-modern-processors/)
  - [why-is-it-faster-to-process-a-sorted-array-than-an-unsorted-array](http://stackoverflow.com/questions/11227809/why-is-it-faster-to-process-a-sorted-array-than-an-unsorted-array)
- [yield-return](https://www.kenneth-truyers.net/2016/05/12/yield-return-in-c/)
- DependencyInjection
  * [Inversion of Control Containers and the Dependency Injection patter](https://martinfowler.com/articles/injection.html)
  * [what-is-dependency-injection](http://stackoverflow.com/questions/130794/what-is-dependency-injection)
  * [Dependency-Injection-Demystified](http://www.jamesshore.com/Blog/Dependency-Injection-Demystified.html)

- CLR
  * [Prefer using Array.Length as upper for loop limit](https://github.com/dotnet/corefx/pull/15192)对于数组来说，将Length属性作为for循环的上限比将局部变量作为循环上限更高效，因为这样做可以使得CLR减少边界检查
  * [Array Bounds Check Elimination in the CLR](https://blogs.msdn.microsoft.com/clrcodegeneration/2009/08/13/array-bounds-check-elimination-in-the-clr/)这篇文章列举了CLR消除边界检查的几种情况 



















# Code

* [FileSystemWatcher](https://referencesource.microsoft.com/#System/services/io/system/io/FileSystemWatcher.cs)












# Code Reviews
* [Add System.Runtime.InteropServices.RuntimeInformation](https://github.com/dotnet/corefx/pull/4334)
    * 变量命名OSDescription
    * 属性-方法
    * 内存模型-多线程






























