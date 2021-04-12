using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using SaveData;

public class LoadSelectionMenuController : MenuWidget
{
    [SerializeField] private RectTransform LoadItemPanel;

    [SerializeField] private GameObject ItemSlotPrefab;

    [SerializeField] private TMP_InputField SaveNameInputField;


    private const string SaveFileKey = "FileSaveData";

    private void Start()
    {
        LoadItemPanel.DetachChildren();
        LoadFileList();
    }

    public void LoadFileList()
    {
        if (PlayerPrefs.HasKey(SaveFileKey))
        {
            GameDataList gameDataList = JsonUtility.FromJson<GameDataList>(PlayerPrefs.GetString(SaveFileKey));

            foreach (string saveName in gameDataList.SaveFileNames)
            {
                RectTransform widget = Instantiate(ItemSlotPrefab).GetComponent<RectTransform>();
                widget.GetComponent<LoaditemWidget>().Initialize(saveName);
                widget.SetParent(LoadItemPanel);
            }
        }
    }

    public void LoadScene(string saveName)
    {
        GameManager.Instance.SelectedSaveName = saveName;
        SceneManager.LoadScene("GameCityMap");
    }

    public void SaveFileList()
    {
        if (PlayerPrefs.HasKey(SaveFileKey))
        {
            GameDataList gameDataList = JsonUtility.FromJson<GameDataList>(PlayerPrefs.GetString(SaveFileKey));
            gameDataList.SaveFileNames.Add(SaveNameInputField.text);

            string jsonString = JsonUtility.ToJson(gameDataList);
            PlayerPrefs.SetString(SaveFileKey, jsonString);
        }
        else
        {
            GameDataList gameDataList = new GameDataList();
            gameDataList.SaveFileNames.Add(SaveNameInputField.text);

            string jsonString = JsonUtility.ToJson(gameDataList);
            PlayerPrefs.SetString(SaveFileKey, jsonString);
        }

        LoadScene(SaveNameInputField.text);
    }
}
