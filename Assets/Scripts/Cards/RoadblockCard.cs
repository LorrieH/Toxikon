using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadblockCard : ActionCard {

    private void Awake()
    {
        m_CardData = CardConfig.s_ActionCards[2];
        SetCardInfo();
    }
}
