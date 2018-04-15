using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour {

    public float beamSpeed = 100;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            FireBeam();
        }
    }

    void FireBeam()
    {
        GameObject obj = ObjectPooler.current.GetPooledObjectTwo();
        if (obj == null) return;
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        Physics.IgnoreCollision(gameObject.GetComponentInParent<Collider>(), obj.GetComponent<Collider>()) ;
        obj.SetActive(true);
        obj.GetComponent<Rigidbody>().AddForce(transform.up * beamSpeed);
    }
}
