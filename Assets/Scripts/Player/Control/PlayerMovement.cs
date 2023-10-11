using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IControllable
{
    [SerializeField] private Transform _playerBodyTransform;
    [SerializeField] private Transform _orientationTransform;
    [SerializeField] private Transform _cameraTransform;

    private Transform _transform;
    private CharacterController _controller;

    private Vector3 _fallingVelocity;

    private float _speed;
    private float _jumpHeight;
    private float _gravity;
    private float _rotationSpeed;

    public void Initialize()
    {
        _transform = GetComponent<Transform>();
        _controller = GetComponent<CharacterController>();

        _fallingVelocity = Vector3.zero;

        _speed = 5f;
        _jumpHeight = 0.3f;
        _gravity = -9.81f * 0.6f;
        _rotationSpeed = 10f;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Move(Vector3 inputDirection)
    {
        CalculateVelocityWhenFallingDown();

        Vector3 viewDirection = DefineViewDirection();

        _orientationTransform.forward = viewDirection.normalized;

        Vector3 movementDirection =
            _orientationTransform.right * inputDirection.x
            + _orientationTransform.forward * inputDirection.z;

        if (movementDirection != Vector3.zero)
        {
            _playerBodyTransform.forward = Vector3.Slerp(
                _playerBodyTransform.forward,
                movementDirection.normalized,
                Time.deltaTime * _rotationSpeed);
        }

        _controller.Move(_speed * Time.deltaTime * (movementDirection + _fallingVelocity));
    }

    private Vector3 DefineViewDirection()
    {
        return _transform.position - new Vector3(
            _cameraTransform.position.x,
            _transform.position.y,
            _cameraTransform.position.z);
    }

    private void CalculateVelocityWhenFallingDown()
    {
        if (_controller.isGrounded && _fallingVelocity.y < 0)
        {
            _fallingVelocity.y = -0.5f;
        }

        _fallingVelocity.y += _gravity * Time.deltaTime;
    }

    public void Jump()
    {
        if(_controller.isGrounded)
        {
            _fallingVelocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
    }
}
