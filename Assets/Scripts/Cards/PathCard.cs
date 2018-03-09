using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PathDirections
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class PathCard : Card
{
    public delegate void PathCardValueChanged(PathCard card, List<PathDirections> directions);
    public static PathCardValueChanged s_OnPathValueChanged;

    private List<PathDirections> m_Directions = new List<PathDirections>();
    public List<PathDirections> Directions
    {
        get { return m_Directions; }
        set { m_Directions = value; if(s_OnPathValueChanged != null) s_OnPathValueChanged(this, m_Directions); }
    }

    public override void UseCard()
    {
        //effect
        base.UseCard();
    }

    public Vector2 GetNodeByDirection(PathDirections direction)
    {
        switch(direction)
        {
            case PathDirections.UP:
                return Vector2.up;
            case PathDirections.DOWN:
                return Vector2.down;
            case PathDirections.LEFT:
                return Vector2.left;
            case PathDirections.RIGHT:
                return Vector2.right;
        }
        Debug.LogError("<color=red>[PathCard.cs]</color> Could not get Node by Direction. (Invalid Direction)");
        return Vector2.zero;
    }
}
