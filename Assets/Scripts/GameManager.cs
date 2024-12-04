using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    [SerializeField] private int currentBars;
    [SerializeField] private int health;
    private bool isDialog = false;


    public static GameManager Instance
    {
        get { return instance; }
    }

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

    #region Health
    public int GetHealth()
    {
        return health;
    }

    public int GetBars()
    {
        return currentBars;
    }

    public void SetHealth(int n, string restore)
    {
        Debug.Log("GM: Health changes");
        switch (n)
        {
            case 1:
                if (restore == null)
                {
                    Debug.Log("GM: Health removed");
                    health -= 1;
                }
                else
                {
                    Debug.Log("GM: Health restored");
                    health += 1;
                }
                break;
            case 9: Debug.Log("GM: Health completly restored"); 
                health = 9; break;
        }
    }
    #endregion

    public bool GetDialog
    {
        get { return isDialog; }
    }

    public void SetDialog(bool x)
    {
        isDialog = x;
    }
}