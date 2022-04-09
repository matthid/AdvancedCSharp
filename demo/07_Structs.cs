namespace Demo;

public struct Teaser {
  public void Reset() {
    this = new Teaser();
  }
}

#if NET6_0_OR_GREATER
/*
public readonly ref struct Measurement
{
    public Measurement()
    {
        Values = new[]{double.NaN};
        //Description = "test";
    }
    public ReadOnlySpan<double> Values { get; init; }
    public readonly string Description { get; init; }

    public readonly Measurement SetValue(ReadOnlySpan<double> values) => this with { Values = values };
}
*/
#endif