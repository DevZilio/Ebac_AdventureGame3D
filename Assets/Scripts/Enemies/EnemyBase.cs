using System.Collections;
using System.Collections.Generic;
using Animation;
using DG.Tweening;
using UnityEngine;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public float startLife = 10f;

        public float timeToDestroy = 2f;
        public Collider collider;
        public FlashColor flashColor;
        public ParticleSystem particleSystem;

        [SerializeField]
        private AnimationBase _animationBase;

        [SerializeField]
        private float _currentLife;

        [Header("Animation")]
        public float startAnimationDuration = .2f;

        public Ease startAnimationEase = Ease.OutBack;

        public bool startWithBornAnimation = true;

        private void Awake()
        {
            Init();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            {
                ResetLife();
                if (startWithBornAnimation) BornAnimation();
            }
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            if(collider != null) collider.enabled = false;
            StartCoroutine(DestroyCoroutine());
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamage(float f)
        {
            {
                if(flashColor != null) flashColor.Flash();
                if(particleSystem != null) particleSystem.Emit(15);

                _currentLife -= f;
                if (_currentLife <= 0)
                {
                    Kill();
                }
            }
        }

        //Debug
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                OnDamage(5f);
                Debug.Log("damage");
            }
        }

        public void Damage(float damage)
        {
            OnDamage(damage);
        }

        IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(timeToDestroy);
            Destroy (gameObject);
        }


#region ANIMATION

        private void BornAnimation()
        {
            transform
                .DOScale(0, startAnimationDuration)
                .SetEase(startAnimationEase)
                .From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger (animationType);
        }
#endregion
    }
}
