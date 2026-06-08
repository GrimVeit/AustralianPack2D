using System;

public class ChooseBuyBoxModel
{
    private int _currentId = -1;
    private readonly ISoundProvider _soundProvider;

    public ChooseBuyBoxModel(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void Choose(int id)
    {
        if (_currentId == id) return;

        OnUnchoose?.Invoke(_currentId);

        _soundProvider.PlayOneShot("ChoosePack");
        _currentId = id;
        OnChoose?.Invoke(_currentId);
    }

    public void HideAll()
    {
        OnHideAll?.Invoke(_currentId);

        _currentId = -1;
    }

    public void ShowAll()
    {
        OnShowAll?.Invoke();
    }

    #region Output

    public event Action<int> OnChoose;
    public event Action<int> OnUnchoose;

    public event Action OnShowAll;
    public event Action<int> OnHideAll;

    #endregion
}
