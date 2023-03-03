using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiChasePlayerState : AiState
{

    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }

    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.speed = 5;
        agent.navMeshAgent.isStopped = false;
        agent.animator.enabled = true;
    }
    public void Update(AiAgent agent)
    {      
        float distance = Vector3.Distance(agent.playerTransform.position, agent.navMeshAgent.transform.position);
        if (distance < agent.config.chaseRange && agent.vidaPlayer1 > 1 && !agent.isHitting)
        {
            agent.navMeshAgent.SetDestination(agent.playerTransform.position);
        }
        if (distance < agent.config.maxDistance && agent.vidaPlayer1 > 1)
        {
               agent.stateMachine.ChangeState(AiStateId.AttackState);
        }
        else if (distance > agent.config.chaseRange && agent.vidaPlayer1 > 1)
        {
              agent.stateMachine.ChangeState(AiStateId.Patrol);
         }
        else if (agent.vida.currentHealth <= 0)
        {
            agent.stateMachine.ChangeState(AiStateId.Death);
        }


    }
    public void Exit(AiAgent agent)
    {
        agent.navMeshAgent.SetDestination(agent.navMeshAgent.transform.position);
    }

    public WeaponPickup FindClosestWeapon(AiAgent agent)
    {
        WeaponPickup[] weapons = Object.FindObjectsOfType<WeaponPickup>();
        WeaponPickup closestWeapon = null;
        float closestDistance = float.MaxValue;
        foreach (var weapon in weapons)
        {
            float distanceToWeapon = Vector3.Distance(agent.transform.position, weapon.transform.position);
            if (distanceToWeapon < closestDistance)
            {
                closestDistance = distanceToWeapon;
                closestWeapon = weapon;
            }
        }
        return closestWeapon;
    }

}
