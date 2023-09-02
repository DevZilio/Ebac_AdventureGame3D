using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot12 : GunShootLimit
{
    public int numProjectiles = 5; // Número de projéteis a serem disparados
    public float spreadAngle = 30f; // Ângulo de dispersão dos projéteis
    public float verticalSpreadAngle = 10f; // Ângulo de dispersão vertical

    public override void Shoot()
    {
        for (int i = 0; i < numProjectiles; i++)
        {
            var projectile = Instantiate(prefabProjectile, positionToShoot);

            // Calcula o ângulo de dispersão horizontal
            float horizontalAngle = Random.Range(-spreadAngle, spreadAngle);

            // Calcula o ângulo de dispersão vertical
            float verticalAngle = Random.Range(-verticalSpreadAngle, verticalSpreadAngle);

            // Aplica os ângulos ao projétil
            Vector3 eulerAngle = new Vector3(verticalAngle, horizontalAngle, 0f);
             projectile.transform.localPosition = Vector3.zero;
            projectile.transform.localEulerAngles = eulerAngle;

            // Define a velocidade do projétil
            projectile.speed = speed;

            // Remove o projétil do pai
            projectile.transform.parent = null;
        }
    }
}
