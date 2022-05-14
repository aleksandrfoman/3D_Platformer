using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Platform platformPrefab;

    [SerializeField]
    private Platform backPlatform;
    [SerializeField]
    private Platform currentPlatform;
    public Platform CurrentPlatform => currentPlatform;
    [SerializeField]
    private Platform nextPlatform;
    public Platform NextPlatform => nextPlatform;
    [SerializeField]
    private float minDist;
    [SerializeField]
    private float maxDist;
    [SerializeField]
    private int minY;
    [SerializeField]
    private int maxY;

    //Паттерн одиночка
    public static GameController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnPlatform();
        }
    }

    //Платформа знает об предедущей и об будущей платформе и таким образом спавнится
    public void SpawnPlatform()
    {
        DestroyPlatform();
        backPlatform = currentPlatform;
        nextPlatform = Instantiate(platformPrefab
                      , GetRandomSpawnPos(platformPrefab.Size)
                      , Quaternion.identity);
        currentPlatform = nextPlatform;
    }

    private void DestroyPlatform()
    {
        if (backPlatform != null)
        {
            Destroy(backPlatform.gameObject);
        }
    }

    private Vector3 GetRandomSpawnPos(int sizePlatform)
    {
        Vector3 rndVect3 = new Vector3(Random.Range(minDist, maxDist),0f,Random.Range(minDist, maxDist));

        if (Random.Range(0, 2) == 1)
        {
            rndVect3.x *= -1;
        }
        if (Random.Range(0, 2) == 1)
        {
            rndVect3.z *= -1;
        }

        int rndY = Random.Range(minY, maxY);
        Debug.DrawRay(currentPlatform.transform.position, rndVect3 * 10f, Color.black, 10f);
        return new Vector3(rndVect3.x, currentPlatform.transform.position.y+rndY, rndVect3.z) + new Vector3(currentPlatform.Size, 0, currentPlatform.Size) 
                           + new Vector3(sizePlatform, 0, sizePlatform);
    }
}
