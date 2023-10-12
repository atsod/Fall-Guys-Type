using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentTimeText;
    [SerializeField] private TextMeshProUGUI _finishTimeText;

    private void OnEnable()
    {
        Timer.OnTimeUpdated += UpdateTime;
        Timer.OnTimeFinished += SetFinishTime;
    }

    private void OnDisable()
    {
        Timer.OnTimeUpdated -= UpdateTime;
        Timer.OnTimeFinished -= SetFinishTime;
    }

    private void UpdateTime(float time)
    {
        _currentTimeText.text = string.Format("Time: {0:0.000}", time);
    }

    private void SetFinishTime(float time)
    {
        _finishTimeText.text = string.Format("Time: {0:0.000}", time);
    }
}
