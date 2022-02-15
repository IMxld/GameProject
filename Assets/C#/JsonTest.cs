using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;



/// <summary>
/// 1.ȷ��ÿ��������json�ı�������ͬ
/// 2.���������л���ʶ:Serializable
/// </summary>
public class ExampleDataTestManager
{
    #region ������Ϣ
    [Serializable]
    public class TestJsonObjectTXT
    {
        public string player;
    }

    [Serializable]
    public class NPCTest
    {
        public string ID; // ���
        public string Name; // ����
        public string AssetName; // ��Դ���
        public int HP; // Ѫ
        public int Attack; // ����
        public int Defence; // ����
        public string DateTest; // ��������
        public List<string> TestJsonArray; // ���Ե�Ԫ���ڵ�Json����
        public TestJsonObjectTXT TestJsonObject; // ���Ե�Ԫ���ڵ�Json����
    }
    [Serializable]
    public class ItemTest
    {
        public string ID; // ���
        public string Name; // ����
        public string AssetName; // ��Դ���
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
    /// ��������첽���أ���Ҫ�ټ�Callback�ص�
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
    /// �������ȫ�ֵ����ã�����ǵ�clear
    /// ���ݲ���һֱ����Ҳ���ԡ�
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