using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("PauseMenu")]
    [SerializeField] private PauseUI _pauseUI;

    [Header("PlayerInput")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerInputController _playerInputController;
    [Header("PlayerCharacter")]
    [SerializeField] private Player _player;
    [Header("PlayerUI")]
    [SerializeField] private HealthBarUI _healthBarUI;
    
    private Timer _timer;

    [Header("StartZone")]
    [SerializeField] private StartZone _startZone;
    [Header("FinishZone")]
    [SerializeField] private FinishZone _finishZone;

    private void Awake()
    {
        _playerMovement.Initialize();
        _playerInputController.Initialize(_playerMovement, _pauseUI);
        _player.Initialize();
        _healthBarUI.Initialize();

        _timer = new Timer(this);

        _startZone.Initialize(_timer);
        _finishZone.Initialize(_timer);
    }
}
