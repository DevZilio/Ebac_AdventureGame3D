using System.Collections;
using System.Collections.Generic;
using DevZilio.StateMachine;
using UnityEngine;

namespace Boss
{
    public class BossStateBase : StateBase
    {
        protected BossBase boss;

        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss = (BossBase) objs[0];
        }
    }

    public class BossStateInit : BossStateBase
    {

        public override void OnStateEnter(params object[] objs)
        {
            
            base.OnStateEnter(objs);
            boss.StartInitAnimation();
        }
    }
    public class BossStateWalk : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.GoToRandomPoint(OnArrive);
        }

        public void OnArrive()
        {
            boss.SwitchState(BossAction.ATTACK);
            Debug.Log("Attack");
        }

        public override void OnStateExit()
        {
            Debug.Log("Exit Walk");
            base.OnStateExit();
            boss.StopAllCoroutines();
        }
    }
    public class BossStateAttack : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.StartAttack(EndAttacks);
        }

        public void EndAttacks()
        {
            boss.SwitchState(BossAction.WALK);
        }

         public override void OnStateExit()
        {
            Debug.Log("Exit Attack");
            base.OnStateExit();
            boss.StopAllCoroutines();
        }
    }
    public class BossStateDeath : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            
            base.OnStateEnter(objs);
            
            
        }

        
    }
}
