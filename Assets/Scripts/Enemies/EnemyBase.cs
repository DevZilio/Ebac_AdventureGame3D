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

        public bool lookAtPlayer = false;

        private Player _player;

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

        private void Start() {
            {
              _player = GameObject.FindObjectOfType<Player>();  
            }
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        public virtual void Init()
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
            if (collider != null) collider.enabled = false;
            StartCoroutine(DestroyCoroutine());
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamage(float f)
        {
            {
                if (flashColor != null) flashColor.Flash();
                if (particleSystem != null) particleSystem.Emit(15);

                transform.position -= transform.forward;

                _currentLife -= f;
                if (_currentLife <= 0)
                {
                    Kill();
                }
            }
        }


        public void Damage(float damage)
        {
            OnDamage (damage);
        }

        public void Damage(float damage, Vector3 dir)
        {
            OnDamage (damage);
            transform.DOMove(transform.position - dir, .1f);
        }

        IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(timeToDestroy);
            Destroy (gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            PlayerLife p = collision.transform.GetComponent<PlayerLife>();

            if (p != null)
            {
                p.Damage(1);
            }
        }

        public virtual void Update()
        {
            if(lookAtPlayer)
            {
                transform.LookAt(_player.transform.position);
            }

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
