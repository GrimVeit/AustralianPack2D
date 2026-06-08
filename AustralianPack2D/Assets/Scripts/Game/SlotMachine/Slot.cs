using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public event Action OnStartSpin;
    public event Action<float> OnWheelSpeed;
    public event Action<int> OnStopSpin;

    public ScrollRect scrollRect;
    public float minScrollSpeed = 0.5f;
    public float maxScrollSpeed = 2f;
    public float minDuration = 0.5f;
    public float maxDuration = 2f;
    public RectTransform content;
    public SlotValue[] slotValues;
    public RectTransform centerPoint;

    public void StartSpin()
    {
        StartCoroutine(Spin());
    }


    private IEnumerator Spin()
    {
        float elapsedTime = 0f;
        float startSpeed = UnityEngine.Random.Range(minScrollSpeed, maxScrollSpeed);
        float duration = UnityEngine.Random.Range(minDuration, maxDuration);
        float endSpeed = 0f;

        OnStartSpin?.Invoke();

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float currentSpeed = Mathf.Lerp(startSpeed, endSpeed, elapsedTime/duration);

            Debug.Log(currentSpeed);

            OnWheelSpeed?.Invoke(currentSpeed);

            scrollRect.verticalNormalizedPosition += currentSpeed * Time.deltaTime;
            scrollRect.verticalNormalizedPosition = scrollRect.verticalNormalizedPosition % 1; // Çàöèêëèâàíèå


            yield return null;
        }


        SlotValue closestSlotValue = GetClosestSlotValue();

        yield return StartCoroutine(SmoothScrollToItem(closestSlotValue.TransformSlotValue));

        OnStopSpin?.Invoke(closestSlotValue.SlotID);
    }


    private SlotValue GetClosestSlotValue()
    {
        float centerPosition = centerPoint.position.y;
        float minDistance = float.MaxValue;
        SlotValue closestItem = null;


        foreach (var slotValue in slotValues)
        {
            float distance = Mathf.Abs(slotValue.TransformSlotValue.position.y - centerPosition);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestItem = slotValue;
            }
        }


        return closestItem;
    }


    private IEnumerator SmoothScrollToItem(RectTransform item)
    {
        float distance = item.position.y - centerPoint.position.y;
        Vector3 targetPosition = content.position + new Vector3(0, -distance, 0);
        float elapsedTime = 0f;
        float smoothDuration = 0.3f;


        while (elapsedTime < smoothDuration)
        {
            elapsedTime += Time.deltaTime;
            content.position = Vector3.Lerp(content.position, targetPosition, elapsedTime / smoothDuration);
            yield return null;
        }

        yield return null;

        content.position = targetPosition;
    }
}
