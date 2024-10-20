using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameOver : MonoBehaviour
{
    public void MenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        //SceneManager.LoadScene("Fortress");
    }




}
