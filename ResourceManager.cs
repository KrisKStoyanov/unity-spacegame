using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {

    public static ResourceManager current;

    public GameObject stagePlanet;

    public bool harvest;
    public ParticleSystem harvestParticle;

    public int minerals;
    public float harvestWait;
    public float waitReset;
    public int harvestRate;
    public int harvestMult;

    Vector3 relativePos;
    Quaternion rotation;

    //UI
    public Text resourceText;
    public Text resourceIncomeText;
    public Image resourceIncomeIndicator;

    //Drill animation
    //Animator droneAnim;
    //static readonly int anim_Drill = Animator.StringToHash("drill");

    private void Awake()
    {
        current = this;
        
    }

    // Use this for initialization
    void Start () {
        waitReset = harvestWait;
        //droneAnim = gameObject.GetComponent<Animator>();
	}
    private void Update()
    {
        ShiftRotation();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Stage")
        {
            stagePlanet = collision.gameObject;
            harvest = true;
            StartCoroutine(PassiveHarvest());
            harvestParticle.Play();
            //droneAnim.SetBool(anim_Drill, harvest);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Stage")
        {
            stagePlanet = null;
            harvest = false;
            StopCoroutine(PassiveHarvest());
            harvestParticle.Stop();
            //droneAnim.SetBool(anim_Drill, harvest);
        }
    }
    public IEnumerator PassiveHarvest()
    {
        while (harvest)
        {
            bool flipIncomeIndicator = false;
            resourceIncomeText.text = "" + harvestRate * harvestMult;
            if (resourceIncomeIndicator.fillAmount < 1 && flipIncomeIndicator == false)
            {
                resourceIncomeIndicator.fillClockwise = true;
                resourceIncomeIndicator.fillAmount = (waitReset - harvestWait) / harvestWait;
                if (resourceIncomeIndicator.fillAmount == 1)
                {
                    flipIncomeIndicator = true;
                }
            }
            if (resourceIncomeIndicator.fillAmount == 1 && flipIncomeIndicator == true)
            {
                resourceIncomeIndicator.fillClockwise = false;
                resourceIncomeIndicator.fillAmount = harvestWait / (waitReset - harvestWait);
                if (resourceIncomeIndicator.fillAmount == 0)
                {
                    flipIncomeIndicator = false;
                }
            }
            /*if(flipIncomeIndicator == false)
            {
                resourceIncomeIndicator.fillClockwise = true;
                resourceIncomeIndicator.fillAmount = (waitReset - harvestWait) / harvestWait;
                if (resourceIncomeIndicator.fillAmount == 1)
                {
                    flipIncomeIndicator = true;
                }
            }
            if(flipIncomeIndicator == true)
            {
                resourceIncomeIndicator.fillClockwise = false;
                resourceIncomeIndicator.fillAmount = harvestWait / (waitReset - harvestWait);
                if (resourceIncomeIndicator.fillAmount == 0)
                {
                    flipIncomeIndicator = false;
                }
            }*/

            harvestWait -= 1 * Time.deltaTime;
            if(harvestWait < 0)
            {
                minerals += harvestRate * harvestMult;
                GameStateRegulator.current.resourceCount = minerals;
                harvestWait = waitReset;
            }
            if (Time.timeScale == 0f)
            {
                resourceText.text = "PAUSED";
            }
            else
            {
                resourceText.text = "" + minerals;
            }
            yield return null;
        }
    }

    void ShiftRotation()
    {
        if(stagePlanet != null)
        {
            relativePos = stagePlanet.transform.position - gameObject.transform.position;
            rotation = Quaternion.LookRotation(new Vector3(gameObject.transform.position.y,  -gameObject.transform.position.x), gameObject.transform.position);
            gameObject.transform.rotation = rotation;
        }
    }
}
