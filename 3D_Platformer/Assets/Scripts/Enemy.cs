using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;
    [SerializeField]
    private Transform currentPoint;
    [SerializeField]
    private float moveSpeed;
    private Vector3 direction;
    [SerializeField]
    private GameObject mesh;
    [SerializeField]
    private float rotSpeed;

    private void Start()
    {
        RndPoint();
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, currentPoint.position) <= 0.3f)
        {
            RndPoint();
        }
        RotateMesh();
    }

    private void RndPoint()
    {
        int rndPoint = Random.Range(0, points.Length);
        currentPoint = points[rndPoint];
        direction = transform.position - currentPoint.position;
    }
    private void RotateMesh()
    {
        var angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        var nextRot = Quaternion.Euler(new Vector3(0, -angle, 0));

        mesh.transform.rotation = Quaternion.Lerp(mesh.transform.rotation, nextRot, rotSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, moveSpeed * Time.fixedDeltaTime);
    }


}
