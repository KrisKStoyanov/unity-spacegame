using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestUpgrade : MonoBehaviour {

    public int upgradeId = 1;

    private void OnEnable()
    {
        AugmentHarvest(upgradeId);
        if (GameStateRegulator.current.player.activeInHierarchy)
        {
            StructureHealthManager.current.AddStructure(gameObject);
        }
    }
    private void OnDisable()
    {
        RemoveAugment(upgradeId);
    }

    void AugmentHarvest(int id)
    {
        if(id == 1)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {
                ResourceManager.current.harvestRate += 1;
                ResourceManager.current.waitReset -= .25f;
            }
        }
        else if(id == 2)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {
                ResourceManager.current.harvestRate += 1;
                ResourceManager.current.waitReset -= .25f;
                ResourceManager.current.harvestMult += 2;
            }
        }
        else if(id == 3)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {
                ResourceManager.current.harvestMult += 10;
                ResourceManager.current.waitReset -= .5f;
                ResourceManager.current.harvestRate += 5;
            }
        }
    }
    void RemoveAugment(int id)
    {
        if (id == 1)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {
                ResourceManager.current.harvestRate -= 1;
                ResourceManager.current.waitReset += .25f;
            }
        }
        else if (id == 2)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {
                ResourceManager.current.harvestRate -= 1;
                ResourceManager.current.waitReset += .25f;
                ResourceManager.current.harvestMult -= 2;
            }
        }
        else if (id == 3)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {
                ResourceManager.current.harvestMult -= 10;
                ResourceManager.current.waitReset += .5f;
                ResourceManager.current.harvestRate -= 5;
            }
        }
    }
}
