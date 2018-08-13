using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class GameFrame
{
    //public static GameFrame GetInstance()
    //{
    //    if (m_Instance == null)
    //    {
    //        m_Instance = new GameFrame();
    //    }

    //    return m_Instance;
    //}

    public static void OnNew(string strEmpName, string strYearName, string strDynastyName)
    {
        //Global.SetMyGame(new MyGame(countryName, yearName, familyName, selfName));
        //Global.GetGameData().Init();
        gameEnd = false;
        Initialize(strEmpName, strYearName, strDynastyName);
        SceneManager.LoadSceneAsync("MainScene");
    }

    public static void OnSave()
    {
        string strSavePath = GetSavePath();
        Debug.Log(strSavePath);
        if (!Directory.Exists(strSavePath))
        {
            Directory.CreateDirectory(strSavePath);
        }

        string json = MyGame.SerializeManager.Serial();
        File.WriteAllText(GetSavePath() + "/game.save", json);



    }

    public static void OnLoad()
    {
        gameEnd = false;


        InitStage();

        string strSavePath = GetSavePath();
        Debug.Log(strSavePath);

        string json = File.ReadAllText(GetSavePath() + "/game.save");

        MyGame.SerializeManager.Deserial(json);

		//MyGame.Inst = JsonUtility.FromJson<MyGame> (json);

        //Global.SetMyGame(new MyGame(JsonUtility.FromJson<GameData>(json)));
        SceneManager.LoadSceneAsync("MainScene");
    }

    public static void OnEnd()
    {
        SceneManager.LoadSceneAsync("EndScene");
    }

    public static void OnQuit()
    {
        Application.Quit();
    }

    public static void OnReturn()
    {
        SceneManager.LoadSceneAsync("StartScene");
    }

    private static string GetSavePath()
    {
        return Application.persistentDataPath + "/save";
    }

    private static void InitStage()
    {
        MyGame.Office.Initialize();
        MyGame.Province.Initialize();
        MyGame.Hougong.Initialize();
        MyGame.Faction.Initialize();
    }

    private static void Initialize(string strEmpName, string strYearName, string strDynastyName)
    {
        InitStage();

        MyGame.Person.Initialize();

        MyGame.RelationManager.Initialize();

        MyGame.Emperor.Initialize(strEmpName, Tools.Probability.GetRandomNum(16, 40), Tools.Probability.GetRandomNum(5, 8));

        MyGame.GameTime.Initialize();
        MyGame.DynastyInfo.Initialize(strYearName, strDynastyName);
    }

    public static bool gameEnd = false;
    public static EventManager eventManager = null;
    //private static GameFrame m_Instance;
}
