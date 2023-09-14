using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Enemy;

public class DestructableBase : MonoBehaviour
{
    public HealthBaseObjects healthBase;
    public float duration = 1f;
    public int shakeForce = 2;

    public int dropCoinsAmount = 10;
    public GameObject coinPrefab;
    public Transform dropPosition;
    public Ease ease = Ease.OutBack;
    public float scaleDuration = 1f;
    public float timeBetweenCoin = .2f;

    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBaseObjects>();
    }

    private void Awake()
    {
        OnValidate();
        healthBase.OnDamage += OnDamage;
    }

    private void OnDamage(HealthBaseObjects h)
    {
        transform.DOShakeScale(duration, Vector3.up/2, shakeForce);
        DropGroupOfCoins();
    }

[NaughtyAttributes.Button]
    private void DropCoins()
    {
        var i = Instantiate(coinPrefab);
        i.transform.position = dropPosition.position;
        i.transform.DOScale(0, scaleDuration).SetEase(ease).From();
    }


[NaughtyAttributes.Button]
    private void DropGroupOfCoins()
    {
        StartCoroutine(DropGroupOfCoinsCoroutine());
    }

    IEnumerator DropGroupOfCoinsCoroutine()
    {
        for(int i = 0; i < dropCoinsAmount;i++)
        {
            DropCoins();
            yield return new WaitForSeconds(timeBetweenCoin);
        }
    }

}
