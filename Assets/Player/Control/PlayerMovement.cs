using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IControllable
{
    private float _speed;
    private float _jumpHeight;
    private float _gravity;

    private Vector3 _velocity;

    private Transform _playerTransform;
    private CharacterController _controller;

    private void Awake()
    {
        _speed = 5f;
        _jumpHeight = 1f;
        _gravity = -9.81f * 1.5f;

        _playerTransform = GetComponent<Transform>();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_controller.isTrigger) Debug.Log("Trigger");
        FallDown();
    }

    public void Move(Vector3 movementDirection)
    {
        Vector3 direction = _playerTransform.right * movementDirection.x
            + _playerTransform.forward * movementDirection.z;
        
        _controller.Move(_speed * Time.deltaTime * direction.normalized);
    }

    private void FallDown()
    {
        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if(_controller.isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
    }
}
