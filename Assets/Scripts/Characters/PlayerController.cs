using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 0.1f;

    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal"); //Horizontal move
        float verticalMove = Input.GetAxisRaw("Vertical"); //Vertical move

        rig.velocity = new Vector2(horizontalMove, verticalMove).normalized * speed; //This change attribute velocity of the RigidBody2D. Moves the character
        Animations(horizontalMove, verticalMove); //Call Animations method to control every animations
    }

    //Method that control all player's animations
    void Animations(float horizontalMove, float verticalMove)
    {

        //Horizontal move
        if (horizontalMove > 0) //Right move
        {
            anim.SetBool("isWalkingH", true);
            spriteRenderer.flipX = false;
        }
        else if (horizontalMove < 0) //Left move
        {
            anim.SetBool("isWalkingH", true);
            spriteRenderer.flipX = true;
        }
        else //Idle
        {
            anim.SetBool("isWalkingH", false);
        }


        //Vertical move
        if (verticalMove > 0)
        {
            anim.SetBool("isWalkingDown", false);
            anim.SetBool("isWalkingUp", true);
        }
        else if (verticalMove < 0) //Left move
        {
            anim.SetBool("isWalkingUp", false);
            anim.SetBool("isWalkingDown", true);
        }
        else //Idle
        {
            anim.SetBool("isWalkingDown", false);
            anim.SetBool("isWalkingUp", false);
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("Attacking");
            anim.SetTrigger("isAttacking");

        }

    }
}