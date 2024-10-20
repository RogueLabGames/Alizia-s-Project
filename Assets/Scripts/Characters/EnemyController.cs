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

    [SerializeField] private float refreshTime;

    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;
    private float remainingTime;


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
        Debug.Log("E: Actualizo Distancia: " + distance);

        if (distance < minDistance)
        {
            // Usar MovePosition con Rigidbody2D
            newPosition = Vector2.MoveTowards(rig.position, playerController.transform.position, velocity * Time.fixedDeltaTime);
            rig.MovePosition(newPosition);
            Vector2 currentVelocity = (newPosition - rig.position) / Time.fixedDeltaTime;
            rig.velocity = currentVelocity;
        }
        else if (distance <= damageRange && !playerController.IsAttacked())
        {
            Attack();
            playerController.SetAttacked();
        }
    

        #region AnimationController
        if (newPosition.magnitude != 0)
        {
            anim.SetFloat("Horizontal", rig.velocity.x);
            anim.SetFloat("Vertical", rig.velocity.y);
            anim.Play("Run");
        }
        else
        {
            anim.Play("Idle");
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("E: Collision with: " + other.gameObject.name);
            playerController.SetAttacked();
        }
    }

    void Attack()
    {

        if(remainingTime <= 0) 
        {
            Debug.Log("Atacando");
            remainingTime = refreshTime;
            anim.SetBool("isMoving", false);
        }
        else
        {
            remainingTime -= Time.deltaTime;
            //Debug.Log("intentando atacar "+remainingTime);
        }


        anim.SetBool("isMoving", false);
        Debug.Log("E: Atacando");
    }
}
