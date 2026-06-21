using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BookCoverPagesView : View
{
    [SerializeField] private List<BookCoverPage> openPages;
    [SerializeField] private RectTransform viewport;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textPage;

    private BookCoverPage currentOpenPage;
    private Coroutine routine;
    private bool isAnimating;

    private Dictionary<CardType, int> firstIndexByType;

    private float Width => viewport.rect.width;

    #region Init

    public void Initialize()
    {
        foreach (var p in openPages)
            p.HideInstant();

        openPages = openPages
            .OrderBy(p => p.BookPageData.Index)
            .ToList();

        currentOpenPage = openPages[0];
        currentOpenPage.ShowInstant();

        UpdateUI();
        UpdateNavigationState();
    }

    #endregion

    #region PUBLIC INPUTS

    public void OpenSecondPage()
    {
        TryMove(currentOpenPage.BookPageData.Index + 1);
    }

    public void OpenPastPage()
    {
        TryMove(currentOpenPage.BookPageData.Index - 1);
    }

    #endregion

    #region CORE ENTRY

    private void TryMove(int targetIndex, float duration = 0.25f)
    {
        if (isAnimating)
            return;

        targetIndex = Mathf.Clamp(targetIndex, 0, openPages.Count - 1);

        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(MoveRoutine(targetIndex, duration));
    }

    #endregion

    #region MOVEMENT

    private IEnumerator MoveRoutine(int targetIndex, float duration = 0.25f)
    {
        isAnimating = true;

        // 🔥 instant jump (NO ANIMATION)
        if (duration <= 0f)
        {
            while (currentOpenPage.BookPageData.Index != targetIndex)
            {
                int current = currentOpenPage.BookPageData.Index;

                if (targetIndex > current)
                    InstantNext(current + 1);
                else
                    InstantPrev(current - 1);
            }

            isAnimating = false;
            yield break;
        }

        // 🔥 animated version
        while (currentOpenPage.BookPageData.Index != targetIndex)
        {
            int current = currentOpenPage.BookPageData.Index;

            if (targetIndex > current)
                yield return MoveNext(current + 1, duration);
            else
                yield return MovePrev(current - 1, duration);
        }

        isAnimating = false;
    }

    private IEnumerator MoveNext(int nextIndex, float duration = 0.25f)
    {
        BookCoverPage oldPage = currentOpenPage;
        BookCoverPage newPage = openPages[nextIndex];

        oldPage.RectTransform.anchoredPosition = Vector2.zero;
        newPage.RectTransform.anchoredPosition = new Vector2(Width, 0);

        newPage.gameObject.SetActive(true);

        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            float k = t / duration;

            oldPage.RectTransform.anchoredPosition =
                Vector2.Lerp(Vector2.zero, new Vector2(-Width, 0), k);

            newPage.RectTransform.anchoredPosition =
                Vector2.Lerp(new Vector2(Width, 0), Vector2.zero, k);

            yield return null;
        }

        oldPage.HideInstant();

        currentOpenPage = newPage;

        UpdateUI();
        UpdateNavigationState();
    }

    private IEnumerator MovePrev(int prevIndex, float duration = 0.25f)
    {
        BookCoverPage oldPage = currentOpenPage;
        BookCoverPage newPage = openPages[prevIndex];

        oldPage.RectTransform.anchoredPosition = Vector2.zero;
        newPage.RectTransform.anchoredPosition = new Vector2(-Width, 0);

        newPage.gameObject.SetActive(true);

        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            float k = t / duration;

            oldPage.RectTransform.anchoredPosition =
                Vector2.Lerp(Vector2.zero, new Vector2(Width, 0), k);

            newPage.RectTransform.anchoredPosition =
                Vector2.Lerp(new Vector2(-Width, 0), Vector2.zero, k);

            yield return null;
        }

        oldPage.HideInstant();

        currentOpenPage = newPage;

        UpdateUI();
        UpdateNavigationState();
    }

    private void InstantNext(int nextIndex)
    {
        BookCoverPage oldPage = currentOpenPage;
        BookCoverPage newPage = openPages[nextIndex];

        oldPage.RectTransform.anchoredPosition = Vector2.zero;

        newPage.gameObject.SetActive(true);
        newPage.RectTransform.anchoredPosition = Vector2.zero;

        oldPage.HideInstant();

        currentOpenPage = newPage;

        UpdateUI();
        UpdateNavigationState();
    }

    private void InstantPrev(int prevIndex)
    {
        BookCoverPage oldPage = currentOpenPage;
        BookCoverPage newPage = openPages[prevIndex];

        oldPage.RectTransform.anchoredPosition = Vector2.zero;

        newPage.gameObject.SetActive(true);
        newPage.RectTransform.anchoredPosition = Vector2.zero;

        oldPage.HideInstant();

        currentOpenPage = newPage;

        UpdateUI();
        UpdateNavigationState();
    }

    private void UpdateNavigationState()
    {
        int index = currentOpenPage.BookPageData.Index;

        bool canLeft = index > 0;
        bool canRight = index < openPages.Count - 1;

        if (canLeft)
            OnCanMoveLeft?.Invoke();
        else
            OnCannotMoveLeft?.Invoke();

        if (canRight)
            OnCanMoveRight?.Invoke();
        else
            OnCannotMoveRight?.Invoke();
    }

    #endregion

    #region UI

    private void UpdateUI()
    {
        textPage.text = $"Page: {currentOpenPage.BookPageData.Index + 1}";
    }

    #endregion

    #region Output

    public event Action OnCanMoveLeft;
    public event Action OnCanMoveRight;

    public event Action OnCannotMoveLeft;
    public event Action OnCannotMoveRight;

    #endregion
}
