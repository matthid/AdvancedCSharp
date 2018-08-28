- title : Advanced C# and internals
- description : internals and implementation
- author : Matthias Dittrich
- theme : league
- transition : default

***

## Advanced C# and internals

<img style="border-style: none" border="0" src="images/AIT-Logo_small.jpg" />

### **Matthias Dittrich**, AIT GmbH <br /> [@matthi\_\_d](http://twitter.com/matthi__d) | [github matthid](https://github.com/matthid) | [aitgmbh.de](http://www.aitgmbh.de/)

***

### Roadmap

 - **Meta**

---

### Meta

- Not complete
- Spec is huge
- Lots of stuff we might skip

***

### Roadmap

- Meta
- **C# Language Spec**

---

```csharp
namespace People
{
   public class Employee 
   { /* ... */ }
}
```

Which other access modifiers could alternatively be applied to this class:

1. `internal`, `protected` and `private`
2. `internal`, `private`
3. `protected` and `private`
4. `internal`, `private` and `protected internal`
5. `internal` 

---

What is equivalent to `char ch = ' ';`:

1. `char ch = "d32";`
2. `char ch = "\x20";`
3. `char ch = '0x20';`
4. `char ch = '\x20';`
5. `char ch = 32;`

---

```csharp
public static class NumberWriter{
    public static void Display(int x) {
        Console.WriteLine("Integer: " + x);
    }
    public static void Display(long x) {
        Console.WriteLine("Long: " + x);
    }
}
short value = 10;
NumberWriter.Display(value);
```

1. `"Long: 10"`
2. `"Integer: 10"`
3. throws `RuntimeBinderException`
4. doesn't compile

---

```csharp
dynamic value = 321;
Console.WriteLine(value.Length);
```

1. `"321"`
2. `"3"`
3. Doesn't compile
4. throw `RuntimeBinderException`

---

```csharp
static void DoSomething() { throw new Exception("Test"); }
static bool MyCondition(Exception ex) { return ex.Code == 42; }
static void Main1() {
    try { DoSomething(); }
    catch (Exception ex) when (MyCondition(ex)) {
        Console.WriteLine("Error 42 occurred"); 
    }
} 
static void Main2() {
    try { DoSomething(); }
    catch (Exception ex) {
        if (MyCondition(ex))
            Console.WriteLine("Error 42 occurred");
        else throw;
    }
}
```

' Stack is does not unwind
' Debugger does not halt (first chance exception)

---

```csharp
static bool MyCondition(Exception ex) {
    Console.WriteLine(Environment.StackTrace);
    return false;
}
```

```
> Main1()
   at System.Environment.get_StackTrace()
   at Submission#76.MyCondition(Exception ex)
   at Submission#77.Main1()
   at Submission#71.DoSomething()
   at Submission#77.Main1()
```

```
> Main2()
   at System.Environment.get_StackTrace()
   at Submission#76.MyCondition(Exception ex)
   at Submission#78.Main2()
```

---

```csharp
static bool MyCondition(Exception ex) {
    Logger.Error("Error happened", ex);
    return false;
}
```

***


### Roadmap

 - Meta
 - **Corner cases**


---



### Disclaimer

https://stackoverflow.com/questions/194484/whats-the-strangest-corner-case-youve-seen-in-c-sharp-or-net

---

### 0

```csharp
enum Symbol { Alpha = 1, Beta = 2, Gamma = 3, Delta = 4 };
class Mate {
  static void Main(string[] args) {
    JustTest(Symbol.Alpha);
    JustTest(0);
    JustTest((int)0);
    JustTest(Convert.ToInt32(0));
    int i = 0; JustTest(i);
    JustTest(1);
    JustTest("string");
    JustTest(Guid.NewGuid());
    JustTest(new DataTable()); }

  static void JustTest(Symbol a) {
    Console.WriteLine("Enum"); }

  static void JustTest(object o) {
    Console.WriteLine("Object"); } }
```

---

### Structs

```csharp
public struct Teaser {
  public void Reset() {
    this = new Teaser();
  }
}
```

--- 

### Refactoring

```csharp
public class MyDummy {
    public override bool Equals(object other) {
        System.Console.WriteLine("Comparing");
        return other == null;
    }
    public MyDummy(string t) { T = t; }
    public string T { get; }
    public override string ToString () { return T; }
    public static MyDummy Default () => new MyDummy("default");
}
var test = new MyDummy("blub");
```

```csharp
test != null ? test : MyDummy.Default()
```
```csharp
test ?? MyDummy.Default()
```

---

```csharp
    public override bool Equals(object other) {
        return other == null;
    }
var test = new MyEquals("");

```

---

### Dynamic?

