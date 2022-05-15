using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offsetCamera;
    [SerializeField]
    private float smoothSpeed;

    private void FixedUpdate()
    {
        MoveCamera();
    }
    private void MoveCamera()
    {
        if (GameController.Instance.isStartGame)
        {
            transform.RotateAround(target.transform.position, Vector3.up, (smoothSpeed*2) * Time.fixedDeltaTime);
        }
        else
        {
            Vector3 desiredPos = target.position + offsetCamera;
            Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.fixedDeltaTime);
            if (smoothPos.y < 8)
            {
                smoothPos = new Vector3(transform.position.x, 8f, transform.position.z);
                GameController.Instance.ActivateLosePanel();
            }
            else
            {
                transform.position = smoothPos;
                transform.LookAt(target);
            }
        }
    }
}
