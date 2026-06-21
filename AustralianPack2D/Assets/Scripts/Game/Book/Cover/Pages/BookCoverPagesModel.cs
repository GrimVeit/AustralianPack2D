using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCoverPagesModel
{
    public event Action OnOpenSecondPage;
    public event Action OnOpenPastPage;

    private ISoundProvider soundProvider;

    public BookCoverPagesModel(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void OpenSecondPage()
    {
        OnOpenSecondPage?.Invoke();
    }

    public void OpenPastPage()
    {
        OnOpenPastPage?.Invoke();
    }
}
