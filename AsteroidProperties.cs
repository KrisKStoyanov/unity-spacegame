using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidProperties : MonoBehaviour {

    public float health = 3;
    private float healthReset;

    float impactDmg = 1;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        healthReset = health;
	}

    public void ReceiveDamage(float damage, float impactStrength, Transform damageSource)
    {
        health -= damage;
        gameObject.transform.localScale -= new Vector3(0.2f,0.2f,0.2f);
        if(health <= 0)
        {
            GameStateRegulator.current.asteroidCount += 1;
            ResourceManager.current.minerals += 10;
            DestroySelf();
        }
        rb.AddForce(damageSource.up * impactStrength);
        
    }

    void DestroySelf()
    {
        health = healthReset;
        RaycastShoot.current.destroyParticle.transform.position = gameObject.transform.position;
        RaycastShoot.current.destroyParticle.Play();
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if(collision.gameObject.tag == "Stage")
        {
            //HealthManager.current.PlanetReceiveDamage(impactDmg);
        }
        if(collision.gameObject.tag == "Player")
        {
            //HealthManager.current.PlayerReceiveDamage(impactDmg);
        }*/
        if(collision.gameObject.tag != "Structure")
        {
            DestroySelf();
        }
        else
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.collider);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Shield")
        {
            DestroySelf();
        }
        if(other.gameObject.tag == "MagneticRadiation")
        {
            StartCoroutine(Disintegrate());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(Disintegrate());
    }
    private IEnumerator Disintegrate()
    {
        while (true)
        {
            health -= .5f * Time.deltaTime;
            if(health <= 0)
            {
                StopCoroutine(Disintegrate());
                DestroySelf();
            }
            yield return null;
        }
    }
}
