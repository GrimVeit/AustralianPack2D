using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPagesModel
{
    public event Action OnEndOpenPage;

    public event Action OnOpenSecondPage;
    public event Action OnOpenPastPage;
    public event Action<int, float> OnOpenPage_Index;
    public event Action<CardType, float> OnOpenPage_Type;

    public event Action<BookPage> OnNumberPage;

    private ISoundProvider soundProvider;

    public BookPagesModel(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void OpenPage(int page, float time)
    {
        OnOpenPage_Index?.Invoke(page, time);
    }

    public void OpenPage(CardType cardType, float time)
    {
        OnOpenPage_Type?.Invoke(cardType, time);
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
