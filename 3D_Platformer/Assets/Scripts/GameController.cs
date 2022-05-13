using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Platform[] platformPrefabs;
    [SerializeField]
    private Platform lastPlatform;
    [SerializeField]
    private int minUpDistSpawn;
    [SerializeField]
    private int maxUpDistSpawn;
    [SerializeField]
    private int minForwardDistSpawn;
    [SerializeField]
    private int maxForwardDistSpawn;
    private SpawnDirection lastSpawnDirection;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnPlatform();
        }
    }

    private void SpawnPlatform()
    {
        int rndPrefab = Random.Range(0, platformPrefabs.Length);
        lastPlatform = Instantiate(platformPrefabs[rndPrefab]
                      ,lastPlatform.transform.position+GetRandomSpawnPos(platformPrefabs[rndPrefab].Size)
                      ,Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPos(int sizePlatform)
    {
        lastSpawnDirection = (SpawnDirection)Random.Range(0, 3);
    

        int rndUp = Random.Range(minUpDistSpawn, maxUpDistSpawn);
        int rndForward = Random.Range(minForwardDistSpawn, maxForwardDistSpawn);

        switch (lastSpawnDirection)
        {
            case SpawnDirection.forward:
                return new Vector3(0f, rndUp, sizePlatform+rndForward);
            case SpawnDirection.left:
                return new Vector3(-(sizePlatform+rndForward), rndUp, 0f);
            case SpawnDirection.right:
                return new Vector3(sizePlatform+rndForward, rndUp, 0f);
            case SpawnDirection.back:
                return new Vector3(0f, rndUp,-(sizePlatform+rndForward));
            default:
                return new Vector3(0f, 0f, 0f);
        }
    }
}
