using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    private float speed; // private seulement utilisable dans ce script, float--> nombre decimal, Speed--> nom de la valeur
    private float maxSpeed;
    private float jumpForce;
    private float gravityScale;
    private Controls ctrl;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        //on créé de nouveau controls nomé ctrl
        ctrl = new Controls();
        ctrl.Enable();
        //ctrl.Main.Jump.performed += JumpOnperformed;
        //ctrl.Main.MoveLR.performed += MoveLROnperformed;
        //ctrl.Main.MoveLR.canceled += MoveLROncanceled;
    }

    void Update()
    {
        
    }
}
/*declarer des valeurs pour vitesse, vitesse max, saut, gravité*/