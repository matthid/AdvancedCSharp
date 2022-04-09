namespace Demo;

public class MyDummy {
    public MyDummy(string t) { T = t; }
    public string T { get; }
    public override string ToString () { return T; }
    public static MyDummy Default () => new MyDummy("default");
    
    public static bool operator==(MyDummy d1, MyDummy d2) =>
        Object.ReferenceEquals(d1, null) ||
        Object.ReferenceEquals(d2, null);
    public static bool operator!=(MyDummy d1, MyDummy d2) =>
        !(d1 == d2);

    public static void NullEqualsRefactoringDemo(){
        var test = new MyDummy("blub");
        Console.WriteLine($" != null: {(test != null ? test : MyDummy.Default())}");
        Console.WriteLine($" ??: {(test ?? MyDummy.Default())}");

    }
}