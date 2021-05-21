using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    [SerializeField] private float speed; // private seulement utilisable dans ce script, float--> nombre decimal, Speed--> nom de la valeur
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScale;
    private Animator anim;
    private Controls ctrl;
    private bool isJumping;
    private bool isGrounded = false;
    private bool isFacingLeft = true;
    private SpriteRenderer spriteRenderer;
    private float move;
    Rigidbody2D rb;
    [SerializeField] private LayerMask ground;

    private void OnEnable()
    {
        ctrl = new Controls();
        ctrl.Enable();
        ctrl.Main.Move.performed += MoveOnPerformed;
        ctrl.Main.Move.canceled += MoveOnCanceled;
        ctrl.Main.Jump.performed += JumpOnPerformed;
    }

    private void MoveOnPerformed(InputAction.CallbackContext obj)
    {
        move = obj.ReadValue<float>();
        spriteRenderer.flipX = (move < 0);
    }

    private void MoveOnCanceled(InputAction.CallbackContext obj)
    {
        move = 0;
    }

    private void JumpOnPerformed(InputAction.CallbackContext obj)
    {
        if (isJumping == true)
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

        anim.SetBool("jumpForce", true);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        var horizontalSpeed = Mathf.Abs(rb.velocity.x);
        if (horizontalSpeed < maxSpeed)
            rb.AddForce(new Vector2(speed * move, 0));

        anim.SetFloat("Speed", horizontalSpeed);
    }

    void Update()
    {
        var touch = Physics2D.Raycast(transform.position, new Vector2(0, -1), 0.001f, 9);
        if (touch.collider != null)
            isJumping = true;

        else
            isJumping = false;
    }
}

/*
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        ctrl = new Controls();
        ctrl.Enable();
        ctrl.Main.Jump.performed += JumpOnperformed;
        ctrl.Main.Move.performed += MoveOnperformed;
        ctrl.Main.Move.canceled += MoveOncanceled;
    }
    private void MoveOnperformed(InputAction.CallbackContext obj)
    {
        directionn = obj.ReadValue<float>();
    }
    private void MoveOncanceled(InputAction.CallbackContext obj)
    {
        directionn = 0;
    }
    //gestion des sauts
    private void JumpOnperformed(InputAction.CallbackContext obj)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        var touchGround = ground == (ground | (1 << other.gameObject.layer));
        var touchFromAbove = other.contacts[0].normal == Vector2.up;
        if (touchGround && touchFromAbove)
        {
            isGrounded = true;
        }
    }

    private void FixedUpdate()
    {
        var horizontalSpeed = Mathf.Abs(rb.velocity.x);

        if (horizontalSpeed < maxSpeed)
        {
            rb.AddForce(new Vector2(speed * directionn, 0));
        }

        var isRunning = isGrounded && Mathf.Abs(rb.velocity.x) > 0.1f;
        anim.SetBool("IsRunning", isRunning);
        var isAscending = !isGrounded && rb.velocity.y > 0;
        anim.SetBool("IsAscending", isAscending);
        var isDescending = !isGrounded && rb.velocity.y < 0;
        anim.SetBool("IsDescending", isDescending);
        Flip();
    }

}
*/