using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayToolbarModel
{
    private bool isActiveToolbar = false;

    public void ClickToolbar()
    {
        isActiveToolbar = !isActiveToolbar;

        if (isActiveToolbar)
        {
            OnShowToolbar?.Invoke();
        }
        else
        {
            OnHideToolbar?.Invoke();
        }
    }

    public void OpenToolbar()
    {
        if(isActiveToolbar) return;

        isActiveToolbar = true;

        if (isActiveToolbar)
        {
            OnShowToolbar?.Invoke();
        }
        else
        {
            OnHideToolbar?.Invoke();
        }
    }

    public void CloseToolbar()
    {
        if (!isActiveToolbar) return;

        isActiveToolbar = false;

        if (isActiveToolbar)
        {
            OnShowToolbar?.Invoke();
        }
        else
        {
            OnHideToolbar?.Invoke();
        }
    }

    #region Output

    public event Action OnClickToRestart;
    public event Action OnClickToExit;

    public void ClickRestart()
    {
        OnClickToRestart?.Invoke();
    }

    public void ClickExit()
    {
        OnClickToExit?.Invoke();
    }



    public event Action OnShowToolbar;
    public event Action OnHideToolbar;

    #endregion


}
