// See https://aka.ms/new-console-template for more information
using System.Linq.Expressions;


void Run(Expression<Action> a) {
    var demoCode = a.ToString();
    Console.WriteLine($"Running Demo '{demoCode}'...");
    try
    {
        a.Compile().Invoke();
    }
    catch (System.Exception e)
    {
        Console.WriteLine($"Unhandled Exception in Demo '{demoCode}': {e}");
    }
}

//Run(() => Demo.CharLiteral.CharLiteralDemo());

//Run(() => Demo.NumberWriter.NumberWriterDemo());

//Run(() => Demo.DynamicTests.DynamicDemo());

// DEMO DLR
//Run(() => Demo.SetFieldHelper.SetFieldHelperDemo());

//Run(() => Demo.ExceptionFilter.ExceptionFilterMain1());
//Run(() => Demo.ExceptionFilter.ExceptionFilterMain2());
//Run(() => Demo.ExceptionFilter.ExceptionFilterMainQuiz1());
//Run(() => Demo.ExceptionFilter.ExceptionFilterMainQuiz2());

//Run(() => Demo.NullIsSpecial.NullIsSpecialDemo());

//Run(() => Demo.MyDummy.NullEqualsRefactoringDemo());


//Run(() => Demo.Test.EnumerateTestObj());

// DEMO Async
//Run(() => Demo.AsyncSwitchTo.AsyncSwitchToDemo());

//Run(() => Demo.CollectionModification.CollectionModificationDemo());

//Run(() => Demo.UriParsing.UriParsingDemo());