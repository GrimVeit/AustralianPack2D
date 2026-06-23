using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSettingsModel
{
    private readonly ISoundVolumeProvider _soundVolumeProvider;

    public VolumeSettingsModel(ISoundVolumeProvider soundVolumeProvider)
    {
        _soundVolumeProvider = soundVolumeProvider;
    }

    public void Initialize()
    {
        Debug.Log($"SOUND => {_soundVolumeProvider.VolumeSound()}");
        Debug.Log($"MUSIC => {_soundVolumeProvider.VolumeMusic()}");

        OnVolumeChanged?.Invoke(AudioType.Music, _soundVolumeProvider.VolumeMusic());
        OnVolumeChanged?.Invoke(AudioType.Sound, _soundVolumeProvider.VolumeSound());
    }

    public void Dispose()
    {

    }

    public void ChangeVolume(AudioType audioType, float value)
    {
        _soundVolumeProvider.SetVolume(value, audioType);
    }

    #region Output

    public event Action<AudioType, float> OnVolumeChanged;

    #endregion
}
