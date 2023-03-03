using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
    public Slider uIHealthBar;
    public Ragdoll ragdoll;
    public CapsuleCollider colliderEmpieza;
    public CapsuleCollider colliderTermina;
    public Rigidbody rigidBodyArmas;
    public Transform playerTransform;
    public Transform grabTransform;
    public Animator animator;
    public float nextAttack = 1f;
    public float attackRate = 2f;
    public AiStateId currentState;
    [SerializeField] public ParticleSystem myMaterial;
    public PlayerMovimiento playerMovimiento;
    public float vidaPlayer1;
    public bool canmove = true;
    public bool isHitting = false;
    public List<Transform> wayPoints = new List<Transform>();
    public int waypointIndex = 0;
    public Health vida;
    public GameObject apagarAlAgarrar;
    public GameObject prenderAlAgarrar;
    public bool favelado;
    // Start is called before the first frame update
    void Start()
    {
        rigidBodyArmas = GetComponent<Rigidbody>();
        ragdoll = GetComponent<Ragdoll>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        myMaterial = GetComponent<ParticleSystem>();
        colliderEmpieza = GetComponent<CapsuleCollider>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        grabTransform = GameObject.FindGameObjectWithTag("GrabPos").transform;
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiPatrol());
        stateMachine.RegisterState(new AiAttackState());
        stateMachine.RegisterState(new AiGrabbedState());
        stateMachine.ChangeState(initialState);
        Physics.IgnoreLayerCollision(11,11, true);
        playerMovimiento = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovimiento>();
        StartCoroutine("StartCoroutine");
        canmove = true;
        vidaPlayer1 = playerMovimiento.vida;
        isHitting = false;
        vida = GetComponent<Health>();
        apagarAlAgarrar.SetActive(true);
        prenderAlAgarrar.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        currentState = stateMachine.currentState;
        vidaPlayer1 = playerMovimiento.vida;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, config.chaseRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, config.attackRange);
    }
    IEnumerator StartCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            animator.SetInteger("AttackIndex", Random.Range(0, 4));
        }
    }
    IEnumerator CanmoveTrue()
    {
        yield return new WaitForSeconds(1f);
        isHitting = false;
    }
}
