using UnityEngine;
public struct Bridges
{
    public GameObject bridgeUp;
    public GameObject bridgeRight;
    public GameObject bridgeDown;
    public GameObject bridgeLeft;

    public Bridges(GameObject bridgeUp, GameObject bridgeRight, GameObject bridgeDown, GameObject bridgeLeft)
    {
        this.bridgeUp = bridgeUp;
        this.bridgeRight = bridgeRight;
        this.bridgeDown = bridgeDown;
        this.bridgeLeft = bridgeLeft;
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

    public static bool operator == (Bridges p1, Bridges p2)
    {
        return p1.Equals(p2);
    }

    public static bool operator != (Bridges p1, Bridges p2)
    {
        return !p1.Equals(p2);
    }
}