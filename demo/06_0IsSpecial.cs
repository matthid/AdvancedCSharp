using System.Data;

namespace Demo;

enum Symbol { Alpha = 1, Beta = 2, Gamma = 3, Delta = 4 };
class NullIsSpecial {
    public static void NullIsSpecialDemo() {
        JustTest(Symbol.Alpha);
        JustTest(0);
        JustTest((int)0);
        JustTest(Convert.ToInt32(0));
        int i = 0; JustTest(i);
        JustTest(1);
        JustTest("string");
        JustTest(Guid.NewGuid());
        JustTest(new DataTable());
    }

    static void JustTest(Symbol a) {
        Console.WriteLine("Enum");
    }

    static void JustTest(object o) {
        Console.WriteLine("Object");
    } 
}
