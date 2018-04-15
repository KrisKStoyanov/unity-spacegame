using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetHealthManager : MonoBehaviour {

    public static PlanetHealthManager current;

    public float planetMaxHealth = 8;
    public float planetHealth;
    public float impactDamage;

    public ParticleSystem collisionImpact;

    public bool modifiedScale = false;
    public GameObject planet;
    public float lastWhole;

    private AudioSource impactAudio;

    //UI
    public Image planetHealthIndicator;

    private void OnEnable()
    {
        current = this;
        planet = this.gameObject;
        planetHealth = planetMaxHealth;
        impactAudio = GetComponent<AudioSource>();
    }

    void ReceiveDamage(float damage)
    {
        planetHealth -= damage;
        planetHealthIndicator.fillAmount = planetHealth / planetMaxHealth;
        impactAudio.volume = GameStateRegulator.current.sfxVol * GameStateRegulator.current.masterVol;
        if (!GameStateRegulator.current.muted)
        {
            impactAudio.Play();
        }
        if (planetHealth > 0)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - .5f, gameObject.transform.localScale.y - .5f, gameObject.transform.localScale.z - .5f);
            GravitySimulator.current.gravityStrength -= GravitySimulator.current.gravityStrength / (planetHealth*4);
            GravitySimulator.current.gravityRadius -= GravitySimulator.current.gravityRadius / (planetHealth*4);
            ResourceManager.current.waitReset += ResourceManager.current.waitReset / planetHealth;
            modifiedScale = true;
            lastWhole = Mathf.Round(planetHealth);
            if (GameStateRegulator.current.planetShied.activeInHierarchy)
            {
                ParticleSystem.ShapeModule sm = PlanetShieldManager.current.shieldEmitter.shape;
                sm.radius = ResourceManager.current.stagePlanet.transform.localScale.x * 10 + 20;
            }
            if (StructureHealthManager.current.structures.Count > 0)
            {
                StructureHealthManager.current.DamageStructures();
            }
        }
        else
        {
            planetHealth = 0;
            ResourceManager.current.harvest = false;
            ResourceManager.current.stagePlanet = null;
            GravitySimulator.current.lostSignal = true;
            GameStateRegulator.current.FailGameState();
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            collisionImpact.transform.position = collision.transform.position;
            collisionImpact.transform.rotation = Quaternion.LookRotation(new Vector3(collision.transform.position.x, collision.transform.position.y));
            collisionImpact.Play();
            ReceiveDamage(impactDamage);
        }
    }

}
