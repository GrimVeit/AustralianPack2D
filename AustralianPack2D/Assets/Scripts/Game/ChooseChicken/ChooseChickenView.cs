using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseChickenView : View
{
    [SerializeField] private List<ChooseChicken> chooseChickens = new(); //5 CHICKENS
    [SerializeField] private ChooseChickenDatas chickenDatas;

    [SerializeField] private UIEffect effectButton;
    [SerializeField] private Button buttonChoose;

    private IEnumerator timerShow;

    public void Initialize()
    {
        chooseChickens.ForEach(data =>
        {
            data.OnChooseChicken += ChooseChicken;
            data.Initialize();
        });

        effectButton.Initialize();
    }

    public void Dispose()
    {
        if (timerShow != null) Coroutines.Stop(timerShow);

        chooseChickens.ForEach(data =>
        {
            data.OnChooseChicken -= ChooseChicken;
            data.Dispose();
        });

        effectButton.Dispose();
    }

    public void ActivateAll()
    {
        chooseChickens.ForEach(cc => cc.Activate());

        DeactivateButtonChoose();
    }

    public void DeactivateAll()
    {
        chooseChickens.ForEach(cc => cc.Deactivate());

        DeactivateButtonChoose();
    }


    public void ShowAll()
    {
        if(timerShow != null) Coroutines.Stop(timerShow);

        timerShow = TimerShow();
        Coroutines.Start(timerShow);
    }

    public void HideAll()
    {
        chooseChickens.ForEach(cc => cc.Hide());
    }


    public void Choose(ChickenType type)
    {
        var chicken = chooseChickens.FirstOrDefault(ch => ch.Type == type);

        if(chicken == null)
        {
            Debug.LogWarning("Not found ChooseChicken with ChickenType - " + type);
            return;
        }

        chicken.Choose();

        ActivateButtonChoose();
    }

    public void Unchoose(ChickenType type)
    {
        var chicken = chooseChickens.FirstOrDefault(ch => ch.Type == type);

        if (chicken == null)
        {
            Debug.LogWarning("Not found ChooseChicken with ChickenType - " + type);
            return;
        }

        chicken.Unchoose();
    }

    public void ChooseAll()
    {
        chooseChickens.ForEach(cc => cc.Choose());
    }

    public void SetTypes(List<ChickenType> chickenTypes)
    {
        if(chooseChickens.Count != chickenTypes.Count)
        {
            Debug.LogError("ERROR COUNT");
            return;
        }

        for (int i = 0; i < chooseChickens.Count; i++)
        {
            var data = chickenDatas.GetChooseChickenData(chickenTypes[i]);

            if(data == null)
            {
                Debug.LogWarning("Not found ChickenData with ChickenType - " + chickenTypes[i]);
                return;
            }

            chooseChickens[i].SetData(data);
        }
    }

    private IEnumerator TimerShow()
    {
        for (int i = 0; i < chooseChickens.Count; i++)
        {
            chooseChickens[i].Show();

            OnSetSoundOpen?.Invoke();

            yield return new WaitForSeconds(0.4f);
        }
    }

    private void ActivateButtonChoose()
    {
        effectButton.ActivateEffect();
        buttonChoose.enabled = true;
    }

    private void DeactivateButtonChoose()
    {
        effectButton.DeactivateEffect();
        buttonChoose.enabled = false;
    }

    #region Output

    public event Action<ChickenType> OnChooseChicken;
    public event Action OnSetSoundOpen;

    private void ChooseChicken(ChickenType type)
    {
        OnChooseChicken?.Invoke(type);
    }

    #endregion
}

[Serializable]
public class ChooseChicken
{
    public ChickenType Type => _data.ChickenType;

    [SerializeField] private Image imageChicken;
    [SerializeField] private Button buttonChicken;
    [SerializeField] private SkeletonGraphic skeleton;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private UIEffect effectName;

    private ChooseChickenData _data;
    private bool isActive = false;

    public void Initialize()
    {
        effectName.Initialize();

        buttonChicken.onClick.AddListener(ChooseChick);
    }

    public void Dispose()
    {
        effectName.Dispose();

        buttonChicken.onClick.RemoveListener(ChooseChick);
    }

    public void SetData(ChooseChickenData data)
    {
        _data = data;

        textName.text = data.Name;
    }

    public void Show()
    {
        skeleton.transform.localScale = Vector3.one;
        imageChicken.transform.localScale = Vector3.zero;

        imageChicken.transform.DOScale(Vector3.one, 0.3f)
        .SetEase(Ease.OutBack);

        skeleton.AnimationState.SetAnimation(0, "avatar", false);
    }

    public void Hide()
    {
        imageChicken.transform.DOScale(Vector3.zero, 0.3f)
        .SetEase(Ease.InBack);
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }


    public void Choose()
    {
        effectName.ActivateEffect();

        imageChicken.sprite = _data.SpriteActivate;
    }

    public void Unchoose()
    {
        effectName.DeactivateEffect();

        imageChicken.sprite = _data.SpriteDeactivate;
    }

    #region Output

    public event Action<ChickenType> OnChooseChicken;
    private void ChooseChick()
    {
        if(!isActive) return;

        OnChooseChicken?.Invoke(_data.ChickenType);
    }

    #endregion
}

[Serializable]
public class ChooseChickenDatas
{
    [SerializeField] private List<ChooseChickenData> chickenDatas;

    public ChooseChickenData GetChooseChickenData(ChickenType type)
    {
        return chickenDatas.FirstOrDefault(data => data.ChickenType == type);
    }
}

[Serializable]
public class ChooseChickenData
{
    public ChickenType ChickenType => chickenType;
    public Sprite SpriteActivate => spriteActivate;
    public Sprite SpriteDeactivate => spriteDeactivate;
    public string Name => name;

    [SerializeField] private ChickenType chickenType;
    [SerializeField] private Sprite spriteActivate;
    [SerializeField] private Sprite spriteDeactivate;
    [SerializeField] private string name;
}
