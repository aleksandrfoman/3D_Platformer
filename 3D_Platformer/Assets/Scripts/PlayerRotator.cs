using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private Transform mesh;
    [SerializeField] private float rotSpeed;

    void Update()
    {
        var dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        var nextRot = Quaternion.Euler(new Vector3(0, angle, 0));

        mesh.transform.rotation = Quaternion.Lerp(mesh.transform.rotation, nextRot, rotSpeed * Time.deltaTime);
    }
}

