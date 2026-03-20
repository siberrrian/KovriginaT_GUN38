using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading.Tasks;

public class Collect : AIState
{
    public void Enter(AIAgent agent)
    {
        agent.Animator.SetBool("IsWalk", true);
    }

    public void Exit(AIAgent agent)
    {
        agent.Animator.SetBool("IsWalk", false);
        agent.Animator.SetBool("IsCollect", false);
    }

    public AIStateID GetID()
    {
        return AIStateID.Collect;
    }

    public void Update(AIAgent agent)
    {
        if (agent.nearest != null)
        {

            agent.NavMeshAgent.destination = agent.nearest.transform.position;

            float distance = Vector3.Distance(agent.transform.position, agent.nearest.transform.position);

            if (distance < 3f)
            {
                agent.Animator.SetBool("IsWalk", false);
                agent.Animator.SetBool("IsCollect", true);
                AnimCollect(agent);
            }
        }

    }

    public async void AnimCollect(AIAgent agent)
    {
        await Task.Delay(500);
        Object.Destroy(agent.nearest);
        agent.nearest = null;
        agent.stateMachine.ChangeState(AIStateID.Idle);
    }
}
