using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }

}
