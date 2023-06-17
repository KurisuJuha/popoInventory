namespace Test;

public sealed class TestItem : IEquatable<TestItem>
{
    public readonly string Value;

    public TestItem(string value)
    {
        Value = value;
    }

    public bool Equals(TestItem? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((TestItem)obj);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}