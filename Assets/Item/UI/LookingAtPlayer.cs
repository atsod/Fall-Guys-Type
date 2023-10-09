using UnityEngine;

public class LookingAtPlayer : MonoBehaviour
{
    private Transform _transform;
    private Transform _playerTransform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _playerTransform = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<Transform>();
    }
    
    void Update()
    {
        Vector3 lookDirection = _transform.position - _playerTransform.position;
        _transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
    }
}
