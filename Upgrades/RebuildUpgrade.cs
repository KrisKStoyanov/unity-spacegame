using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebuildUpgrade : MonoBehaviour {

    public int upgradeId = 1;

    private void OnEnable()
    {
        AugmentRebuild(upgradeId);
        if (GameStateRegulator.current.player.activeInHierarchy)
        {
            StructureHealthManager.current.AddStructure(gameObject);
        }
    }
    private void OnDisable()
    {
        RemoveAugment(upgradeId);
    }

    void AugmentRebuild(int id)
    {
        if (id == 1)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {
                StartCoroutine(RebuildPlanet());
            }
        }
        else if (id == 2)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {
                StartCoroutine(RebuildPlayer());
            }
        }
        else if (id == 3)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {
                StartCoroutine(RebuildStructures());
            }
        }
    }
    void RemoveAugment(int id)
    {
        if (id == 1)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {
                StopCoroutine(RebuildPlanet());
            }
        }
        else if (id == 2)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {
                StopCoroutine(RebuildPlayer());
            }
        }
        else if (id == 3)
        {
            if (GameStateRegulator.current.player.activeInHierarchy)
            {
                StopCoroutine(RebuildStructures());
            }
        }
    }
    private IEnumerator RebuildPlanet()
    {
        while (true)
        {

            //print(PlanetHealthManager.current.gameObject.name);
            if (PlanetHealthManager.current.planetHealth > PlanetHealthManager.current.planetMaxHealth)
            {
                PlanetHealthManager.current.planetHealth = PlanetHealthManager.current.planetMaxHealth;
                PlanetHealthManager.current.planetHealthIndicator.fillAmount = PlanetHealthManager.current.planetHealth / PlanetHealthManager.current.planetMaxHealth;
            }
            else if (PlanetHealthManager.current.planetHealth != PlanetHealthManager.current.planetMaxHealth && PlanetHealthManager.current.planetHealth > 0)
            {
                PlanetHealthManager.current.planetHealth += 0.05f * Time.fixedDeltaTime;
                PlanetHealthManager.current.planetHealthIndicator.fillAmount = PlanetHealthManager.current.planetHealth / PlanetHealthManager.current.planetMaxHealth;
            }
            //Needs bool to control amount of scale: (running in coroutine)
            /*if(ResourceManager.current.stagePlanet.transform.localScale.x * 2 < PlanetHealthManager.current.planetHealth)
            {
                ResourceManager.current.stagePlanet.transform.localScale = new Vector3(ResourceManager.current.stagePlanet.transform.localScale.x + 0.5f, ResourceManager.current.stagePlanet.transform.localScale.y + 0.5f, ResourceManager.current.stagePlanet.transform.localScale.z + 0.5f);
            }*/
            if (Mathf.Round(PlanetHealthManager.current.planetHealth) > PlanetHealthManager.current.lastWhole && PlanetHealthManager.current.modifiedScale)
            {
                PlanetHealthManager.current.lastWhole = Mathf.Round(PlanetHealthManager.current.planetHealth);
                PlanetHealthManager.current.planet.transform.localScale = new Vector3(PlanetHealthManager.current.planet.transform.localScale.x + 0.5f, PlanetHealthManager.current.planet.transform.localScale.y + 0.5f, PlanetHealthManager.current.planet.transform.localScale.z + 0.5f);
                GravitySimulator.current.gravityStrength += GravitySimulator.current.gravityStrength / (PlanetHealthManager.current.planetHealth * 4);
                GravitySimulator.current.gravityRadius += GravitySimulator.current.gravityRadius / (PlanetHealthManager.current.planetHealth * 4);
                ResourceManager.current.waitReset -= ResourceManager.current.waitReset / PlanetHealthManager.current.planetHealth;
                if(PlanetHealthManager.current.planetHealth == PlanetHealthManager.current.planetMaxHealth)
                {
                    PlanetHealthManager.current.modifiedScale = false;
                }
            }
            yield return null;
        }
    }
    private IEnumerator RebuildPlayer()
    {
        while (true)
        {
            if(PlayerHealthManager.current.health > PlayerHealthManager.current.maxHealth)
            {
                PlayerHealthManager.current.health = PlayerHealthManager.current.maxHealth;
                PlayerHealthManager.current.playerHealthIndicator.fillAmount = PlayerHealthManager.current.health / PlayerHealthManager.current.maxHealth;
            }
            else if(PlayerHealthManager.current.health != PlayerHealthManager.current.maxHealth && PlayerHealthManager.current.health > 0)
            {
                PlayerHealthManager.current.health += 0.05f * Time.fixedDeltaTime;
                PlayerHealthManager.current.playerHealthIndicator.fillAmount = PlayerHealthManager.current.health / PlayerHealthManager.current.maxHealth;
            }
            yield return null;
        }
    }
    private IEnumerator RebuildStructures()
    {
        while (true)
        {
            for(int i = 0; i < StructureHealthManager.current.structures.Count; i++)
            {
                if(StructureHealthManager.current.structures[i].health > StructureHealthManager.current.strMaxHealth)
                {
                    StructureHealthManager.current.structures[i].health = StructureHealthManager.current.strMaxHealth;
                }
                else if(StructureHealthManager.current.structures[i].health != StructureHealthManager.current.strMaxHealth && StructureHealthManager.current.structures[i].health > 0)
                {
                    StructureHealthManager.current.structures[i].health += 0.05f * Time.fixedDeltaTime;
                }
            }

            yield return null;
        }
    }
}
