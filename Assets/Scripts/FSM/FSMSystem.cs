using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FSMSystem : MonoBehaviour
{
    public FSMState currentState;

    public void GotoState(FSMState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
            currentState = newState;
            currentState.OnEnter();
        }
        else
        {
            currentState = newState;
            currentState.OnEnter();
        }
    }

    private void Update()
    {
        if (currentState != null)
            currentState.OnUpdate();
        OnSystemUpdate();
    }

    public virtual void OnSystemUpdate()
    {

    }
}
