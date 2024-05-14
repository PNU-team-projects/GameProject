using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public static bool isPaused;

    public GameObject saveButtonPrefab;
    public Vector2 startingPosition;
    public GameObject saveButtonsContainer;
    public CanvasGroup loadingCanvas;


    private void Awake()
    {
        saveButtonsContainer.SetActive(false);
        CreateSaveButtons();
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene("TestScene");
        Time.timeScale = 1f;
        isPaused = false;

        saveButtonsContainer.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void CreateSaveButtons()
    {
        Dictionary<int, Save> saves = SaveSystem.instance.GetSavesData();

        for (int i = 0; i < 3; i++)
        {
            GameObject button = Instantiate(saveButtonPrefab);

            button.transform.parent = saveButtonsContainer.transform;
            button.transform.localPosition = new Vector3(startingPosition.x, startingPosition.y + (-110 * i), 0);
            button.transform.localScale = Vector3.one;
            button.GetComponent<SaveButton>().loadingCanvas = loadingCanvas;

            if (!saves.ContainsKey(i + 1))
            {
                button.GetComponent<SaveButton>().SetData(i + 1);

            }
            else
            {
                button.GetComponent<SaveButton>().SetData(saves[i + 1].index, saves[i + 1].label, saves[i + 1].date);
            }

        }
    }
}
