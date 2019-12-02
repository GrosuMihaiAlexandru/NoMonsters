using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (InGame))]
public class LevelSelector : MonoBehaviour
{
    private InGame game;

    // The list of the random levels to be selected
    public List<GameObject> levels;

    private int currentLevel = -1;

    // The positions where the levels will have to be instanciated
    private Vector2 oldPosition;
    private Vector2 currentPosition;
    private Vector2 nextPosition;

    [SerializeField]
    private int levelLength;

    // The y coordinate that represents the end of the current level;
    private int endOfCurrentLevel;

    // Check if the player reached the end of the current level
    private bool endOfLevelReached;

    private bool canDestroyLevel = false;

    // The level that is deleted after instantiating the new one
    private GameObject oldLevel;
    private GameObject thisLevel;
    private GameObject nextLevel;


    // Start is called before the first frame update
    void Awake()
    {
        currentPosition = new Vector2(1, 0);
        nextPosition = currentPosition + new Vector2(0, levelLength);
        thisLevel = Instantiate(levels[SelectLevel(levels.Count - 1)], currentPosition, Quaternion.identity);
        nextLevel = Instantiate(levels[SelectLevel(levels.Count - 1)], nextPosition, Quaternion.identity);
        endOfCurrentLevel = (int)(currentPosition.y + levelLength);
    }

    void Start()
    {
        game = GetComponent<InGame>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update Levels only while player is still alive
        if (InGame.playerAlive)
        {
            // Check if the player reached the end of the current level
            if (game.player.transform.position.y - endOfCurrentLevel >= -0.1f)
            {
                endOfLevelReached = true;
            }

            // If end of level is reached then update the current level to next level and instantiate the next level
            if (endOfLevelReached)
            {
                oldLevel = thisLevel;
                oldPosition = currentPosition;
                thisLevel = nextLevel;
                //Debug.Log("End of Level Reached");
                endOfLevelReached = false;
                currentPosition = nextPosition;
                nextPosition = currentPosition + new Vector2(0, levelLength);
                //Debug.Log(nextPosition);
                endOfCurrentLevel = (int)(currentPosition.y + levelLength);
                nextLevel = Instantiate(levels[SelectLevel(levels.Count - 1)], nextPosition, Quaternion.identity);

                canDestroyLevel = true;
            }
            if (canDestroyLevel && game.player.transform.position.y - currentPosition.y >= 5f)
            {
                //Debug.Log(game.player.transform.position.y + " " + currentPosition.y);
                Destroy(oldLevel);
                canDestroyLevel = false;
            }
        }
    }

    public int SelectLevel(int maxLevel)
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        int r = Random.Range(0, maxLevel + 1);
        while (r == currentLevel)       
        {
            r = Random.Range(0, maxLevel + 1);
        }
        currentLevel = r;
        return r;
    }
 
    public GameObject ThisLevel { get { return thisLevel; } set { thisLevel = value; } }
}
