using System;
using UnityEngine;

public class FinishZone : MonoBehaviour
{
    public static event Action OnFinished;

    private Timer _timer;

    private bool _isPlayerStarted;

    private void OnEnable()
    {
        StartZone.OnStarted += OnPlayerStarted;        
    }

    private void OnDisable()
    {
        StartZone.OnStarted -= OnPlayerStarted;
    }

    public void Initialize(Timer timer)
    {
        _timer = timer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Player>() != null)
        {
            if(_isPlayerStarted)
            {
                _timer.Finish();
                OnFinished?.Invoke();
            }
        }
    }

    private void OnPlayerStarted()
    {
        _isPlayerStarted = true;
    }
}
