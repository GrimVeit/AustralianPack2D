using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowChickenPictureModel
{
    private readonly IVisualChickenPictureListener _visualChickenPictureListener;

    public ShowChickenPictureModel(IVisualChickenPictureListener visualChickenPictureListener)
    {
        _visualChickenPictureListener = visualChickenPictureListener;

        _visualChickenPictureListener.OnClickPicture += ShowPicture;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _visualChickenPictureListener.OnClickPicture -= ShowPicture;
    }

    private void ShowPicture(ChickenType typePicture, int idPicture, int countHave, int countAll)
    {
        OnSetData?.Invoke(typePicture, idPicture, countHave, countAll);

        if(countHave >= countAll)
        {
            OnShowFullPicture?.Invoke();
        }
        else
        {
            OnShowNotFullPicture?.Invoke();
        }
    }

    #region Output

    public event Action OnShowFullPicture;
    public event Action OnShowNotFullPicture;

    public event Action<ChickenType, int, int, int> OnSetData;

    #endregion
}
