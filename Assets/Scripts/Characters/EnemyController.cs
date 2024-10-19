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

    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;
    
   private void Awake(){
    rig = GetComponent<Rigidbody2D>();
    anim = GetComponentInChildren<Animator>();
    spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerController = followObject.GetComponent<PlayerController>();
   }

    /* private void FixedUpdate(){

        float distance = Vector2.Distance(transform.position, followObject.transform.position);
        
        if (distance < minDistance && distance>damageRange) {
            anim.SetBool("isMoving", true);

            if (playerController.getSpeed() >= velocity)
            {
                //walk
                anim.SetBool("isRunning", false);
               
            }
            else
            {
                //run
                anim.SetBool("isRunning", true);
            }

           
            transform.position = Vector2.MoveTowards(transform.position, followObject.transform.position, velocity * Time.deltaTime);

        }
        else if (distance < minDistance && distance<damageRange)
        {
            //atacar
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }
        else
        {
            anim.SetBool("isMoving", false);
            anim.SetBool("isRunning", false);
        }*/

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, followObject.transform.position);

        if (distance < minDistance && distance > damageRange)
        {
            anim.SetBool("isMoving", true);

            if (playerController.getSpeed() >= velocity)
            {
                anim.SetBool("isRunning", false);
            }
            else
            {
                anim.SetBool("isRunning", true);
            }

            // Usar MovePosition con Rigidbody2D
            Vector2 newPosition = Vector2.MoveTowards(rig.position, followObject.transform.position, velocity * Time.fixedDeltaTime);
            rig.MovePosition(newPosition);
        }
        else if (distance <= damageRange)
        {
            Attack();
        }
        else
        {
            anim.SetBool("isMoving", false);
            anim.SetBool("isRunning", false);
        }
    

        #region AnimationController
        if (transform.position.y-followObject.transform.position.y > 0)
        {
  
            if (transform.position.x-followObject.transform.position.x > 0.1)
            {
                // mirar hacia izquierda
                Debug.Log("mira hacia izquierda "+( transform.position.x - followObject.transform.position.x));
                anim.SetInteger("intDirection", 2);
                spriteRenderer.flipX = true;
            }
            else if (transform.position.x-followObject.transform.position.x < -0.1)
            {
                //mirar derecha
                Debug.Log("mira hacia derecha");
                anim.SetInteger("intDirection", 2);
                spriteRenderer.flipX= false;

            }
            else
            {
                spriteRenderer.flipX = false;
                // mirar hacia abajo
                Debug.Log("mira hacia abajo");
                anim.SetInteger("intDirection", 0);

            }


        }
        else
        {
            //mirar hacia arriba
            Debug.Log("mira hacia arriba");
            anim.SetInteger("intDirection", 1);
            spriteRenderer.flipX = false;
        }
    }
    #endregion

    void OnCollisionEnter(Collision colision)
    {
        Debug.Log("Colisionó con: " + colision.gameObject.name);

    }

    void Attack()
    {
        anim.SetBool("isMoving", false);
    }

}
