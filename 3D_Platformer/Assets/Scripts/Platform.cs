using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SpawnDirection
{
    Top,
    RightTop,
    Right,
    RightDown,
    Down,
    LeftDown,
    Left,
    LeftTop
}
public class Platform : MonoBehaviour
{
    [SerializeField]
    private int size;
    [SerializeField]
    private float spawnCoinChance;
    [SerializeField]
    private Coin coinPrefab;
    public int Size => size;
    [SerializeField]
    private bool randomSize;
    [SerializeField]
    private bool spawnCoin;
    [SerializeField]
    private int minSize;
    [SerializeField]
    private int maxSize;

    [SerializeField]
    private GameObject enemyPlatform;
    
    public Vector3 SetRandomSize()
    {
        if (randomSize)
        {
            int rndSize = Random.Range(minSize, maxSize+1);
            size = rndSize;
            Vector3 scaleSize = new Vector3(size, size / 2, size);
            return scaleSize;
        }

        return transform.localScale;
    }

    private Coin currentCoin = null;
    public void AcitavatePlatform()
    {
        if (size == 7)
        {
            enemyPlatform.SetActive(true);
        }
        else if (size<7 && spawnCoin)
        {
            float rndDrop = Random.Range(0.01f, 100f);
            if (rndDrop <= spawnCoinChance)
            {
                var coin = PoolController.Instance.PoolCoin.GetFreeElement();
                coin.transform.position = transform.position + new Vector3(0f, 3f, 0f);
                currentCoin = coin;
            }
        }
    }

    public void DestroyPlatform()
    {
        if (currentCoin.gameObject.activeSelf && currentCoin != null)
        {
            gameObject.SetActive(false);
            currentCoin.gameObject.SetActive(false);
            currentCoin = null;
        }
    }
}
