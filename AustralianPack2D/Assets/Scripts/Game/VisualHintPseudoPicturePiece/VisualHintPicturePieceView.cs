using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class VisualHintPicturePieceView : View
{
    [SerializeField] private Transform transformHint;
    [SerializeField] private HintPictureZone hintPictureZone;
    [SerializeField] private HintText hintText;
    [SerializeField] private List<TypeText> typeTexts = new List<TypeText>();

    public void Initialize()
    {
        hintPictureZone.OnSetType += SetType;
    }

    public void Dispose()
    {
        hintPictureZone.OnSetType -= SetType;
    }

    public void Show()
    {
        transformHint.DOScale(1, 0.1f);
    }

    public void Hide()
    {
        transformHint.DOScale(0, 0.1f);
    }

    private void SetType(ChickenType type)
    {
        var text = typeTexts.FirstOrDefault(data => data.Type == type).Name;

        hintText.Show(text);
    }

    [System.Serializable]
    private class HintText
    {
        [SerializeField] private Transform transformText;
        [SerializeField] private TextMeshProUGUI textHint;

        private Sequence sequence;

        public void Show(string text)
        {
            textHint.text = text;

            sequence?.Kill();
            transformText.localScale = Vector3.zero;

            sequence = DOTween.Sequence();

            sequence.Append(transformText.DOScale(1, 0.2f).SetEase(Ease.OutBack));
            sequence.AppendInterval(0.5f);
            sequence.Append(transformText.DOScale(0, 0.2f));
        }
    }

    [System.Serializable]
    private class TypeText
    {
        [SerializeField] private ChickenType type;
        [SerializeField] private string name;

        public ChickenType Type => type;
        public string Name => name;
    }
}
