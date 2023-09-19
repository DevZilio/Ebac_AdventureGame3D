using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevZilio.Core.Singleton;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField]private SaveSetup _saveSetup;
    private string _path = Application.streamingAssetsPath + "/save.txt";

    public int lastLevel;

    public  Action<SaveSetup> FileLoaded;

    public SaveSetup Setup{
        get {return _saveSetup;}
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "Paulo";
        _saveSetup.coins = 0;
        _saveSetup.health = 0;
        _saveSetup.lastChekPoint = 0;
    }

    private void Start()
    {
       Invoke(nameof(LoadFile), .1f);
    }

#region SAVE
    [NaughtyAttributes.Button]
    public void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        SaveFile(setupToJson);
        Debug.Log(setupToJson);
    }

    public void SaveItems()
    {
        _saveSetup.coins = Items.ItemManager.Instance.GetItemByType(Items.ItemType.COIN).soInt.value;
        _saveSetup.health = Items.ItemManager.Instance.GetItemByType(Items.ItemType.LIFE_PACK).soInt.value;
        Save();
    }

[NaughtyAttributes.Button]
    public void SaveLastCheckPoint()
    {
        _saveSetup.lastChekPoint = CheckPointManager.Instance.lastCheckPointKey;
        Save();
        Debug.Log("SaveCheckPoint");
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();
        SaveLastCheckPoint();
        Save();
    }

    public void SaveName(string text)
    {
        _saveSetup.playerName = text;
        Save();
    }

#endregion
    [NaughtyAttributes.Button]
    private void SaveFile(string json)
    {

        File.WriteAllText(_path, json);
    }

    [NaughtyAttributes.Button]
    public void LoadFile()
    {
        string fileLoaded = "";

        if(File.Exists(_path))         
        {
        fileLoaded = File.ReadAllText(_path);

        _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);

        lastLevel = _saveSetup.lastLevel;
        }
        else
        {
            CreateNewSave();
            Save();
        }

        FileLoaded?.Invoke(_saveSetup);

        Debug.Log(_path);
    }

  
    

#region DEBUG

    [NaughtyAttributes.Button]
    private void SaveLevelOne()
    {
        SaveLastLevel(1);
    }
    [NaughtyAttributes.Button]
    private void SaveLevelFive()
    {
        SaveLastLevel(5);
    }
}

#endregion

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string playerName;
    public float coins;
    public float health;
    public int lastChekPoint;
}
