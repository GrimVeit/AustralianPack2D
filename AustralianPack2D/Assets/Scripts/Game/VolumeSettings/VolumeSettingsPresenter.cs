using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSettingsPresenter
{
    private readonly VolumeSettingsModel _model;
    private readonly VolumeSettingsView _view;

    public VolumeSettingsPresenter(VolumeSettingsModel model, VolumeSettingsView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _view.Initialize();
        _model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnChangeVolume += _model.ChangeVolume;

        _model.OnVolumeChanged += _view.SetVolume;
    }

    private void DeactivateEvents()
    {
        _view.OnChangeVolume -= _model.ChangeVolume;

        _model.OnVolumeChanged -= _view.SetVolume;
    }
}
