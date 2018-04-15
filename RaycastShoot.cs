using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour {

    public static RaycastShoot current;

    public int laserDmg = 1;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform emitterEnd;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    //public AudioSource[] audioArray;
    public AudioSource audioSource;
    public AudioClip laserAudio;
    public AudioClip hitAudio;
    private LineRenderer laserLine;
    private float nextFire;

    public ParticleSystem launchParticle;
    public ParticleSystem destroyParticle;

    private void OnEnable()
    {
        current = this;
        audioSource = GetComponent<AudioSource>();
    }

    void Start () {
        laserLine = GetComponent<LineRenderer>();
        //laserAudio = audioArray[0];
        //hitAudio = audioArray[1];
        //laserAudio = GetComponent<AudioSource>();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            FireLaserbeam();
        }
    }
    private IEnumerator ShotEffect()
    {
        if (!GameStateRegulator.current.muted)
        {
            //laserAudio.volume = GameStateRegulator.current.sfxVol * GameStateRegulator.current.masterVol;
            //audioArray[0].PlayOneShot(audioArray[0].clip, 1f);
            audioSource.PlayOneShot(laserAudio, GameStateRegulator.current.sfxVol * GameStateRegulator.current.masterVol);
        }
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
    void FireLaserbeam()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = gameObject.transform.position;

            RaycastHit hit;

            laserLine.SetPosition(0, emitterEnd.position);

            if (Physics.Raycast(rayOrigin, gameObject.transform.up, out hit, weaponRange))
            {
                if (!GameStateRegulator.current.muted)
                {
                    audioSource.PlayOneShot(hitAudio, GameStateRegulator.current.sfxVol * GameStateRegulator.current.masterVol);
                }

                laserLine.SetPosition(1, hit.point);

                AsteroidProperties prop = hit.collider.GetComponent<AsteroidProperties>();

                //Influence hit object's health
                if (hit.rigidbody != null)
                {
                    destroyParticle.transform.position = prop.transform.position;
                    destroyParticle.Play();
                    prop.ReceiveDamage(laserDmg, hitForce, gameObject.transform);
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (gameObject.transform.up * weaponRange));
            }

            launchParticle.transform.position = emitterEnd.transform.position;
        }
    }
    public void TouchFire(GameObject cannon)
    {
        if (Time.time > nextFire)
        {
            laserLine = cannon.GetComponent<LineRenderer>();
            //laserAudio = cannon.GetComponent<AudioSource>();

            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = cannon.transform.position;

            RaycastHit hit;

            laserLine.SetPosition(0, emitterEnd.position);

            int layerMask = 1 << 14;
            layerMask = ~layerMask;

            if (Physics.Raycast(rayOrigin, cannon.transform.up, out hit, weaponRange,layerMask))
            {
                laserLine.SetPosition(1, hit.point);

                audioSource.PlayOneShot(hitAudio, GameStateRegulator.current.sfxVol * GameStateRegulator.current.masterVol);
                //Influence hit object's health
                AsteroidProperties prop = hit.collider.GetComponent<AsteroidProperties>();

                if (prop != null)
                {
                    destroyParticle.transform.position = hit.transform.position;
                    destroyParticle.Play();
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                    prop.ReceiveDamage(laserDmg, hitForce, cannon.transform);
                }

            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (cannon.transform.up * weaponRange));
            }

            launchParticle.transform.position = emitterEnd.transform.position;
            launchParticle.Play();
        }
    }
}
