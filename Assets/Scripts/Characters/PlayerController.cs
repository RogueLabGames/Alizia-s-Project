using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Player Stats")]
    [SerializeField] private float speed = 5;

    [Header("I-Frames")]
    [SerializeField] private float iframesDuration;
    [SerializeField] private int flashNumbers;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private HUDController hudController;

    private bool isAttacking;
    private bool isAttacked = false;

    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Vector2 move;
    

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
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

    private void EndAttack() { isAttacking = false; }

    public float GetSpeed() { return speed; }

    public void Attacked() {
        Debug.Log("P: Has being attacked");
        if (gameManager.GetHealth() > 0){
            isAttacked = true;
            gameManager.SetHealth(1, null);
            hudController.UpdateCurrentBars();
            StartCoroutine(Invurnerability());
        }
        if (gameManager.GetHealth() == 0)
        {
            Physics2D.IgnoreLayerCollision(6, 3, false);
            gameManager.SetHealth(9, null);
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
        

        Debug.Log("P: Attacked");
    }

    private IEnumerator Invurnerability()
    {
        Physics2D.IgnoreLayerCollision(6,3,true);

        for (int i = 0; i < flashNumbers; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(1);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(1);
        }

        Physics2D.IgnoreLayerCollision(6, 3, false);

    }

    public bool IsAttacked() { return isAttacked; }

}
