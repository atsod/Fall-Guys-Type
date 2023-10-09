using UnityEngine;

public class Composition : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        TriggerZone.OnEventTriggered += OnCompositionActivated;
    }

    private void OnDisable()
    {
        TriggerZone.OnEventTriggered -= OnCompositionActivated;
    }

    private void OnCompositionActivated()
    {
        _animator.Play("composition_animation");
    }
}
