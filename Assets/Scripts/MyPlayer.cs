using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    private float Speed; // private seulement utilisable dans ce script, float--> Speed value
    private float MaxSpeed;
    private float JumpForce;
    private float GravityScale;
    private Controls Ctrl;
    private Animator Anim;

    Rigidbody2D rb;



    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
/*declarer des valeurs pour vitesse, vitesse max, saut, gravité*/