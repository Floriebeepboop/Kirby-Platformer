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
            //Debug.Log($"{name}Triggered");

        }
        else
        {
            trapAnim.SetBool("isOnFire", false);
        }
       
    }

}
/*
 * le jeu est lancé
 * {
 *      on recupere le component collider du feu qui est trigger/activé par défaut
 *      on recupere l'animator du feu
 * }
 * quand le collider du joueur touche le collider du feu
 * {
 *  le feu s'active
 *      lancer l'animation du feu qui explose
 *  si il ne se passe rien
 *  {
 *  l'animation ne se lance pas 
 * }
 */

