using System;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationPanel_Menu : MovePanel
{
    [SerializeField] private UIEffectCombination combination;
    [SerializeField] private Button buttonSave;

    public override void Initialize()
    {
        base.Initialize();

        combination.Initialize();

        buttonSave.onClick.AddListener(ClickToSave);
    }

    public override void Dispose()
    {
        base.Dispose();

        combination.Dispose();

        buttonSave.onClick.RemoveListener(ClickToSave);
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        combination.ActivateEffect();
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        combination.DeactivateEffect();
    }

    #region Output

    public event Action OnClickToSave;

    private void ClickToSave()
    {
        OnClickToSave?.Invoke();
    }

    #endregion
}
