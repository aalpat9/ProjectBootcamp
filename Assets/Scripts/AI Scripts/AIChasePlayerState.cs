using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChasePlayerState : AIState
{
    public Transform player;

    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }

    public void Enter(AIAgent agent)
    {
        if (player == null){
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        agent.navMeshAgent.speed = 5.0f;
    }

    public void Exit(AIAgent agent)
    {
    }

    public void Update(AIAgent agent)
    {
        agent.navMeshAgent.destination = player.position;
    }
}
