using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Image _fillHealthBar;
    
    private float _maxHealthPoints;
    private float _currentHealthPoints;

    private void OnEnable() => Player.OnDamaged += OnHealthPointsChanged;
    
    private void OnDisable() => Player.OnDamaged -= OnHealthPointsChanged;

    public void Initialize()
    {
        _fillHealthBar = GetComponent<Image>();

        _maxHealthPoints = 100f;
        _currentHealthPoints = _maxHealthPoints;
    }

    private void Start()
    {           
        _fillHealthBar.fillAmount = _currentHealthPoints / _maxHealthPoints;
    }

    private void OnHealthPointsChanged(int damage)
    {
        _currentHealthPoints -= damage;
        
        _fillHealthBar.fillAmount = _currentHealthPoints / _maxHealthPoints;
    }
}
