using System;
using System.Collections;
using UnityEngine;

public class SpikeTrap : Trap
{
    public static event Action<int> OnTrapHit;

    private Animator _animator;

    private string _animationName;

    private float _timeOfAnimation;

    private void OnEnable()
    {
        Player.OnLose += OnPlayerLose;
    }

    private void OnDisable()
    {
        Player.OnLose -= OnPlayerLose;
    }

    private void Awake()
    {
        Damage = 15;

        _animator = GetComponentInParent<Animator>();

        _animationName = "spike_animation";

        _timeOfAnimation = _animator.runtimeAnimatorController.animationClips[0].length;
    }

    private void Start()
    {
        StartTrapCoroutine();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            HitPlayer();
        }
    }

    protected override IEnumerator ActivateTrap()
    {
        while(true)
        {
            _animator.Play(_animationName, 0, 0f);

            yield return new WaitForSeconds(_timeOfAnimation);

            float randomDelay = (float) UnityEngine.Random.Range(0f, 300f) / 100;

            yield return new WaitForSeconds(randomDelay);
        }
    }

    private void HitPlayer()
    {
        OnTrapHit?.Invoke(Damage);
    }

    private void OnPlayerLose()
    {
        StopTrapCoroutine();
    }
}
