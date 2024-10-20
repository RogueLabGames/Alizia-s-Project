using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Controller : MonoBehaviour
{
    [SerializeField] private float time;
    private Animator animator;
    private float timeRemaining;


    void Start()
    {
        animator = GetComponent<Animator>();
        timeRemaining = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining <= 0)
        {
            timeRemaining = time;

            Attack();

        }
        else
        {
            timeRemaining-=Time.deltaTime;
        }
    }


    void Attack()
    {
        animator.Play("Gamba_Attack");
    }

    void EndAttack()
    {
        animator.Play("Gamba_Idle");
    }


}
