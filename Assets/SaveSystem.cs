using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using SaveData;

[Serializable]
public class GameSaveData
{
    public PlayerSaveData PlayerSaveData;

    public GameSaveData()
    {
        PlayerSaveData = new PlayerSaveData();
    }
}

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    private GameSaveData GameSave;

    private const string SaveFileKey = "FileSaveData";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (string.IsNullOrEmpty(GameManager.Instance.SelectedSaveName)) return;
        if (!PlayerPrefs.HasKey(GameManager.Instance.SelectedSaveName)) return;

        string jsonString = PlayerPrefs.GetString(GameManager.Instance.SelectedSaveName);
        GameSave = JsonUtility.FromJson<GameSaveData>(jsonString);
        LoadGame();
    }

    public void SaveGame()
    {
        // not equal to null
        if (GameSave == null) GameSave = new GameSaveData();

        var saveableObjects = FindObjectsOfType<MonoBehaviour>()
            .Where((MonoBehaviour monoObject) => monoObject is ISaveable).ToList();

        ISaveable playerSaveObject = saveableObjects.First((MonoBehaviour monoObject) => monoObject is PlayerController) as ISaveable;
        GameSave.PlayerSaveData = (PlayerSaveData) playerSaveObject?.SaveData();

        string jsonString = JsonUtility.ToJson(GameSave);
        PlayerPrefs.SetString(GameManager.Instance.SelectedSaveName, jsonString);

        SaveToGameSaveList();
    }

    private void SaveToGameSaveList()
    {
        if (PlayerPrefs.HasKey(SaveFileKey))
        {
            GameDataList saveList = JsonUtility.FromJson<GameDataList>(PlayerPrefs.GetString(SaveFileKey));
            if (saveList.SaveFileNames.Contains(GameManager.Instance.SelectedSaveName)) return;

            saveList.SaveFileNames.Add(GameManager.Instance.SelectedSaveName);

            PlayerPrefs.SetString(SaveFileKey, JsonUtility.ToJson(saveList));
        }
        else
        {
            GameDataList saveList = new GameDataList();
            saveList.SaveFileNames.Add(GameManager.Instance.SelectedSaveName);

            PlayerPrefs.SetString(SaveFileKey, JsonUtility.ToJson(saveList));
        }
    }

    public void LoadGame()
    {
        var saveableObjects = FindObjectsOfType<MonoBehaviour>()
            .Where((MonoBehaviour monoObject) => monoObject is ISaveable).ToList();

        ISaveable playerObject = saveableObjects.First((MonoBehaviour monoObject) => monoObject is PlayerController) as ISaveable;
        playerObject?.LoadData(GameSave.PlayerSaveData);
    }
}
