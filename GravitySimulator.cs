using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySimulator : MonoBehaviour {

    public static GravitySimulator current;

    public GameObject player;
    public bool lostSignal;
    float playerDistFromPlanet;
        
    Vector3 gravityPull;
    Rigidbody foreignRB;
    public float gravityRadius = 300f;
    public float gravityStrength = 9f;

    //Asteroid rotation based on gravity strength
    Quaternion rotation;

    //Scale gravity based on distance from planet
    Vector3 orbitalDist;

    private void Awake()
    {
        current = this;
        lostSignal = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        SimulateGravity();
	}
    void SimulateGravity()
    {
        foreach(Collider targetCollider in Physics.OverlapSphere(transform.position, gravityRadius,5))
        {
            if (!targetCollider.isTrigger)
            {
                gravityPull = (gameObject.transform.position - targetCollider.transform.position) * gravityStrength;
                if (targetCollider.attachedRigidbody != null)
                {
                    //relativePos = InterstellarOrbit.current.curStagePlanet.transform.position - targetCollider.transform.position;
                    rotation = Quaternion.LookRotation(new Vector3(targetCollider.transform.position.y, -targetCollider.transform.position.x), targetCollider.transform.position);
                    targetCollider.transform.rotation = rotation;
                    foreignRB = targetCollider.GetComponent<Rigidbody>();
                    orbitalDist = new Vector3(gameObject.transform.position.x - foreignRB.transform.position.x, gameObject.transform.position.y - foreignRB.transform.position.y);
                    foreignRB.AddForce(orbitalDist.normalized * gravityStrength * 4);
                }
            }
        }
        playerDistFromPlanet = player.transform.position.x - gameObject.transform.position.x;
        if(playerDistFromPlanet < 0)
        {
            playerDistFromPlanet *= -1;
        }
        if(playerDistFromPlanet>gravityRadius * 2)
        {
            lostSignal = true;
            GameStateRegulator.current.FailGameState();
        }
    }
}
