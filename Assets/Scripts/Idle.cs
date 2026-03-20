using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class Idle : AIState
{
    public void Enter(AIAgent agent)
    {
        agent.Animator.SetBool("IsIdle", true);
        DelayAndSearch(agent);
    }

    public void Exit(AIAgent agent)
    {
        agent.Animator.SetBool("isIdle", false);
    }

    public AIStateID GetID()
    {
        return AIStateID.Idle;
    }

    public async void DelayAndSearch(AIAgent agent)
    {
        await Task.Delay(5000);
        agent.stateMachine.ChangeState(AIStateID.Search);
    }

    public void Update(AIAgent agent)
    {

    }
    

    
}
