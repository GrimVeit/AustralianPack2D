using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookPagesView : View
{
    [SerializeField] private List<BookPage> openPages;
    [SerializeField] private RectTransform viewport;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textPage;
    [SerializeField] private TextMeshProUGUI textCardType;

    private BookPage currentOpenPage;
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

        BuildIndex();

        currentOpenPage = openPages[0];
        currentOpenPage.ShowInstant();

        UpdateUI();
    }

    private void BuildIndex()
    {
        firstIndexByType = new Dictionary<CardType, int>();

        foreach (var page in openPages)
        {
            var type = page.BookPageData.TypeCards;

            if (!firstIndexByType.ContainsKey(type))
                firstIndexByType[type] = page.BookPageData.Index;
        }
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

    public void OpenPage(int index, float time)
    {
        TryMove(index, time);
    }

    public void OpenFirstPageOfType(CardType type, float time)
    {
        if (!firstIndexByType.TryGetValue(type, out int index))
            return;

        TryMove(index, time);
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
        BookPage oldPage = currentOpenPage;
        BookPage newPage = openPages[nextIndex];

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
    }

    private IEnumerator MovePrev(int prevIndex, float duration = 0.25f)
    {
        BookPage oldPage = currentOpenPage;
        BookPage newPage = openPages[prevIndex];

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
    }

    private void InstantNext(int nextIndex)
    {
        BookPage oldPage = currentOpenPage;
        BookPage newPage = openPages[nextIndex];

        oldPage.RectTransform.anchoredPosition = Vector2.zero;

        newPage.gameObject.SetActive(true);
        newPage.RectTransform.anchoredPosition = Vector2.zero;

        oldPage.HideInstant();

        currentOpenPage = newPage;

        UpdateUI();
    }

    private void InstantPrev(int prevIndex)
    {
        BookPage oldPage = currentOpenPage;
        BookPage newPage = openPages[prevIndex];

        oldPage.RectTransform.anchoredPosition = Vector2.zero;

        newPage.gameObject.SetActive(true);
        newPage.RectTransform.anchoredPosition = Vector2.zero;

        oldPage.HideInstant();

        currentOpenPage = newPage;

        UpdateUI();
    }

    #endregion

    #region UI

    private void UpdateUI()
    {
        textPage.text = $"Page: {currentOpenPage.BookPageData.Index + 1}";

        textCardType.text = currentOpenPage.BookPageData.TypeCards.ToString();
    }

    #endregion
}
