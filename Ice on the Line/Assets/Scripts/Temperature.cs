using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temperature : MonoBehaviour
{
    // The temperature indicates how fast the ice melts in the game
    [SerializeField]
    private int globalTemperature;

    // The globalTemperature starting point
    [SerializeField]
    private int initialTemperature;

    public int InitialTemperature
    {
        get
        {
            return initialTemperature;
        }
    }

    // The time between temperature rising update
    [SerializeField]
    private float temperatureUpdateTime = 1f;

    // The last time the globalTemperature was updated
    private float lastUpdateTime;

    [SerializeField]
    private int maxTemperature = 50;
    public int MaxTemperature { get { return maxTemperature; } set { maxTemperature = value; } }

    // Variables that affect the temperature increase

    // The minimum distance after which the difficulty starts increasing
    public int minX;
    // Base time per level that is added to the temperatureUpdateTime 
    public float difficultyScaling = 0.05f;

    private InGame ingame;

    // Start is called before the first frame update
    void Start()
    {
        globalTemperature = initialTemperature;
        lastUpdateTime = Time.time;
        ingame = GetComponent<InGame>();
    }


    // Update is called once per frame
    void Update()
    {
        if (InGame.playerAlive)
        {
            float time = Time.time;

            // Temperature rises by 1 degree everytime temperatureUpdateTime
            if (time - lastUpdateTime >= CalculateTemperatureTime(CalculateDifficulty(ingame.player.transform.position.y), GameManager.instance.GetUpgradeLevels(GameManager.Upgrade.temperatureSpeed)))
            {
                if (globalTemperature < MaxTemperature)
                {
                    globalTemperature++;

                    // Update the last time the temperature increased
                    lastUpdateTime = Time.time;
                }
            }
        }
    }

    // returns the difficulty of the game based on current distance
    private int CalculateDifficulty(float distance)
    {
        if (distance < minX)
            return 0;
        else if (distance < minX * 2)
            return 1;
        else if (distance < minX * 3)
            return 2;
        else
            return 3;
    }

    // Return the base temperature update time based on the current difficulty
    private int BaseTemperature(int difficulty)
    {
        return 4 - difficulty;
    }

    // Function which calculates how much time is added per upgrade level based on the current difficulty
    private float LevelTemperature(int difficulty)
    {
        return difficulty * difficultyScaling + difficultyScaling;
    }

    public float CalculateTemperatureTime(int difficulty, int upgradeLevel)
    {
        int temperature = BaseTemperature(difficulty);
        float levelTemperature = LevelTemperature(difficulty);

        return temperature + levelTemperature * upgradeLevel;
    }

    // Activated by collecting lowering temperature powerups
    public void LowerTemperature(int amount)
    {
        globalTemperature -= amount;
    }

    public int GlobalTemperature{
        get
        {
            return globalTemperature;
        }
        set
        {
            globalTemperature = value;
        }
    }
}
