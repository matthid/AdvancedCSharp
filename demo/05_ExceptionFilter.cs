namespace Demo;

public class ExceptionFilter {
    static void DoSomething() { throw new Exception("Test"); }
    static bool MyCondition(Exception ex) { 
        Console.WriteLine($"Stacktrace: \n{Environment.StackTrace}");
        return false;
    }
    public static void ExceptionFilterMain1() {
        try { DoSomething(); }
        catch (Exception ex) when (MyCondition(ex)) {
            Console.WriteLine("Error 42 occurred"); 
        }
    } 
    public static void ExceptionFilterMain2() {
        try { DoSomething(); }
        catch (Exception ex) {
            if (MyCondition(ex))
                Console.WriteLine("Error 42 occurred");
            else throw;
        }
    }

        
    static void DoSomethingThrowsArg() { throw new ArgumentException("ArgumentExn"); }
    static bool MyConditionThrowsIO(Exception ex) { throw new IOException("IOExn"); }
    public static void ExceptionFilterMainQuiz1() {
        try { DoSomethingThrowsArg(); }
        catch (ArgumentException ex) when (MyConditionThrowsIO(ex)) {
            Console.WriteLine("Catched ArgumentException"); 
        }
    }
    public static void ExceptionFilterMainQuiz2() {
        try { DoSomethingThrowsArg(); }
        catch (ArgumentException ex) when (MyConditionThrowsIO(ex)) {
            Console.WriteLine("Catched ArgumentException"); 
        }
        catch (ArgumentException) {
            Console.WriteLine("Catched ArgumentException (2)"); 
        }
    }
}