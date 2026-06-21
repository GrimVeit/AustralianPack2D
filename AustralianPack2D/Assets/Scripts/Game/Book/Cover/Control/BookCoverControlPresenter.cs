using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCoverControlPresenter
{
    private readonly BookCoverControlModel _model;
    private readonly BookCoverControlView _view;

    public BookCoverControlPresenter(BookCoverControlModel model, BookCoverControlView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnClickToLeft += _model.MoveLeft;
        _view.OnClickToRight += _model.MoveRight;

        _model.OnShowLeft += _view.ShowLeft;
        _model.OnShowRight += _view.ShowRight;
        _model.OnHideLeft += _view.HideLeft;
        _model.OnHideRight += _view.HideRight;
    }

    private void DeactivateEvents()
    {
        _view.OnClickToLeft -= _model.MoveLeft;
        _view.OnClickToRight -= _model.MoveRight;

        _model.OnShowLeft -= _view.ShowLeft;
        _model.OnShowRight -= _view.ShowRight;
        _model.OnHideLeft -= _view.HideLeft;
        _model.OnHideRight -= _view.HideRight;
    }
}
