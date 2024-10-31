using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [ValidateInput("IsGreaterThanZero", "The value must be greater than 0.")]
    [SerializeField] private int _maxStaminaValue;

    public int _maxStamina
    {
        get { return _maxStaminaValue; }
        private set { _maxStaminaValue = value; }
    }
    
    private int _currentStaminaValue;
    public int _currentStamina
    {
        get => _currentStaminaValue;
        private set => _currentStaminaValue = Mathf.Clamp(value, 0, _maxStaminaValue);
    }

    [SerializeField] private Slider _staminaBar;

    private void Awake()
    {
        _currentStamina = _maxStaminaValue;
        _staminaBar.maxValue = _maxStaminaValue;
        _staminaBar.value = _currentStamina;
    }

    private void FixedUpdate()
    {
        _staminaBar.value = _currentStamina;
    }

    public void UseStamina(int value)
    {
        _currentStamina -= value;
    }

    public void GainStamina(int value)
    {
        _currentStamina += value;
    }
}