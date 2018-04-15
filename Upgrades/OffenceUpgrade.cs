using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffenceUpgrade : MonoBehaviour {
    public int upgradeId = 1;

    private void OnEnable()
    {
        AugmentDefence(upgradeId);
        if (GameStateRegulator.current.player.activeInHierarchy)
        {
            StructureHealthManager.current.AddStructure(gameObject);
        }
    }
    private void OnDisable()
    {
        RemoveAugment(upgradeId);
    }

    void AugmentDefence(int id)
    {
        if (id == 1)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {

            }
        }
        else if (id == 2)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {

            }
        }
        else if (id == 3)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {

            }
        }
    }
    void RemoveAugment(int id)
    {
        if (id == 1)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {

            }
        }
        else if (id == 2)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {

            }
        }
        else if (id == 3)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {

            }
        }
    }
}
