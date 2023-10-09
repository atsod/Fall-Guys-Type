using UnityEngine;

public class MouseLook : MonoBehaviour, IViewable
{
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _smoothTime;
    [SerializeField] private Transform _horizontalRotationHelperTransform;
    [SerializeField] private Transform _playerTransform;

    private Transform _cameraTransform;

    private float _verticalOld;
    private float _verticalAngularVelocity;
    private float _horizontalAngularVelocity;

    private float _verticalRotation;

    private void Awake()
    {
        _cameraTransform = GetComponent<Transform>();
        _verticalRotation = 0f;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        _horizontalRotationHelperTransform.localRotation = _cameraTransform.localRotation;
    }

    public void Look(Vector2 mouseDelta)
    {
        float mouseX = mouseDelta.x * _mouseSensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * _mouseSensitivity * Time.deltaTime;

        _verticalOld = _verticalRotation;
        _verticalRotation -= mouseY;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -90f, 90f);

        RotateHorizontal(mouseX);
        RotateVertical(_verticalRotation);
    }

    private void RotateHorizontal(float mouseX)
    {
        _horizontalRotationHelperTransform.Rotate(Vector3.up * mouseX, Space.Self);
        _playerTransform.localRotation
            = Quaternion.Euler(
                0f,
                Mathf.SmoothDampAngle(_playerTransform.localEulerAngles.y,
                                      _horizontalRotationHelperTransform.localEulerAngles.y,
                                      ref _horizontalAngularVelocity,
                                      _smoothTime),
                0f);
    }

    private void RotateVertical(float verticalRotation)
    {
        verticalRotation = Mathf.SmoothDampAngle(_verticalOld, verticalRotation, ref _verticalAngularVelocity, _smoothTime);
        _cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
