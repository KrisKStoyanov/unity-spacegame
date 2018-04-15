using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Camera mainCamera;

    bool trackPlayer;

    public float airTime = 2f;
    float resetTime;
    public int speed = 100;
    Vector3 movement = new Vector3();
    private Rigidbody rb;


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        resetTime = airTime;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
#if UNITY_ANDROID
        TouchMove();
#elif !UNITY_ANDROID
        Move();
#endif
        AlignCamera();
	}

    void Move()
    {
        movement = new Vector3(Input.GetAxis("Horizontal") * speed,Input.GetAxis("Vertical") * speed);
        rb.AddForce(transform.position + movement);
    }

    void AlignCamera()
    {
        if (ResourceManager.current.harvest)
        {
            trackPlayer = false;
            airTime = resetTime;
            mainCamera.transform.LookAt(ResourceManager.current.stagePlanet.transform);
        }
        else
        {
            airTime -= 1 * Time.deltaTime;
            if(airTime < 0)
            {
                trackPlayer = true;
                airTime = resetTime;
            }
        }
        if (trackPlayer)
        {
            mainCamera.transform.LookAt(gameObject.transform);
        }
    }

    void TouchMove()
    {
        /*for(int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchDeltaPos = Input.GetTouch(0).deltaPosition;
            movement = new Vector3(Input.GetTouch(0).deltaPosition.x * speed, Input.GetTouch(0).deltaPosition.y * speed);
            rb.AddForce(transform.position + movement);
        }*/
        Vector3 touchDeltaPos = Input.GetTouch(0).deltaPosition;
        movement = new Vector3(Input.GetTouch(0).deltaPosition.x, Input.GetTouch(0).deltaPosition.y);
        rb.AddForce(transform.position + movement.normalized * speed);
        /*movement = new Vector3(Input.GetTouch(0).deltaPosition.x * speed, Input.GetTouch(0).deltaPosition.y * speed);
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector3 touchDeltaPos = Input.GetTouch(0).deltaPosition;

            movement = new Vector3(-touchDeltaPos.x * speed, -touchDeltaPos.y * speed);
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            rb.AddForce(-collision.gameObject.transform.position * 50);
            if (!GameStateRegulator.current.muted)
            {
                RaycastShoot.current.audioSource.PlayOneShot(RaycastShoot.current.hitAudio, GameStateRegulator.current.sfxVol * GameStateRegulator.current.masterVol);
            }
        }
    }
}
