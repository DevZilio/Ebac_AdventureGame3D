using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;

    public float timeBetweenShoot = .2f;
    public float speed = 50f;

    private Coroutine _currentCoroutine;

     [Header("Sound")]
    public SFXType sfxType;


    //Coroutine
   protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public virtual void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.speed = speed;
        Play();

        
    }

    public void StartShoot()
    {
        StopShoot();
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShoot()
    {
        if (_currentCoroutine != null) 
        StopCoroutine(_currentCoroutine);
    }
   
    public void Play()
    {
        SFXPool.Instance.Play(sfxType);
    }
}
