using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

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
    private GameObject losePanel;
    [SerializeField]
    private TMP_Text textTopScore;
    [SerializeField]
    private TMP_Text textLoseScore;
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnPlatform();
        }
    }

    private void CheckStartUi()
    {
        if (isStartGame)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 || Input.GetAxis("Jump") != 0)
            {
                gamePanel.SetActive(true);
                startPanel.SetActive(false);
                losePanel.SetActive(false);
                isStartGame = false;
            }
        }
    }

    private void UpdateTopScoreText()
    {
        startPanel.SetActive(true);
        int topScore = PlayerPrefs.GetInt("TopScore",0);
        textTopScore.text = "TOP SCORE" + "\n" + topScore.ToString();
    }

    public void SpawnPlatform()
    {
        DestroyPlatform();
        backPlatform = currentPlatform;

        var platform = PoolController.Instance.PoolPlatofrm.GetFreeElement();
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
            backPlatform.DeactivatePlatform();
            //UpdateScore();
        }
    }

    public void UpdateScore()
    {
        score++;
        textScore.text = "SCORE "+score.ToString();
    }

    public void ActivateLosePanel()
    {
        gamePanel.SetActive(false);
        startPanel.SetActive(false);
        losePanel.SetActive(true);
        textLoseScore.text = "SCORE "+score.ToString();
    }

    public void RestartGame()
    {
        int topScore = PlayerPrefs.GetInt("TopScore", 0);
        isStartGame = true;
        if (GameController.Instance.Score > topScore)
        {
            PlayerPrefs.SetInt("TopScore", GameController.Instance.Score);
        }
        SceneManager.LoadScene(0);
    }
    private Vector3 GetRandomSpawnPos(int sizePlatform)
    {
        Vector3 rndVect3; 

        SpawnDirection rndDirection = (SpawnDirection)Random.Range(0, Enum.GetValues(typeof(SpawnDirection)).Length);
        
        float rndDist = Random.Range(minDist, maxDist);

        Debug.Log(rndDirection);

        switch (rndDirection)
        {
            case SpawnDirection.Top:
                rndVect3 = new Vector3(0f, 0f,rndDist);
                break;
            case SpawnDirection.RightTop:
                rndVect3 = new Vector3(rndDist, 0f, rndDist);
                break;
            case SpawnDirection.Right:
                rndVect3 = new Vector3(rndDist, 0f, 0f);
                break;
            case SpawnDirection.RightDown:
                rndVect3 = new Vector3(rndDist, 0f, -rndDist);
                break;
            case SpawnDirection.Down:
                rndVect3 = new Vector3(0f, 0f, -rndDist);
                break;
            case SpawnDirection.LeftDown:
                rndVect3 = new Vector3(-rndDist, 0f, -rndDist);
                break;
            case SpawnDirection.Left:
                rndVect3 = new Vector3(-rndDist, 0f,0f);
                break;
            case SpawnDirection.LeftTop:
                rndVect3 = new Vector3(-rndDist, 0f, rndDist);
                break;
            default:
                rndVect3 = new Vector3(0f, 0f, 0f);
                break;
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
