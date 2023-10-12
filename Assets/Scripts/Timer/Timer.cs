using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public static event Action<float> OnTimeUpdated;
    public static event Action<float> OnTimeFinished;

    private float _time;

    private bool _isPlayerFinished;

    private IEnumerator _countCoroutine;

    private MonoBehaviour _context;

    public Timer(MonoBehaviour context)
    {
        _context = context;
    }

    public void Start()
    {
        _countCoroutine = Count();
        _context.StartCoroutine(_countCoroutine);
    }

    public void Finish()
    {
        if(_countCoroutine != null)
        {
            _isPlayerFinished = true;
            _context.StopCoroutine(_countCoroutine);

            OnTimeFinished?.Invoke(_time);
        }
    }

    private IEnumerator Count()
    {
        while (!_isPlayerFinished)
        {
            _time += Time.deltaTime;

            OnTimeUpdated?.Invoke(_time);

            yield return null;
        }
    }
}
