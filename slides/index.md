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


***


### Roadmap

 - Meta
 - Corner cases

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


***

### Thank you!

Further reading

* https://channel9.msdn.com/Events/TechDays/Techdays-2016-The-Netherlands/C-Language-Internals
* https://stackoverflow.com/questions/194484/whats-the-strangest-corner-case-youve-seen-in-c-sharp-or-net
