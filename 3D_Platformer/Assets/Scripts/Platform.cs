using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ToDo скользящие платформы предметы на платформах
public class Platform : MonoBehaviour
{
    [SerializeField]
    private int size;
    public int Size => size;
    [SerializeField]
    private bool randomSize;
    [SerializeField]
    private int minSize;
    [SerializeField]
    private int maxSize;

    public Vector3 SetRandomSize()
    {
        if (randomSize)
        {
            int rndSize = Random.Range(minSize, maxSize);
            size = rndSize;
            Vector3 scaleSize = new Vector3(size, size / 2, size);
            return scaleSize;
        }

        return transform.localScale;
    }
}
