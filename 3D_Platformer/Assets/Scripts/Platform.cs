using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnDirection
{
    forward,
    left,
    right,
    back
}

//ToDo ловушка на платформе, скользкая платформа , разные размеры платформ
public class Platform : MonoBehaviour
{
    [SerializeField]
    private int size;
    public int Size => size;
}
