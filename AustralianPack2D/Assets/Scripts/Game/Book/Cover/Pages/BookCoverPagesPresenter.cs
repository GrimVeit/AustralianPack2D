using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCoverPagesPresenter : IBookCoverPageProvider, IBookCoverPageListener
{
    private readonly BookCoverPagesModel _bookPagesModel;
    private readonly BookCoverPagesView _bookPagesView;

    public BookCoverPagesPresenter(BookCoverPagesModel bookPagesModel, BookCoverPagesView bookPagesView)
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
        _bookPagesModel.OnOpenSecondPage += _bookPagesView.OpenSecondPage;
        _bookPagesModel.OnOpenPastPage += _bookPagesView.OpenPastPage;
    }

    private void DeactivateEvents()
    {
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

public interface IBookCoverPageProvider
{
    public void OpenSecondPage();
    public void OpenPastPage();
}

public interface IBookCoverPageListener
{
    public event Action OnCanMoveLeft;
    public event Action OnCanMoveRight;

    public event Action OnCannotMoveLeft;
    public event Action OnCannotMoveRight;
}