using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPagesPresenter
{
    private readonly BookPagesModel bookPagesModel;
    private readonly BookPagesView bookPagesView;

    public BookPagesPresenter(BookPagesModel bookPagesModel, BookPagesView bookPagesView)
    {
        this.bookPagesModel = bookPagesModel;
        this.bookPagesView = bookPagesView;
    }

    public void Initialize()
    {
        ActivateEvents();

        bookPagesView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        bookPagesModel.OnOpenPage_Type += bookPagesView.OpenFirstPageOfType;
        bookPagesModel.OnOpenPage_Index += bookPagesView.OpenPage;
        bookPagesModel.OnOpenSecondPage += bookPagesView.OpenSecondPage;
        bookPagesModel.OnOpenPastPage += bookPagesView.OpenPastPage;
    }

    private void DeactivateEvents()
    {
        bookPagesModel.OnOpenPage_Type -= bookPagesView.OpenFirstPageOfType;
        bookPagesModel.OnOpenPage_Index -= bookPagesView.OpenPage;
        bookPagesModel.OnOpenSecondPage -= bookPagesView.OpenSecondPage;
        bookPagesModel.OnOpenPastPage -= bookPagesView.OpenPastPage;
    }

    #region Input

    public void OpenPage(CardType cardType, float time = 0f)
    {
        bookPagesModel.OpenPage(cardType, time);
    }

    public void OpenPage(int pageIndex, float time = 0f)
    {
        bookPagesModel.OpenPage(pageIndex, time);
    }

    public void OpenSecondPage()
    {
        bookPagesModel.OpenSecondPage();
    }

    public void OpenPastPage()
    {
        bookPagesModel.OpenPastPage();
    }

    #endregion
}
