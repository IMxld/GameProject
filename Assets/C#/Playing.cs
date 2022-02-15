using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class Playing : MonoBehaviour
{
    //文件读取路径
    private string path;

    //逐行读取内容
    private string[] strs;

    //记录读取到哪一行
    private int row = 0;

    //舞台物体
    public GameObject stage;

    //事件触发器
    private EventTrigger temp;

    //已加载物体字典
    private Dictionary<string, GameObject> loadObjects = new Dictionary<string, GameObject>();

    //被移动物体的起始和终点坐标
    private Vector3 nowPosition, targetPosition;

    //被移动物体
    private GameObject ga;

    //移动速度
    private float speed;

    //音效播放器
    private AudioSource SoundPlayer = new AudioSource();

    //判断是否播放完毕
    public static bool isend = false;

    //记录上一个说话的人
    private GameObject lastSay;

    //判断是否正在auto
    private bool isAuto;

    //角色和演员列表
    private Dictionary<string, string> actors = new Dictionary<string, string>();

    //剧本和上一个列表
    private Dictionary<string, Dictionary<string, string>> bookAndActor = new Dictionary<string, Dictionary<string, string>>();

    //对应第几个演员坑位
    private int q = 1;

    //对应第几个剧本
    private int b = 1;

    //角色物体
    private GameObject g;

    //需要播放的列表
    private List<int> books = new List<int>();

    //背景物体
    [SerializeField]
    public GameObject backGround;

    //背景图片
    private Sprite backGroundPic;

    //移动协程
    private IEnumerator MoveObject_Speed(GameObject ga, Vector3 startPos, Vector3 endPos, float speed, GameObject dia)
    {
        float startTime = Time.time;
        float length = Vector3.Distance(startPos, endPos);
        float frac = 0;
        
        while (frac < 1.0f)
        {
            float dist = (Time.time - startTime) * speed;
            frac = dist / length;
            ga.GetComponent<RectTransform>().localPosition = Vector3.Lerp(startPos, endPos, frac);
            try
            {
                dia.GetComponent<RectTransform>().localPosition = 
                    ga.GetComponent<RectTransform>().localPosition + new Vector3(0, 250, 0);
            }
            catch (KeyNotFoundException) { }
            catch (NullReferenceException) { }
            yield return null;
        }
    }

    //滚动协程
    private IEnumerator Roll(GameObject rolling, float t, float speed)
    {
        float startTime = Time.time;

        while (Time.time - startTime < t)
        {
            rolling.GetComponent<RectTransform>().localEulerAngles -= new Vector3(0, 0, speed);
            yield return null;
        }
    }

    private void Auto()
    {
        ReadTxtAndPlay(row);
    }

    public void OnMouseDown()
    {
        isAuto = !isAuto;
        if(isAuto)
        {
            InvokeRepeating("Auto", 2, 2);
        }
        else
        {
            CancelInvoke();
        }
    }

    //读取文件并添加eventtrigger组件
    private void Awake()
    {
        //path = Application.dataPath + "/Resources/play_1.txt";
        //strs = File.ReadAllLines(path);

        stage = this.gameObject;

        stage.AddComponent<EventTrigger>();
        temp = stage.GetComponent<EventTrigger>();
        temp.triggers = new List<EventTrigger.Entry>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        UnityAction<BaseEventData> callBack = new UnityAction<BaseEventData>(MouseDown);
        entry.callback.AddListener(callBack);
        temp.triggers.Add(entry);

        foreach(var i in MainController.mainController.PreDramas)
        {
            if (i)
            {
                foreach (var n in i.GetComponent<MainDrama>().CharactersList)
                {
                    actors.Add(q.ToString(), n.CharacterSprite.name);
                    q++;
                }
                bookAndActor.Add(b.ToString(), actors);
                actors = new Dictionary<string, string>();
                books.Add(i.GetComponent<MainDrama>().Num + 1);
            }
            else
            {
                books.Add(0);
            }
            b++;
            q = 1;
        }
        b = 1;
    }

    //读取文件首行，在鼠标点击前加载好场景
    private void Start()
    {
        try
        {
            path = Application.dataPath + "/Resources/play_" + books[b - 1].ToString() + ".txt";
            strs = File.ReadAllLines(path);
            ReadTxtAndPlay(row);
        }
        catch(FileNotFoundException)
        {
            b++;
        }
    }

    //鼠标每点击一次读取一行（或多行）
    public void MouseDown(BaseEventData baseEventData)
    {
        ReadTxtAndPlay(row);
    }

    private void ReadTxtAndPlay(int i)
    {
        //加个判断是否正在逐字显示中
        try
        {
            string[] a = strs[i].Split('/');
            switch (a[0])
            {
                //加载人物
                case "LOADCHARACTORS":
                    try
                    {
                        g = (GameObject)Instantiate(Resources.Load(bookAndActor[b.ToString()][a[1]]));
                    }
                    catch(KeyNotFoundException)
                    {
                        b++;
                        break;
                    }
                    g.GetComponent<RectTransform>().SetParent(this.transform);
                    loadObjects.Add(a[5], g);
                    loadObjects[a[5]].GetComponent<RectTransform>().localPosition = 
                        new Vector3(float.Parse(a[2]), float.Parse(a[3]), float.Parse(a[4]));
                    row++;
                    ReadTxtAndPlay(row);
                    break;

                //加载其他物体
                case "LOAD":
                    GameObject gt = (GameObject)Instantiate(Resources.Load(a[1]));
                    gt.GetComponent<RectTransform>().SetParent(this.transform);
                    loadObjects.Add(a[5], gt);
                    loadObjects[a[5]].GetComponent<RectTransform>().localPosition =
                        new Vector3(float.Parse(a[2]), float.Parse(a[3]), float.Parse(a[4]));
                    row++;
                    ReadTxtAndPlay(row);
                    break;

                //加载对话框
                case "LOADDIALOGUE":
                    GameObject g1 = (GameObject)Instantiate(Resources.Load("Dialogue"));
                    g1.GetComponent<RectTransform>().SetParent(this.transform);
                    loadObjects.Add(a[1] + "_dia", g1);
                    loadObjects[a[1] + "_dia"].GetComponent<RectTransform>().localPosition = 
                        loadObjects[a[1]].GetComponent<RectTransform>().localPosition + new Vector3(0, 250, 0);
                    row++;
                    ReadTxtAndPlay(row);
                    break;

                //非对话行动结束
                case "END":
                    row++;
                    break;

                //对话
                case "SAY":
                    try
                    {
                        lastSay.gameObject.SetActive(false);
                    }
                    catch (NullReferenceException) { }
                    catch (MissingReferenceException) { }
                    ga = loadObjects[a[1] + "_dia"];
                    ga.gameObject.SetActive(true);
                    ga.GetComponentInChildren<Text>().text = a[2];
                    row++;
                    lastSay = ga;
                    break;

                //移动
                case "MOVE":
                    nowPosition = loadObjects[a[1]].GetComponent<RectTransform>().localPosition;
                    targetPosition = new Vector3(float.Parse(a[2]), float.Parse(a[3]), float.Parse(a[4]));
                    ga = loadObjects[a[1]];
                    speed = float.Parse(a[5]);
                    GameObject gadia = null;
                    try
                    {
                        gadia = loadObjects[a[1] + "_dia"];
                    }
                    catch (KeyNotFoundException) { }
                    //启用协程
                    StartCoroutine(MoveObject_Speed(ga, nowPosition, targetPosition, speed, gadia));
                    row++;
                    ReadTxtAndPlay(row);
                    break;

                //物体消失
                case "DISAPPEAR":
                    loadObjects[a[1]].gameObject.SetActive(false);
                    row++;
                    ReadTxtAndPlay(row);
                    break;

                //物体出现
                case "APPEAR":
                    loadObjects[a[1]].gameObject.SetActive(true);
                    row++;
                    ReadTxtAndPlay(row);
                    break;

                //播放音效
                case "MUSICPLAY":
                    AudioClip clip = Resources.Load<AudioClip>(a[1]);
                    SoundPlayer = this.GetComponent<AudioSource>();
                    SoundPlayer.clip = clip;
                    SoundPlayer.PlayOneShot(clip);
                    row++;
                    ReadTxtAndPlay(row);
                    break;

                //物体旋转
                case "ROTATE":
                    ga = loadObjects[a[1]];
                    ga.GetComponent<RectTransform>().localEulerAngles = 
                        new Vector3(float.Parse(a[2]), float.Parse(a[3]), float.Parse(a[4]));
                    row++;
                    ReadTxtAndPlay(row);
                    break;

                //物体滚动
                case "ROLL":
                    StartCoroutine(Roll(loadObjects[a[1]], float.Parse(a[2]), float.Parse(a[3])));
                    row++;
                    ReadTxtAndPlay(row);
                    break;

                //切换场景背景
                case "BGCHANGE":
                    backGround.GetComponent<Image>().sprite = Instantiate(Resources.Load<Sprite>(a[1]));
                    row++;
                    ReadTxtAndPlay(row);
                    break;
            }
        }
        catch (IndexOutOfRangeException)
        {
            b++;
            try
            {
                GameObject test = (GameObject)Instantiate(Resources.Load((bookAndActor[b.ToString()])["1"]));
                foreach (var g in loadObjects)
                {
                    Destroy(g.Value);
                }
                row = 0;
                loadObjects = new Dictionary<string, GameObject>();
                q = 1;
                path = Application.dataPath + "/Resources/play_" + books[b - 1].ToString() + ".txt";
                strs = File.ReadAllLines(path);
            }
            catch (KeyNotFoundException)
            {
                b++;
                if (b >= 4)
                {
                    isend = true;
                    Invoke("StopAllCoroutines", 3);
                }
                else
                {
                    try
                    {
                        GameObject test = (GameObject)Instantiate(Resources.Load((bookAndActor[b.ToString()])["1"]));
                        foreach (var g in loadObjects)
                        {
                            Destroy(g.Value);
                        }
                        row = 0;
                        loadObjects = new Dictionary<string, GameObject>();
                        q = 1;
                        path = Application.dataPath + "/Resources/play_" + books[b - 1].ToString() + ".txt";
                        strs = File.ReadAllLines(path);
                    }
                    catch (KeyNotFoundException)
                    {
                        isend = true;
                        Invoke("StopAllCoroutines", 3);
                    }
                }
            }
        }
        catch (NullReferenceException)
        {
            b++;
            try
            {
                path = Application.dataPath + "/Resources/play_" + books[b - 1].ToString() + ".txt";
                strs = File.ReadAllLines(path);
                ReadTxtAndPlay(row);
            }
            catch (FileNotFoundException)
            {
                b++;
                path = Application.dataPath + "/Resources/play_" + books[b - 1].ToString() + ".txt";
                strs = File.ReadAllLines(path);
                ReadTxtAndPlay(row);
            }
        }
    }
}