using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePathCard : ActionCard {

    private void Awake()
    {
        m_CardData = CardConfig.s_ActionCards[0];
        SetCardInfo();
    }
}
