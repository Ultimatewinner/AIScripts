using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitBox : MonoBehaviour
{
    public Health health;
    public float damageReceived = 20f;
    public bool canReceiveDamage = false;

    public PlayerMovimiento player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovimiento>();
        canReceiveDamage = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Puños") && canReceiveDamage && this.gameObject.layer == LayerMask.NameToLayer("DamageEnemy"))
        {
            health.TakeDamage(30);
            canReceiveDamage = false;
            StartCoroutine("RestartAttack");       
        }
        if (other.CompareTag("PuñosFuertes") && canReceiveDamage && this.gameObject.layer == LayerMask.NameToLayer("DamageEnemy"))
        {
            health.TakeDamage(50);
            canReceiveDamage = false;
            StartCoroutine("RestartAttack");
        }
        if (other.CompareTag("Weapon") && canReceiveDamage && this.gameObject.layer == LayerMask.NameToLayer("DamageEnemy"))
        {
            health.TakeDamage(70);
            canReceiveDamage = false;
            StartCoroutine("RestartAttack");
        }
        if (other.CompareTag("Instakill") && canReceiveDamage && this.gameObject.layer == LayerMask.NameToLayer("DamageEnemy"))
        {
            health.TakeDamage(125);
            canReceiveDamage = false;
            StartCoroutine("RestartAttack");
        }
        if (other.CompareTag("Fierro") && canReceiveDamage && this.gameObject.layer == LayerMask.NameToLayer("DamageEnemy"))
        {
            health.TakeDamage(80);
            canReceiveDamage = false;
            StartCoroutine("RestartAttack");
        }
        if (other.CompareTag("Palo") && canReceiveDamage && this.gameObject.layer == LayerMask.NameToLayer("DamageEnemy"))
        {
            health.TakeDamage(60);
            canReceiveDamage = false;
            StartCoroutine("RestartAttack");
        }
        if (other.CompareTag("Grab") && this.gameObject.layer == LayerMask.NameToLayer("DamageEnemy"))
        {
            health.Grabbed();
            player.enemigo = this.gameObject;
            player.enemigoAgarrado = true;
        }
    }
    IEnumerator RestartAttack()
    {
        yield return new WaitForSeconds(0.3f);
        canReceiveDamage = true;
    }
}
