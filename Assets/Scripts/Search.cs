using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search : AIState
{
    public void Enter(AIAgent agent)
    {

        GameObject[] items = GameObject.FindGameObjectsWithTag("item");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        if (agent != null)
        {
            Vector3 position = agent.transform.position;

            foreach (GameObject item in items)
            {
                Vector3 diff = item.transform.position - position;
                float curDistance = diff.sqrMagnitude;

                if (curDistance < distance)
                {
                    closest = item;
                    distance = curDistance;
                }
            }

            agent.nearest = closest;
            agent.stateMachine.ChangeState(AIStateID.Collect);
        }
    }

    public void Exit(AIAgent agent)
    {

    }

    public AIStateID GetID()
    {
        return AIStateID.Search;
    }

    public void Update(AIAgent agent)
    {

        

    }
}
