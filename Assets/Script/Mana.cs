using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class Mana : MonoBehaviour
{
    [ValidateInput("IsGreaterThanZero", "The value must be greater than 0.")]
    [SerializeField] private int _maxManaValue;
    
    public int _maxMana
    {
        get { return _maxManaValue; }
        private set { _maxManaValue = value; }
    }
    
    private int _currentManaValue;
    public int _currentMana
    {
        get => _currentManaValue;
        private set => _currentManaValue = Mathf.Clamp(value, 0, _maxManaValue);
    }

    [SerializeField] private Slider _manaBar;
    
    private void Awake()
    {
        _currentMana = _maxManaValue;
        _manaBar.maxValue = _maxManaValue;
        _manaBar.value = _currentMana;
    }

    private void FixedUpdate()
    {
        _manaBar.value = _currentMana;
    }

    public void UseMana(int value)
    {
        _currentMana -= value;
    }

    public void GainMana(int value)
    {
        _currentMana += value;
    }
}
