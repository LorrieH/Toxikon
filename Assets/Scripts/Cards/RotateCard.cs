using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCard : ActionCard {

    private void Awake()
    {
        m_CardData = new CardData(CardTypes.ACTION_CARD, "Rotate Path", "Rotate a path", null);
        m_CardData.Name = "Rotate Path";
        m_CardData.Description = "Rotate a path";
    }
}
