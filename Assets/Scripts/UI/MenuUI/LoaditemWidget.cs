using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoaditemWidget : MonoBehaviour
{
    [SerializeField] private TMP_Text SaveNameText;
    private string SaveName;

    private LoadSelectionMenuController LoadSelectionMenu;

    private void Awake()
    {
        LoadSelectionMenu = FindObjectOfType<LoadSelectionMenuController>();
    }

    public void Initialize(string saveName)
    {
        SaveName = saveName;
        if (SaveNameText)
        {
            SaveNameText.text = saveName;
        }
    }

    public void SelectSave()
    {
        LoadSelectionMenu.LoadScene(SaveName);
    }
}
