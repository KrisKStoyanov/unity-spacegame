using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisruptionField : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Asteroid")
        {
            StartCoroutine(ImmobiliseAsteroid(other.gameObject));
            //other.GetComponent<Rigidbody>().AddForce(other.transform.up * GravitySimulator.current.gravityStrength * gameObject.GetComponent<Rigidbody>().mass);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Asteroid")
        {
            StopCoroutine(ImmobiliseAsteroid(other.gameObject));
        }
    }

    private IEnumerator ImmobiliseAsteroid(GameObject asteroid)
    {
        while (true)
        {
            if(asteroid.GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                asteroid.GetComponent<Rigidbody>().AddForce(asteroid.transform.up * GravitySimulator.current.gravityStrength);
            }
            yield return null;
        }
    }

}
