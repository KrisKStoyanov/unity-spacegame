using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour {

    public static PlayerHealthManager current;

    public float maxHealth = 3;
    public float health;
    public float impactDamage;

    //UI
    public Image playerHealthIndicator;

    private void OnEnable()
    {
        current = this;
        health = maxHealth;
    }

    void ReceiveDamage(float damage)
    {
        health -= damage;
        playerHealthIndicator.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            health = 0;
            gameObject.SetActive(false);
            GameStateRegulator.current.FailGameState();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            ReceiveDamage(impactDamage);
        }
    }

}
