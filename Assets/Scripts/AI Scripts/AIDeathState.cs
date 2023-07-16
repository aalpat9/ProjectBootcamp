using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDeathState : AIState
{
    AiStateId AIState.GetId()
    {
        return AiStateId.Death;
    }

    void AIState.Enter(AIAgent agent)
    {
    }

    void AIState.Exit(AIAgent agent)
    {
    }

    void AIState.Update(AIAgent agent)
    {
    }
}
