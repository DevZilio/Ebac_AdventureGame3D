using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class AreaTriggerEnemy : MonoBehaviour
    {
        public EnemyShoot enemy;
        private bool playerEntered = false;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Player") && !playerEntered)
            {
                playerEntered = true; // Evita que a ação seja executada novamente
                if (enemy != null)
                {
                    enemy.gunBaseEnemy.StartShoot();
                }

                Debug.Log("Trigger collided");
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.CompareTag("Player") && playerEntered)
            {
                playerEntered = false;
                if (enemy != null)
                {
                    enemy.gunBaseEnemy.StopShoot();
                }

                Debug.Log("Trigger exited");
            }
        }
    }
}
