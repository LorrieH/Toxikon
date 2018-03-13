using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundNames
{
    BUTTON_CLICK,
    BACKGROUND_MUSIC
}

[System.Serializable]
public struct SoundData
{
    public SoundNames SoundID;
    public AudioSource AudioSource;
}

public struct SoundMixerKeys
{
    public static readonly string MASTER = "MasterVolumeValue";
    public static readonly string MUSIC = "MusicVolumeValue";
    public static readonly string SFX = "SFXVolumeValue";
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager s_Instance;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer m_AudioMixer;
    [Header("Sound Data")]
    [SerializeField] private List<SoundData> m_AudioData = new List<SoundData>();

    private void Awake()
    {
        Init();
        PlaySound(SoundNames.BACKGROUND_MUSIC);
    }

    private void Init()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(string soundName)
    {
        SetMixerValue(SoundMixerKeys.MUSIC, 0.15f);
        PlaySound((SoundNames)System.Enum.Parse(typeof(SoundNames), soundName));
    }

    public void PlaySound(SoundNames soundName)
    {
        SoundData soundData = m_AudioData.Find(x => x.SoundID == soundName);
        Debug.Log("<color=red>[SoundManager.cs]</color> Now Playing: " + soundData.SoundID.ToString());
        soundData.AudioSource.Play();
    }

    public void SetMixerValue(string key, float value)
    {
        float calculatedValue = CalculateVolume(value, 0, 1, -80, 0);
        Debug.Log("key: " + key + " | value: " + value + " | calculatedValue: " + calculatedValue);
        m_AudioMixer.SetFloat(key, calculatedValue);
    }

    private float CalculateVolume(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
