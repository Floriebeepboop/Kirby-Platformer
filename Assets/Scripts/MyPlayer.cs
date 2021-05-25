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
    [SerializeField] private LayerMask ground;
    private Animator anim;
    private float move;
    private float direction;
    private Controls ctrl;
    private bool isOnFire = false;
    private bool isGrounded = false;
    private bool isFacingLeft = true;
    private SpriteRenderer spriteRenderer;
    private Vector3 respawnPoint;
    Rigidbody2D rb;
    public List<Transform> pList = new List<Transform>();

    void Start()
    {
        respawnPoint = new Vector3(0, 0, -0.1f);
        GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

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

        anim.SetBool("run", true);
    }

    private void MoveOnCanceled(InputAction.CallbackContext obj)
    {
        move = 0;
        anim.SetBool("run", false);
    }

    private void JumpOnPerformed(InputAction.CallbackContext obj)
    {

        if (isGrounded)
        {
            isGrounded = false;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            anim.SetBool("Jump", true);
        }

    }

    private void FixedUpdate()
    {
        var isRunning = Mathf.Abs(rb.velocity.x); //isGrounded && 
        if (isRunning < maxSpeed)
        {
            rb.AddForce(new Vector2(speed * move, 0));
        }
    }

    private void Flip()
    {
        if (direction < -0.1f)
        {
            isFacingLeft = true;
        }

        if (direction > 0.1f)
        {
            isFacingLeft = false;
        }
        spriteRenderer.flipX = isFacingLeft;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    void Update()
    {

        if (isGrounded)
        {
            anim.SetBool("Jump", false);
        }

    }
    IEnumerator RespawnTimer()
    {

        yield return new WaitForSeconds(3);

        transform.position = respawnPoint;
        anim.SetBool("isOnFire", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
 
        if (collision.CompareTag("Fire"))
        {
            speed = 0;
            isGrounded = false;
            rb.AddForce(new Vector2(0, 0));
            anim.SetBool("isOnFire", true);
            StartCoroutine(RespawnTimer());
        }



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