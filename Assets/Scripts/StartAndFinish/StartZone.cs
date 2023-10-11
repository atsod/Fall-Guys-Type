using System;
using UnityEngine;

public class StartZone : MonoBehaviour
{
    public static event Action OnStarted;

    private Timer _timer;

    public void Initialize(Timer timer)
    {
        _timer = timer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Player>())
        {
            _timer.Start();
            OnStarted?.Invoke();
        }
    }
}
