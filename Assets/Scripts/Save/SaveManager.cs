using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevZilio.Core.Singleton;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;
    private string _path = Application.streamingAssetsPath + "/save.txt";

    public int lastLevel;
    public Action<SaveSetup> FileLoaded;

    public SaveSetup Setup { get { return _saveSetup; } }

    public GameObject startPosition;

    public Vector3 playerStartPosition;

    public SaveManager() : base(true)
    {
        
    }

   

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "Paulo";
        _saveSetup.coins = 0;
        _saveSetup.health = 0;
        _saveSetup.lastChekPoint = 0;        
        _saveSetup.playerStartPosition = new Vector3(startPosition.transform.position.x, startPosition.transform.position.y, startPosition.transform.position.z);
           
         Save();
    }

    private void Start()
    {
        LoadFile();
    }

    #region SAVE
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
        Debug.Log("Items Saved");
    }

    public void SaveLastCheckPoint()
    {
        _saveSetup.lastChekPoint = CheckPointManager.Instance.lastCheckPointKey;
        Save();
        Debug.Log("Checkpoint " + _saveSetup.lastChekPoint + " updated");
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();
        SaveLastCheckPoint();
        Save();
        Debug.Log("Last Level saved: " + _saveSetup.lastLevel);
    }

    public void SaveName(string text)
    {
        _saveSetup.playerName = text;
        Save();
        Debug.Log("Player Name saved: " + _saveSetup.playerName);
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

        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            lastLevel = _saveSetup.lastLevel;
            // playerName = _saveSetup.playerName;
            // coins = _saveSetup.coins;
            // health = _saveSetup.health;
            // lastCheckPoint = _saveSetup.lastChekPoint;
            
            Debug.Log("File loaded successfully from: " + _path);
        }
        else
        {
            CreateNewSave();
            Save();
            Debug.Log("New save file created and saved to: " + _path);
        }

        FileLoaded?.Invoke(_saveSetup);
    }

  public int GetSavedCoins()
{
    return (int)_saveSetup.coins;
}

public int GetSavedLifePacks()
{
    return (int)_saveSetup.health;
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
    #endregion
}



[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string playerName;
    public float coins;
    public float health;
    public int lastChekPoint;
    public Vector3 playerStartPosition;
}
 