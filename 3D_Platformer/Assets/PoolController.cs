using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField]
    private int poolCoinCount = 1;
    [SerializeField]
    private bool autoCoinExpand = false;
    [SerializeField]
    private Coin prefabCoin;
    public PoolMono<Coin> PoolCoin { get; private set; }

    [Header("Platforms")]
    [SerializeField]
    private int poolPlatformCount = 1;
    [SerializeField]
    private bool autoPlatformExpand = false;
    [SerializeField]
    private Platform prefabPlatform;
    public PoolMono<Platform> PoolPlatofrm { get; private set; }


    public static PoolController Instance { get; private set; }

    private void Awake()
    {
        PoolCoin = new PoolMono<Coin>(prefabCoin,poolCoinCount,transform);
        PoolCoin.autoExpand = autoCoinExpand;

        PoolPlatofrm = new PoolMono<Platform>(prefabPlatform, poolPlatformCount,transform);
        PoolPlatofrm.autoExpand = autoPlatformExpand;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
