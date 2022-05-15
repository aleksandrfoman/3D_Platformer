using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolCoins : MonoBehaviour
{
    [SerializeField]
    private int poolCount = 1;
    [SerializeField]
    private bool autoExpand = false;
    [SerializeField]
    private Coin prefab;
    public PoolMono<Coin> Pool { get; private set; }

    public static PoolCoins Instance { get; private set; }

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
        this.Pool = new PoolMono<Coin>(this.prefab, this.poolCount, this.transform);
        this.Pool.autoExpand = autoExpand;
    }
}
