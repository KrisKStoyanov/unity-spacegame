using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStateRegulator : MonoBehaviour {

    bool paused;

    public static GameStateRegulator current;

    public GameObject player;
    public GameObject asteroidSpawner;
    public Canvas statistics;
    public Canvas generalUI;
    public Canvas startUI;
    public Canvas restartUI;
    public Canvas buildUI;
    public Canvas settingsUI;

    public GameObject droneShield;
    public GameObject planetShied;

    //Tracked Stats
    public Text bestTime;
    public Text asteroidScore;
    public Text resourceScore;
    public float asteroidCount = 0;
    public float resourceCount = 0;

    public ParticleSystem leftNavigationInd;
    public ParticleSystem rightNavigationInd;

    public ParticleSystem planetDestruction;
    public ParticleSystem playerDestruction;

    public Button toggleMuteBtn;
    public Button masterVolBtn;
    public Button musicVolBtn;
    public Button sfxVolBtn;

    public Sprite mutedVolImage;
    public Sprite unmutedVolImage;

    //AudioRegulator
    AudioSource mainAS;
    public AudioClip startSoundtrack;
    public AudioClip playSoundtrack;
    public Slider masterVolSlider;
    public Slider musicVolSlider;
    public Slider sfxVolSlider;

    public bool muted;
    bool displayed = false;
    public float masterVol = 1;
    float musicVol = 1;
    public float sfxVol = 1;

	// Use this for initialization
	void Start () {
        mainAS = GetComponent<AudioSource>();
        mainAS.clip = startSoundtrack;
        mainAS.Play();
        current = this;
        paused = false;
        muted = false;

        masterVolSlider.value = masterVol;
        musicVolSlider.value = musicVol;
        sfxVolSlider.value = sfxVol;
        if(StatsManager.current.longestSurvivalTime > 0)
        {
            bestTime.gameObject.SetActive(true);
            bestTime.text = "Longest Survival: " + StatsManager.current.longestSurvivalTime;
            asteroidScore.gameObject.SetActive(true);
            asteroidScore.text = "Most Asteroids Destroyed: " + StatsManager.current.mostAsteroidsDestroyed;
            resourceScore.gameObject.SetActive(true);
            resourceScore.text = "Most Resources Gathered: " + StatsManager.current.mostResourcesGathered;
        }
	}

    public void ShieldPlayer()
    {
        droneShield.SetActive(true);
    }

    public void ShieldPlanet()
    {
        planetShied.SetActive(true);
    }
	
    public void TouchStart()
    {
        StartCoroutine(NavigationIndicators());
        player.SetActive(true);
        asteroidSpawner.SetActive(true);
        statistics.gameObject.SetActive(true);
        generalUI.gameObject.SetActive(true);
        startUI.gameObject.SetActive(false);
        ResourceManager.current.minerals = 0;
        //mainAS.clip = playSoundtrack;
        mainAS.Stop();
        mainAS.PlayOneShot(playSoundtrack, musicVol * masterVol);
    }

    public void TouchRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void TogglePausedGameState()
    {
        if (paused)
        {
            Time.timeScale = 1f;
            paused = false;
            ResourceManager.current.resourceText.text = "" + ResourceManager.current.minerals;

            displayed = false;

            masterVolBtn.gameObject.SetActive(true);
            musicVolBtn.gameObject.SetActive(true);
            sfxVolBtn.gameObject.SetActive(true);

            masterVolSlider.gameObject.SetActive(false);
            musicVolSlider.gameObject.SetActive(false);
            sfxVolSlider.gameObject.SetActive(false);

            settingsUI.gameObject.SetActive(false);
            //Deactivate Settings Menu
        }
        else
        {
            Time.timeScale = 0f;
            paused = true;
            ResourceManager.current.resourceText.text = "PAUSED";
            settingsUI.gameObject.SetActive(true);
            //Activate Settings Menu
        }
    }

    public void FailGameState()
    {
        //player.SetActive(false);
        asteroidSpawner.SetActive(false);
        //statistics.gameObject.SetActive(true);
        if(GravitySimulator.current.lostSignal == true)
        {
            planetDestruction.Play();
            ResourceManager.current.resourceText.text = "LOST SIGNAL";
        }
        else
        {
            playerDestruction.transform.position = player.transform.position;
            playerDestruction.Play();
            ResourceManager.current.resourceText.text = "DESTROYED";
        }
        generalUI.gameObject.SetActive(false);
        buildUI.gameObject.SetActive(false);
        restartUI.gameObject.SetActive(true);
        if(Time.time > StatsManager.current.longestSurvivalTime)
        {
            StatsManager.current.longestSurvivalTime = Time.time;
            StatsManager.current.Save();
        }
        if(asteroidCount > StatsManager.current.mostAsteroidsDestroyed)
        {
            StatsManager.current.mostAsteroidsDestroyed = asteroidCount;
            StatsManager.current.Save();
        }
        if(resourceCount > StatsManager.current.mostResourcesGathered)
        {
            StatsManager.current.mostResourcesGathered = resourceCount;
            StatsManager.current.Save();
        }
    }

    public void ToggleAudioMute()
    {
        if (muted)
        {
            mainAS.mute = false;
            muted = false;
            toggleMuteBtn.image.sprite = unmutedVolImage;
        }
        else
        {
            mainAS.mute = true;
            muted = true;
            toggleMuteBtn.image.sprite = mutedVolImage;
        }
    }

    public void ModifyMasterVol()
    {
        masterVol = masterVolSlider.value;
        mainAS.volume = musicVol * masterVol;
    }

    public void ModifyMusicVol()
    {
        musicVol = musicVolSlider.value;
        mainAS.volume = musicVol * masterVol;
    }

    public void ModifySfxVol()
    {
        sfxVol = sfxVolSlider.value;
    }

    public void ToggleSliderDisplay(int id)
    {
        if(id == 1)
        {
            if (displayed)
            {
                masterVolSlider.gameObject.SetActive(false);
                displayed = false;
                musicVolBtn.gameObject.SetActive(true);
                sfxVolBtn.gameObject.SetActive(true);
            }
            else
            {
                masterVolSlider.gameObject.SetActive(true);
                displayed = true;
                musicVolBtn.gameObject.SetActive(false);
                sfxVolBtn.gameObject.SetActive(false);
            }
        }

        if (id == 2)
        {
            if (displayed)
            {
                musicVolSlider.gameObject.SetActive(false);
                displayed = false;
                masterVolBtn.gameObject.SetActive(true);
                sfxVolBtn.gameObject.SetActive(true);

            }
            else
            {
                musicVolSlider.gameObject.SetActive(true);
                displayed = true;
                masterVolBtn.gameObject.SetActive(false);
                sfxVolBtn.gameObject.SetActive(false);
            }
        }

        if (id == 3)
        {
            if (displayed)
            {
                sfxVolSlider.gameObject.SetActive(false);
                displayed = false;
                masterVolBtn.gameObject.SetActive(true);
                musicVolBtn.gameObject.SetActive(true);
            }
            else
            {
                sfxVolSlider.gameObject.SetActive(true);
                displayed = true;
                masterVolBtn.gameObject.SetActive(false);
                musicVolBtn.gameObject.SetActive(false);
            }
        }
    }
    private IEnumerator NavigationIndicators()
    {
        bool playedLeft = false;
        bool playedRight = false;
        while (true)
        {
            if (!leftNavigationInd.isPlaying && !playedLeft)
            {
                leftNavigationInd.Play();
                playedLeft = true;
            }
            if (!leftNavigationInd.isPlaying && playedLeft && !playedRight)
            {
                rightNavigationInd.Play();
                playedRight = true;
            }
            if(playedLeft && playedRight && !rightNavigationInd.isPlaying)
            {
                StopCoroutine(NavigationIndicators());
            }
            yield return null;
        }
    }
}
