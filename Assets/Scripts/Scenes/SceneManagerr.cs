using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerr : MonoBehaviour
{
    public static SceneManagerr instance;
    public Player player;
    public SaveObject saveObject;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
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

        LevelManager.instance.LoadShop();
    }



    public void OnLevelStart()
    {
        saveObject = SaveSystem.instance.Load();

        if (saveObject != null)
        {
            player.maxHP = saveObject.hp;
            player.damageBonus = saveObject.damageBonus;
            player.Coins = saveObject.coins;
            player.Keys = saveObject.keys;

            ActiveInventory inventory = player.activeInventoryObject.GetComponent<ActiveInventory>();

            inventory.health_p = saveObject.healPotions;
            inventory.speed_p = saveObject.speedPotions;
            inventory.rage_p = saveObject.ragePotions;

            LevelManager.instance.currentLevel = saveObject.level;
        }
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
        LevelManager.instance.LoadNextScene();
    }
}
