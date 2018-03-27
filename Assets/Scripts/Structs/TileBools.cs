[System.Serializable]
public struct TileBools
{
    public bool Up;
    public bool Right;
    public bool Down;
    public bool Left;
    public bool Middle;

    public TileBools(bool up = false, bool right = false, bool down = false, bool left = false, bool middle = false)
    {
        Up = up;
        Right = right;
        Down = down;
        Left = left;
        Middle = middle;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public static bool operator ==(TileBools p1, TileBools p2)
    {
        return p1.Equals(p2);
    }

    public static bool operator !=(TileBools p1, TileBools p2)
    {
        return !p1.Equals(p2);
    }
}