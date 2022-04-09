namespace Demo;

public class CollectionModification{
    public static void CollectionModificationDemo() {
        List<int> list = new List<int>() { 1, 2, 3 };
        list.ForEach(i =>
        {
            Console.WriteLine(i);
            if (i < 3) { list.Add(i + 1); }
        });
    }
}