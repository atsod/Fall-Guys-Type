using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinishUI : MonoBehaviour
{
    public static event Action OnGameFinished;

    [SerializeField] private GameObject _loseUI;
    [SerializeField] private GameObject _winUI;

    [SerializeField] private GameObject _timerUI;
    [SerializeField] private GameObject _healthBarUI;
    [SerializeField] private GameObject _pauseAdviceUI;

    private void OnEnable()
    {
        Player.OnLose += OpenLoseUI;
        FinishZone.OnFinished += OpenWinUI;
    }

    private void OnDisable()
    {
        Player.OnLose -= OpenLoseUI;
        FinishZone.OnFinished -= OpenWinUI;
    }

    private void OpenLoseUI()
    {
        _loseUI.SetActive(true);
        RunSafeFinish();
    }

    private void OpenWinUI()
    {
        _winUI.SetActive(true);
        _healthBarUI.SetActive(false);
        RunSafeFinish();
    }

    private void RunSafeFinish()
    {
        Time.timeScale = 0f;
        ActivateCursor();
        OnGameFinished?.Invoke();

        _timerUI.SetActive(false);
        _pauseAdviceUI.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ActivateCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
