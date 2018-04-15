using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstellarOrbit : MonoBehaviour {

    public static InterstellarOrbit current;

    public GameObject[] planets;
    public GameObject orbitCentre;
    public GameObject curStagePlanet;

    float galacticOrbitSpeed = 1.25f;
    float selfOrbitSpeed = 2f;
    float solarOrbit = .5f;

	// Use this for initialization
	void Start () {
        StartCoroutine(PlanetaryOrbit());
	}
	
    private IEnumerator PlanetaryOrbit()
    {
        while (true)
        {
            foreach (GameObject planet in planets)
            {
                //galacticOrbitSpeed = planet.transform.position.x - orbitCentre.transform.position.x;
                //selfOrbitSpeed -= planet.transform.localScale.x;
                planet.transform.Rotate(planet.transform.position,Time.deltaTime * selfOrbitSpeed);
                planet.transform.RotateAround(orbitCentre.transform.position, Vector3.down, Time.deltaTime * galacticOrbitSpeed);
            }
            if (curStagePlanet != null)
            {
                orbitCentre.transform.RotateAround(curStagePlanet.transform.position, Vector3.down, Time.deltaTime * solarOrbit);
                curStagePlanet.transform.Rotate(Vector3.one, Time.deltaTime * selfOrbitSpeed/4);
            }
            orbitCentre.transform.Rotate(orbitCentre.transform.position, Time.deltaTime * selfOrbitSpeed);
            yield return null;
        }
    }
}
