using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolState : AIState
{

    GameObject target;

    private int destPoint = 0;

    AiStateId AIState.GetId()
    {
        return AiStateId.Patrol;
    }

    void AIState.Enter(AIAgent agent)
    {
        agent.navMeshAgent.speed = 3.0f;
    }

    void AIState.Exit(AIAgent agent)
    {
    }

    void AIState.Update(AIAgent agent)
    {
        if(!target) {
            target = FindTarget(agent);

            if (target) {
                agent.stateMachine.ChangeState(agent.chaseState);
            }
        }
        if (!agent.navMeshAgent.hasPath) {
            GoToNextPoint(agent);
        }
    }

    GameObject FindTarget(AIAgent agent) {
        if(agent.sensor.Objects.Count > 0) {
            return agent.sensor.Objects[0];
        }
        return null;
    }

    void GoToNextPoint(AIAgent agent) {
        if (agent.points.Length == 0) {
            return;
        }

        agent.navMeshAgent.destination = agent.points[destPoint].position;

        destPoint = (destPoint + 1) % agent.points.Length;
    }
}
