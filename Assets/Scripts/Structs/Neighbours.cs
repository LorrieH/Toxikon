public struct Neighbours
{
    public TileNode up;
    public TileNode right;
    public TileNode down;
    public TileNode left;

    public Neighbours(TileNode up , TileNode right, TileNode down, TileNode left)
    {
        this.up = up;
        this.right = right;
        this.down = down;
        this.left = left;
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

    public static bool operator ==(Neighbours p1, Neighbours p2)
    {
        return p1.Equals(p2);
    }

    public static bool operator !=(Neighbours p1, Neighbours p2)
    {
        return !p1.Equals(p2);
    }
}