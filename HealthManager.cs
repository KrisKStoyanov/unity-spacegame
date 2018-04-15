using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    public static HealthManager current;

    public GameObject player;
    public GameObject curPlanet;

    public float maxPlayerHealth;
    public float curPlayerHealth;

    public float maxPlanetHealth;
    public float curPlanetHealth;

    public Image playerHealthIndicator;
    public Image planetHealthIndicator;

    // Use this for initialization
    private void OnEnable()
    {
        current = this;
        curPlayerHealth = maxPlayerHealth;
        curPlanetHealth = maxPlanetHealth;
        playerHealthIndicator.fillAmount = curPlayerHealth / maxPlayerHealth;
        planetHealthIndicator.fillAmount = curPlanetHealth / maxPlanetHealth;
    }

    public void PlayerReceiveDamage(float damage)
    {
        curPlayerHealth -= damage;
        playerHealthIndicator.fillAmount = curPlayerHealth / maxPlayerHealth;
        if (curPlayerHealth <= 0)
        {
            curPlayerHealth = 0;
            GameStateRegulator.current.FailGameState();
            player.SetActive(false);
        }
    }

    public void PlanetReceiveDamage(float damage)
    {
        curPlanetHealth -= damage;
        planetHealthIndicator.fillAmount = curPlanetHealth / maxPlanetHealth;
        if (curPlanetHealth > 0)
        {
            curPlanet.transform.localScale = new Vector3(gameObject.transform.localScale.x - 1, gameObject.transform.localScale.y - 1, gameObject.transform.localScale.z - 1);
            GravitySimulator.current.gravityStrength -= GravitySimulator.current.gravityStrength / curPlanetHealth;
            GravitySimulator.current.gravityRadius -= GravitySimulator.current.gravityRadius / (curPlanetHealth * 2);
            ResourceManager.current.waitReset += ResourceManager.current.waitReset / curPlanetHealth;
        }
        else
        {
            curPlanetHealth = 0;
            GameStateRegulator.current.FailGameState();
            curPlanet.SetActive(false);
        }
    }

}
