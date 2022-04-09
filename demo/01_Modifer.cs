namespace People;

public class Employee 
{ /* ... */ }


public class ProtectedPrivate 
{ 
    protected private ProtectedPrivate(){}
}

public class Inheritor : ProtectedPrivate{
    public Inheritor() : base(){}
}