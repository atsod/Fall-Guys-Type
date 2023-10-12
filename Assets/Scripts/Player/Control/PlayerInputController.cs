using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(IControllable))]
public class PlayerInputController : MonoBehaviour
{
    private IControllable _controllable;
    private IPausable _pausable;

    private PlayerInput _playerInput;

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Gameplay.Jump.performed += OnJumpPerformed;
        _playerInput.Gameplay.Pause.performed += OnPausePerformed;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Gameplay.Jump.performed -= OnJumpPerformed;
        _playerInput.Gameplay.Pause.performed -= OnPausePerformed;
    }

    public void Initialize(IControllable controllable, IPausable pausable)
    {
        _controllable = controllable;
        _pausable = pausable;

        _playerInput = new PlayerInput();
    }

    private void Update()
    {
        ReadMovement();
    }

    private void ReadMovement()
    {
        var inputMovementDirection = _playerInput.Gameplay.Movement.ReadValue<Vector3>();

        _controllable.Move(inputMovementDirection);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        _controllable.Jump();
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        _pausable.OpenPauseMenu();
    }
}
