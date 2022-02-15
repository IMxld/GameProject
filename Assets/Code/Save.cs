using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Option
{
    public float music;
    public float voice;
    public float allMusic;
}
[Serializable]
public class Files
{
    public Option options;
}

public class Save : MonoBehaviour
{
    public static string jsonPath;
    public static Files files;

    //记录走过的路
    public static List<Vector3> walked = new List<Vector3>();

    private void Start()
    {
        jsonPath = Application.streamingAssetsPath + "/Option.json";
        ReadJson();

        walked.Add(new Vector3(-6f, 2.33f, 0));
    }

    private static void InitJsonData()
    {
        files = new Files
        {
            options = new Option
            {
                music = 50,
                voice = 50,
                allMusic = 50
            }
        };
    }

    public static void SaveJson()
    {
        if (!File.Exists(jsonPath))
        {
            File.Create(jsonPath).Dispose();
            InitJsonData();
        }
        string json = JsonUtility.ToJson(files, true);
        File.WriteAllText(jsonPath, json);
    }

    public static void ReadJson()
    {
        if (!File.Exists(jsonPath))
        {
            File.Create(jsonPath).Dispose();
            InitJsonData();
            SaveJson();
        }
        string json = File.ReadAllText(jsonPath);
        files = JsonUtility.FromJson<Files>(json);
    }
}
