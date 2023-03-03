using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAttackState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.AttackState;
    }
    public void Enter(AiAgent agent)
    {
        agent.config.timer = 0;
        agent.navMeshAgent.isStopped = false;
        agent.animator.enabled = true;
    }
    public void Update(AiAgent agent)
    {
        float distance = Vector3.Distance(agent.playerTransform.position, agent.navMeshAgent.transform.position);
        if (agent.vidaPlayer1 > 1 && distance < agent.config.attackRange && Time.time > agent.nextAttack && !agent.isHitting && agent.config.timer < 1f && agent.vida.currentHealth > 1)
        {
            agent.navMeshAgent.transform.LookAt(agent.playerTransform);
            agent.animator.SetBool("isAttacking", true);
            agent.isHitting = true;
            agent.StartCoroutine("CanmoveTrue");
            agent.nextAttack = Time.time + agent.attackRate;
        }
        else if (!agent.isHitting)
        {
            agent.animator.SetBool("isAttacking", false);
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