using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : EnemyBase 
    { 
        public GunBaseEnemy gunBaseEnemy;

        protected override void Init()
        {
            base.Init();

            gunBaseEnemy.StartShoot();
        }
    }
}
