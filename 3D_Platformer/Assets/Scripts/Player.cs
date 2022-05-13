using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float groundCheckSize = 0.9f;
    [SerializeField]
    private LayerMask GroundLayer = 1; // 1 == "Default"
    [SerializeField]
    private new Rigidbody rigidbody;
    [SerializeField]
    private new Collider collider;


    private void Start()
    {
        if (GroundLayer == gameObject.layer)
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
    private bool isGrounded
    {
        get
        {
            var bottomCenterPoint = new Vector3(collider.bounds.center.x, collider.bounds.min.y, collider.bounds.center.z);
            //создаем невидимую физическую капсулу и проверяем не пересекает ли она обьект который относится к полу
            //_collider.bounds.size.x / 2 * 0.9f -- эта странная конструкция берет радиус обьекта.
            // был бы обязательно сферой -- брался бы радиус напрямую, а так пишем по-универсальнее
            return Physics.CheckCapsule(collider.bounds.center, bottomCenterPoint, collider.bounds.size.x / 2 * groundCheckSize, GroundLayer);
            // если можно будет прыгать в воздухе, то нужно будет изменить коэфициент 0.9 на меньший.
        }
    }

    private void MoveLogic()
    {
        //rigidbody.AddForce(movementVector * moveSpeed, ForceMode.Impulse);
        transform.Translate(movementVector * moveSpeed * Time.fixedDeltaTime);
    }

    private void JumpLogic()
    {
        if (isGrounded && (Input.GetAxis("Jump") > 0))
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
