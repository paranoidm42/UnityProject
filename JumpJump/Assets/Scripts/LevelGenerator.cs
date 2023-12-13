
using UnityEngine;


public class LevelGenerator : MonoBehaviour
{
    float lastPositionY;
    float swapCameraWeight;
    float randomPositionX;
    float randomPositionY;
    float playerYPosition;
    int levelUpDistance;

    bool stickRespawnCheck;
    bool nextLevelGeneratorCheck;

    Vector2 xPosition;
    Vector2 lastPositionVector;
    PlayerController playerController;
    ObjectPool objectPool;
    void Start()
    {
        levelUpDistance = 75;
        lastPositionY = 0.5f;
        objectPool = ObjectPool.Instance;
        swapCameraWeight = CameraCalculator.instance.CameraWeight;
        playerController = PlayerController.Instance;
    }
    

    void Update()
    {
        playerYPosition = playerController.playerPositionY;
        stickRespawnCheck = FastApproximately(playerYPosition, lastPositionY, 10);

        if(lastPositionY < 1)
        {
            WhenGameStart();
        }

        if(stickRespawnCheck)
        {
            
            GenerateNextLevel();
        }
    }
    void GenerateNextLevel()
    {
        PlatformSpawner();
        nextLevelGeneratorCheck = FastApproximately(lastPositionY, levelUpDistance, 5);
        if(nextLevelGeneratorCheck)
        {
            WhenGameStart();
            levelUpDistance += 75;
        }
    }
    void WhenGameStart()
    {
        objectPool.SpawnFromPool("Ground", new Vector2(0, lastPositionY));
        objectPool.SpawnFromPool("Left", new Vector2(-swapCameraWeight, lastPositionY + 40));
        objectPool.SpawnFromPool("Right", new Vector2(swapCameraWeight, lastPositionY + 40));
    }

    void PlatformSpawner()
    {           
        lastPositionVector =  RandomPositionGenerator();
        objectPool.SpawnFromPool(RandomObjectSpawnValue(), lastPositionVector);
        
        BuffSpawner();
    }

    Vector2 RandomPositionGenerator()
    {
        randomPositionX = Random.Range(-swapCameraWeight + 1.56f, swapCameraWeight - 1.56f);
        randomPositionY = Random.Range(3,5);
        lastPositionY += randomPositionY;
        return new Vector2(randomPositionX, lastPositionY);
    }

    string RandomObjectSpawnValue()
    {
        int randomPanelType = Random.Range(1, 101);

        if(randomPanelType <= 60)
            return "Platform";
        else
            return "DynamicPlatform";

    }
    void BuffSpawner()
    {
        
        int randomBuff = Random.Range(1, 101);
        int swapRandom = randomBuff;
        if(swapRandom != randomBuff && randomBuff >= 80)
            objectPool.SpawnFromPool("AppleBuff", lastPositionVector * 1.2f);
        
    }
    


    public bool FastApproximately(float a, float b, float threshold) 
    {
        float difference = Mathf.Abs(a - b);
        return difference <= threshold || difference < Mathf.Epsilon;
    }

}
