using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Notification
{
    public Notification(string Text, float Duration)
    {
        this.Text = Text;
        this.Duration = Duration;
    }

    public string Text;
    public float Duration;
}

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager s_Instance;
    [SerializeField] private Text m_NotificationText;

    private List<Notification> m_NotificationQueue = new List<Notification>();
    private bool m_NotificationQueueActive;
    private bool m_ShowingNotification;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
            Destroy(gameObject);
    }


    public void EnqueueNotification(string text, float duration)
    {
        m_NotificationQueue.Add(new Notification(text, duration));
        if (!m_NotificationQueueActive)
            StartCoroutine(NotificationQueue());
    }

    private IEnumerator NotificationQueue()
    {
        if (m_NotificationQueueActive) yield break;

        m_NotificationQueueActive = true;

        while(m_NotificationQueue.Count > 0)
        {
            yield return new WaitWhile(() => m_ShowingNotification);
            m_ShowingNotification = true;
            m_NotificationText.text = m_NotificationQueue[0].Text;
            yield return new WaitForSeconds(m_NotificationQueue[0].Duration);
            m_NotificationQueue.RemoveAt(0);
            m_ShowingNotification = false;
        }
        m_NotificationText.text = "";
        m_NotificationQueueActive = false;
    }
}
