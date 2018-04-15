using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldManager : MonoBehaviour {

    public static PlayerShieldManager current;

    public float playerShieldStrength = 0;
    public float regenWait = 0;
    public float regenTime = 10f;

    public int playerShieldStructures = 0;

    public ParticleSystem shieldEmitter;
    public Collider shieldZone;

    private void OnEnable()
    {
        shieldZone = gameObject.GetComponent<SphereCollider>();
        current = this;
        shieldZone.enabled = true;
    }
    private void OnDisable()
    {
        playerShieldStrength = 0;
        playerShieldStructures = 0;
    }

    public void AddStrength()
    {
        playerShieldStrength += 1;
        playerShieldStructures += 1;
        shieldEmitter.Play();
    }
    public void AbsorbImpact()
    {
        playerShieldStrength -= 1;
        if (playerShieldStrength <= 0)
        {
            shieldZone.enabled = false;
            shieldEmitter.Stop();
            if (playerShieldStructures > 0)
            {
                StartCoroutine(RegenShield());
            }
        }
    }
    public void DecreaseStrength()
    {
        playerShieldStructures -= 1;
        playerShieldStrength -= 1;
        if (playerShieldStrength <= 0 && playerShieldStructures > 0)
        {
            playerShieldStrength = 0;
            shieldZone.enabled = false;
            shieldEmitter.Stop();
            StartCoroutine(RegenShield());
        }

        if (playerShieldStructures <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private IEnumerator RegenShield()
    {
        while (playerShieldStrength != playerShieldStructures)
        {
            regenWait += 1 * Time.fixedDeltaTime;
            if (regenWait >= regenTime)
            {
                regenWait = 0;
                playerShieldStrength += 1;
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
