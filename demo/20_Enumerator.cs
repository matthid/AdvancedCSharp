namespace Demo;

class MyEnumerator {
    private bool finished;
    public object Current { get; } = "ok";
    public bool MoveNext() { var f = finished; finished = true; return !f; }
}
class Test {
    public MyEnumerator GetEnumerator() => new MyEnumerator();

    public static void EnumerateTestObj(){
        var t = new Test();
        foreach(string s in t) { Console.WriteLine(s); }
    }
}