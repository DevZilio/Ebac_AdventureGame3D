using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBaseObjects : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    public bool destroyOnKill = false;
    [SerializeField]private float _currentLife;

    public Action<HealthBaseObjects> OnDamage;
    public Action<HealthBaseObjects> OnKill;

    public float damage = 2;

    public void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }

    protected void ResetLife()
    {
        _currentLife = startLife;
    }

    protected virtual void Kill()
    {
        if (destroyOnKill) Destroy(gameObject);

        OnKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(damage);
    }
   

    public void Damage(float damage)
    {
        _currentLife -= damage;
        if (_currentLife <= 0)
        {
            Kill();
        }
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir){
        _currentLife -= damage;
        if (_currentLife <= 0)
        {
            Kill();
        }
        OnDamage?.Invoke(this);
    }

}