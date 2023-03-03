using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigoloot : MonoBehaviour
{
    public int id;
    private LootableObject lb;

    // Start is called before the first frame update
    void Start()
    {
        lb = GetComponent<LootableObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Muerte"))
        {
            Muerte();
        }
    }

    public void Muerte()
    {
        lb.RealizarLoot();
    }
}
