using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevZilio.Core.Singleton;

namespace Cloth
{
    public class ClothItemBase : Singleton<ClothItemBase>
    {
        public ClothType clothType;
        public string compareTag = "Player";

        public float duration = 1f;

        private void OnTriggerEnter(Collider collision) {
            if(collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        public virtual void Collect()
        {
            var setup = ClothManager.Instance.GetSetupByType(clothType);

            Player.Instance.ChangeTexture(setup, duration);

            HideObject();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }
}