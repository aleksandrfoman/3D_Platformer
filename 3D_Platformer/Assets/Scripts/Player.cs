using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private LayerMask groundLayer = 1; // 1 == "Default"
    [SerializeField]
    private float groundCheckRadius;
    [SerializeField]
    private bool isJump;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    private new Rigidbody rigidbody;
    [SerializeField]
    private new Collider collider;
    [SerializeField]
    private GameObject playerJumpEffect;
    [SerializeField]
    private float checkHitDist;
    RaycastHit hit;

    public UnityEvent OnJump;

    private void Start()
    {
        if (groundLayer == gameObject.layer)
            Debug.LogError("Player SortingLayer must be different from Ground SourtingLayer!");
    }

    private void FixedUpdate()
    {
        MoveLogic();
        JumpLogic();
        CheckSpawn();
    }
    private Vector3 movementVector
    {
        get
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            return new Vector3(horizontal, 0.0f, vertical);
        }
    }


    private void MoveLogic()
    {
        //rigidbody.AddForce(movementVector * moveSpeed, ForceMode.Impulse);
        transform.Translate(movementVector * moveSpeed * Time.fixedDeltaTime);
    }

    private void CheckSpawn()
    {
        //Physics.SphereCast(transform.position, checkHitRadius, Vector3.down, out hit, checkHitDist, groundLayer);
        Physics.Raycast(transform.position, Vector3.down, out hit, checkHitDist,groundLayer);
        if (hit.collider != null)
        {
            if (GameController.Instance.NextPlatform == null || hit.transform == GameController.Instance.CurrentPlatform.transform)
            {
                GameController.Instance.SpawnPlatform();
            }
        }
    }
    private void JumpLogic()
    {
        if(isGrounded && !isJump && (Input.GetAxis("Jump") > 0))
        {
            Instantiate(playerJumpEffect, groundCheck.position, Quaternion.identity);
            OnJump.Invoke();
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJump = true;
        }
        else if (isGrounded && isJump)
        {
            isJump = false;
        }
    }
    private bool isGrounded
    {
        get
        {
            var bottomCenterPoint = new Vector3(collider.bounds.center.x, collider.bounds.min.y, collider.bounds.center.z);
            bool check = Physics.CheckCapsule(collider.bounds.center, bottomCenterPoint, collider.bounds.size.x / 2 * groundCheckRadius, groundLayer);
            return check;
        }
    }
}
