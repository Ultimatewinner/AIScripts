using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Idle;
    }
    public void Enter(AiAgent agent)
    {
        agent.config.timer = 0;
        agent.navMeshAgent.isStopped = false;
        agent.animator.enabled = true;
        agent.navMeshAgent.speed = 0;
    }
    public void Update(AiAgent agent)
    {
        agent.config.timer += Time.deltaTime;
        if(agent.config.timer > 15)
        {
            agent.stateMachine.ChangeState(AiStateId.Patrol);
        }

        float distance = Vector3.Distance(agent.playerTransform.position, agent.transform.position);
        if (distance < agent.config.maxSightDistance && agent.vidaPlayer1 > 0)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
        else if (agent.vida.currentHealth <= 0)
        {
            agent.stateMachine.ChangeState(AiStateId.Death);
        }
    }
    public void Exit(AiAgent agent)
    {
        
    }
}
