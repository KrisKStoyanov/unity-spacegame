using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionManager : MonoBehaviour {

    public static ConstructionManager current;

    bool openConsMenu;
    public Canvas constructionMenu;
    public Transform playerLocation;

    public int offenceLevel;
    public int defenceLevel;
    public int harvestLevel;
    public int rebuildLevel;

    bool openOffenceMenu;
    bool openDefenceMenu;
    bool openHarvestMenu;
    bool openRebuildMenu;

    public Image offencePanel;
    public Image defencePanel;
    public Image harvestPanel;
    public Image rebuildPanel;

    public Text upgradeTable;

    public Button ToggleOffence;
    public Button ToggleDefence;
    public Button ToggleHarvest;
    public Button ToggleRebuild;

    float researchTime = 10;
    float resetRTime;

    int resReq;

    bool researching;
    bool upgraded;

    int templateLevel = 0;

    public Image researchPanel;
    public Image researchProgressIndicator;
    public Text researchTimeLeft;

    public Text upgradeStats;
    public Text upAvailable;
    public Text upAchieved;

    //Building radius
    float buildRadius = 3f;

    private void OnEnable()
    {
        current = this;

        offenceLevel = 0;
        defenceLevel = 0;
        harvestLevel = 0;
        rebuildLevel = 0;

        openConsMenu = false;
        openOffenceMenu = false;
        openDefenceMenu = false;
        openHarvestMenu = false;
        openRebuildMenu = false;

        resetRTime = researchTime;

        upgradeStats.text = "Offense: " + offenceLevel + "\n" + "Defense: " + defenceLevel + "\n" + "Harvest: " + harvestLevel + "\n" + "Rebuild: " + rebuildLevel;
    }

    public void ToggleConsMenu()
    {
        if (openConsMenu)
        {
            upgradeStats.gameObject.SetActive(true);
            if (upgraded)
            {
                upAvailable.gameObject.SetActive(true);
            }
            offencePanel.gameObject.SetActive(false);
            defencePanel.gameObject.SetActive(false);
            harvestPanel.gameObject.SetActive(false);
            rebuildPanel.gameObject.SetActive(false);
            ToggleOffence.gameObject.SetActive(true);
            ToggleDefence.gameObject.SetActive(true);
            ToggleHarvest.gameObject.SetActive(true);
            ToggleRebuild.gameObject.SetActive(true);
            constructionMenu.gameObject.SetActive(false);
            upgradeTable.gameObject.SetActive(false);
            openConsMenu = false;
        }
        else
        {
            upAvailable.gameObject.SetActive(false);
            upgradeStats.gameObject.SetActive(false);
            constructionMenu.gameObject.SetActive(true);
            openConsMenu = true;
        }
    }

    public void ToggleOffenceMenu()
    {
        if (openOffenceMenu)
        {
            offencePanel.gameObject.SetActive(false);
            openOffenceMenu = false;
            upgradeTable.gameObject.SetActive(false);
            ToggleDefence.gameObject.SetActive(true);
            ToggleHarvest.gameObject.SetActive(true);
            ToggleRebuild.gameObject.SetActive(true);
        }
        else
        {
            offencePanel.gameObject.SetActive(true);
            openOffenceMenu = true;
            if (!researching)
            {
                upgradeTable.gameObject.SetActive(true);
            }
            ToggleDefence.gameObject.SetActive(false);
            ToggleHarvest.gameObject.SetActive(false);
            ToggleRebuild.gameObject.SetActive(false);
        }
    }

    public void ToggleDefenceMenu()
    {
        if (openDefenceMenu)
        {
            defencePanel.gameObject.SetActive(false);
            openDefenceMenu = false;
            upgradeTable.gameObject.SetActive(false);
            ToggleOffence.gameObject.SetActive(true);
            ToggleHarvest.gameObject.SetActive(true);
            ToggleRebuild.gameObject.SetActive(true);
        }
        else
        {
            defencePanel.gameObject.SetActive(true);
            openDefenceMenu = true;
            if (!researching)
            {
                upgradeTable.gameObject.SetActive(true);
            }
            ToggleOffence.gameObject.SetActive(false);
            ToggleHarvest.gameObject.SetActive(false);
            ToggleRebuild.gameObject.SetActive(false);
        }
    }

    public void ToggleHarvestMenu()
    {
        if (openHarvestMenu)
        {
            harvestPanel.gameObject.SetActive(false);
            openHarvestMenu = false;
            upgradeTable.gameObject.SetActive(false);
            ToggleOffence.gameObject.SetActive(true);
            ToggleDefence.gameObject.SetActive(true);
            ToggleRebuild.gameObject.SetActive(true);
        }
        else
        {
            harvestPanel.gameObject.SetActive(true);
            openHarvestMenu = true;
            if (!researching)
            {
                upgradeTable.gameObject.SetActive(true);
            }
            ToggleOffence.gameObject.SetActive(false);
            ToggleDefence.gameObject.SetActive(false);
            ToggleRebuild.gameObject.SetActive(false);
        }
    }

    public void ToggleRebuildMenu()
    {
        if (openRebuildMenu)
        {
            rebuildPanel.gameObject.SetActive(false);
            openRebuildMenu = false;
            upgradeTable.gameObject.SetActive(false);
            ToggleOffence.gameObject.SetActive(true);
            ToggleDefence.gameObject.SetActive(true);
            ToggleHarvest.gameObject.SetActive(true);
        }
        else
        {
            rebuildPanel.gameObject.SetActive(true);
            openRebuildMenu = true;
            if (!researching)
            {
                upgradeTable.gameObject.SetActive(true);
            }
            ToggleOffence.gameObject.SetActive(false);
            ToggleDefence.gameObject.SetActive(false);
            ToggleHarvest.gameObject.SetActive(false);
        }
    }

    public void BuildOffenceStructure(int levelReq)
    {
        if(levelReq == 1)
        {
            resReq = 10;
        }
        else if(levelReq == 2)
        {
            resReq = 100;
        }
        else if(levelReq == 3)
        {
            resReq = 1000;
        }

        //Check if an upgrade is available and enough resources are present
        if(offenceLevel < levelReq && ResourceManager.current.minerals >= resReq && researching == false)
        {
            //If the research hasnt started start it and take the resources
            if (!upgraded)
            {
                if(offenceLevel == levelReq - 1)
                {
                    ResourceManager.current.minerals -= resReq;
                    offenceLevel = StartResearching(offenceLevel, levelReq);
                    ToggleConsMenu();
                    ToggleOffenceMenu();
                }
            }
            //If the research has finished increase the selected level
            else if(upgraded)
            {
                offenceLevel = StartResearching(offenceLevel, levelReq);
                upgraded = false;
                upAvailable.gameObject.SetActive(false);
                upAchieved.text = "Offence" + " +1";
                upAchieved.gameObject.SetActive(true);
                StartCoroutine(HideUpAchieved());
                upgradeStats.text = "Offense: " + offenceLevel + "\n" + "Defense: " + defenceLevel + "\n" + "Harvest: " + harvestLevel + "\n" + "Rebuild: " + rebuildLevel;
                ToggleConsMenu();
                ToggleOffenceMenu();
            }

        }
        else if(offenceLevel >= levelReq && ResourceManager.current.minerals >= resReq)
        {
            if (levelReq == 1)
            {
                Collider[] hitColliders = Physics.OverlapSphere(playerLocation.position, buildRadius);
                int check = 0;
                bool availablePos = false;
                while(check < hitColliders.Length && ResourceManager.current.harvest)
                {
                    if(hitColliders[check].tag != "Structure")
                    {
                        availablePos = true;
                        check++;        
                    }
                    else
                    {
                        availablePos = false;
                        break;
                    }
                }
                if(availablePos == true)
                {
                    SpawnStructure(1, levelReq);
                    ToggleConsMenu();
                    ToggleOffenceMenu();
                    ResourceManager.current.minerals -= resReq;
                    print("build structure tier one");
                }
            }
            if (levelReq == 2)
            {
                Collider[] hitColliders = Physics.OverlapSphere(playerLocation.position, buildRadius);
                int check = 0;
                bool availablePos = false;
                while (check < hitColliders.Length && ResourceManager.current.harvest)
                {
                    if (hitColliders[check].tag != "Structure")
                    {
                        availablePos = true;
                        check++;
                    }
                    else
                    {
                        availablePos = false;
                        break;
                    }
                }
                if (availablePos == true)
                {
                    SpawnStructure(1, levelReq);
                    ToggleConsMenu();
                    ToggleOffenceMenu();
                    ResourceManager.current.minerals -= resReq;
                    print("build structure tier two");
                }
            }
            if (levelReq == 3)
            {
                Collider[] hitColliders = Physics.OverlapSphere(playerLocation.position, buildRadius);
                int check = 0;
                bool availablePos = false;
                while (check < hitColliders.Length && ResourceManager.current.harvest)
                {
                    if (hitColliders[check].tag != "Structure")
                    {
                        availablePos = true;
                        check++;
                    }
                    else
                    {
                        availablePos = false;
                        break;
                    }
                }
                if (availablePos == true)
                {
                    SpawnStructure(1, levelReq);
                    ToggleConsMenu();
                    ToggleOffenceMenu();
                    ResourceManager.current.minerals -= resReq;
                    print("build structure tier three");
                }
            }
        }
    }

    public void BuildDefenceStructure(int levelReq)
    {
        if (levelReq == 1)
        {
            resReq = 10;
        }
        else if (levelReq == 2)
        {
            resReq = 100;
        }
        else if (levelReq == 3)
        {
            resReq = 1000;
        }

        if (defenceLevel < levelReq && ResourceManager.current.minerals >= resReq && researching == false)
        {
            if (!upgraded)
            {
                if(defenceLevel == levelReq - 1)
                {
                    ResourceManager.current.minerals -= resReq;
                    defenceLevel = StartResearching(defenceLevel, levelReq);
                    ToggleConsMenu();
                    ToggleDefenceMenu();
                }
            }
            else if(upgraded)
            {
                defenceLevel = StartResearching(defenceLevel, levelReq);
                upgraded = false;
                upAvailable.gameObject.SetActive(false);
                upAchieved.text = "Defence" + " +1";
                upAchieved.gameObject.SetActive(true);
                StartCoroutine(HideUpAchieved());
                upgradeStats.text = "Offense: " + offenceLevel + "\n" + "Defense: " + defenceLevel + "\n" + "Harvest: " + harvestLevel + "\n" + "Rebuild: " + rebuildLevel;
                ToggleConsMenu();
                ToggleDefenceMenu();
            }

        }
        else if (defenceLevel >= levelReq && ResourceManager.current.minerals >= resReq)
        {
            if (levelReq == 1)
            {
                Collider[] hitColliders = Physics.OverlapSphere(playerLocation.position, buildRadius);
                int check = 0;
                bool availablePos = false;
                while (check < hitColliders.Length && ResourceManager.current.harvest)
                {
                    if (hitColliders[check].tag != "Structure")
                    {
                        availablePos = true;
                        check++;
                    }
                    else
                    {
                        availablePos = false;
                        break;
                    }
                }
                if (availablePos == true)
                {
                    SpawnStructure(2, levelReq);
                    ToggleConsMenu();
                    ToggleDefenceMenu();
                    ResourceManager.current.minerals -= resReq;
                    print("build structure tier one");
                }
            }
            if (levelReq == 2)
            {
                Collider[] hitColliders = Physics.OverlapSphere(playerLocation.position, buildRadius);
                int check = 0;
                bool availablePos = false;
                while (check < hitColliders.Length && ResourceManager.current.harvest)
                {
                    if (hitColliders[check].tag != "Structure")
                    {
                        availablePos = true;
                        check++;
                    }
                    else
                    {
                        availablePos = false;
                        break;
                    }
                }
                if (availablePos == true)
                {
                    SpawnStructure(2, levelReq);
                    ToggleConsMenu();
                    ToggleDefenceMenu();
                    ResourceManager.current.minerals -= resReq;
                    print("build structure tier two");
                }
            }
            if (levelReq == 3)
            {
                Collider[] hitColliders = Physics.OverlapSphere(playerLocation.position, buildRadius);
                int check = 0;
                bool availablePos = false;
                while (check < hitColliders.Length && ResourceManager.current.harvest)
                {
                    if (hitColliders[check].tag != "Structure")
                    {
                        availablePos = true;
                        check++;
                    }
                    else
                    {
                        availablePos = false;
                        break;
                    }
                }
                if (availablePos == true)
                {
                    SpawnStructure(2, levelReq);
                    ToggleConsMenu();
                    ToggleDefenceMenu();
                    ResourceManager.current.minerals -= resReq;
                    print("build structure tier three");
                }
            }
        }
    }

    public void BuildHarvestStructure(int levelReq)
    {
        if (levelReq == 1)
        {
            resReq = 10;
        }
        else if (levelReq == 2)
        {
            resReq = 100;
        }
        else if (levelReq == 3)
        {
            resReq = 1000;
        }

        if (harvestLevel < levelReq && ResourceManager.current.minerals >= resReq && researching == false)
        {
            if (!upgraded)
            {
                if(harvestLevel == levelReq - 1)
                {
                    ResourceManager.current.minerals -= resReq;
                    harvestLevel = StartResearching(harvestLevel, levelReq);
                    ToggleConsMenu();
                    ToggleHarvestMenu();
                }
            }
            else if(upgraded)
            {
                harvestLevel = StartResearching(harvestLevel, levelReq);
                upgraded = false;
                upAvailable.gameObject.SetActive(false);
                upAchieved.text = "Harvest" + " +1";
                upAchieved.gameObject.SetActive(true);
                StartCoroutine(HideUpAchieved());
                upgradeStats.text = "Offense: " + offenceLevel + "\n" + "Defense: " + defenceLevel + "\n" + "Harvest: " + harvestLevel + "\n" + "Rebuild: " + rebuildLevel;
                ToggleConsMenu();
                ToggleHarvestMenu();
            }
        }
        else if (harvestLevel >= levelReq && ResourceManager.current.minerals >= resReq)
        {
            if (levelReq == 1)
            {
                Collider[] hitColliders = Physics.OverlapSphere(playerLocation.position, buildRadius);
                int check = 0;
                bool availablePos = false;
                while (check < hitColliders.Length && ResourceManager.current.harvest)
                {
                    if (hitColliders[check].tag != "Structure")
                    {
                        availablePos = true;
                        check++;
                    }
                    else
                    {
                        availablePos = false;
                        break;
                    }
                }
                if (availablePos == true)
                {
                    SpawnStructure(3, levelReq);
                    ToggleConsMenu();
                    ToggleHarvestMenu();
                    ResourceManager.current.minerals -= resReq;
                    print("build structure tier one");
                }
            }
            if (levelReq == 2)
            {
                Collider[] hitColliders = Physics.OverlapSphere(playerLocation.position, buildRadius);
                int check = 0;
                bool availablePos = false;
                while (check < hitColliders.Length && ResourceManager.current.harvest)
                {
                    if (hitColliders[check].tag != "Structure")
                    {
                        availablePos = true;
                        check++;
                    }
                    else
                    {
                        availablePos = false;
                        break;
                    }
                }
                if (availablePos == true)
                {
                    SpawnStructure(3, levelReq);
                    ToggleConsMenu();
                    ToggleHarvestMenu();
                    ResourceManager.current.minerals -= resReq;
                    print("build structure tier two");
                }
            }
            if (levelReq == 3)
            {
                Collider[] hitColliders = Physics.OverlapSphere(playerLocation.position, buildRadius);
                int check = 0;
                bool availablePos = false;
                while (check < hitColliders.Length && ResourceManager.current.harvest)
                {
                    if (hitColliders[check].tag != "Structure")
                    {
                        availablePos = true;
                        check++;
                    }
                    else
                    {
                        availablePos = false;
                        break;
                    }
                }
                if (availablePos == true)
                {
                    SpawnStructure(3, levelReq);
                    ToggleConsMenu();
                    ToggleHarvestMenu();
                    ResourceManager.current.minerals -= resReq;
                    print("build structure tier three");
                }
            }
        }
    }

    public void BuildRebuildStructure(int levelReq)
    {
        if (levelReq == 1)
        {
            resReq = 10;
        }
        else if (levelReq == 2)
        {
            resReq = 100;
        }
        else if (levelReq == 3)
        {
            resReq = 1000;
        }

        if (rebuildLevel < levelReq && ResourceManager.current.minerals >= resReq && researching == false)
        {
            if (!upgraded)
            {
                if(rebuildLevel == levelReq - 1)
                {
                    ResourceManager.current.minerals -= resReq;
                    rebuildLevel = StartResearching(rebuildLevel, levelReq);
                    ToggleConsMenu();
                    ToggleRebuildMenu();
                }
            }
            else if(upgraded)
            {
                rebuildLevel = StartResearching(rebuildLevel, levelReq);
                upgraded = false;
                upAvailable.gameObject.SetActive(false);
                upAchieved.text = "Rebuild" + " +1";
                upAchieved.gameObject.SetActive(true);
                StartCoroutine(HideUpAchieved());
                upgradeStats.text = "Offense: " + offenceLevel + "\n" + "Defense: " + defenceLevel + "\n" + "Harvest: " + harvestLevel + "\n" + "Rebuild: " + rebuildLevel;
                ToggleConsMenu();
                ToggleRebuildMenu();
            }
        }
        else if (rebuildLevel >= levelReq && ResourceManager.current.minerals >= resReq)
        {
            if (levelReq == 1)
            {
                Collider[] hitColliders = Physics.OverlapSphere(playerLocation.position, buildRadius);
                int check = 0;
                bool availablePos = false;
                while (check < hitColliders.Length && ResourceManager.current.harvest)
                {
                    if (hitColliders[check].tag != "Structure")
                    {
                        availablePos = true;
                        check++;
                    }
                    else
                    {
                        availablePos = false;
                        break;
                    }
                }
                if (availablePos == true)
                {
                    SpawnStructure(4, levelReq);
                    ToggleConsMenu();
                    ToggleRebuildMenu();
                    ResourceManager.current.minerals -= resReq;
                    print("build structure tier one");
                }
            }
            if(levelReq == 2)
            {
                Collider[] hitColliders = Physics.OverlapSphere(playerLocation.position, buildRadius);
                int check = 0;
                bool availablePos = false;
                while (check < hitColliders.Length && ResourceManager.current.harvest)
                {
                    if (hitColliders[check].tag != "Structure")
                    {
                        availablePos = true;
                        check++;
                    }
                    else
                    {
                        availablePos = false;
                        break;
                    }
                }
                if (availablePos == true)
                {
                    SpawnStructure(4, levelReq);
                    ToggleConsMenu();
                    ToggleRebuildMenu();
                    ResourceManager.current.minerals -= resReq;
                    print("build structure tier two");
                }
            }
            if(levelReq == 3)
            {
                Collider[] hitColliders = Physics.OverlapSphere(playerLocation.position, buildRadius);
                int check = 0;
                bool availablePos = false;
                while (check < hitColliders.Length && ResourceManager.current.harvest)
                {
                    if (hitColliders[check].tag != "Structure")
                    {
                        availablePos = true;
                        check++;
                    }
                    else
                    {
                        availablePos = false;
                        break;
                    }
                }
                if (availablePos == true)
                {
                    SpawnStructure(4, levelReq);
                    ToggleConsMenu();
                    ToggleRebuildMenu();
                    ResourceManager.current.minerals -= resReq;
                    print("build structure tier three");
                }
            }
        }
    }

    int StartResearching(int levelToUpgrade, int levelReq)
    {
        templateLevel = levelToUpgrade;
        if (upgraded)
        {
            templateLevel = levelReq;
            //upgraded = false;
            return templateLevel;
        }
        else
        {
            researching = true;
            StartCoroutine(ResearchNewTech());
            return levelToUpgrade;
        }
    }

    private IEnumerator ResearchNewTech()
    {
        while (researching)
        {
            researchPanel.gameObject.SetActive(true);
            researchTime -= Time.deltaTime;
            researchProgressIndicator.fillAmount = (resetRTime - researchTime) / resetRTime;
            if(Mathf.RoundToInt(researchTime) >= 10)
            {
                researchTimeLeft.text = "0:" + Mathf.RoundToInt(researchTime);
            }
            else
            {
                researchTimeLeft.text = "0:0" + Mathf.RoundToInt(researchTime);
            }
            if (researchTime <= 0)
            {
                researchPanel.gameObject.SetActive(false);
                upAvailable.gameObject.SetActive(true);
                researchTime = resetRTime;
                upgraded = true;
                researching = false;
            }
            yield return null;
        }
    }
    private IEnumerator HideUpAchieved()
    {
        float timeLeft = 100f;
        while (true)
        {
            timeLeft-=1;
            if(timeLeft < 0)
            {
                upAchieved.gameObject.SetActive(false);
                timeLeft = 100f;
            }

            yield return null;
        }
    }

    void SpawnStructure(int specID, int curLevel)
    {
        if(specID == 1)
        {
            if(curLevel == 1)
            {
                GameObject obj = ObjectPooler.current.GetPooledObjectTwo();
                if (obj == null) return;
                else
                {
                    obj.transform.position = playerLocation.position;
                    obj.transform.rotation = playerLocation.rotation;
                    obj.SetActive(true);
                    Physics.IgnoreCollision(obj.GetComponent<Collider>(), playerLocation.gameObject.GetComponent<Collider>());
                    foreach(Collider surroundingObj in Physics.OverlapSphere(obj.transform.position, buildRadius * 2))
                    {
                        if (surroundingObj.attachedRigidbody != null)
                        {
                            Physics.IgnoreCollision(obj.GetComponent<Collider>(), surroundingObj.gameObject.GetComponent<Collider>());
                        }
                    }  
                }
                //spawn off tier 1
            }
            if(curLevel == 2)
            {
                //spawn off tier 2
                GameObject obj = ObjectPooler.current.GetPooledObjectThree();
                if (obj == null) return;
                else
                {
                    obj.transform.position = playerLocation.position;
                    obj.transform.rotation = playerLocation.rotation;
                    obj.SetActive(true);
                    Physics.IgnoreCollision(obj.GetComponent<Collider>(), playerLocation.gameObject.GetComponent<Collider>());
                    foreach (Collider surroundingObj in Physics.OverlapSphere(obj.transform.position, buildRadius * 2))
                    {
                        if (surroundingObj.attachedRigidbody != null)
                        {
                            Physics.IgnoreCollision(obj.GetComponent<Collider>(), surroundingObj.gameObject.GetComponent<Collider>());
                        }
                    }
                }
            }
            if(curLevel == 3)
            {
                //spawn off tier 3
                GameObject obj = ObjectPooler.current.GetPooledObjectFour();
                if (obj == null) return;
                else
                {
                    obj.transform.position = playerLocation.position;
                    obj.transform.rotation = playerLocation.rotation;
                    obj.SetActive(true);
                    Physics.IgnoreCollision(obj.GetComponent<Collider>(), playerLocation.gameObject.GetComponent<Collider>());
                    foreach (Collider surroundingObj in Physics.OverlapSphere(obj.transform.position, buildRadius * 2))
                    {
                        if (surroundingObj.attachedRigidbody != null)
                        {
                            Physics.IgnoreCollision(obj.GetComponent<Collider>(), surroundingObj.gameObject.GetComponent<Collider>());
                        }
                    }
                }
            }
        }
        if(specID == 2)
        {
            if (curLevel == 1)
            {
                //spawn def tier 1
                GameObject obj = ObjectPooler.current.GetPooledObjectFive();
                if (obj == null) return;
                else
                {
                    obj.transform.position = playerLocation.position;
                    obj.transform.rotation = playerLocation.rotation;
                    obj.SetActive(true);
                    Physics.IgnoreCollision(obj.GetComponent<Collider>(), playerLocation.gameObject.GetComponent<Collider>());
                    foreach (Collider surroundingObj in Physics.OverlapSphere(obj.transform.position, buildRadius * 2))
                    {
                        if (surroundingObj.attachedRigidbody != null)
                        {
                            Physics.IgnoreCollision(obj.GetComponent<Collider>(), surroundingObj.gameObject.GetComponent<Collider>());
                        }
                    }
                }
            }
            if (curLevel == 2)
            {
                //spawn def tier 2
                GameObject obj = ObjectPooler.current.GetPooledObjectSix();
                if (obj == null) return;
                else
                {
                    obj.transform.position = playerLocation.position;
                    obj.transform.rotation = playerLocation.rotation;
                    obj.SetActive(true);
                    Physics.IgnoreCollision(obj.GetComponent<Collider>(), playerLocation.gameObject.GetComponent<Collider>());
                    foreach (Collider surroundingObj in Physics.OverlapSphere(obj.transform.position, buildRadius * 2))
                    {
                        if (surroundingObj.attachedRigidbody != null)
                        {
                            Physics.IgnoreCollision(obj.GetComponent<Collider>(), surroundingObj.gameObject.GetComponent<Collider>());
                        }
                    }
                }
            }
            if (curLevel == 3)
            {
                //spawn def tier 3
                GameObject obj = ObjectPooler.current.GetPooledObjectSeven();
                if (obj == null) return;
                else
                {
                    obj.transform.position = playerLocation.position;
                    obj.transform.rotation = playerLocation.rotation;
                    obj.SetActive(true);
                    Physics.IgnoreCollision(obj.GetComponent<Collider>(), playerLocation.gameObject.GetComponent<Collider>());
                    foreach (Collider surroundingObj in Physics.OverlapSphere(obj.transform.position, buildRadius * 2))
                    {
                        if (surroundingObj.attachedRigidbody != null)
                        {
                            Physics.IgnoreCollision(obj.GetComponent<Collider>(), surroundingObj.gameObject.GetComponent<Collider>());
                        }
                    }
                }
            }
        }
        if(specID == 3)
        {
            if (curLevel == 1)
            {
                //spawn har tier 1
                GameObject obj = ObjectPooler.current.GetPooledObjectEight();
                if (obj == null) return;
                else
                {
                    obj.transform.position = playerLocation.position;
                    obj.transform.rotation = playerLocation.rotation;
                    obj.SetActive(true);
                    Physics.IgnoreCollision(obj.GetComponent<Collider>(), playerLocation.gameObject.GetComponent<Collider>());
                    foreach (Collider surroundingObj in Physics.OverlapSphere(obj.transform.position, buildRadius * 2))
                    {
                        if (surroundingObj.attachedRigidbody != null)
                        {
                            Physics.IgnoreCollision(obj.GetComponent<Collider>(), surroundingObj.gameObject.GetComponent<Collider>());
                        }
                    }
                }
            }
            if (curLevel == 2)
            {
                //spawn har tier 2
                GameObject obj = ObjectPooler.current.GetPooledObjectNine();
                if (obj == null) return;
                else
                {
                    obj.transform.position = playerLocation.position;
                    obj.transform.rotation = playerLocation.rotation;
                    obj.SetActive(true);
                    Physics.IgnoreCollision(obj.GetComponent<Collider>(), playerLocation.gameObject.GetComponent<Collider>());
                    foreach (Collider surroundingObj in Physics.OverlapSphere(obj.transform.position, buildRadius * 2))
                    {
                        if (surroundingObj.attachedRigidbody != null)
                        {
                            Physics.IgnoreCollision(obj.GetComponent<Collider>(), surroundingObj.gameObject.GetComponent<Collider>());
                        }
                    }
                }
            }
            if (curLevel == 3)
            {
                //spawn har tier 3
                GameObject obj = ObjectPooler.current.GetPooledObjectTen();
                if (obj == null) return;
                else
                {
                    obj.transform.position = playerLocation.position;
                    obj.transform.rotation = playerLocation.rotation;
                    obj.SetActive(true);
                    Physics.IgnoreCollision(obj.GetComponent<Collider>(), playerLocation.gameObject.GetComponent<Collider>());
                    foreach (Collider surroundingObj in Physics.OverlapSphere(obj.transform.position, buildRadius * 2))
                    {
                        if (surroundingObj.attachedRigidbody != null)
                        {
                            Physics.IgnoreCollision(obj.GetComponent<Collider>(), surroundingObj.gameObject.GetComponent<Collider>());
                        }
                    }
                }
            }
        }
        if (specID == 4)
        {
            if (curLevel == 1)
            {
                //spawn reb tier 1
                GameObject obj = ObjectPooler.current.GetPooledObjectEleven();
                if (obj == null) return;
                else
                {
                    obj.transform.position = playerLocation.position;
                    obj.transform.rotation = playerLocation.rotation;
                    obj.SetActive(true);
                    Physics.IgnoreCollision(obj.GetComponent<Collider>(), playerLocation.gameObject.GetComponent<Collider>());
                    foreach (Collider surroundingObj in Physics.OverlapSphere(obj.transform.position, buildRadius * 2))
                    {
                        if (surroundingObj.attachedRigidbody != null)
                        {
                            Physics.IgnoreCollision(obj.GetComponent<Collider>(), surroundingObj.gameObject.GetComponent<Collider>());
                        }
                    }
                }
            }
            if (curLevel == 2)
            {
                //spawn reb tier 2
                GameObject obj = ObjectPooler.current.GetPooledObjectTwelve();
                if (obj == null) return;
                else
                {
                    obj.transform.position = playerLocation.position;
                    obj.transform.rotation = playerLocation.rotation;
                    obj.SetActive(true);
                    Physics.IgnoreCollision(obj.GetComponent<Collider>(), playerLocation.gameObject.GetComponent<Collider>());
                    foreach (Collider surroundingObj in Physics.OverlapSphere(obj.transform.position, buildRadius * 2))
                    {
                        if (surroundingObj.attachedRigidbody != null)
                        {
                            Physics.IgnoreCollision(obj.GetComponent<Collider>(), surroundingObj.gameObject.GetComponent<Collider>());
                        }
                    }
                }
            }
            if (curLevel == 3)
            {
                //spawn reb tier 3
                GameObject obj = ObjectPooler.current.GetPooledObjectThirteen();
                if (obj == null) return;
                else
                {
                    obj.transform.position = playerLocation.position;
                    obj.transform.rotation = playerLocation.rotation;
                    obj.SetActive(true);
                    Physics.IgnoreCollision(obj.GetComponent<Collider>(), playerLocation.gameObject.GetComponent<Collider>());
                    foreach (Collider surroundingObj in Physics.OverlapSphere(obj.transform.position, buildRadius * 2))
                    {
                        if (surroundingObj.attachedRigidbody != null)
                        {
                            Physics.IgnoreCollision(obj.GetComponent<Collider>(), surroundingObj.gameObject.GetComponent<Collider>());
                        }
                    }
                }
            }
        }
    }
}
