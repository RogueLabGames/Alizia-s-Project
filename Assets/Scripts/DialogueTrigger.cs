using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private string[] dialogue;
    [SerializeField] private int scene;
    private int index;
    private Rigidbody2D rig;
    private RigidbodyConstraints2D originalRig;
    private Vector2 linearBackup;


    [SerializeField] private float wordSpeed;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject player;

    void Awake()
    {
        rig = player.GetComponentInChildren<Rigidbody2D>();
        linearBackup = rig.velocity;
        rig.velocity = Vector2.zero;
        originalRig = rig.constraints;
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            nextButton.SetActive(false);
            zeroText();
            rig.constraints = originalRig;
            gameObject.SetActive(false);

            if (scene != 0)
            {
                SceneManager.LoadScene(scene);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            rig.constraints = RigidbodyConstraints2D.FreezeAll;
            zeroText();
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());

            if(dialogueText.text == dialogue[index])
            {
                nextButton.SetActive(true);
            }
            
        }
    }
}
