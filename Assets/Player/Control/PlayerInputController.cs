using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private IControllable _controllable;
    private IViewable _viewable;

    private Camera _mainCamera;
    private I_Interactable _previousInteractable;

    private PlayerInput _playerInput;

    private void OnEnable()
    {
        _playerInput.Gameplay.Jump.performed += OnJumpPerformed;
        _playerInput.Gameplay.Interaction.performed += OnInteraction;
    }

    private void OnDisable()
    {
        _playerInput.Gameplay.Jump.performed -= OnJumpPerformed;
        _playerInput.Gameplay.Interaction.performed -= OnInteraction;
    }

    private void Awake()
    {
        _controllable = GetComponent<IControllable>();
        _viewable = GetComponentInChildren<IViewable>();

        if(_controllable == null)
        {
            throw new System.Exception($"There is no IControllable component on the object: {gameObject.name}");
        }

        _mainCamera = GetComponentInChildren<Camera>();

        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }

    private void Update()
    {
        ReadMovement();
        OnOutline();
    }

    private void LateUpdate()
    {
        OnMouseLook();
    }

    private void ReadMovement()
    {
        var inputMovementDirection = _playerInput.Gameplay.Movement.ReadValue<Vector3>();

        _controllable.Move(inputMovementDirection);
    }

    private void OnJumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _controllable.Jump();
    }

    private void OnMouseLook()
    {
        var inputMousePosition = _playerInput.Gameplay.View.ReadValue<Vector2>();

        _viewable.Look(inputMousePosition);
    }

    private void OnOutline()
    {
        var mousePosition = _playerInput.Gameplay.OutlineItem.ReadValue<Vector2>();

        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 2.5f))
        {
            var interactable = hit.collider.GetComponent<I_Interactable>();

            OutlineItem(interactable);
        }
        else
        {
            if(_previousInteractable != null)
            {
                _previousInteractable.OnHoverExit();
                _previousInteractable = null;
            }
        }
    }
    
    private void OutlineItem(I_Interactable interactable)
    {
        if (interactable != null)
        {
            if(interactable != _previousInteractable)
            {
                interactable.OnHoverEnter();
                _previousInteractable = interactable;
            }
        }
        else if (_previousInteractable != null)
        {
            _previousInteractable.OnHoverExit();
            _previousInteractable = null;
        }
    }

    private void OnInteraction(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(_previousInteractable != null)
        {
            _previousInteractable.Interact();
        }
    }
}
