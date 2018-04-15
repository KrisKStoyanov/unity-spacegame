using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour {

    public float speedMultiplier = 1.8f;

	// Use this for initialization
	void Start () {
        StartCoroutine(RotateGalaxy());
	}
	
     private IEnumerator RotateGalaxy()
    {
        while(true){
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * speedMultiplier);

            yield return null;
        }
    }
}
