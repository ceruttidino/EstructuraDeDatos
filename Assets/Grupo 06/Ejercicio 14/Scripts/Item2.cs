using UnityEngine;
public class Item2
{
    public string Name;
    public float Price;

    public Item2(string name, float price)
    {
        Name = name;
        Price = price;
    }

    public override string ToString() => $"{Name} (${Price:F2})";

    public override bool Equals(object obj)
    {
        return obj is Item2 other && other.Name == Name;
    }

    public override int GetHashCode()
    {
        return (Name ?? "").GetHashCode();
    }
}

