using System;
using UnityEngine;

public class FinishZone : MonoBehaviour
{
    public static event Action OnFinished;

    private Timer _timer;

    public void Initialize(Timer timer)
    {
        _timer = timer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Player>() != null)
        {
            _timer.Finish();
            OnFinished?.Invoke();
        }
    }
}
