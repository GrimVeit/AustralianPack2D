using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayVideoPanel_Game : MovePanel
{
    [SerializeField] private Button buttonPlay;

    public override void Initialize()
    {
        base.Initialize();

        buttonPlay.onClick.AddListener(ClickPlay);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonPlay.onClick.RemoveListener(ClickPlay);
    }

    #region Output

    public event Action OnClickPlay;

    private void ClickPlay()
    {
        OnClickPlay?.Invoke();
    }

    #endregion
}
