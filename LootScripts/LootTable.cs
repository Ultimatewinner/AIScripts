using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootData", menuName = "Loot Table")]
public class LootTable : ScriptableObject
{
    [System.Serializable]
    public struct Probabilidades
    {
        public int rareza;
        public GameObject reward;
    }

    public Probabilidades[] prob;
}
