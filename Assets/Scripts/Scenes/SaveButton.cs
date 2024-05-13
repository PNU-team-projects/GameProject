using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class SaveButton : MonoBehaviour
{
    public TextMeshProUGUI indexText;
    public TextMeshProUGUI labelText;
    public TextMeshProUGUI dateText;
    public GameObject deleteBtn;
    public CanvasGroup loadingCanvas;

    private int index;
    private bool isNewGame = false;

    public void SetData(int index, string label, string date)
    {
        indexText.text = $"{index}:"; 
        labelText.text = label;
        dateText.text = date;

        this.index = index;
    }

    public void SetData(int index)
    {
        SetData(index, "New Game", "");
        this.isNewGame = true;
        deleteBtn.SetActive(false);
    }

    public void OnClick()
    {
        SaveSystem.instance.selectedSaveIndex = index;

        loadingCanvas.alpha = 0;
        loadingCanvas.gameObject.SetActive(true);
        StartCoroutine(OpeningCanvasOpacity());



    }

    private void LoadLevel()
    {
        if (isNewGame)
        {
            LevelManager.instance.currentLevel = 0;
            LevelManager.instance.LoadNextScene();
        }
        else
        {
            LevelManager.instance.LoadShop();
        }
    }

    private IEnumerator OpeningCanvasOpacity()
    {

        while (loadingCanvas.alpha < 1)
        {
            loadingCanvas.alpha += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        LoadLevel();

    }

    public void OnDeleteClick()
    {
        SaveSystem.instance.DeleteSave(index);
        SetData(index);
    }
}
