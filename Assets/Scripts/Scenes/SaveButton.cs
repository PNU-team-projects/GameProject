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

        if (isNewGame)
        {
            LevelManager.instance.currentLevel = 0;
            LevelManager.instance.LoadNextScene();
        } else
        {
            LevelManager.instance.LoadShop();
        }

    }

    public void OnDeleteClick()
    {
        SaveSystem.instance.DeleteSave(index);
        SetData(index);
    }
}
