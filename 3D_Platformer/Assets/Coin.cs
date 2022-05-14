using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private GameObject coinEffect;
    [SerializeField]
    private float rotateSpeed;
    void Update()
    {
        transform.Rotate(new Vector3(0f, 1f*rotateSpeed, 0f)*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        {
            Instantiate(coinEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameController.Instance.UpdateScore();
        }    
    }
}
