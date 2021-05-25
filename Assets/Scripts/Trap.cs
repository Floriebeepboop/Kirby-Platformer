using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Trap : MonoBehaviour
{
    private Animator trapAnim;
    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true; //setting boxcollider trigger by default
        trapAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag( "Player"))
        {
            trapAnim.SetBool("isOnFire", true);
            Debug.Log($"{name}Triggered");

        }
        else
        {
            trapAnim.SetBool("isOnFire", false);
        }

    }

}

//ttps://www.youtube.com/watch?v=3f-yrV7uoC4