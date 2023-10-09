using System;
using UnityEngine;

public class Item : MonoBehaviour, I_Interactable
{
    public static event Action OnItemInteracted;

    private Outline _outline;

    private ParticleSystem _particleSystem;
    private Animator _animator;
    
    private Camera _mainCamera;
    private Canvas _itemInterface;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0f;

        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _animator = GetComponent<Animator>();

        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _itemInterface = GetComponentInChildren<Canvas>();
    }

    private void Start()
    {
        _itemInterface.worldCamera = _mainCamera;
    }

    public void OnHoverEnter()
    {
        _outline.OutlineWidth = 8f;
    }

    public void OnHoverExit()
    {
        _outline.OutlineWidth = 0f;
    }

    public void Interact()
    {
        OnItemInteracted?.Invoke();

        _particleSystem.Play();
        _animator.Play("destroy_animation");
        Destroy(gameObject, 1f);
    }
}
