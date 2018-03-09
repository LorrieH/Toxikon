using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPathCard : ActionCard {

    private void Awake()
    {
        m_CardData.Name = "Swap Path";
        m_CardData.Description = "Swap 2 paths that are next to each other";
    }
}
