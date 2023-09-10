using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public FlashColor flashColor;

    public ParticleSystem particleSystem;

    public float startLife = 10f;

    public bool destroyOnKill = false;

    [SerializeField]
    private float _currentLife;

    // public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

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
        if (destroyOnKill) Destroy(gameObject, 3f);

        OnKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5);
    }

#region DAMAGE
    public void OnDamage(float damage)
    {
        if (flashColor != null) flashColor.Flash();
        if (particleSystem != null) particleSystem.Emit(15);

        // transform.position -= transform.forward;
        _currentLife -= damage;
        if (_currentLife <= 0)
        {
            Kill();
        }
    } 

    public void Damage(float damage)
    {
        OnDamage (damage);
    }

    public void Damage(float damage, Vector3 dir)
    {
        OnDamage (damage);
        transform.DOMove(transform.position - dir, .1f);
    }
#endregion
}
