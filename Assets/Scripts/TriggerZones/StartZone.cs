using System;
using UnityEngine;

public class StartZone : MonoBehaviour
{
    public static event Action OnStarted;

    private Timer _timer;

    private bool _isTimerStarted;

    public void Initialize(Timer timer)
    {
        _timer = timer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Player>())
        {
            if(!_isTimerStarted)
            {
                _isTimerStarted = true;

                _timer.Start();
                OnStarted?.Invoke();
            }
        }
    }
}
