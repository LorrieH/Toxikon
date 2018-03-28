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
    }

    /// <summary>
    /// Creates a instance of this object, if there is an instance already delete the new one
    /// </summary>
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

    /// <summary>
    /// Plays a sound by name
    /// </summary>
    /// <param name="soundName">Name of the sound to play</param>
    public void PlaySound(string soundName)
    {
        PlaySound((SoundNames)System.Enum.Parse(typeof(SoundNames), soundName));
    }

    public void PlaySound(SoundNames soundName)
    {
        SoundData soundData = m_AudioData.Find(x => x.SoundID == soundName);
        if(soundData.AudioSource != null)
            soundData.AudioSource.Play();
    }

    /// <summary>
    /// Sets a mixer groups volume
    /// </summary>
    /// <param name="key">Key of the volume to set</param>
    /// <param name="value">Value to set the volume to</param>
    public void SetMixerValue(string key, float value)
    {
        float calculatedValue = CalculateVolume(value, 0, 1, -80, 0);
        m_AudioMixer.SetFloat(key, calculatedValue);
    }

    private float CalculateVolume(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}