using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPathCard : ActionCard {

    private void Awake()
    {
        m_CardData = CardConfig.s_ActionCards[1];
        SetCardInfo();
    }
}