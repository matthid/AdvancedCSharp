namespace Demo;

class LiteralsVsVaraibles{

    public static void LiteralsVsVaraiblesDemo(){
        bool abool = true;
        //Byte by1 = (abool ? 1 : 2);
        Byte by2 = (true ? 1 : 2);
    }
}