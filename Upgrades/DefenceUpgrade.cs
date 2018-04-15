using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceUpgrade : MonoBehaviour {
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
                GameStateRegulator.current.ShieldPlayer();
                PlayerShieldManager.current.AddStrength();
            }
        }
        else if (id == 2)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {
                GameStateRegulator.current.ShieldPlanet();
                PlanetShieldManager.current.AddStrength();
            }
        }
        else if (id == 3)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {
                GameStateRegulator.current.ShieldPlanet();
                GameStateRegulator.current.ShieldPlayer();
                PlayerShieldManager.current.AddStrength();
                PlanetShieldManager.current.AddStrength();
                PlayerShieldManager.current.regenTime /= 2;
                PlanetShieldManager.current.regenTime /= 2;
            }
        }
    }
    void RemoveAugment(int id)
    {
        if (id == 1)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {
                PlayerShieldManager.current.DecreaseStrength();
            }
        }
        else if (id == 2)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {
                PlanetShieldManager.current.DecreaseStrength();
            }
        }
        else if (id == 3)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {
                PlayerShieldManager.current.DecreaseStrength();
                PlanetShieldManager.current.DecreaseStrength();
                PlayerShieldManager.current.regenTime *= 2;
                PlanetShieldManager.current.regenTime *= 2;
            }
        }
    }
}
