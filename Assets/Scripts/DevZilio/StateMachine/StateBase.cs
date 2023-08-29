using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevZilio.StateMachine
{
public class StateBase : MonoBehaviour
{
    public virtual void OnStateEnter(object o = null)
    {
        Debug.Log("OnStateEnter");
    }
    public virtual void OnStateStay()
    {
        Debug.Log("OnStateStay");
    }
    public virtual void OnStateExit()
    {
        Debug.Log("OnStateExit");
    }
}
}