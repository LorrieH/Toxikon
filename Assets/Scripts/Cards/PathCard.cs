using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCard : Card
{
    private void Awake()
    {
        m_CardData.Type = CardTypes.PATH_CARD;
    }

    public override void UseCard()
    {
        //effect
        base.UseCard();
    }
}
