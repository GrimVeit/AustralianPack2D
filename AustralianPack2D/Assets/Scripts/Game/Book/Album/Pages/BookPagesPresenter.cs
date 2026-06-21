using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPagesPresenter : IBookPageProvider, IBookPageListener
{
    private readonly BookPagesModel _bookPagesModel;
    private readonly BookPagesView _bookPagesView;

    public BookPagesPresenter(BookPagesModel bookPagesModel, BookPagesView bookPagesView)
    {
        _bookPagesModel = bookPagesModel;
        _bookPagesView = bookPagesView;
    }

    public void Initialize()
    {
        ActivateEvents();

        _bookPagesView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        _bookPagesModel.OnOpenPage_Type += _bookPagesView.OpenFirstPageOfType;
        _bookPagesModel.OnOpenPage_Index += _bookPagesView.OpenPage;
        _bookPagesModel.OnOpenSecondPage += _bookPagesView.OpenSecondPage;
        _bookPagesModel.OnOpenPastPage += _bookPagesView.OpenPastPage;
    }

    private void DeactivateEvents()
    {
        _bookPagesModel.OnOpenPage_Type -= _bookPagesView.OpenFirstPageOfType;
        _bookPagesModel.OnOpenPage_Index -= _bookPagesView.OpenPage;
        _bookPagesModel.OnOpenSecondPage -= _bookPagesView.OpenSecondPage;
        _bookPagesModel.OnOpenPastPage -= _bookPagesView.OpenPastPage;
    }

    #region Output

    public event Action OnCanMoveLeft
    {
        add => _bookPagesView.OnCanMoveLeft += value;
        remove => _bookPagesView.OnCanMoveLeft -= value;
    }

    public event Action OnCanMoveRight
    {
        add => _bookPagesView.OnCanMoveRight += value;
        remove => _bookPagesView.OnCanMoveRight -= value;
    }

    public event Action OnCannotMoveLeft
    {
        add => _bookPagesView.OnCannotMoveLeft += value;
        remove => _bookPagesView.OnCannotMoveLeft -= value;
    }

    public event Action OnCannotMoveRight
    {
        add => _bookPagesView.OnCannotMoveRight += value;
        remove => _bookPagesView.OnCannotMoveRight -= value;
    }

    #endregion

    #region Input

    public void OpenPage(CardType cardType, float time = 0f)
    {
        _bookPagesModel.OpenPage(cardType, time);
    }

    public void OpenPage(int pageIndex, float time = 0f)
    {
        _bookPagesModel.OpenPage(pageIndex, time);
    }

    public void OpenSecondPage()
    {
        _bookPagesModel.OpenSecondPage();
    }

    public void OpenPastPage()
    {
        _bookPagesModel.OpenPastPage();
    }

    #endregion
}

public interface IBookPageProvider
{
    public void OpenPage(CardType cardType, float time = 0f);
    public void OpenPage(int pageIndex, float time = 0f);
    public void OpenSecondPage();
    public void OpenPastPage();
}

public interface IBookPageListener
{
    public event Action OnCanMoveLeft;
    public event Action OnCanMoveRight;

    public event Action OnCannotMoveLeft;
    public event Action OnCannotMoveRight;
}
