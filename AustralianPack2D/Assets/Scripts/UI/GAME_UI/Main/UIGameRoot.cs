using System;
using UnityEngine;

public class UIGameRoot : UIRoot
{
    private ISoundProvider _soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {

    }

    public void Activate()
    {
        
    }

    public void Deactivate()
    {
        if (currentPanel != null)
            CloseOtherPanel(currentPanel);
    }

    public void Dispose()
    {
        
    }

    #region Input



    #endregion




    #region Output



    #endregion
}
