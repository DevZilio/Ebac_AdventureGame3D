using System;
using System.Collections;
using System.Collections.Generic;
using DevZilio.Core.Singleton;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerLife : Singleton<PlayerLife>, IDamageable
{
    public List<FlashColor> flashColors;

    public List<Collider> colliders;

    public Animator animator;

    public GameObject bossCamera;

    // public ParticleSystem particleSystem;
    public float startLife = 10f;

    public float timeToDestroy = 1f;

    [SerializeField]
    private float _currentLife;

    private bool _alive = true;

    [Header("Life")]
    public UIFillUpdater uiLifeBarUpdater;
    public float damageMultiply = 1;

    [Header("Sound")]
    public SFXType sfxType;
    public AudioMixerGroup audioMixerGroup;

    private CharacterController _characterController;


    protected override void Awake()
    {
        base.Awake();
        _characterController = GetComponent<CharacterController>();
        Init();
    }

    bool firstFrame = true;
    private void Update()
    {
        if (firstFrame)
        {
            LoadRespawnPoint();
            firstFrame = false;
        }
    }


    private void LoadRespawnPoint()
    {
        Respawn();
    }


    public void Init()
    {
        ResetLife();
    }

    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
    }

    public void Play()
    {
        SFXPool.Instance.Play(sfxType);
    }

    protected virtual void Kill()
    {
        // if (destroyOnKill) Destroy(gameObject, timeToDestroy);
        if (_alive)
        {
            _alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);
            bossCamera.SetActive(false);

            Invoke(nameof(Revive), 3f);
        }
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5);
    }


    #region DAMAGE

    public void OnDamage(float damage)
    {
        if (flashColors != null) flashColors.ForEach(i => i.Flash());
        Play();
        EffectsManager.Instance.ChangeVignette();
        ShakeCamera.Instance.Shake();

        // if (particleSystem != null) particleSystem.Emit(15);
        // transform.position -= transform.forward;
        _currentLife -= damage * damageMultiply;
        if (_currentLife <= 0)
        {
            Kill();
        }
        UpdateUI();
    }

    public void Damage(float damage)
    {
        OnDamage(damage);
    }

    public void Damage(float damage, Vector3 dir)
    {
        OnDamage(damage);
        transform.DOMove(transform.position - dir, .1f);
    }
    #endregion



    #region LIFE
    private void UpdateUI()
    {
        if (uiLifeBarUpdater != null)
        {
            uiLifeBarUpdater.UpdateValue((float)_currentLife / startLife);
            Debug.Log("Update PlayerLife");
        }
    }

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if (CheckPointManager.Instance.HasCheckPoint())
        {
            _characterController.enabled = false;
            transform.position =
                CheckPointManager.Instance.GetPositionFromLastCheckPoint();
            Debug.Log("Respawned at checkpoint " + CheckPointManager.Instance.lastCheckPointKey);
            _characterController.enabled = true;
        }
    }

    [NaughtyAttributes.Button]
    public void Revive()
    {
        _alive = true;
        ResetLife();
        Respawn();
        animator.SetTrigger("Revive");
        colliders.ForEach(i => i.enabled = true);
    }


    #endregion

    public void ChangeDamageMultiply(float damage, float duration)
    {
        StartCoroutine(ChangeDamageMultiplyCoroutine(damageMultiply, duration));
    }

    IEnumerator ChangeDamageMultiplyCoroutine(float damageMultiply, float duration)
    {
        this.damageMultiply = damageMultiply;
        yield return new WaitForSeconds(duration);
        this.damageMultiply = 1;
    }

}
