using System;
using System.Collections;
using System.Collections.Generic;
using DevZilio.StateMachine;
using UnityEngine;
using DG.Tweening;

namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }

    public class BossBase : MonoBehaviour
    {
        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;
        

       
        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBetweenAttack = 0.5f;

        public BossGunBase bossGunBase;
        public bool lookAtPlayer = false;
        
        // public Collider areaTrigger;

        public float speed = 5f;
        public List<Transform> waypoints;

        public HealthBase healthBase;

        private StateMachine<BossAction> stateMachine;
        private Player _player;

        private void Awake()
        {
            Init();
            
            healthBase.OnKill += OnBossKill;
        }
         

        private void Init()
        {
            _player = GameObject.FindObjectOfType<Player>();

            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());
        }


public virtual void Update()
        {
            if(lookAtPlayer)
            {
                transform.LookAt(_player.transform.position);
            }

        }
        private void OnBossKill(HealthBase h)
        {
            SwitchState(BossAction.DEATH);
        }

#region INIT



#endregion

#region WALK
        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
{
    while(Vector3.Distance(transform.position, t.position) > 1f)
    {
        transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
        yield return new WaitForEndOfFrame();
    }

    onArrive?.Invoke();
}
#endregion

#region ATTACK

public void StartAttack(Action endCallback = null)
{
    StartCoroutine(AttackCoroutine(endCallback));
}

IEnumerator AttackCoroutine(Action endCallback)
{
    int attacks = 0;
    while (attacks < attackAmount)
    {
        attacks++;
        bossGunBase.StartShoot();
        yield return new WaitForSeconds(timeBetweenAttack);
    }
    endCallback?.Invoke();
}
#endregion



#region ANIMATION
public void StartInitAnimation()
{
    transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
}
#endregion


#region  DEBUG
        [NaughtyAttributes.Button]
        public void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }//
        [NaughtyAttributes.Button]
        public void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }//
        [NaughtyAttributes.Button]
        public void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
        }//


#endregion



#region STATE MACHINE
        public void SwitchState(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }
#endregion
    }
}
