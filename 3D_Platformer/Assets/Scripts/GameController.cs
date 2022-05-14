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
    public bool isStartGame = true;
    [SerializeField]
    private GameObject gamePanel;
    [SerializeField]
    private GameObject startPanel;
    [SerializeField]
    private TMP_Text textTopScore;
    private int score;
    public int Score => score;
    [SerializeField]
    private TMP_Text textScore;
    public static GameController Instance { get; private set; }

    private void Awake()
    {
        Application.targetFrameRate = 60;

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
        UpdateTopScoreText();
    }
    private void Update()
    {
        CheckStartUi();
    }

    private void CheckStartUi()
    {
        if (isStartGame)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 || Input.GetAxis("Jump") != 0)
            {
                gamePanel.SetActive(true);
                startPanel.SetActive(false);
                isStartGame = false;
            }
        }
    }

    private void UpdateTopScoreText()
    {
        int topScore = PlayerPrefs.GetInt("TopScore",0);
        textTopScore.text = "TOP SCORE" + "\n" + topScore.ToString();
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

    public void ExitGame()
    {
        Application.Quit();
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
