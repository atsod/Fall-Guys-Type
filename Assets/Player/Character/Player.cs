using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action<int> OnDamaged;
    public static event Action OnLose;

    private float _healthPoints;

    private void OnEnable()
    {
        DelayedDamageTrap.OnTrapHit += OnPlayerDamaged;
        SpikeTrap.OnTrapHit += OnPlayerDamaged;
    }

    private void OnDisable()
    {
        DelayedDamageTrap.OnTrapHit -= OnPlayerDamaged;
        SpikeTrap.OnTrapHit -= OnPlayerDamaged;
    }

    private void Awake()
    {
        _healthPoints = 100f;
    }

    private void OnPlayerDamaged(int damage)
    {
        _healthPoints -= damage;

        OnDamaged?.Invoke(damage);

        if (_healthPoints <= 0)
        {
            OnPlayerLose();
        }
    }

    private void OnPlayerLose()
    {
        OnLose?.Invoke();
    }
}
