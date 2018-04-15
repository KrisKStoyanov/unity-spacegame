using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour
{

    public static StatsManager current { get; private set; }

    public float longestSurvivalTime = 0;
    public float mostAsteroidsDestroyed = 0;
    public float mostResourcesGathered = 0;

    public int deathCounter = 0;

    private void Awake()
    {
        current = this;
        Load();
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        data.playerResourceCounter = mostResourcesGathered;
        data.playerAsteroidCounter = mostAsteroidsDestroyed;
        data.playerBestTime = longestSurvivalTime;
        data.playerDeaths = deathCounter;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            mostResourcesGathered = data.playerResourceCounter;
            mostAsteroidsDestroyed = data.playerAsteroidCounter;
            longestSurvivalTime = data.playerBestTime;
            deathCounter = data.playerDeaths;
        }
    }
}

[Serializable]
class PlayerData
{
    public float playerResourceCounter;
    public float playerAsteroidCounter;
    public float playerBestTime;
    public int playerDeaths;
}