```csharp
public class Base {
  public virtual void Initialize(dynamic stuff) { }
}
public class Derived : Base {
  public override void Initialize(dynamic stuff) {
    base.Initialize(stuff);
  }
}

```

' (4,4): error CS1971: The call to method 'Initialize' needs to be dynamically dispatched, but cannot be because it is part of a base access expression. Consider casting the dynamic arguments or eliminating the base access.

---

### Literals

```csharp
bool abool = true;
Byte by1 = (abool ? 1 : 2);
Byte by3 = (true ? 1 : 2);
```

***

### Breaking changes

- Binary-level break
- Source-level break
- Source-level quiet semantics change

https://stackoverflow.com/questions/1456785/a-definitive-guide-to-api-breaking-changes-in-net

---

### Adding a member

```csharp
 public class Foo
 {
+  public void Bar();
 }
```

' source-level quiet semantics change. (extension methods)

---

### Changing return type

```csharp
 public static class Foo
 {
-  public static void bar(int i);
+  public static bool bar(int i);
 }
```

' binary breaking change
' source-level break (used in lambda)

---

### Enum?

```csharp
 enum MyEnum
 {
     Red,
+    Black,
     Blue,
 }
```

---

### Default parameter?

```csharp
-public void MyDummy(string s, int v = 4);
+public void MyDummy(string s, int v = 4, int w = 3);
```

---

### Refactoring

```csharp
class Foo {
    public virtual void Bar() {}
    public virtual void Baz() {}
}
```

```csharp
class FooBase {
    public virtual void Bar() {}
}
class Foo : FooBase {
    public virtual void Baz() {}
}
```

' not breaking

---

### Refactoring

```csharp
interface IFoo {
    void Bar();
    void Baz();
}
```

```csharp
interface IFooBase {
    void Bar();
}
interface IFoo : IFooBase {
    void Baz();
}
```

' Explicit implementations -> Source
' binding breaks -> Binary

***

### Roadmap

 - Meta
 - Corner cases
 - **Runtime & EcoSystem**

---

### Runtime & EcoSystem

- (1) in .NET only a single assembly with the same name (+PublicKeyToken) can be loaded
- (2) .NET will automatically redirect all assembly requests to the latest version 
- (3) NuGet package version matches the version of the assembly

---

app.config

https://docs.microsoft.com/en-us/dotnet/framework/configure-apps/file-schema/runtime/index

---

```csharp
List<int> list = new List<int>() { 1, 2, 3 };
list.ForEach(i =>
{
    Console.WriteLine(i);
    if (i < 3) { list.Add(i + 1); }
});
```

1. prints 1, 2, 3, 2, 3, 3
2. throws `InvalidOperationException("Collection was modified")`

---

```csharp
Console.WriteLine("Uri: " + new Uri("http://my.ser/path./item"));
```

1. prints `http://my.ser/path./item`
2. prints `http://my.ser/path/item`

---

Assembly.Load executing user code?
GetCustomAttributes?

***

### Roadmap

 - Meta
 - Corner cases
 - Runtime & EcoSystem
 - **Internals**

---

### Compiler features

```csharp
class MyEnumerator {
    private bool finished;
    public object Current { get; } = "ok";
    public bool MoveNext() { var f = finished; finished = true; return !f; }
}
class Test {
    public MyEnumerator GetEnumerator() => new MyEnumerator();
}
var t = new Test();
foreach(string s in t) { Console.WriteLine(s); }
```

' Where is IEnumerable<>?
' Most compiler "duck"-typed -> Extendible

---

### Yield Return?

```csharp
public IEnumerable<int> GetFirst10Nos() {
    for (int i = 0; i < 10; i++)
        yield return i;
}
```

```csharp
public IEnumerable<int> GetFirst10Nos() {
    <GetFirst10Nos>d__0 d__ = new <GetFirst10Nos>d__0(-2);
    d__.<>4__this = this;
    return d__;
}
```

---

### Async

```csharp
[AsyncMethodBuilder(typeof(MyTaskBuilder))]
class MyTask {
    public Awaiter GetAwaiter();
}
class Awaiter : INotifyCompletion {
    public bool IsCompleted { get; }
    public void GetResult();
    public void OnCompleted(Action completion);
}
class MyTaskBuilder {
    public static MyTaskBuilder Create() => null;
    public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine { }
    public void SetStateMachine(IAsyncStateMachine stateMachine) { }
    public void SetResult() { }
    public void SetException(Exception exception) { }
    public MyTask Task => default(MyTask);
    public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine { }
    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine { }
}
```

---

### Thank you!

Further reading

* https://channel9.msdn.com/Events/TechDays/Techdays-2016-The-Netherlands/C-Language-Internals
* https://stackoverflow.com/questions/194484/whats-the-strangest-corner-case-youve-seen-in-c-sharp-or-net
