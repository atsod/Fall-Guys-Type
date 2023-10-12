using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour, IPausable
{
    [SerializeField] private GameObject _pauseMenu;

    private bool _isPauseMenuOpen;
    private bool _isGameFinished;

    private void OnEnable()
    {
        GameFinishUI.OnGameFinished += LockPauseMenu;
    }

    private void OnDisable()
    {
        GameFinishUI.OnGameFinished -= LockPauseMenu;
    }

    private void LockPauseMenu()
    {
        _isGameFinished = true;
    }

    public void OpenPauseMenu()
    {
        if(!_isGameFinished)
        {
            if (!_isPauseMenuOpen)
            {
                ActivateCursor();
                Time.timeScale = 0f;
                _pauseMenu.SetActive(true);
            }
            else
            {
                DeactivateCursor();
                ClosePauseMenu();
            }

            _isPauseMenuOpen = !_isPauseMenuOpen;
        }
    }

    private void ClosePauseMenu()
    {
        Time.timeScale = 1f;
        _pauseMenu.SetActive(false);
    }

    public void Resume()
    {
        DeactivateCursor();
        Time.timeScale = 1f;
        _pauseMenu.SetActive(false);

        _isPauseMenuOpen = !_isPauseMenuOpen;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void ActivateCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void DeactivateCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
