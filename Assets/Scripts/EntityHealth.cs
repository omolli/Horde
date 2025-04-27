using UnityEngine;
using System;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] float _maxHp;
    [SerializeField] float _currentHp;
    [SerializeField] float _hpRegen;

    public Action OnDeath;
    public Action<float, float> OnHpChange;
    void Awake()
    {
        _currentHp = _maxHp;
    }

    void Start()
    {
        InvokeRepeating(nameof(HandleHpRegen), 1f, 1f);
    }

    public void loseHp(float hpLost)
    {
        _currentHp -= hpLost;
        OnHpChange?.Invoke(Mathf.Clamp(_currentHp, 0, _maxHp), _maxHp);
        if (_currentHp <= 0)
        {
            Death();
        }
    }

    void HandleHpRegen()
    {
        if (_currentHp == _maxHp)
        {
            return;
        }
        _currentHp = Mathf.Clamp(_currentHp + _maxHp * _hpRegen, 0, _maxHp);
        OnHpChange?.Invoke(_currentHp, _maxHp);
    }

    public void Death()
    {
        OnDeath?.Invoke();
    }
}
