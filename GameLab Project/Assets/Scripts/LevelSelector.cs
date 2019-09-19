using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (GameManager))]
public class LevelSelector : MonoBehaviour
{

    // The list of the random levels to be selected
    public List<GameObject> levels;

    GameManager game;

    private int currentLevel;

    // The positions where the levels will have to be instanciated
    private Vector2 currentPosition;
    private Vector2 nextPosition;

    [SerializeField]
    private int levelLength;

    // The y coordinate that represents the end of the current level;
    private int endOfCurrentLevel;

    // Check if the player reached the end of the current level
    private bool endOfLevelReached;

    // Start is called before the first frame update
    void Awake()
    {
        game = GetComponent<GameManager>();
        currentPosition = new Vector2(1, 0);
        nextPosition = currentPosition + new Vector2(0, levelLength);
        Instantiate(levels[SelectLevel(levels.Count - 1)], currentPosition, Quaternion.identity);
        Instantiate(levels[SelectLevel(levels.Count - 1)], nextPosition, Quaternion.identity);
        endOfCurrentLevel = (int)(currentPosition.y + levelLength);

        //Debug.Log(currentPosition + " " + nextPosition);
    }

    // Update is called once per frame
    void Update()
    {
        // Update Levels only while player is still alive
        if (game.playerAlive)
        {
            // Check if the player reached the end of the current level
            if (game.player.transform.position.y - endOfCurrentLevel >= -0.1f)
            {
                endOfLevelReached = true;
            }

            // If end of level is reached then update the current level to next level and instantiate the next level
            if (endOfLevelReached)
            {
                Debug.Log("End of Level Reached");
                endOfLevelReached = false;
                currentPosition = nextPosition;
                nextPosition = currentPosition + new Vector2(0, levelLength);
                Debug.Log(nextPosition);
                endOfCurrentLevel = (int)(currentPosition.y + levelLength);
                Instantiate(levels[SelectLevel(levels.Count - 1)], nextPosition, Quaternion.identity);
            }
        }
    }

    public int SelectLevel(int maxLevel)
    {
        int r = Random.Range(0, maxLevel);
        return r;
    }
 
}
