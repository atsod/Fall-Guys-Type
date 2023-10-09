using System.Collections;
using UnityEngine;

public class WindTrap : Trap
{
    private CharacterController _playerController;

    private Vector3 _windDirection;
    private float _windSpeed;
    private float _windChangeDelay;

    private bool _isWindTrapActivated;

    private void Awake()
    {
        _windDirection = Vector3.zero;
        _windSpeed = 3f;
        _windChangeDelay = 2f;
    }

    private void Update()
    {
        if(_isWindTrapActivated)
        {
            BlowWind();
        }
    }

    private void BlowWind()
    {
        _playerController.Move(_windSpeed * Time.deltaTime * _windDirection.normalized);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            _playerController = other.gameObject.GetComponent<CharacterController>();

            _isWindTrapActivated = true;

            StartTrapCoroutine();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            _isWindTrapActivated = false;

            StopTrapCoroutine();
        }
    }

    protected override IEnumerator ActivateTrap()
    {
        while(true)
        {
            ChangeWindDirection();

            yield return new WaitForSeconds(_windChangeDelay);
        }
    }

    private void ChangeWindDirection()
    {
        float xRandom = (float) Random.Range(-100, 100) / 100;
        float zRandom = (float) Random.Range(-100, 100) / 100;

        _windDirection = new(xRandom, 0f, zRandom);
    }
}
