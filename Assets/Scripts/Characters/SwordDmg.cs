using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDmg : MonoBehaviour
{

    private BoxCollider2D swordCollider;
    // Start is called before the first frame update
    void Start()
    {
        swordCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit");
        if(other.CompareTag("Enemy") || other.CompareTag("ObjectFragile"))
        {
            Destroy(other.gameObject);
        }
    }

}
