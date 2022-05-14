using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [Header("Generation")]
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
    [Header("Ui")]
    private int score;
    public int Score => score;
    [SerializeField]
    private TMP_Text textScore;
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

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnPlatform();
        }
    }

    public void SpawnPlatform()
    {
        DestroyPlatform();
        backPlatform = currentPlatform;
        Platform platform = Instantiate(platformPrefab
                      , Vector3.zero
                      , Quaternion.identity);

        Vector3 size = platform.SetRandomSize();
        platform.transform.position = GetRandomSpawnPos((int) size.x);
        platform.transform.localScale = size;
        platform.AcitavatePlatform();
        nextPlatform = platform;
        currentPlatform = nextPlatform;
    }

    private void DestroyPlatform()
    {
        if (backPlatform != null)
        {
            Destroy(backPlatform.gameObject);
            UpdateScore();
        }
    }

    public void UpdateScore()
    {
        score++;
        textScore.text = "SCORE:" + ""+score.ToString();
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


        var pos = new Vector3(rndVect3.x, currentPlatform.transform.localScale.y + rndY, rndVect3.z);

        pos += new Vector3(sizePlatform * Mathf.Sign(rndVect3.x), 0, sizePlatform * Mathf.Sign(rndVect3.z)) / 2f;
        pos += new Vector3(currentPlatform.Size * Mathf.Sign(rndVect3.x), 0, currentPlatform.Size * Mathf.Sign(rndVect3.z)) / 2f;

        pos += rndVect3;
        
        pos += currentPlatform.transform.position;
        
        return pos;
    }
}
