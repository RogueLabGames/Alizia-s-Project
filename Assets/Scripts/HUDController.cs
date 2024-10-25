using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    [SerializeField] private Image[] playerBars;
    [SerializeField] private Sprite[] barStatus;

    private int health, currentBars;

    static int minBars = 3;
    static int maxBars = 3;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null)
        {
            health = GameManager.Instance.GetHealth();
            currentBars = GameManager.Instance.GetBars();
        }
        currentBars = Mathf.Clamp(currentBars, minBars, maxBars);
        health = Mathf.Clamp(health, 1, health * currentBars);
        UpdateBars();
    }

    private void UpdateBars()
    {
        int aux = health;

        for (int i = 0; i < maxBars; i++)
        {
            if (i < currentBars)
            {
                playerBars[i].enabled = true;
                playerBars[i].sprite = GetBarStatus(aux);
                aux -= 3;
            }
            else
            {
                playerBars[i].enabled = false;
            }

        }
    }

    public void UpdateCurrentBars()
    {
        health -= 1;
        health = Mathf.Clamp(health, 0, health * currentBars);
        UpdateBars();
    }

    private Sprite GetBarStatus(int aux)
    {
        switch (aux)
        {
            case 1: return barStatus[1];
            case 2: return barStatus[2];
            case >= 3: return barStatus[3];
            default: return barStatus[0];
        }

    }
}
