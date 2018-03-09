using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCard : Card
{
    private void Awake()
    {
        m_CardData.Type = CardTypes.ACTION_CARD;
    }
    public override void UseCard()
    {
        //effect
        base.UseCard();
    }
}
