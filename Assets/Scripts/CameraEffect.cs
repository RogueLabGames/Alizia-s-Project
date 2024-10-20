using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    Transform player;

    [SerializeField] private float yDistance;
    [SerializeField] private float yMovement;

    [SerializeField] private float xDistance;
    [SerializeField] private float xMovement;

    [SerializeField] private float transitionTime;
    [SerializeField] private bool isMoving;

    private Vector3 cameraOrigin;
    private Vector3 cameraDestination;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving){
            if (player.position.y - transform.position.y >= yDistance )
            {
                Debug.Log("Entra Arriba");
                cameraDestination += new Vector3(0, yMovement, 0);
                StartCoroutine(Movement());

            }
            else if (transform.position.y - player.position.y >= yDistance)
            {
                Debug.Log("Entra abajo");
                cameraDestination -= new Vector3(0, yMovement, 0);
                StartCoroutine(Movement());
            }
            else if (player.position.x - transform.position.x >= xDistance)
            {
                Debug.Log("Entra Derecha");
                cameraDestination += new Vector3(xMovement, 0, 0);
                StartCoroutine(Movement());
            }
            else if (transform.position.x - player.position.x >= xDistance)
            {
                Debug.Log("Entra Izquierda");
                cameraDestination -= new Vector3(xMovement, 0, 0);
                StartCoroutine(Movement());
            }
        }
    }

    IEnumerator Movement()
    {
        isMoving = true;
        var t = 0f;
        var currentPosition = transform.position;
        while(t < 1)
        {   
            t += Time.deltaTime / transitionTime;
            transform.position = Vector3.Lerp(currentPosition, cameraDestination, t);
            transform.position = new Vector3(transform.position.x, transform.position.y, currentPosition.z);
            yield return null;
        }
        isMoving = false;
    }
}
