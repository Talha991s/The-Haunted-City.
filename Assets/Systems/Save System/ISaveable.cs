using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ISaveable
{
    SaveDataBase SaveData();
    void LoadData(SaveDataBase saveData);
}

[Serializable]
public class SaveDataBase
{
    public string Name;
}