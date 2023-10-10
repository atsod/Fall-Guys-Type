using UnityEngine;

[RequireComponent(typeof(IControllable))]
public class PlayerInputController : MonoBehaviour
{
    private IControllable _controllable;
    private IViewable _viewable;

    private PlayerInput _playerInput;

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Gameplay.Jump.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Gameplay.Jump.performed -= OnJumpPerformed;
    }

    private void Awake()
    {
        _controllable = GetComponent<IControllable>();
        _viewable = GetComponentInChildren<IViewable>();

        _playerInput = new PlayerInput();
    }

    private void Update()
    {
        ReadMovement();
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
}
