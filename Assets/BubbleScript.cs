using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{

    private CapsuleCollider2D capsuleCollider2D;
    private Animator animator;
    [SerializeField] private PlayerController player;
    [SerializeField] private float velocity;
    private Vector2 newPosition;
    private Rigidbody2D rig;
    void Start()
    {
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();

        newPosition = Vector2.MoveTowards(rig.position, player.transform.position, velocity * Time.fixedDeltaTime);
        rig.MovePosition(newPosition);
    }

    // Update is called once per frame
    void FixedUpdate()
    {



        if(Vector2.Distance(transform.position, player.transform.position) > 15) 
        { 
            DestroyBubble();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = Vector2.zero;

       


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        animator.SetBool("Explode", true);
        if (other.CompareTag("Player"))
        {
            player.Attacked();
        }
    }

    private void DestroyBubble()
    {
        Destroy(this.gameObject);
    }

}
