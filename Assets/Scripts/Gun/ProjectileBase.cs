using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{

    public float timeToDestroy = 2f;
    public float speed = 50f;

  
    public int damageAmount = 1;


    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed); //Cria gameobject   
    }

   private void OnTriggerEnter(Collider collider) {
       

        if (collider.CompareTag("Enemy"))
        {
            var damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                Vector3 dir = collider.transform.position - transform.position;
                    dir = -dir.normalized;
                    dir.y = 0;

                    damageable.Damage(damageAmount, dir);

            }

            Destroy(gameObject);
    }
   }

}
