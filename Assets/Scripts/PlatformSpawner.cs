using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlatformGroupType
{
    Grass,
    Winter
}

public class PlatformSpawner : MonoBehaviour
{

    float nextXPos = 0.554f, nextYPos = 0.645f;
    public float NextXPos{ get { return nextXPos; }}
    public float NextYPos { get { return nextYPos; } }


    public GameObject[] platformPrefabs;
    GameObject platformPrefab;
    public GameObject CurrPlatformPrefab { get { return platformPrefab; } }

    public GameObject[] winterObstaclePlatformPrefabs;
    public GameObject[] grassObstaclePlatformPrefabs;

    public int spawnPlatformCount;
    public Vector3 initialPlatformPos;
    Vector3 currPlatformPos;
    bool isLeft = false;

    private PlatformGroupType groupType;

    public static PlatformSpawner instance;

    Vector3 obstaclePos;
    Vector3 afterObstaclePlatformPos;
    int afterSpikeSpawnCount;
    int maxAfterSpikeSpawnCount=4;
    bool ObstacleSpawnLeft = false;

    public GameObject diamondPrefab;

    private void Awake()
    {
        instance = this;
        currPlatformPos = initialPlatformPos;
        spawnPlatformCount = 5;
        afterSpikeSpawnCount = 0; 

    }

    private void Start()
    {
        //randomize a theme;
        RandomPlatformTheme();

        for (int i = 0; i < 5; i++)
        { 
            DecidePath();
        }
    }



    public void DecidePath()
    {
        if (spawnPlatformCount > 0)
        {
            spawnPlatformCount--;
            SpawnPlatform();
        }
        else
        {
            isLeft = !isLeft;
            spawnPlatformCount = Random.Range(1, 5);
            SpawnPlatform();
        }

        if (afterSpikeSpawnCount > 0)
        {
            SpawnPlatformAfterObstacle();
        }

    }

    void SpawnPlatform()
    {
        
        GameObject newPlatform = Instantiate(platformPrefab, transform);

        if (isLeft)
        {
            currPlatformPos = new Vector3(currPlatformPos.x - nextXPos, currPlatformPos.y + nextYPos, currPlatformPos.z);
        }
        else
        {
            currPlatformPos = new Vector3(currPlatformPos.x + nextXPos, currPlatformPos.y + nextYPos, currPlatformPos.z);
        }

        newPlatform.transform.localPosition = currPlatformPos;

        if (spawnPlatformCount == 0)
        {
            SpawnObstacle();
        }

        //spawn diamond;
        SpawnDiamond(currPlatformPos);


    }

    void SpawnObstacle()
    {   
        //the obstacles have two theme, winter and grass, each will spawn different kinds of obstacles.
        var obstaclePlatformPrefabs = groupType == PlatformGroupType.Winter ? winterObstaclePlatformPrefabs : grassObstaclePlatformPrefabs;
            
        int index = Random.Range(0, obstaclePlatformPrefabs.Length);
        GameObject newObstacle = Instantiate(obstaclePlatformPrefabs[index],transform);
       
        if (index == obstaclePlatformPrefabs.Length-1)
        {
            //a special spike obstacle platform (with animation) was added as the last element of both winterObstaclePlatformPrefabs and grassObstaclePlatformPrefabs arrays.
            //whenever a spike obstacle is instantiated, it will create platform branch to confuse the players.
            afterSpikeSpawnCount = maxAfterSpikeSpawnCount;
            //initialize the platforms to be spawned after the spike platform.
        }

        float obstacleXPosCoeffi = isLeft ? -1 : 1;
        obstaclePos = new Vector3(currPlatformPos.x + obstacleXPosCoeffi * nextXPos, currPlatformPos.y+nextYPos, currPlatformPos.z);
        
        newObstacle.transform.localPosition = obstaclePos;
        //save current obstaclePos for later branch platform spawning.
        

    }

    void SpawnPlatformAfterObstacle()
    {
        if (afterSpikeSpawnCount == maxAfterSpikeSpawnCount)
        {
            afterObstaclePlatformPos = obstaclePos;
            ObstacleSpawnLeft = isLeft; //set the platform branch spawning orientation
        }

        GameObject newPlatformAfterObstacle = Instantiate(platformPrefab, transform);

        if (ObstacleSpawnLeft)
        {
            afterObstaclePlatformPos = new Vector3(afterObstaclePlatformPos.x - nextXPos, afterObstaclePlatformPos.y + nextYPos, afterObstaclePlatformPos.z);
        }
        else
        {
            afterObstaclePlatformPos = new Vector3(afterObstaclePlatformPos.x + nextXPos, afterObstaclePlatformPos.y + nextYPos, afterObstaclePlatformPos.z);
        }

        newPlatformAfterObstacle.transform.localPosition = afterObstaclePlatformPos;

        afterSpikeSpawnCount--;
    }

    private void RandomPlatformTheme()
    {
        int ran = Random.Range(0, platformPrefabs.Length - 1);
        platformPrefab = platformPrefabs[ran];

        //the index 2 is winter theme platform.
        if (ran == 2)
        {
            groupType = PlatformGroupType.Winter;
        }
        else
        {
            groupType = PlatformGroupType.Grass;
        }
        
    }


    void SpawnDiamond(Vector3 platformPos)
    {
        int index = Random.Range(0, 10);
        if (index == 1)
        {
            GameObject newDiamond = Instantiate(diamondPrefab, transform);
            newDiamond.transform.localPosition = new Vector3(platformPos.x, platformPos.y + 0.4f, platformPos.z);
        }
    }
}
