using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotMachineModel
{
    public bool IsActiveMachine { get; private set; }

    public event Action OnActivateMachine;
    public event Action<int> OnStopSpinnedSlot;
    public event Action OnDeactivateMachine;

    private ISoundProvider _soundProvider;
    private ISound _wheelSound;

    public SlotMachineModel(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;

        _wheelSound = _soundProvider.GetSound("Wheel_Spin");
    }

    #region Sounds

    private void PlayWheelSound()
    {
        _wheelSound.SetVolume(0.1f);
        _wheelSound.SetPitch(1);
        _wheelSound.Play();
    }

    private void StopWheelSounds()
    {
        _wheelSound.Stop();
    }

    public void WheelSpeed(float speed)
    {
        if (speed > 2f)
        {
            speed = 1f;
        }

        _wheelSound.SetVolume(speed);

        float pitch = Mathf.Lerp(1, 0.88f, 1 - speed);
        _wheelSound.SetPitch(pitch * 1f);


    }

    #endregion

    public void ActivateMachine()
    {
        if (IsActiveMachine) return;

        IsActiveMachine = true;
        PlayWheelSound();
        OnActivateMachine?.Invoke();
    }

    public void StopSpinSlot(int Id)
    {
        OnStopSpinnedSlot?.Invoke(Id);
        OnGetLocation?.Invoke(Id);


        DeactivateMachine();
    }

    public void DeactivateMachine()
    {
        OnDeactivateMachine?.Invoke();
        OnEnd?.Invoke();
        IsActiveMachine = false;

        StopWheelSounds();

    }

    #region Output

    public event Action<int> OnGetLocation;
    public event Action OnEnd;

    #endregion
}
