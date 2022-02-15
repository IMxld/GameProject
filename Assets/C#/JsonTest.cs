using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;



/// <summary>
/// 1.确保每个变量和json的变量名相同
/// 2.添加类可序列化标识:Serializable
/// </summary>
public class ExampleDataTestManager
{
    #region 类型信息
    [Serializable]
    public class TestJsonObjectTXT
    {
        public string player;
    }

    [Serializable]
    public class NPCTest
    {
        public string ID; // 编号
        public string Name; // 名称
        public string AssetName; // 资源编号
        public int HP; // 血
        public int Attack; // 攻击
        public int Defence; // 防御
        public string DateTest; // 测试日期
        public List<string> TestJsonArray; // 测试单元格内的Json数组
        public TestJsonObjectTXT TestJsonObject; // 测试单元格内的Json对象
    }
    [Serializable]
    public class ItemTest
    {
        public string ID; // 编号
        public string Name; // 名称
        public string AssetName; // 资源编号
    }
    [Serializable]
    public class TestArray
    {
        public List<NPCTest> NPC;
        public List<ItemTest> Item;
    }

    #endregion

    public static Dictionary<string, NPCTest> mNPCTextDict = new Dictionary<string, NPCTest>();
    public static Dictionary<string, ItemTest> mItemDict = new Dictionary<string, ItemTest>();
    //private bool isFirstUse = true;

    /// <summary>
    /// 如果考虑异步加载，需要再加Callback回调
    /// </summary>
    public static void Init()
    {
        Clear();
        string jsonTest = ((TextAsset)Resources.Load("ExampleData")).text;
        TestArray jsonObject = JsonUtility.FromJson<TestArray>(jsonTest);

        if (jsonObject == null)
        {
            Debug.LogError("ExampleData data null");
        }

        if (jsonObject.NPC == null)
        {
            Debug.LogError("NPC data null");
        }
        if (jsonObject.Item == null)
        {
            Debug.LogError("Item data null");
        }

        foreach (NPCTest item in jsonObject.NPC)
        {
            mNPCTextDict[item.ID] = item;
        }
        foreach (ItemTest item in jsonObject.Item)
        {
            mItemDict[item.ID] = item;
        }
    }
    /// <summary>
    /// 如果不是全局的配置，用完记得clear
    /// 数据不大，一直缓存也可以。
    /// </summary>
    public static void Clear()
    {
        mNPCTextDict.Clear();
        mItemDict.Clear();
    }

}



public class JsonTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //string jsonTest = File.ReadAllText(@"E:\unity\JsonTest\New Unity Project\Assets\Resources\jsonFile.json", Encoding.UTF8);
        // string jsonTest = ((TextAsset)Resources.Load("jsonFile")).text;
        // JsonObjectModel jsonObject = JsonUtility.FromJson<JsonObjectModel>(jsonTest);

        ExampleDataTestManager.Init();
        Debug.Log("");

    }

    // Update is called once per frame
    void Update()
    {

    }
}