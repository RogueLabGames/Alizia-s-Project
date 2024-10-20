using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{

    private BoxCollider2D AttackBoxCollider;
    private EnemyController enemyController;
    private PlayerController playerController;

    private void Start()
    {
        AttackBoxCollider = GetComponent<BoxCollider2D>();
        enemyController = GetComponentInParent<EnemyController>();
        playerController = enemyController.GetPlayerController();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("E: Collision with: " + other.gameObject.name);
            playerController.Attacked();
        }
    }

}
