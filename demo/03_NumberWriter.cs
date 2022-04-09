namespace Demo;

public static class NumberWriter {
    public static void Display(int x) {
        Console.WriteLine("Integer: " + x);
    }
    public static void Display(long x) {
        Console.WriteLine("Long: " + x);
    }

    public static void NumberWriterDemo(){
        short value = 10;
        NumberWriter.Display(value);
    }
}



