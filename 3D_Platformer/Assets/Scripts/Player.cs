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
    Transform groundCheck;
    [SerializeField]
    private new Rigidbody rigidbody;
    [SerializeField]
    private new Collider collider;
    [SerializeField]
    private UnityEvent OnJump;
    private void Start()
    {
        if (groundLayer == gameObject.layer)
            Debug.LogError("Player SortingLayer must be different from Ground SourtingLayer!");
    }

    private void FixedUpdate()
    {
        MoveLogic();
        JumpLogic();
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

    private void JumpLogic()
    {
        Debug.Log(isGrounded);
        if (isGrounded && (Input.GetAxis("Jump") > 0))
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool isGrounded
    {
        get
        {
            // Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
            RaycastHit hit;
            bool check = Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down, out hit,3f, groundLayer);


            if (hit.collider != null)
            {
                
                if (GameController.Instance.NextPlatform == null || hit.transform == GameController.Instance.CurrentPlatform.transform)
                {
                    OnJump.Invoke();
                }
            }

            return check;
        }
    }
}
