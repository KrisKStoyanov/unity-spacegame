using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureHealthManager : MonoBehaviour {

    public static StructureHealthManager current;

    public int strCount = 0;

    public float strMaxHealth = 3;
    public List<structure> structures;

    public class structure
    {
        public float health;
        public GameObject model;
        public structure(float hp, GameObject mdl)
        {
            health = hp;
            model = mdl;
        }
    }

    private void OnEnable()
    {
        current = this;
        structures = new List<structure>();
    }

    public void AddStructure(GameObject str)
    {
        strCount += 1;
        structures.Add(new structure(strMaxHealth, str));
    }

    public void DamageStructures()
    {
        for(int i = 0; i < structures.Count; i++)
        {
            structures[i].health -= 1;
            if(structures[i].health <= 0)
            {
                structures[i].model.SetActive(false);
                structures.Remove(structures[i]);
            }
        }
    }
}
