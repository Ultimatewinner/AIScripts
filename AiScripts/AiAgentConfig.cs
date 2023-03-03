using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float maxTime = 1;
    public float maxDistance = 1;
    public float dieForce = 5;
    public float maxSightDistance = 10;
    public float timer;
    public float attackRange = 2.5f;
    public float chaseRange = 15f;
    public float weaponFinder = 10;
    public float grabDistance = 1.1f;
}
