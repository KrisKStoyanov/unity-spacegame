using System;
using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class AdManager : MonoBehaviour
{
    public void ShowRewardedAd()
    {
        StatsManager.current.deathCounter += 1;
        if(StatsManager.current.deathCounter >= 3)
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
            print("ads played");
            StatsManager.current.deathCounter = 0;
        }
        StatsManager.current.Save();
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}
