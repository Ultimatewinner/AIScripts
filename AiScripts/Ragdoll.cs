using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public Rigidbody[] rigidBodies;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();

        DeactivateRagdoll();
    }

    public void DeactivateRagdoll()
    {
        foreach(var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = true;
        }
        animator.enabled = true;
    }
    public void ActivateRagdoll()
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = false;
        }
        animator.enabled = false;
    }

    public void ApplyForce (Vector3 force)
    {
        foreach(var rigidBody in rigidBodies)
        {
            rigidBody.AddForce(force, ForceMode.VelocityChange);
        }     
    }
    public void Grabbed()
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.constraints = RigidbodyConstraints.FreezeRotationX;
        }
    }
}
