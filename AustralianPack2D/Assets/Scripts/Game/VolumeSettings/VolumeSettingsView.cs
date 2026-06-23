using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VolumeSettingsView : View
{
    [SerializeField] private List<VolumeSettings> volumeSettings = new();

    private readonly Dictionary<AudioType, VolumeSettings> volumeSettingsDict = new();

    public void Initialize()
    {
        for (int i = 0; i < volumeSettings.Count; i++)
        {
            volumeSettingsDict.Add(volumeSettings[i].AudioType, volumeSettings[i]);
        }

        foreach(var kvp in volumeSettingsDict.Values)
        {
            kvp.OnChangeVolume += ChangeVolume;
            kvp.Initialize();
        }
    }

    public void Dispose()
    {
        foreach (var kvp in volumeSettingsDict.Values)
        {
            kvp.OnChangeVolume -= ChangeVolume;
            kvp.Dispose();
        }
    }

    public void SetVolume(AudioType audioType, float volume)
    {
        if(volumeSettingsDict.TryGetValue(audioType, out var volumeSettings))
        {
            volumeSettings.SetValue(volume);
        }
    }

    #region Output

    public event Action<AudioType, float> OnChangeVolume;

    private void ChangeVolume(AudioType type, float value)
    {
        OnChangeVolume?.Invoke(type, value);
    }

    #endregion

    [Serializable]
    private class VolumeSettings
    {
        public AudioType AudioType => audioType;

        [SerializeField] private AudioType audioType;
        [SerializeField] private Slider sliderVolume;
        [SerializeField] private TextMeshProUGUI textVolume;

        public void Initialize()
        {
            sliderVolume.onValueChanged.AddListener(OnValueChanged);
        }

        public void Dispose()
        {
            sliderVolume.onValueChanged.RemoveListener(OnValueChanged);
        }

        public void SetValue(float volume)
        {
            sliderVolume.value = volume;

            textVolume.text = ((int)(volume * 100)).ToString();
        }

        #region Output

        public event Action<AudioType, float> OnChangeVolume;

        private void OnValueChanged(float value)
        {
            textVolume.text = ((int)(value * 100)).ToString();

            OnChangeVolume?.Invoke(audioType, value);
        }

        #endregion
    }
}
