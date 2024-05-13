using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneManagerr : MonoBehaviour
{
    public static SceneManagerr instance;
    public Player player;
    public SaveObject saveObject;
    
    public CanvasGroup canvas;
    public TextMeshProUGUI canvasText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        canvas.gameObject.SetActive(true);
    }

    public void EndLevel()
    {
        ActiveInventory inventory = player.activeInventoryObject.GetComponent<ActiveInventory>();

        SaveObject saveObject = new SaveObject()
        {
            hp = player.maxHP,
            damageBonus = player.damageBonus,
            coins = player.Coins,
            keys = player.Keys,
            healPotions = inventory.health_p,
            speedPotions = inventory.speed_p,
            ragePotions = inventory.rage_p,
            level = LevelManager.instance.currentLevel + 1
        };

        SaveSystem.instance.Save(saveObject);

        canvas.alpha = 0;
        canvas.gameObject.SetActive(true);
        canvasText.text = $"Level {LevelManager.instance.currentLevel + 1} - Completed";
        StartCoroutine(ClosingCanvasOpacity());



    }



    public void OnLevelStart()
    {
        saveObject = SaveSystem.instance.Load();

        if (saveObject != null)
        {
            player.maxHP = saveObject.hp;
            player.currentHP = saveObject.hp;
            player.damageBonus = saveObject.damageBonus;
            player.Coins = saveObject.coins;
            player.Keys = 0;

            ActiveInventory inventory = player.activeInventoryObject.GetComponent<ActiveInventory>();

            inventory.health_p = saveObject.healPotions;
            inventory.speed_p = saveObject.speedPotions;
            inventory.rage_p = saveObject.ragePotions;

            LevelManager.instance.currentLevel = saveObject.level;


        }
        if (LevelManager.instance.isInShop)
        {
            canvasText.text = "Shop";
        } else
        {
            canvasText.text = $"Level {LevelManager.instance.currentLevel + 1}";
        }

        StartCoroutine(OpeningCanvasOpacity());

    }

    public void OnPlayerDied()
    {
        canvas.alpha = 0;
        canvas.gameObject.SetActive(true);
        canvasText.text = $"You Died";
        StartCoroutine(ClosingCanvasOpacity());
    }


    public void NextLevel()
    {
        ActiveInventory inventory = player.activeInventoryObject.GetComponent<ActiveInventory>();

        SaveObject saveObject = new SaveObject()
        {
            hp = player.maxHP,
            damageBonus = player.damageBonus,
            coins = player.Coins,
            keys = player.Keys,
            healPotions = inventory.health_p,
            speedPotions = inventory.speed_p,
            ragePotions = inventory.rage_p,
            level = LevelManager.instance.currentLevel
        };

        SaveSystem.instance.Save(saveObject);

        canvas.alpha = 0;
        canvas.gameObject.SetActive(true);
        canvasText.text = $"";
        StartCoroutine(ClosingCanvasOpacity());

    }

    public void EndGame()
    {
        canvas.alpha = 0;
        canvas.gameObject.SetActive(true);
        canvasText.text = "Congratulations! You've come out of the dungeon.";
        StartCoroutine(FinalCanvasOpacity());
    }



    private IEnumerator OpeningCanvasOpacity()
    {

        yield return new WaitForSeconds(0.5f);

        while (canvas.alpha > 0)
        {
            canvas.alpha -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        canvas.gameObject.SetActive(false);
    }

    private IEnumerator ClosingCanvasOpacity()
    {

        while (canvas.alpha < 1)
        {
            canvas.alpha += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        if (LevelManager.instance.isInShop)
        {
            LevelManager.instance.LoadNextScene();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            LevelManager.instance.LoadShop();
        }
    }

    private IEnumerator FinalCanvasOpacity()
    {

        while (canvas.alpha < 1)
        {
            canvas.alpha += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1.5f);
        SaveSystem.instance.DeleteSave(SaveSystem.instance.selectedSaveIndex);
        LevelManager.instance.LoadMainMenu();
    }
}
