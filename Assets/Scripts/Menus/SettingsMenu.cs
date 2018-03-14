using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : Menu
{
    [Header("Sound Sliders")]
    [SerializeField] private Slider m_MasterSlider;
    [SerializeField] private Slider m_MusicSlider;
    [SerializeField] private Slider m_SFXSlider;

    private void Awake()
    {
        m_MasterSlider.onValueChanged.AddListener(delegate { OnMasterVolumeChanged(); });
        m_MusicSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChanged(); });
        m_SFXSlider.onValueChanged.AddListener(delegate { OnSFXVolumeChanged(); });
    }

    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }

    private void OnMasterVolumeChanged()
    {
        SoundManager.s_Instance.SetMixerValue(SoundMixerKeys.MASTER, m_MasterSlider.value);
    }

    private void OnMusicVolumeChanged()
    {
        SoundManager.s_Instance.SetMixerValue(SoundMixerKeys.MUSIC, m_MusicSlider.value);
    }

    private void OnSFXVolumeChanged()
    {
        SoundManager.s_Instance.SetMixerValue(SoundMixerKeys.SFX, m_SFXSlider.value);
    }
}
