using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class AreaTriggerBoss : MonoBehaviour
    {
        public BossBase boss;
        public MeshRenderer bossGraphic;

        public void Awake() {
            bossGraphic.enabled = false;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Player"))
            {
                if (boss != null) bossGraphic.enabled = true;
                if (boss != null) boss.SwitchState(BossAction.WALK);
                Debug.Log("Trigger boss collided");
            }
        }
    }
}
