using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardSelector : MonoBehaviour {
    
    public delegate void ToggleCardSelectEvent(Card selectedCard, Transform cardTransform);
    public static ToggleCardSelectEvent s_OnToggleCardSelect;

    private Card m_SelectedCard;

    private void OnEnable()
    {
        s_OnToggleCardSelect += ToggleCardSelect;
    }

    private void ToggleCardSelect(Card selectedCard,Transform cardTransform)
    {
        if(m_SelectedCard != selectedCard)
        {
            SelectCard(selectedCard, cardTransform);
        }else if(m_SelectedCard == selectedCard)
        {
            DeselectCard(cardTransform);
        }
    }

    private void SelectCard(Card selectedCard, Transform cardTransform)
    {
        Sequence CardSelectSequence = DOTween.Sequence();
        CardSelectSequence.Append(cardTransform.DOMoveX(cardTransform.position.x - 125, 0.3f));
        CardSelectSequence.Append(cardTransform.DOScale(1.3f, 0.2f));
        m_SelectedCard = selectedCard;
    }

    private void DeselectCard(Transform cardTransform)
    {
        Sequence CardDeselectSequence = DOTween.Sequence();
        CardDeselectSequence.Append(cardTransform.DOScale(1, 0.2f));
        CardDeselectSequence.Append(cardTransform.DOMoveX(cardTransform.position.x + 125, 0.3f));
    }

    private void OnDisable()
    {
        s_OnToggleCardSelect -= ToggleCardSelect;
    }
}