using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    [SerializeField] private ParticleSystem myMaterial;
    AiAgent agent;
    public PlayerMovimiento player;
    public Slider healthBar;
    private LootableObject lb;
    public GameObject proximoEnemigo;
    public GameObject proximoCanvas;
    public bool esJefe;
    public bool sePuedeCurar;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovimiento>();
        lb = GetComponent<LootableObject>();
        agent = GetComponent<AiAgent>();
        currentHealth = maxHealth;

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach(var rigidBody in rigidBodies)
        {
            HitBox hitBox = rigidBody.gameObject.AddComponent<HitBox>();
            hitBox.health = this;
        }
    }
    private void Update()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        healthBar.value = currentHealth;
        if(currentHealth <= 0f)
        {
            player.enemigoAgarrado = false;
        }
        if(esJefe && sePuedeCurar && currentHealth < 150)
        {
            currentHealth = 250;
            sePuedeCurar = false;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0f)
        {
            Menu.dineroValor += 50;
            Die();
            lb.RealizarLoot();
            agent.stateMachine.ChangeState(AiStateId.Death);
            proximoEnemigo.SetActive(true);
            proximoCanvas.SetActive(true);
            gameObject.GetComponent<Health>().enabled = false;
            player.enemigoAgarrado = false;
        }
        else
        {
            StartCoroutine("FlashRed");
            agent.animator.SetTrigger("Attacked");
        }
    }

    private void Die()
    {
        AiDeathState deathState = agent.stateMachine.GetState(AiStateId.Death) as AiDeathState;
        agent.stateMachine.ChangeState(AiStateId.Death);
    }
    IEnumerator FlashRed()
    {
        myMaterial.Play();
        AudioManager.instance.PlayRandom();
        yield return new WaitForSeconds(0.6f);
        agent.animator.ResetTrigger("Attacked");
    }
    public void Grabbed()
    {
        AiGrabbedState grabbedState = agent.stateMachine.GetState(AiStateId.GrabbedState) as AiGrabbedState;
        agent.stateMachine.ChangeState(AiStateId.GrabbedState);
        agent.animator.SetTrigger("Grab");
        agent.animator.SetBool("isAttacking", false);
    }
}
