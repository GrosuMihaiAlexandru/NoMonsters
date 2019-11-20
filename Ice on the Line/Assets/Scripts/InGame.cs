using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InGame : MonoBehaviour
{
    public static bool playerAlive;
    public static bool gamePaused;

    private bool movesLeft = true;

    public CharacterController player;

    private List<IceBlock> iceblocks = new List<IceBlock>();

    public GameObject gameOverScreen;

    public bool isTutorial;
    public bool isEndless;

    // Start is called before the first frame update
    void Start()
    {
        gamePaused = false;
        playerAlive = true;
        player = GameObject.Find("Player").GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        // Losing condition shouldn't work during the tutorial
        if (!isTutorial)
        {
            if (isEndless)
            {
                iceblocks = GetComponent<LevelSelector>().ThisLevel.GetComponentsInChildren<IceBlock>().ToList();
            }
            else
            {
                iceblocks = GetComponent<CampaignLevelSelector>().selectedLevel.GetComponentsInChildren<IceBlock>().ToList();

            }
            movesLeft = false;
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            for (int i = 0; i < iceblocks.Count; i++)
            {
                if (GeometryUtility.TestPlanesAABB(planes, iceblocks[i].transform.GetComponent<Renderer>().bounds))
                {
                    movesLeft = true;
                }
            }
            //Debug.Log(GetComponent<LevelSelector>().ThisLevel);
            //Debug.Log(movesLeft);

            if (playerAlive)
            {
                if (!movesLeft)
                {
                    Destroy(player.gameObject);
                    playerAlive = false;
                    gameOverScreen.SetActive(true);
                    Time.timeScale = 0;
                    GameManager.instance.SaveProgress();
                }
            }
        }
    }
}
