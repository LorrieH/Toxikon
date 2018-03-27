using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NextTurnPopup : Menu
{
    [SerializeField] private Image m_CharacterBody;
    [SerializeField] private Image m_CharacterHat;
    [SerializeField] private Image[] m_CharacterHands;
    [SerializeField] private Image[] m_Rays;
    [SerializeField] private Image[] m_Sparkles;
    [SerializeField] private Image m_BlackBar;
    [SerializeField] private CanvasGroup m_Button;
    [Space]
    [SerializeField] private Text m_Info;

    private void OnEnable()
    {
        m_BlackBar.rectTransform.sizeDelta = Vector2.zero;
        m_CharacterHat.rectTransform.anchoredPosition = new Vector2(0, 450);
        m_CharacterBody.transform.localScale = Vector2.zero;
        m_CharacterHat.color = new Color(m_CharacterHat.color.r, m_CharacterHat.color.g, m_CharacterHat.color.b, 0);
        m_Button.transform.localScale = Vector2.zero;
        m_Button.alpha = 0;

        for (int i = 0; i < m_CharacterHands.Length; i++)
            m_CharacterHands[i].transform.localScale = Vector2.zero;

        for (int i = 0; i < m_Sparkles.Length; i++)
            m_Sparkles[i].transform.localScale = Vector2.zero;

        for (int i = 0; i < m_CharacterHands.Length; i++)
            m_CharacterHands[i].rectTransform.anchoredPosition = new Vector2(m_CharacterHands[i].rectTransform.anchoredPosition.x, 0);

        for (int i = 0; i < m_Rays.Length; i++)
            m_Rays[i].color = new Color(m_Rays[i].color.r, m_Rays[i].color.g, m_Rays[i].color.b, 0);

        m_Info.color = new Color(m_Info.color.r, m_Info.color.g, m_Info.color.b, 0);
        m_Info.rectTransform.anchoredPosition = new Vector2(0, -350);
    }

    public void StartNextTurn()
    {
        TurnManager.s_Instance.NextTurn();
    }

    public override void Show()
    {
        if (TurnManager.s_Instance.CurrentPlayerIndex == PlayersManager.s_Instance.Players.Count - 1)
            ShowNextPlayer(PlayersManager.s_Instance.Players[0].PlayerData);
        else
            ShowNextPlayer(PlayersManager.s_Instance.Players[TurnManager.s_Instance.CurrentPlayerIndex + 1].PlayerData);

        base.Show();

        //Initial base animation
        Sequence show = DOTween.Sequence();
        show.Append(m_BlackBar.rectTransform.DOSizeDelta(new Vector2(0, 300), 0.5f).SetEase(Ease.OutExpo));
        show.Join(m_Info.rectTransform.DOAnchorPosY(-100, 0.5f).SetEase(Ease.OutExpo).SetDelay(0.15f));
        show.Join(m_Info.DOFade(1, 0.2f).SetEase(Ease.OutExpo).SetDelay(0.05f));
        show.Join(m_CharacterBody.transform.DOScale(1, 0.5f).SetEase(Ease.OutExpo).SetDelay(0.1f));
        show.Join(m_CharacterHat.rectTransform.DOAnchorPosY(250, 0.5f).SetEase(Ease.OutExpo).SetDelay(0.2f));
        show.Join(m_CharacterHat.DOFade(1, 0.2f).SetEase(Ease.OutExpo).SetDelay(0.05f));
        show.Join(m_CharacterHands[0].transform.DOScale(1, 0.5f).SetEase(Ease.OutBack).SetDelay(0.15f));
        show.Join(m_CharacterHands[1].transform.DOScale(1, 0.5f).SetEase(Ease.OutBack).SetDelay(0.2f));
        show.Join(m_Button.DOFade(1, 0.5f).SetDelay(0.3f));
        show.Join(m_Button.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack).SetDelay(0.3f));
        for (int i = 0; i < m_Sparkles.Length; i++)
            show.Join(m_Sparkles[i].transform.DOScale(1, 0.5f).SetEase(Ease.OutBack).SetDelay((i * 0.05f)));
        for (int i = 0; i < m_Rays.Length; i++)
            m_Rays[i].DOFade(1, 1f).SetEase(Ease.OutQuad).SetDelay(1f);

        //Looping layer animations
        for (int i = 0; i < m_CharacterHands.Length; i++)
            m_CharacterHands[i].rectTransform.DOAnchorPosY(30, 1f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo).SetId("NextTurnPopupLoopAnims");

        for (int i = 0; i < m_Rays.Length; i++)
            m_Rays[i].transform.DOScale(0.7f, 2f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo).SetDelay(i).SetId("NextTurnPopupLoopAnims");
    }

    public override void Hide()
    {
        DOTween.Kill("NextTurnPopupLoopAnims", true);
        base.Hide();
    }

    private void ShowNextPlayer(PlayerData nextPlayer)
    {
        m_CharacterBody.sprite = Resources.Load<Sprite>("Characters/" + nextPlayer.AvatarImageName + "/" + "Chara");
        m_CharacterHat.sprite = Resources.Load<Sprite>("Characters/" + nextPlayer.AvatarImageName + "/" + "Hat");
        m_Info.text = nextPlayer.Name + "'s turn is up next!";
    }
}
