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
    //�ļ���ȡ·��
    private string path;

    //���ж�ȡ����
    private string[] strs;

    //��¼��ȡ����һ��
    private int row = 0;

    //��̨����
    public GameObject stage;

    //�¼�������
    private EventTrigger temp;

    //�Ѽ��������ֵ�
    private Dictionary<string, GameObject> loadObjects = new Dictionary<string, GameObject>();

    //���ƶ��������ʼ���յ�����
    private Vector3 nowPosition, targetPosition;

    //���ƶ�����
    private GameObject ga;

    //�ƶ��ٶ�
    private float speed;

    //��Ч������
    private AudioSource SoundPlayer = new AudioSource();

    //�ж��Ƿ񲥷����
    public static bool isend = false;

    //��¼��һ��˵������
    private GameObject lastSay;

    //�ж��Ƿ�����auto
    private bool isAuto;

    //��ɫ����Ա�б�
    private Dictionary<string, string> actors = new Dictionary<string, string>();

    //�籾����һ���б�
    private Dictionary<string, Dictionary<string, string>> bookAndActor = new Dictionary<string, Dictionary<string, string>>();

    //��Ӧ�ڼ�����Ա��λ
    private int q = 1;

    //��Ӧ�ڼ����籾
    private int b = 1;

    //��ɫ����
    private GameObject g;

    //��Ҫ���ŵ��б�
    private List<int> books = new List<int>();

    //��������
    [SerializeField]
    public GameObject backGround;

    //����ͼƬ
    private Sprite backGroundPic;

    //�ƶ�Э��
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

    //����Э��
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

    //��ȡ�ļ������eventtrigger���
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

    //��ȡ�ļ����У��������ǰ���غó���
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

    //���ÿ���һ�ζ�ȡһ�У�����У�
    public void MouseDown(BaseEventData baseEventData)
    {
        ReadTxtAndPlay(row);
    }

    private void ReadTxtAndPlay(int i)
    {
        //�Ӹ��ж��Ƿ�����������ʾ��
        try
        {
            string[] a = strs[i].Split('/');
            switch (a[0])
            {
                //��������
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

                //������������
                case "LOAD":
                    GameObject gt = (GameObject)Instantiate(Resources.Load(a[1]));
                    gt.GetComponent<RectTransform>().SetParent(this.transform);
                    loadObjects.Add(a[5], gt);
                    loadObjects[a[5]].GetComponent<RectTransform>().localPosition =
                        new Vector3(float.Parse(a[2]), float.Parse(a[3]), float.Parse(a[4]));
                    row++;
                    ReadTxtAndPlay(row);
                    break;

                //���ضԻ���
                case "LOADDIALOGUE":
                    GameObject g1 = (GameObject)Instantiate(Resources.Load("Dialogue"));
                    g1.GetComponent<RectTransform>().SetParent(this.transform);
                    loadObjects.Add(a[1] + "_dia", g1);
                    loadObjects[a[1] + "_dia"].GetComponent<RectTransform>().localPosition = 
                        loadObjects[a[1]].GetComponent<RectTransform>().localPosition + new Vector3(0, 250, 0);
                    row++;
                    ReadTxtAndPlay(row);
                    break;

                //�ǶԻ��ж�����
                case "END":
                    row++;
                    break;

                //�Ի�
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

                //�ƶ�
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
                    //����Э��
                    StartCoroutine(MoveObject_Speed(ga, nowPosition, targetPosition, speed, gadia));
                    row++;
                    ReadTxtAndPlay(row);
                    break;

                //������ʧ
                case "DISAPPEAR":
                    loadObjects[a[1]].gameObject.SetActive(false);
                    row++;
                    ReadTxtAndPlay(row);
                    break;

                //�������
                case "APPEAR":
                    loadObjects[a[1]].gameObject.SetActive(true);
                    row++;
                    ReadTxtAndPlay(row);
                    break;

                //������Ч
                case "MUSICPLAY":
                    AudioClip clip = Resources.Load<AudioClip>(a[1]);
                    SoundPlayer = this.GetComponent<AudioSource>();
                    SoundPlayer.clip = clip;
                    SoundPlayer.PlayOneShot(clip);
                    row++;
                    ReadTxtAndPlay(row);
                    break;

                //������ת
                case "ROTATE":
                    ga = loadObjects[a[1]];
                    ga.GetComponent<RectTransform>().localEulerAngles = 
                        new Vector3(float.Parse(a[2]), float.Parse(a[3]), float.Parse(a[4]));
                    row++;
                    ReadTxtAndPlay(row);
                    break;

                //�������
                case "ROLL":
                    StartCoroutine(Roll(loadObjects[a[1]], float.Parse(a[2]), float.Parse(a[3])));
                    row++;
                    ReadTxtAndPlay(row);
                    break;

                //�л���������
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