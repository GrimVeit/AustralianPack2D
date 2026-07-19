using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMiddlePanel_Menu : MovePanel
{
    [SerializeField] private UIEffectCombination effectCombination;
    [SerializeField] private Button buttonLevel;
    [SerializeField] private Button buttonSettings;
    [SerializeField] private Button buttonStore;
    [SerializeField] private Button buttonAlbum;
    [SerializeField] private Button buttonLeaders;

    public override void Initialize()
    {
        base.Initialize();

        buttonLevel.onClick.AddListener(ClickLevel);
        buttonSettings.onClick.AddListener(ClickSettings);
        buttonStore.onClick.AddListener(ClickStore);
        buttonAlbum.onClick.AddListener(ClickAlbum);
        buttonLeaders.onClick.AddListener(ClickLeaders);

        effectCombination.Initialize();
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonLevel.onClick.RemoveListener(ClickLevel);
        buttonSettings.onClick.RemoveListener(ClickSettings);
        buttonStore.onClick.RemoveListener(ClickStore);
        buttonAlbum.onClick.RemoveListener(ClickAlbum);
        buttonLeaders.onClick.RemoveListener(ClickLeaders);

        effectCombination.Dispose();
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        effectCombination.ActivateEffect();
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        effectCombination.DeactivateEffect();
    }

    #region Output

    public event Action OnClickLevel;
    public event Action OnClickSettings;
    public event Action OnClickAlbum;
    public event Action OnClickStore;
    public event Action OnClickLeaders;
    

    private void ClickLevel()
    {
        OnClickLevel?.Invoke();
    }

    private void ClickSettings()
    {
        OnClickSettings?.Invoke();
    }

    private void ClickAlbum()
    {
        OnClickAlbum?.Invoke();
    }

    private void ClickStore()
    {
        OnClickStore?.Invoke();
    }

    private void ClickLeaders()
    {
        OnClickLeaders?.Invoke();
    }

    #endregion
}
