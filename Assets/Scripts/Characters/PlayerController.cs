using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 0.1f;
    bool isAttacking;

    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    Vector2 move;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        rig.velocity = move * speed; //This change attribute velocity of the RigidBody2D. Moves the character
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal"); //Horizontal move
        float verticalMove = Input.GetAxisRaw("Vertical"); //Vertical move

        if (isAttacking) return;
        move = new Vector2(horizontalMove, verticalMove).normalized;

        if (Input.GetMouseButton(0))
        {
            Debug.Log("Attacking");
            anim.Play("Attack");
            isAttacking = true;
            move = Vector2.zero;

        }

        Animations(); //Call Animations method to control every animations
    }

    //Method that control all player's animations
    private void Animations()
    {

        if (isAttacking) return;

        if (move.magnitude!=0)
        {
            anim.SetFloat("Horizontal", move.x);
            anim.SetFloat("Vertical", move.y);
            anim.Play("Walk");
        }
        else
        {
            anim.Play("Idle");
        }

        

    }

    private void EndAttack()
    {
        isAttacking = false;
    }
}