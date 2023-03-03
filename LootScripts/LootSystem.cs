using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSystem : MonoBehaviour
{
    [SerializeField]
    private LootTable lootTable;

    public static LootSystem Instance { get; private set; }

    [SerializeField]
    private int probabilidad;

    [SerializeField]
    private int total;

    private LootTable.Probabilidades[] localProb;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        localProb = lootTable.prob;
        System.Array.Sort(localProb, new ComparadorDeRarezas());
        System.Array.Reverse(localProb);

        foreach(var weight in lootTable.prob)
        {
            total += weight.rareza;
        }
    }

    public void SpawnLoot(Transform spawnPoint, int factorAdicional, int cantidadDeItemsADropear, int dropChance, bool? repeated = null)
    {
        probabilidad = Random.Range(0, (total + 1));
        int miProb = probabilidad * factorAdicional;

        if(repeated != null)
        {
            if(repeated == true)
            {
                for (int i = 0; i < lootTable.prob.Length; i++)
                {
                    if(miProb <= lootTable.prob[i].rareza)
                    {
                        GameObject go = Instantiate(lootTable.prob[i].reward, spawnPoint.position, Quaternion.identity);                     

                        if(cantidadDeItemsADropear == 1)
                        {
                            return;
                        }
                        cantidadDeItemsADropear--;
                    }
                    else
                    {
                        miProb -= lootTable.prob[i].rareza;
                    }
                }
            }
        }

        //Chequeo rapido para ver si se obtiene el loot
        int dropChanceCalculo = Random.Range(0, 101);

        if(dropChanceCalculo >= dropChance)
        {
            print("No hay loot");
            return;
        }
        //fin

        if(miProb >= total)
        {
            miProb = total;
        }

        for (int i = 0; i < lootTable.prob.Length; i++)
        {
            if (miProb <= lootTable.prob[i].rareza)
            {
                GameObject go = Instantiate(lootTable.prob[i].reward, spawnPoint.position, Quaternion.identity);

                if(cantidadDeItemsADropear == 1)
                {
                    return;
                }
                cantidadDeItemsADropear--;
            }
            else
            {
                miProb -= lootTable.prob[i].rareza;
            }
        }

        if(cantidadDeItemsADropear > 1)
        {
            SpawnLoot(spawnPoint, factorAdicional, cantidadDeItemsADropear, dropChance, true);
        }
    }
}

public class ComparadorDeRarezas : IComparer
{
    public int Compare(object x, object y)
    {
        int n1 = ((LootTable.Probabilidades)x).rareza;
        int n2 = ((LootTable.Probabilidades)y).rareza;

        if(n1 > n2)
        {
            return 1;
        }
        else if (n1 == n2)
        {
            return 0;
        }
        else
        {
            return -1;
        }
    }
}
