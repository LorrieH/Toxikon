using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadblockCard : ActionCard {

    private void Awake()
    {
        m_CardData.Name = "Roadblock";
        m_CardData.Description = "Block off a path";
    }
}
