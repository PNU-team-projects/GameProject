using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    
    public int currentLevel = 0;

    public int shopSceneIndex = 1;
    public int levelsStartIndex = 2;
    public bool isInShop = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(levelsStartIndex+currentLevel);
        isInShop = false;
    }

    public void LoadShop()
    {
        SceneManager.LoadScene(shopSceneIndex);
        isInShop = true;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        isInShop = false;
    }
}
