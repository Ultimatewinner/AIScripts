using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPatrol : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Patrol;
    }
    public void Update(AiAgent agent)
    {

        if (agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance)
        {
            var targetPosition = agent.wayPoints[agent.waypointIndex].position;
            agent.navMeshAgent.speed = 2;
            agent.navMeshAgent.SetDestination(targetPosition);
            if (Vector2.Distance(agent.transform.position, targetPosition) < 1.4f)
            {
                agent.waypointIndex++;
            }
            if (agent.waypointIndex == 4)
            {
                agent.waypointIndex = 0;
            }
            //agent.navMeshAgent.SetDestination(agent.wayPoints[Random.Range(0, agent.wayPoints.Count)].position);
        }

        agent.config.timer += Time.deltaTime;

        if (agent.config.timer > 30)
        {
            agent.stateMachine.ChangeState(AiStateId.Idle);
        }
        float distance = Vector3.Distance(agent.playerTransform.position, agent.transform.position);

        if (distance < agent.config.maxSightDistance && agent.vidaPlayer1 > 1)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
        else if (agent.vida.currentHealth <= 0)
        {
            agent.stateMachine.ChangeState(AiStateId.Death);
        }
    }
    public void Enter(AiAgent agent)
    {
        agent.config.timer = 0;
        agent.navMeshAgent.speed = 2;
        agent.navMeshAgent.isStopped = false;
        agent.animator.enabled = true;
    }
    public void Exit(AiAgent agent)
    {
        agent.navMeshAgent.SetDestination(agent.transform.position);
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
