using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    [SerializeField] private Image[] playerBars;
    [SerializeField] private Sprite[] barStatus;
    [SerializeField] private int currentBars;
    [SerializeField] private int health;

    static int minBars = 3;
    static int maxBars = 3;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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

    private Sprite GetBarStatus(int aux)
    {
        switch (aux)
        {
            case 1: return barStatus[1];
            case 2: return barStatus[2];
            case >= 3 : return barStatus[3];
            default: return barStatus[0];
        }

    }

    public void UpdateCurrentBars(int n)
    {
        health -= 1;
        health = Mathf.Clamp(health, 0, health * currentBars);
        UpdateBars();
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth()
    {
        health = 9;
    }
}
