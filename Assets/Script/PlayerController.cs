using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Attribute : ")] [SerializeField]
    float _speed;

    [SerializeField] private GameObject _IncantationPrefab;
    [SerializeField] private GameObject _AttackZone;
    int _CoinsCount;
    [SerializeField] TextMeshProUGUI _CoinsTextUI;

    [Space] [SerializeField] InputActionReference _moveInput;
    [SerializeField] InputActionReference _attackInput;
    [SerializeField] InputActionReference _magicalAttackInput;
    
    public event Action OnCollectCoins;
    private Vector2 _joystick;
    Vector3 _movement;

    [Space] [Header("Outside Attribute : ")] [SerializeField]
    CharacterController _characterController;

    [SerializeField] Stamina _stamina;
    [SerializeField] Mana _mana;
    [SerializeField] Camera _camera;
    [SerializeField] Animator _animator;
    
    private bool _IsMoving;

    Coroutine _UseStaminaCoroutine;
    Coroutine _GainStaminaCoroutine;

    [SerializeField] private Transform _IncantationTransform;

    void Reset()
    {
        _characterController = GetComponent<CharacterController>();
        _stamina = GetComponent<Stamina>();
        _animator = GetComponent<Animator>();
        _mana = GetComponent<Mana>();
    }
    
    void FixedUpdate()
    {
        var forward = _camera.transform.forward;
        forward.y = 0;
        var right = _camera.transform.right;
        right.y = 0;

        _movement = _joystick.y * forward + _joystick.x * right;

        if (_IsMoving)
        {
            _characterController.Move(_movement * (_speed * Time.deltaTime));
            transform.LookAt(transform.position + _movement);
            _animator.SetBool("IsWalking", true);
            UseStamina();
        }
        else
        {
            _animator.SetBool("IsWalking", false);
            if (_UseStaminaCoroutine != null)
            {
                StopCoroutine(_UseStaminaCoroutine);
            }

            GainStamina();
        }
        
    }

    void Start()
    {
        OnCollectCoins += UpdateCoinsText;
        _CoinsTextUI.text = "0";

        #region Move Event

        _moveInput.action.started += StartMove;
        _moveInput.action.performed += UpdateMove;
        _moveInput.action.canceled += StopMove;

        #endregion

        #region Attack Event

        _attackInput.action.performed += Attack;

        #endregion

        #region Magical Attack Event

        _magicalAttackInput.action.performed += MagicalAttack;

        #endregion
    }

    void OnDestroy()
    {
        #region Move Event

        _moveInput.action.started -= StartMove;
        _moveInput.action.performed -= UpdateMove;
        _moveInput.action.canceled -= StopMove;

        #endregion

        #region Attack Event

        _attackInput.action.performed -= Attack;

        #endregion

        #region Magical Attack Event

        _magicalAttackInput.action.performed -= MagicalAttack;

        #endregion
    }

    /// <summary>
    /// Read Player Inputs
    /// </summary>
    /// <param name="obj"></param>
    #region Input reading

    #region Movement

    void StartMove(InputAction.CallbackContext obj)
    {
        _IsMoving = true;
    }

    void UpdateMove(InputAction.CallbackContext obj)
    {
        _joystick = obj.ReadValue<Vector2>();
    }

    void StopMove(InputAction.CallbackContext obj)
    {
        _IsMoving = false;
    }

    #endregion

    #region Classic Attack

    private void Attack(InputAction.CallbackContext obj)
    {
        _animator.SetTrigger("Attack");
        _AttackZone.GetComponent<AttackManager>().Attack(10);
    }

    #endregion

    #region Magical Attack

    private void MagicalAttack(InputAction.CallbackContext obj)
    {
        _mana.UseMana(35);
        Instantiate(_IncantationPrefab, _IncantationTransform.position, Quaternion.identity);
        
        _AttackZone.GetComponent<AttackManager>().Attack(20);
    }

    #endregion

    #endregion

    void UseStamina()
    {
        _UseStaminaCoroutine = StartCoroutine(UseStamina());

        IEnumerator UseStamina()
        {
            while (true)
            {
                yield return new WaitForSeconds(1.0f);
                _stamina.UseStamina(1);
                if (_stamina._currentStamina >= 0)
                {
                    _IsMoving = false;
                    _animator.SetBool("IsWalking", false);
                    break;
                }
            }
        }
    }

    void GainStamina()
    {
        _GainStaminaCoroutine = StartCoroutine(GainStamina());

        IEnumerator GainStamina()
        {
            while (true)
            {
                yield return new WaitForSeconds(1.0f);
                _stamina.GainStamina(5);
            }
        }

        if (_stamina._currentStamina >= _stamina._maxStamina)
        {
            StopCoroutine(_GainStaminaCoroutine);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coins"))
        {
            _CoinsCount += 10;
            OnCollectCoins?.Invoke();
            Destroy(other.gameObject);
        }
    }
    
    private void UpdateCoinsText()
    {
        _CoinsTextUI.text = _CoinsCount.ToString();
    }
}