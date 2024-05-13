using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;
    public int selectedSaveIndex;

    private static readonly string SAVES_FOLDER = Application.dataPath + "/Saves/";

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        if(!Directory.Exists(SAVES_FOLDER))
        {
            Directory.CreateDirectory(SAVES_FOLDER);
        }
    }


    public void Save(SaveObject saveObject) { 
        
        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(SAVES_FOLDER + $"{selectedSaveIndex}.txt", json);

    }

    public SaveObject Load() {

        if (File.Exists(SAVES_FOLDER + $"{selectedSaveIndex}.txt"))
        {
            string savedJson = File.ReadAllText(SAVES_FOLDER + $"{selectedSaveIndex}.txt");
            return JsonUtility.FromJson<SaveObject>(savedJson);
        }

        return null;
    }

    public Dictionary<int, Save> GetSavesData()
    {
        Dictionary<int, Save> saves = new Dictionary<int, Save>();

        Save save; SaveObject saveObj;
        foreach (string filePath in Directory.GetFiles(SAVES_FOLDER, "*.txt")) {

            saveObj = JsonUtility.FromJson<SaveObject>(File.ReadAllText(filePath));

            save = new Save() {
                index = Int32.Parse(Path.GetFileName(filePath).Split(".")[0]),
                label = $"Level {saveObj.level+1}",
                date = File.GetLastAccessTime(filePath).ToString("dd-MM")
            };

            saves.Add(save.index, save);
        }


        return saves;
    }

    public void DeleteSave(int index)
    {
        File.Delete(SAVES_FOLDER + $"{index}.txt");
    }
}
