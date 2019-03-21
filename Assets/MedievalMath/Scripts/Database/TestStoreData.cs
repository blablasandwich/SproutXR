﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStoreData : MonoBehaviour
{
    string path;
    public GameRound gameRound;
    public GameSession gameSession;
    public bool reload = false;

    void Start()
    {
        path = Application.persistentDataPath + "/testRound.json";
        LoadGameData();
    }

    public void LoadGameData()
    {
        if (!System.IO.File.Exists(path) || reload)
        {
            Debug.Log("Created the testing json file");
            GameRound gameRound = new GameRound();
            string gameRoundData = JsonUtility.ToJson(gameRound);

            System.IO.File.WriteAllText(path, gameRoundData);
            Debug.Log(path);
            PlayerPrefs.SetString("TestRound", gameRoundData);
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetString("TestRound"));
        } else
        {
            string jsonData = System.IO.File.ReadAllText(path);
            Debug.Log("File exists");
            gameRound = JsonUtility.FromJson<GameRound>(jsonData);
            
        }
    }
}
