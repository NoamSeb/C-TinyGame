using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    [ValidateInput("IsGreaterThanZero", "The value must be greater than 0.")] [SerializeField]
    int _maxHealth;


    [ProgressBar("Health", nameof(_maxHealth), EColor.Red)] [SerializeField]
    int _currentHealth;

    [SerializeField] Slider _healthBar = null;

    [SerializeField] UnityEvent _onDie;
    [SerializeField] Animator _animator = null;
    [SerializeField] GameObject _explosion;
    [SerializeField] GameObject _coins;


    private void Reset()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_healthBar != null) _healthBar.value = _currentHealth;
    }

    void Awake()
    {
        _currentHealth = _maxHealth;
        if (_healthBar != null)
        {
            _healthBar.maxValue = _maxHealth;
            _healthBar.value = _currentHealth;
        }
    }

    bool IsGreaterThanZero(int value)
    {
        return value > 0;
    }

    public bool TakeDamage(int damage)
    {
        _currentHealth -= damage;

        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        if (_animator != null)
        {
            _animator?.SetTrigger("Hit");
        }

        if (_currentHealth <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    private void Die()
    {
        Coroutine c1 = StartCoroutine(DieRoutine());

        IEnumerator DieRoutine()
        {
            _onDie.Invoke();
            if (_animator != null)
            {
                _animator?.SetTrigger("Die");
            }
            else
            {
                Instantiate(_explosion, transform.position, Quaternion.identity);
                Instantiate(_coins, transform.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(2f);
            Destroy(this.gameObject);
        }
    }

    public void Heal(int heal)
    {
        _currentHealth += heal;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }
}