using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    [SerializeField] private float speed; // private seulement utilisable dans ce script, float--> nombre decimal, Speed--> nom de la valeur
    [SerializeField] private float maxSpeed;
    [SerializeField]private float jumpForce;
    [SerializeField] private float gravityScale;
    private Animator anim;
    private Controls ctrl;
    private bool isGrounded = false;
    private bool isFacingLeft = true;
    private SpriteRenderer spriteRenderer;
    private Vector2 directionn;
    Rigidbody2D rb;
    [SerializeField] private LayerMask ground;


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
        directionn = obj.ReadValue<Vector2>();
    }
    private void MoveOncanceled(InputAction.CallbackContext obj)
    {
        directionn = Vector2.zero;
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
        var direction = new Vector2
        {
           x = directionn.x,
           y = 0
        };
            // Déplacement
        if (rb.velocity.sqrMagnitude < maxSpeed)
        {
            rb.AddForce(direction * speed);
        }

        /*var isRunning = isGrounded && Mathf.Abs(rb.velocity.x) > 0.1f;
        anim.SetBool("IsRunning", isRunning);
        var isAscending = !isGrounded && rb.velocity.y > 0;
        anim.SetBool("IsAscending", isAscending);
        var isDescending = !isGrounded && rb.velocity.y < 0;
        anim.SetBool("IsDescending", isDescending);
        anim.SetBool("IsGrounded", isGrounded);
        Flip();*/
    }

    private void Flip()
    {
       if (directionn.x < -0.1f)
       {
           isFacingLeft = true;
       }

       if (directionn.x > 0.1f)
       {
           isFacingLeft = false;
       }
     spriteRenderer.flipX = isFacingLeft;
    }

}
