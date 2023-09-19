using System.Collections;
using System.Collections.Generic;
using Cloth;
using UnityEngine;


namespace Cloth
{
    public class ClothItemStrong : ClothItemBase
    {
        public float damageMultiply = .5f;

        public override void Collect()
        {
            base.Collect();
            PlayerLife.Instance.ChangeDamageMultiply(damageMultiply, duration);
        }
    }
}