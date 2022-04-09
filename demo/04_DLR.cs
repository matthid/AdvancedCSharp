using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace Demo;

#if NET6_0_OR_GREATER

public class DynamicTests{
    public static void DynamicDemo(){
        
        dynamic value = 321;
        Console.WriteLine(value.Length);
    }
}



public sealed class MyMetaObject : DynamicMetaObject {
    private Type _type;
    private SetFieldHelper _helper;

    public MyMetaObject(Expression expression, SetFieldHelper helper)
        : base(expression, BindingRestrictions.Empty, helper)
    {   
        _type = helper.Wrapped.GetType();
        _helper = helper; 
    }

    public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
    {
        var self = Expression;

        var f = _type.GetField(binder.Name, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        //f.SetValue(helper.Wrapped, <NewValue>)

        var fi = f.GetType();
        var setValueMethod = fi.GetMethod(nameof(f.SetValue), new Type[] {typeof(object), typeof(object)});
        
        var instanceExp = Expression.Constant(_helper.Wrapped);
        //var instanceExp = Expression.Constant(null);
        var setValueCall = Expression.Call(
            Expression.Constant(f),
            setValueMethod,
            instanceExp, Expression.Convert(value.Expression, typeof(object)));
        var retNull = Expression.Block(setValueCall, Expression.Constant(null));
        var typeRestriction = BindingRestrictions.GetTypeRestriction(self, typeof(SetFieldHelper));
        //typeRestriction = typeRestriction.Merge(BindingRestrictions.GetInstanceRestriction(self, _helper));
        return new DynamicMetaObject(retNull, typeRestriction);
    }
}

public class SetFieldHelper : DynamicObject { 
    public object Wrapped {get; init;}
    public SetFieldHelper(object data) => Wrapped = data;
    public static dynamic From(object d) => new SetFieldHelper(d);
    public override DynamicMetaObject GetMetaObject(Expression parameter) => new MyMetaObject(parameter, this);

    class C1 { public readonly int _sd = 1; }
    class C2 { public readonly int _sd = 2; }
    public static void SetFieldHelperDemo(){
        var c1 = new C1(); var d1 = From(c1);
        var c2 = new C2(); var d2 = From(c2);
        d1._sd = 11;
        d2._sd = 22;
        Console.WriteLine($"c1._sd = {c1._sd}");
        Console.WriteLine($"c2._sd = {c2._sd}");
    }
}

#endif