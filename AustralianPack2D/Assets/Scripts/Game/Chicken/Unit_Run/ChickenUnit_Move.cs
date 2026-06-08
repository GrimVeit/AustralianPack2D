using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenUnit_Move : MonoBehaviour
{
    public Vector3 LocalPosition => transform.localPosition;

    [SerializeField] private float moveSpeed = 2f;

    private Vector3 _target;
    public float _currentSpeed;

    private Coroutine _moveCoroutine;
    private Coroutine _speedCoroutine;

    public void Initialize()
    {
        _currentSpeed = moveSpeed;
    }

    public void SetTarget(Vector3 target)
    {
        _target = new Vector3(target.x, transform.localPosition.y, transform.localPosition.z);
    }

    public void SetSpeed(float speed)
    {
        if (_speedCoroutine != null)
            StopCoroutine(_speedCoroutine);

        _currentSpeed = speed;
    }

    public void SetSpeed(float speed, float duration)
    {
        if (_speedCoroutine != null)
            StopCoroutine(_speedCoroutine);

        _speedCoroutine = StartCoroutine(ChangeSpeed(speed, duration));
    }

    public void StartMove()
    {
        if (_moveCoroutine != null) return;

        StopMove();
        _moveCoroutine = StartCoroutine(MoveRoutine());
    }

    public void StopMove()
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);
    }

    private IEnumerator MoveRoutine()
    {
        while (Vector3.Distance(transform.localPosition, _target) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                _target,
                _currentSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.localPosition = _target;
        OnEndMove?.Invoke();
        _moveCoroutine = null;
    }

    private IEnumerator ChangeSpeed(float target, float duration)
    {
        if (duration <= 0f)
        {
            _currentSpeed = target;
            yield break;
        }

        float start = _currentSpeed;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            _currentSpeed = Mathf.Lerp(start, target, time / duration);
            yield return null;
        }

        _currentSpeed = target;
        _speedCoroutine = null;
    }

    #region Output

    public event Action OnEndMove;

    #endregion
}
