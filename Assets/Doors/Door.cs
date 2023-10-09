using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Item.OnItemInteracted += OnDoorInteracted;
    }

    private void OnDisable()
    {
        Item.OnItemInteracted -= OnDoorInteracted;
    }

    private void OnDoorInteracted()
    {
        _animator.Play("door_animation");
    }
}
