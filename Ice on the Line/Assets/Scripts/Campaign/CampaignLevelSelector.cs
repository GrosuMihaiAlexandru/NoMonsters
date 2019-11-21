using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignLevelSelector : MonoBehaviour
{
    //The list of Game Levels
    public List<GameObject> levels;

    public GameObject selectedLevel;

    // Start is called before the first frame update
    void Awake()
    {
        Vector2 spawnPos = new Vector2(1, 0);
        selectedLevel = Instantiate(levels[LevelController.instance.selectedLevel.ID], spawnPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
