using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiGrabbedState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.GrabbedState;
    }
    public void Enter(AiAgent agent)
    {
        agent.animator.SetBool("isAttacking", false);
        agent.config.timer = 0;
        agent.navMeshAgent.speed = 0;
        agent.navMeshAgent.isStopped = false;
        agent.animator.enabled = true;
        agent.animator.SetTrigger("Grab");
        agent.apagarAlAgarrar.SetActive(false);
        agent.prenderAlAgarrar.SetActive(true);
    }
    public void Update(AiAgent agent)
    {
        float distance = Vector3.Distance(agent.playerTransform.position, agent.navMeshAgent.transform.position);
        if (distance > agent.config.grabDistance)
        {
            agent.navMeshAgent.SetDestination(agent.grabTransform.position);
            agent.navMeshAgent.speed = 1;
            agent.transform.LookAt(agent.grabTransform.position);
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
