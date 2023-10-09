using System;
using System.Collections;
using UnityEngine;

public class DelayedDamageTrap : Trap
{
    public static event Action<int> OnTrapHit;

    private bool _isPlayerOnTrap;
    private bool _isCoroutineRunning;

    private Animator _animator;
    private string _warnAnimationName;
    private string _hitAnimationName;

    private float _warningDelay;
    private float _reloadDelay;

    private void Awake()
    {
        Damage = 10;

        TrapRenderer = GetComponentInParent<MeshRenderer>();
        NormalTrapColor = TrapRenderer.material.color;

        _animator = GetComponentInParent<Animator>();
        _warnAnimationName = "warn_animation";
        _hitAnimationName = "hit_animation";

        _warningDelay = 1f;
        _reloadDelay = 5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            _isPlayerOnTrap = true;

            if (!_isCoroutineRunning)
            {
                StartTrapCoroutine();
            } 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            _isPlayerOnTrap = false;
        }
    }

    protected override IEnumerator ActivateTrap()
    {
        _isCoroutineRunning = true;

        while(true)
        {
            WarnPlayer();

            yield return new WaitForSeconds(_warningDelay);

            HitPlayer();

            yield return new WaitForSeconds(_reloadDelay);
        }
    }

    private void WarnPlayer()
    {
        _animator.Play(_warnAnimationName, 0, 0);
    }

    private void HitPlayer()
    {
        _animator.Play(_hitAnimationName, 0, 0);

        if(_isPlayerOnTrap)
        {
            OnTrapHit?.Invoke(Damage);
        }
    }
}
