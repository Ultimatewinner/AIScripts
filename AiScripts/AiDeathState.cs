using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDeathState : AiState
{
    public Vector3 direction;
    public AiStateId GetId()
    {
        return AiStateId.Death;
    }
    public void Enter(AiAgent agent)
    {
        agent.config.timer = 0f;
        if (agent.favelado)
        {
            agent.ragdoll.ActivateRagdoll();
            direction.y = 1;
            AudioManager.instance.PlayRandom();
            agent.navMeshAgent.enabled = false;
        }
        else
        {
            agent.navMeshAgent.baseOffset = 1;
            //agent.ragdoll.ApplyForce(direction * agent.config.dieForce);
            AudioManager.instance.PlayRandom();
            //agent.uIHealthBar.gameObject.SetActive(false);
            agent.colliderTermina.enabled = true;
            agent.colliderEmpieza.enabled = false;
            agent.rigidBodyArmas.isKinematic = true;
            agent.rigidBodyArmas.useGravity = false;
            //agent.weapons.DropWeapon();
            agent.navMeshAgent.enabled = false;
            agent.animator.SetTrigger("Muerte");
            agent.transform.position = new Vector3(agent.transform.position.x, -0.8f, agent.transform.position.z);
        }
        agent.apagarAlAgarrar.SetActive(true);
        agent.prenderAlAgarrar.SetActive(false);
    }
    public void Update(AiAgent agent)
    {
        if (agent.favelado)
        {
            agent.ragdoll.ActivateRagdoll();
            direction.y = 1;
            //AudioManager.instance.PlayRandom();
            agent.navMeshAgent.enabled = false;
        }
        else
        {
            //agent.ragdoll.ApplyForce(direction * agent.config.dieForce);
            //AudioManager.instance.PlayRandom();
            //agent.uIHealthBar.gameObject.SetActive(false);
            agent.colliderTermina.enabled = true;
            agent.colliderEmpieza.enabled = false;
            agent.rigidBodyArmas.isKinematic = true;
            agent.rigidBodyArmas.useGravity = false;
            //agent.weapons.DropWeapon();
            agent.navMeshAgent.enabled = false;
            agent.animator.SetTrigger("Muerte");
            //agent.transform.position = new Vector3(agent.transform.position.x, -0.031f, agent.transform.position.z);
        }
    }
    public void Exit(AiAgent agent)
    {
        
    }
}
