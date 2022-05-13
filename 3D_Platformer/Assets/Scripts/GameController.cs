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
    private float minDist;
    [SerializeField]
    private float maxDist;

    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            SpawnPlatform();
        }
    }

    public void SpawnPlatform()
    {
        int rndPrefab = Random.Range(0, platformPrefabs.Length);
        lastPlatform = Instantiate(platformPrefabs[rndPrefab]
                      ,GetRandomSpawnPos(platformPrefabs[rndPrefab].Size)
                      ,Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPos(int sizePlatform)
    {
        var newDir = Random.insideUnitCircle.normalized;
        newDir *= Random.Range(minDist, maxDist);

        return new Vector3(newDir.x, lastPlatform.transform.position.y + Random.Range(1, 2), newDir.y)
         + new Vector3(lastPlatform.transform.localScale.x, 0, lastPlatform.transform.localScale.z) + new Vector3(sizePlatform, 0, sizePlatform);
    }
}
