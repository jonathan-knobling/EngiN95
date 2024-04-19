namespace EngiN95.Core.Rendering;

public readonly struct Handle : IEquatable<int>
{
    public int Value { get; init; }
    
    public static implicit operator Handle(int value)
    {
        return new Handle { Value = value };
    }

    public static implicit operator int(Handle handle)
    {
        return handle.Value;
    }

    public bool Equals(Handle other)
    {
        return Value == other.Value;
    }

    public bool Equals(int other)
    {
        return Value == other;
    }

    public override bool Equals(object? obj)
    {
        return (obj is Handle otherHandle && Equals(otherHandle)) || (obj is int otherInt && Equals(otherInt));
    }

    public override int GetHashCode()
    {
        return Value;
    }

    public static bool operator ==(Handle left, Handle right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Handle left, Handle right)
    {
        return !(left == right);
    }

    public static bool operator ==(Handle left, int right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Handle left, int right)
    {
        return !(left == right);
    }
}