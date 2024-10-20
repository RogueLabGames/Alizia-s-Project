using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float velocity;

    [SerializeField] private GameObject followObject;

    [SerializeField] private float minDistance;

    [SerializeField] private float damageRange;

    [SerializeField] private float damage;

    [SerializeField] private float health;

    //[SerializeField] private float refreshTime;

    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;
    //private float remainingTime;
    private bool isAttacking=false;

    private Vector2 newPosition;
    
   private void Awake(){
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerController = followObject.GetComponentInChildren<PlayerController>();
   }

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, playerController.transform.position);
        //Debug.Log("E: IsAtacking: " + isAttacking);

        if (distance < minDistance && distance>damageRange )
        {
            // Usar MovePosition con Rigidbody2D
            newPosition = Vector2.MoveTowards(rig.position, playerController.transform.position, velocity * Time.fixedDeltaTime);
            rig.MovePosition(newPosition);
            Vector2 currentVelocity = (newPosition - rig.position) / Time.fixedDeltaTime;
            rig.velocity = currentVelocity;
        }
        else if(distance < damageRange) 
        {
            anim.Play("Attacking");
            isAttacking = true;
            newPosition = Vector2.zero;
        }
    

        #region AnimationController
        if (newPosition.magnitude != 0)
        {
            anim.SetFloat("Horizontal", rig.velocity.x);
            anim.SetFloat("Vertical", rig.velocity.y);
            anim.Play("Run");
        }
        else if(!isAttacking)
        {
            anim.Play("Idle");
        }
    }
    #endregion

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("E: Collision with: " + other.gameObject.name);
            playerController.SetAttacked();
            Attack();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("E: Stay Colliding");
            Attack();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Attack finished");
        }
    }*/

    void Attack()
    {
        Debug.Log("Atacando");
        anim.SetBool("isMoving", false);
    }

    void setAttacked()
    {
        health -= 1;

        if(health <= 0)
        {
            setDeath();
        }

    }

    private void EndAttack()
    {
        isAttacking = false;
        Debug.Log("Ataque finalizado");
    }

    void setDeath()
    {
        Debug.Log("Enemy death");
    }

}
