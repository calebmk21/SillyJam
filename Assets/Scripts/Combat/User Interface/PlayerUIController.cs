using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] Slider _healthBar;
    [SerializeField] TextMeshProUGUI _healthText;
    private float _maxHealth;
    private PlayerUnit _unit;

    private void OnEnable()
    {
        if (_unit != null)
            Initialize(_unit);
    }

    private void OnDisable()
    {
        if (_unit != null)
        {
            _unit.OnHealthChanged -= UpdateHealth;
        }
    }

    public void Initialize(PlayerUnit unit)
    {
        _unit = unit;
        _nameText.SetText(unit.Name);

        _maxHealth = unit.MaxHealth;
        _healthBar.maxValue = _maxHealth;
        UpdateHealth(unit.CurrentHealth);
        gameObject.SetActive(true);

        unit.OnHealthChanged += UpdateHealth;
    }

    private void UpdateHealth(int health)
    {
        _healthBar.value = Mathf.Clamp(health, 0, _maxHealth);
        _healthText.SetText(health + " / " + _maxHealth);
    }
    
}