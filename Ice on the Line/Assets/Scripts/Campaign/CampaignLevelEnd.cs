using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CampaignLevelEnd : MonoBehaviour
{
    private int starsCollected = 0;

    void Start()
    {
        InGameEvents.OnItemCollected += InGameEvents_OnItemCollected;
    }

    private void InGameEvents_OnItemCollected(ICollectible collectible)
    {
        if (collectible.ID == 11)
        {
            starsCollected++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LevelController.instance.CompleteLevel(LevelController.instance.selectedLevel.LevelName, starsCollected);
            LevelController.instance.UnlockNextLevel(LevelController.instance.selectedLevel.LevelName);
            LevelController.instance.SaveCampaignProgression();
            SceneManager.LoadScene("Campaign");
        }
    }
}
