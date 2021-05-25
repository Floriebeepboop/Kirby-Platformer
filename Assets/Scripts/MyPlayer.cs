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
    private Controls ctrl;
    private bool isGrounded = false;
    private SpriteRenderer spriteRenderer;
    private Vector3 respawnPoint;
    Rigidbody2D rb;


    void Start()
    {
        respawnPoint = new Vector3(0, 0.2f, -2);
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
        var isRunning = Mathf.Abs(rb.velocity.x);
        if (isRunning < maxSpeed)
        {
            rb.AddForce(new Vector2(speed * move, 0));
        }
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
