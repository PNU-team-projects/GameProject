using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool isPaused;

    public void PlayGame() {
        SceneManager.LoadScene("TestScene");
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitGame() { 
        Application.Quit();
    }
}
