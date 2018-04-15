using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShieldManager : MonoBehaviour {

    public static PlanetShieldManager current;

    public float planetShieldStrength = 0;
    public float regenWait = 0;
    public float regenTime = 10f;

    public int planetShieldStructures = 0;

    public ParticleSystem shieldEmitter;
    public Collider shieldZone;

    private void OnEnable()
    {
        shieldZone = gameObject.GetComponent<SphereCollider>();
        current = this;
        shieldZone.enabled = true;
        ParticleSystem.ShapeModule sm = shieldEmitter.shape;
        sm.radius = ResourceManager.current.stagePlanet.transform.localScale.x * 10 + 20;
    }
    private void OnDisable()
    {
        planetShieldStrength = 0;
        planetShieldStructures = 0;
    }

    public void AddStrength()
    {
        planetShieldStrength += 1;
        planetShieldStructures += 1;
        shieldEmitter.Play();
    }
    public void AbsorbImpact()
    {
        planetShieldStrength -= 1;
        if (planetShieldStrength <= 0)
        {
            shieldZone.enabled = false;
            shieldEmitter.Stop();
            if(planetShieldStructures > 0)
            {
                StartCoroutine(RegenShield());
            }
        }
    }
    public void DecreaseStrength()
    {
        planetShieldStructures -= 1;
        planetShieldStrength -= 1;
        if(planetShieldStrength <= 0 && planetShieldStructures > 0)
        {
            planetShieldStrength = 0;
            shieldZone.enabled = false;
            shieldEmitter.Stop();
            StartCoroutine(RegenShield());
        }

        if (planetShieldStructures <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private IEnumerator RegenShield()
    {
        while (planetShieldStrength != planetShieldStructures)
        {
            regenWait += 1 * Time.fixedDeltaTime;
            if (regenWait >= regenTime)
            {
                regenWait = 0;
                planetShieldStrength += 1;
                shieldEmitter.Play();
                shieldZone.enabled = true;
            }
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Asteroid")
        {
            AbsorbImpact();
        }
    }
}
