using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiStateId {
    ChasePlayer,
    Patrol,
    Death
}

public interface AIState
{
    AiStateId GetId();
    void Enter(AIAgent agent);
    void Update(AIAgent agent);
    void Exit(AIAgent agent);
}
