using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public struct Notification
{
    public Notification(string Text, float Duration, Color Color)
    {
        this.Text = Text;
        this.Duration = Duration;
        this.Color = Color;
    }

    public string Text;
    public float Duration;
    public Color Color;
}

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager s_Instance;
    [SerializeField] private Text m_NotificationText;

    private List<Notification> m_NotificationQueue = new List<Notification>();
    private bool m_NotificationQueueActive;
    private bool m_ShowingNotification;

    private Coroutine m_Queue;

    private void Awake()
    {
        Init();
        CardPositionHolder.s_OnDrawCard += ClearQueue;
        TurnManager.s_OnTurnEnd += ClearQueue;
    }

    private void OnDestroy()
    {
        CardPositionHolder.s_OnDrawCard += ClearQueue;
        TurnManager.s_OnTurnEnd -= ClearQueue;
    }

    private void Init()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void EnqueueNotification(string text, float duration)
    {
        if (NotificationAlreadyExists(text)) return;
        m_NotificationQueue.Add(new Notification(text, duration, Color.white));

        StartQueue();
    }

    public void EnqueueNotification(string text, float duration, Color color)
    {
        if (NotificationAlreadyExists(text)) return;
        m_NotificationQueue.Add(new Notification(text, duration, color));

        StartQueue();
    }

    private void StartQueue()
    {
        if (!m_NotificationQueueActive)
            m_Queue = StartCoroutine(NotificationQueue());
    }



    private bool NotificationAlreadyExists(string text)
    {
        return m_NotificationQueue.Contains(m_NotificationQueue.Find(x => x.Text == text));
    }

    private IEnumerator NotificationQueue()
    {
        if (m_NotificationQueueActive) yield break;

        m_NotificationQueueActive = true;

        while(m_NotificationQueue.Count > 0)
        {
            if (!m_NotificationQueueActive) yield break;

            yield return new WaitWhile(() => m_ShowingNotification);
            ShowAnimation();
            m_NotificationText.text = m_NotificationQueue[0].Text;
            m_NotificationText.color = m_NotificationQueue[0].Color;
            yield return new WaitForSeconds(m_NotificationQueue[0].Duration);
            m_NotificationQueue.RemoveAt(0);
            HideAnimation();
        }
        yield return new WaitWhile(() => m_ShowingNotification);

        ClearQueue();
    }

    private void ShowAnimation()
    {
        m_ShowingNotification = true;

        //Just to be sure
        m_NotificationText.transform.localScale = new Vector2(0.7f, 0.7f);
        m_NotificationText.color = new Color(m_NotificationText.color.r, m_NotificationText.color.g, m_NotificationText.color.b, 0);

        //Animation
        Sequence showSequence = DOTween.Sequence();
        showSequence.Append(m_NotificationText.transform.DOScale(1f, 0.33f).SetEase(Ease.OutQuad));
        showSequence.Join(m_NotificationText.DOFade(1, 0.2f).SetEase(Ease.OutQuad));
    }

    private void HideAnimation()
    {
        //Just to be sure
        m_NotificationText.transform.localScale = Vector2.one;
        m_NotificationText.color = new Color(m_NotificationText.color.r, m_NotificationText.color.g, m_NotificationText.color.b, 1);

        //Animation
        Sequence hideSequence = DOTween.Sequence();
        hideSequence.Append(m_NotificationText.transform.DOScale(0.7f, 0.33f).SetEase(Ease.InQuad));
        hideSequence.Join(m_NotificationText.DOFade(0, 0.2f).SetEase(Ease.InQuad)).OnComplete(() => m_ShowingNotification = false);
    }



    public void ClearQueue()
    {
        m_NotificationQueueActive = false;
        m_ShowingNotification = false;
        m_NotificationQueue.Clear();

        if(m_Queue != null)
            StopCoroutine(m_Queue);
    }
}
