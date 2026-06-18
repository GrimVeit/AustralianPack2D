using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameCard : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private RectTransform rectTransformParent;
    [SerializeField] private RectTransform rectTransformMiddle;
    [SerializeField] private RectTransform rectTransformCover;
    [SerializeField] private RectTransform rectTransformCard;
    [SerializeField] private Image imageCard;

    [Header("Shake - Position")]
    [SerializeField] private float shakeDuration = 0.4f;
    [SerializeField] private Vector2 shakeStrength = new Vector2(0.1f, 0.15f);
    [SerializeField] private int shakeVibrato = 10;
    [SerializeField] private float shakeRandomness = 90f;
    [SerializeField] private bool shakeFadeOut = true;

    [Header("Shake - Rotation")]
    [SerializeField] private float rotationPunchZ = 20f;
    [SerializeField] private float rotationDuration = 0.4f;
    [SerializeField] private int rotationVibrato = 10;
    [SerializeField] private float rotationElasticity = 1f;

    private Vector3 _startParentPos;
    private Quaternion _startParentRot;

    private Sequence sequenceShowHide;

    private Vector2 _direction = Vector2.right;
    private float _cardOffset;

    public void Initialize()
    {
        _startParentPos = rectTransformParent.localPosition;
        _startParentRot = rectTransformParent.localRotation;

        _cardOffset = rectTransformCard.sizeDelta.x;

        RollDirection();

        rectTransformCard.localPosition = _direction * _cardOffset;
        rectTransformCover.localPosition = Vector3.zero;
    }

    private void RollDirection()
    {
        float r = Random.value;

        if (r < 0.25f)
            _direction = Vector2.right;
        else if (r < 0.5f)
            _direction = Vector2.left;
        else if (r < 0.75f)
            _direction = Vector2.up;
        else
            _direction = Vector2.down;
    }

    public void SetData(Sprite sprite)
    {
        imageCard.sprite = sprite;
    }

    public void SetSizeDelta(Vector2 size)
    {
        rectTransformCover.sizeDelta = size;
        rectTransformCard.sizeDelta = size;
        rectTransformMiddle.sizeDelta = size;
        rectTransformParent.sizeDelta = size;

        _cardOffset = size.x;
    }

    public void SetAnchoredPosition(Vector2 pos)
    {
        rectTransformParent.anchoredPosition = pos;
    }

    public void PlayWrongShake()
    {
        rectTransformParent.DOKill();

        rectTransformParent.SetAsLastSibling();

        Sequence seq = DOTween.Sequence();

        seq.Join(rectTransformParent.DOShakePosition(
            shakeDuration,
            new Vector3(shakeStrength.x, shakeStrength.y, 0f),
            shakeVibrato,
            shakeRandomness,
            false,
            shakeFadeOut
        ));

        seq.Join(rectTransformParent.DOPunchRotation(
            new Vector3(0f, 0f, rotationPunchZ),
            rotationDuration,
            rotationVibrato,
            rotationElasticity
        ));

        seq.Append(rectTransformParent.DOLocalMove(_startParentPos, 0.15f).SetEase(Ease.OutQuad));
        seq.Join(rectTransformParent.DOLocalRotateQuaternion(_startParentRot, 0.15f).SetEase(Ease.OutQuad));
    }

    public void Show()
    {
        sequenceShowHide?.Kill();

        Vector2 offset = _direction * _cardOffset;

        sequenceShowHide = DOTween.Sequence();

        sequenceShowHide.Append(
            rectTransformCover.DOLocalMove(-offset, 0.2f)
                .SetEase(Ease.InOutQuad)
        );

        sequenceShowHide.Join(
            rectTransformCard.DOLocalMove(Vector3.zero, 0.25f)
                .SetEase(Ease.OutBack)
        );
    }

    public void Hide()
    {
        sequenceShowHide?.Kill();

        Vector2 offset = _direction * _cardOffset;

        sequenceShowHide = DOTween.Sequence();

        sequenceShowHide.Append(
            rectTransformCover.DOLocalMove(Vector3.zero, 0.2f)
                .SetEase(Ease.InOutQuad)
        );

        sequenceShowHide.Join(
            rectTransformCard.DOLocalMove(offset, 0.2f)
                .SetEase(Ease.InBack)
        );

        sequenceShowHide.OnComplete(() =>
        {
            RollDirection();
        });
    }
}
