using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public MainScene mainScene;
    public BlackCurtain curtain;
    //�ж��Ƿ�����ƶ�
    public bool canMove = true;
    //�ж��Ƿ������ƶ�
    private bool isMoving = false;
    //�ƶ�Ŀ���
    private Vector3 targetPos;
    //�������
    public Camera mainCamera;
    //�ƶ������б�
    private List<bool> moveBools = new List<bool>() {false, false, false, false};
    //��ʾ��
    public GameObject point;
    //����Ҫһ��һ����Ԫ��
    public Tile tigeryaShadow;
    //��Ҫһ��һ���ĵ�ͼ��
    public Tilemap tigerya;
    //�ж�boss�ܲ����ƶ�
    private bool canBossMove = true;

    public Tilemap taoArea;
    public Tilemap fish;
    public Tilemap menya;
    public Tilemap swordArea;
    public Tilemap coiled;
    public Tilemap leviathan;
    public Tilemap vortex;
    public Tilemap bookArea;
    public Tilemap door;
    public Tilemap wave;
    public Tilemap octan;

    public GameObject remind;
    public GameObject keyRemind;

    public List<Vector3Int> vector3s;

    public Tile swordIslandWithLight;
    public Tile yellowDoor;
    public Tile lang1, lang2, lang3;

    private int upClick;
    private int rightClick;
    private int upTempClick;
    private int rightTempClick;

    public Slider foodSlider;
    public Slider hpSlider;

    public GameObject book;

    public Vector3 cameraTrans;

    /// <summary>
    /// �����Ƿ񱻰���
    /// </summary>
    private bool isKeyPressed = false;
    /// <summary>
    /// ��������
    /// </summary>
    private float keyPressedTime;
    /// <summary>
    private bool isPressing;

    /// ����
    /// </summary>
    public int DayTime;
    /// <summary>
    /// ���Ѫ��
    /// </summary>
    public int MaxHp = 5;
    /// <summary>
    /// ��ǰѪ��
    /// </summary>
    public int CurrentHp = 5;
    /// <summary>
    /// ����Ҫ��
    /// </summary>
    public bool IsDying = false;
    //��������ʱ
    public int dyingDay;
    /// <summary>
    /// ���ʳ������
    /// </summary>
    public int MaxFood = 40;
    /// <summary>
    /// ��ǰ��ʳ��
    /// </summary>
    public int CurrentFood = 40;
    /// <summary>
    /// ��ʳ������
    /// </summary>
    public int DecreaseFood;
    /// <summary>
    /// ι������
    /// </summary>
    int FeedingComsume = 5;
    /// <summary>
    /// �����ջ�
    /// </summary>
    int FishingFood = 18;

    /// <summary>
    /// �ж�״̬
    /// </summary>
    /// 
    bool IsPoison;
    public Dictionary<string, bool> IsKillBoss;//boss��ɱ��
    private int lastStepNum;//����ʣ�µ�����


    /// <summary>
    /// ��ǰ����Tag
    /// </summary>
    public string CurrentAreaTag;
    /// <summary>
    /// ��������Tag
    /// </summary>
    public string PreAreaTag;
    /// <summary>
    /// ��ǰ��������
    /// </summary>
    public GameObject CurrentArea;
    /// <summary>
    /// ������������
    /// </summary>
    public GameObject PreArea;
    int StepNum;
    //ι����
    public int fedNum = 0;

    public static PlayerController _playerController;
    public static PlayerController instance
    {
        get
        {
            if (_playerController != null)
            {
                return _playerController;
            }
            else
            {
                _playerController = Transform.FindObjectOfType<PlayerController>();
                return _playerController;
            }
        }
    }

    private void Start()
    {
        IsKillBoss = new Dictionary<string, bool>();
        IsKillBoss.Add("Coiled", false);
        IsKillBoss.Add("Leviathan", false);
        IsKillBoss.Add("Tigerya", false);
        IsKillBoss.Add("Menya", false);
        IsKillBoss.Add("Tao", false);

        mainScene.ConstellationImages[Chunk.randomTemp[0]].gameObject.SetActive(false);

        PreArea = this.gameObject;

        CurrentHp = 5;
        MaxHp = 5;
        CurrentFood = 50;
        MaxFood = 50;

        vector3s = new List<Vector3Int>();

        cameraTrans = new Vector3(0, 0, 0);

        Save.walked.Clear();
    }

    private void Update()
    {
        MoveDirection();

        PressedSpace();

        foodSlider.value = (float)CurrentFood / (float)MaxFood;
        hpSlider.value = (float)CurrentHp / (float)MaxHp;
    }

    private void FixedUpdate()
    {
        //��������ƶ�
        if (isMoving)
        {

            //����
            if (Chunk.tigeryaPos == 11 && canBossMove)
            {
                tigerya.SetTile(Chunk.tigeryaPoints[11], null);
                tigerya.SetTile(Chunk.tigeryaPoints[0], tigeryaShadow);
                Chunk.tigeryaPos = 0;
                canBossMove = false;
            }
            else if (Chunk.tigeryaPos < 11 && canBossMove)
            {
                tigerya.SetTile(Chunk.tigeryaPoints[Chunk.tigeryaPos], null);
                tigerya.SetTile(Chunk.tigeryaPoints[Chunk.tigeryaPos + 1], tigeryaShadow);
                Chunk.tigeryaPos += 1;
                canBossMove = false;
            }


            //��ֵ�ƶ�
            transform.position = Vector3.Lerp(transform.position, targetPos, 15 * Time.deltaTime);
            //����������ھ��뵽Ŀ�ĵؾ���С��0.01m
            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                //���Խ�����һ���ƶ�
                canMove = true;
                //û�������ƶ�
                isMoving = false;
                //������������
                transform.position = targetPos;

                Save.walked.Add(targetPos);

                //����֮��
                Collider2D collider2D = Physics2D.OverlapCircle(transform.position + new Vector3(0, -0.31f, 1), 0.1f);
                CurrentArea = collider2D.gameObject;
                CurrentAreaTag = CurrentArea.tag;

                CheckCurrentArea();

                FoodComsume(1);

                //�����ж�
                if (IsDying)
                {
                    dyingDay += 1;
                }
                if (dyingDay >= 7)
                {
                    CurrentHp = 0;
                    GameOver();
                }

                TaoAreaRemind();

                PoisonFade();

                StepNum++;

                if(fedNum >= 3)
                {
                    vortex.gameObject.SetActive(false);
                }

                WaveUpgrade();
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    //Gizmos.DrawSphere(transform.position + new Vector3(-0.5f, -0.57735f), 0.225f);
    //    Gizmos.DrawSphere(transform.position - new Vector3(0, 0.288f, 0), 0.15f);
    //}

    private void MoveDirection()
    {
        //����������洬
        mainCamera.transform.position = transform.position;
        mainCamera.transform.Translate(0, -0.31f, -10);
        mainCamera.transform.Translate(cameraTrans);
        //Collider2D raycas tHit2D = Physics2D.OverlapCircle(transform.position + new Vector3(0.5f, 0f), 0.05f);
        //RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, new Vector3(0.5f, 0.288675f), 1f);
        //Debug.DrawRay(transform.position, new Vector3(-0.5f, -0.57735f));

        if (Input.GetKeyDown(KeyCode.W) && canMove)
        {
            Collider2D raycastHit2D = Physics2D.OverlapCircle(transform.position + new Vector3(0.5f, 0f, 0), 0.1f);
            PreArea = raycastHit2D.gameObject;
            if (moveBools[0])
            {
                if (!raycastHit2D.gameObject.CompareTag("WallArea") && !raycastHit2D.gameObject.CompareTag("Tao") && !raycastHit2D.gameObject.CompareTag("Sword")
                     && !raycastHit2D.gameObject.CompareTag("CoiledArea") && !raycastHit2D.gameObject.CompareTag("Leviathan") && !raycastHit2D.gameObject.CompareTag("RisingDoor")
                      && !raycastHit2D.gameObject.CompareTag("Vortex") && !raycastHit2D.gameObject.CompareTag("Octopus"))
                {
                    //�����˶�״̬
                    isMoving = true;
                    canMove = false;
                    //Ŀ�ĵ�����
                    targetPos = transform.position + new Vector3(0.5f, 0.288675f, 0);//0.288675����3/6��������������1/2
                    canBossMove = true;
                    point.SetActive(false);
                    moveBools[0] = !moveBools[0];
                    rightClick += 1;
                    rightTempClick = 0;
                }
            }
            else
            {
                point.SetActive(true);
                point.transform.position = transform.position + new Vector3(0.5f, 0f);
                moveBools[0] = !moveBools[0];
                moveBools[1] = false;
                moveBools[2] = false;
                moveBools[3] = false;
                CheckPreArea();
                rightTempClick = 1;
                upTempClick = 0;
            }

            //������û���߳ɶ�ת���ض�����
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.S) && canMove)
        {
            Collider2D raycastHit2D = Physics2D.OverlapCircle(transform.position + new Vector3(-0.5f, -0.57735f), 0.1f);
            PreArea = raycastHit2D.gameObject;
            if (moveBools[1])
            {
                if (!raycastHit2D.gameObject.CompareTag("WallArea") && !raycastHit2D.gameObject.CompareTag("Tao") && !raycastHit2D.gameObject.CompareTag("Sword")
                    && !raycastHit2D.gameObject.CompareTag("CoiledArea") && !raycastHit2D.gameObject.CompareTag("Leviathan") && !raycastHit2D.gameObject.CompareTag("RisingDoor")
                     && !raycastHit2D.gameObject.CompareTag("Vortex") && !raycastHit2D.gameObject.CompareTag("Octopus"))
                {
                    isMoving = true;
                    canMove = false;
                    targetPos = transform.position + new Vector3(-0.5f, -0.288675f, 0);
                    canBossMove = true;
                    point.SetActive(false);
                    moveBools[1] = !moveBools[1];
                    rightClick -= 1;
                    rightTempClick = 0;
                }
            }
            else
            {
                point.SetActive(true);
                point.transform.position = transform.position + new Vector3(-0.5f, -0.57735f);
                moveBools[1] = !moveBools[1];
                moveBools[0] = false;
                moveBools[2] = false;
                moveBools[3] = false;
                CheckPreArea();
                rightTempClick = -1;
                upTempClick = 0;
            }

            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.A) && canMove)
        {
            Collider2D raycastHit2D = Physics2D.OverlapCircle(transform.position + new Vector3(-0.5f, 0f), 0.1f);
            PreArea = raycastHit2D.gameObject;
            if (moveBools[2])
            {
                if (!raycastHit2D.gameObject.CompareTag("WallArea") && !raycastHit2D.gameObject.CompareTag("Tao") && !raycastHit2D.gameObject.CompareTag("Sword")
                    && !raycastHit2D.gameObject.CompareTag("CoiledArea") && !raycastHit2D.gameObject.CompareTag("Leviathan") && !raycastHit2D.gameObject.CompareTag("RisingDoor")
                     && !raycastHit2D.gameObject.CompareTag("Vortex") && !raycastHit2D.gameObject.CompareTag("Octopus"))
                {
                    isMoving = true;
                    canMove = false;
                    targetPos = transform.position + new Vector3(-0.5f, 0.288675f, 0);
                    canBossMove = true;
                    point.SetActive(false);
                    moveBools[2] = !moveBools[2];
                    upClick += 1;
                    upTempClick = 0;
                }
            }
            else
            {
                point.SetActive(true);
                point.transform.position = transform.position + new Vector3(-0.5f, 0f);
                moveBools[2] = !moveBools[2];
                moveBools[0] = false;
                moveBools[1] = false;
                moveBools[3] = false;
                CheckPreArea();
                rightTempClick = 0;
                upTempClick = 1;
            }

            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.D) && canMove)
        {
            Collider2D raycastHit2D = Physics2D.OverlapCircle(transform.position + new Vector3(0.5f, -0.57735f), 0.1f);
            PreArea = raycastHit2D.gameObject;
            if (moveBools[3])
            {
                if (!raycastHit2D.gameObject.CompareTag("WallArea") && !raycastHit2D.gameObject.CompareTag("Tao") && !raycastHit2D.gameObject.CompareTag("Sword")
                    && !raycastHit2D.gameObject.CompareTag("CoiledArea") && !raycastHit2D.gameObject.CompareTag("Leviathan") && !raycastHit2D.gameObject.CompareTag("RisingDoor")
                     && !raycastHit2D.gameObject.CompareTag("Vortex") && !raycastHit2D.gameObject.CompareTag("Octopus"))
                {
                    isMoving = true;
                    canMove = false;
                    targetPos = transform.position + new Vector3(0.5f, -0.288675f, 0);
                    canBossMove = true;
                    point.SetActive(false);
                    moveBools[3] = !moveBools[3];
                    upClick -= 1;
                    upTempClick = 0;
                }
            }
            else
            {
                point.SetActive(true);
                point.transform.position = transform.position + new Vector3(0.5f, -0.57735f);
                moveBools[3] = !moveBools[3];
                moveBools[0] = false;
                moveBools[1] = false;
                moveBools[2] = false;
                CheckPreArea();
                rightTempClick = 0;
                upTempClick = -1;
            }

            this.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    
    void FoodComsume(int _num)
    {
        if (CurrentFood - _num >= 0)
        {
            CurrentFood -= _num;
            DecreaseFood += _num;
        }
        else
        {
            CurrentFood = 0;
            IsDying = true;
        }
    }

    void FoodAdd(int _num)
    {
        if (CurrentFood + _num <= MaxFood)
        {
            CurrentFood += _num;
        }
        else
        {
            CurrentFood = MaxFood;
        }
        IsDying = false;

    }

    ///// <summary>
    ///// ������������ҵĶ�������
    ///// </summary>
    ///// <param name="_ObjectPos"></param>
    ///// <returns></returns>
    //string CaculateHorizontalPos(Vector2 _ObjectPos)
    //{
    //    if (transform.position.x - _ObjectPos.x > 0)
    //    {
    //        return "��";
    //    }
    //    else if (transform.position.x - _ObjectPos.x < 0)
    //    {
    //        return "��";
    //    }
    //    else
    //    {
    //        return "";
    //    }
    //}
    ///// <summary>
    ///// ������������ҵ��ϻ��Ǳ�
    ///// </summary>
    ///// <param name="_ObjectPos"></param>
    ///// <returns></returns>
    //string CaculateVerticalPos(Vector2 _ObjectPos)
    //{
    //    if (transform.position.y - _ObjectPos.y > 0)
    //    {
    //        return "��";
    //    }
    //    else if (transform.position.y - _ObjectPos.y < 0)
    //    {
    //        return "��";
    //    }
    //    else
    //    {
    //        return "";
    //    }
    //}

    private string PlayerToTaoPos(Vector3Int taoPos)
    {
        Vector3 tao = new Vector3(0.5f * (taoPos.x - taoPos.y), 0.288675f * (taoPos.x + taoPos.y + 1));
        Vector3 vector = tao - transform.position;
        if(Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
        {
            if(vector.x > 0 && vector.y > 0)
            {
                return "��";
            }
            else if(vector.x > 0 && vector.y < 0)
            {
                return "��";
            }
            else if(vector.x < 0 && vector.y > 0)
            {
                return "��";
            }
            else if(vector.x < 0 && vector.y < 0)
            {
                return "��";
            }
            else
            {
                return " ";
            }
        }
        else
        {
            if(vector.y > 0)
            {
                return "����";
            }
            else if (vector.y < 0)
            {
                return "����";
            }
            else
            {
                return " ";
            }
        }
    }

    /// <summary>
    /// ������ʾ
    /// </summary>
    void TaoAreaRemind()
    {
        if (DecreaseFood - 10 >= 0 && !IsKillBoss["Tao"])
        {
            DecreaseFood -= 10;
            //EventRemind("��" + CaculateHorizontalPos(GameObject.FindGameObjectWithTag("Tao").transform.position) + CaculateVerticalPos(GameObject.FindGameObjectWithTag("Tao").transform.position) + "����Ʈ����ʳ�����������");
            StartCoroutine(EventRemind("��" + PlayerToTaoPos(Chunk.taoPos) + "����Ʈ����ʳ�����������"));
        }
    }

    /// <summary>
    /// ��⵱ǰ����
    /// </summary>
    void CheckCurrentArea()
    {
        switch (CurrentAreaTag)
        {
            //������
            case "FishingArea":
                if(CurrentFood >= 5 && fedNum < 3)
                {
                    fedNum++;
                    FoodComsume(FeedingComsume);
                    StartCoroutine(KeyRemind("��Ⱥ���������ʳ�"));
                    if (fedNum >= 3)
                    {
                        vortex.gameObject.SetActive(false);
                        StartCoroutine(EventRemind("��֪Ϊ�Σ���ӿ���ȵĲ��ν���ƽ������..."));
                    }
                }
                break;
            //������
            case "FogArea":
                Fog();
                break;
            //������
            case "PoisonArea":
                PoisonGet();
                break;
            //��������
            case "Tigerya":
                StartCoroutine(EventRemind("���������ڵ���ʯ֮��������˴���һ��ײ���ˣ�������û�ио����κ�ʹ�ࡣ"));
                GameOver();
                break;
            //������
            case "DieFish":
                if (!IsPoison && !IsKillBoss["Coiled"])
                {
                    Music.PlayingMusic(1,Resources.Load<AudioClip>("Audio/����ä״̬�ߵ�����Ⱥ��"));
                    GameOver();
                }
                break;
            //����������
            case "RealMenya":
                Music.PlayingMusic(1, Resources.Load<AudioClip>("Audio/��������櫵ĵ�ש"));
                StartCoroutine(EventRemind("��ײ����һ��͸��ʵ�����ů���������������⻳�������ս��������䰵��"));
                GameOver();
                break;
            case "Vortex":
                GameOver();
                break;
            case "Book":
                book.SetActive(true);
                bookArea.gameObject.SetActive(false);
                break;
            default:
                break;

        }
    }
    void CheckPreArea()
    {
        switch (PreArea.tag)
        {
            //������
            case "RisingDoor":
                StartCoroutine(KeyRemind("�����ո��Դ򿪷���֮��"));
                break;
            //������
            case "FishingArea":
                StartCoroutine(KeyRemind("�����ո��Բ���"));
                break;
            //������
            case "Tao":
                StartCoroutine(KeyRemind("�����ո���նɱ����"));
                break;
            //����������
            case "RealMenya":
                StartCoroutine(KeyRemind("�����ո���նɱ�����"));
                break;
            //��������
            case "Tigerya":
                StartCoroutine(KeyRemind("�����ո���նɱ��������"));
                break;
            //��
            case "Sword":
                StartCoroutine(KeyRemind("�����ո��Ե���"));
                break;
            //���
            case "CoiledArea":
                if (IsPoison)
                {
                    StartCoroutine(KeyRemind("�����ո���նɱ���"));
                }
                break;
            //��ά��ɣ��
            case "Leviathan":
                StartCoroutine(KeyRemind("�����ո���նɱ��ά��ɣ"));
                break;
            //������
            case "Octopus":
                StartCoroutine(KeyRemind("�����ո���ն�ϴ���"));
                break;
            case "Pirate":
                StartCoroutine(KeyRemind("��ܺ���"));
                break;
            case "Vortex":
                StartCoroutine(KeyRemind("�����ļ������ɽ��롣"));
                break;
            case "Book":
                StartCoroutine(KeyRemind("�����ո���ʰȡʷ��"));
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// �����ո񴥷����������¼�
    /// </summary>
    void PressPreArea()
    {
        switch (PreArea.tag)
        {
            //������
            case "RisingDoor":
                //��֮ǰҪSave
                SceneManager.LoadScene("MyGod");
                break;
            //������
            case "FishingArea":
                FoodAdd(18);
                Music.PlayingMusic(1, Resources.Load<AudioClip>("Audio/����"));
                Vector3Int temp = new Vector3Int(-3, 9, 0) + new Vector3Int(rightClick, upClick, 0) + new Vector3Int(rightTempClick, upTempClick, 0);
                fish.SetTile(temp, null);
                if (fedNum > 0)
                {
                    fedNum--;
                }
                break;
            //������
            case "Tao":
                if (CurrentFood == 0)
                {
                    mainScene.ChangeTotem(4);
                    curtain.IsClosing = true;
                    curtain.Video.GetComponent<VideoPlayer>().clip = Resources.Load<VideoClip>("Video/5");
                    curtain.Video.GetComponent<VideoPlayer>().Play();
                    IsKillBoss["Tao"] = true;
                    FoodAdd(100);
                    taoArea.SetTile(Chunk.taoPos, null);
                }
                else
                {
                    StartCoroutine(EventRemind("�ҳ�Ц�������д������������������չʾ��һλ����ʳ�͵�ʢ�磬�����ǽ�������ˡ�"));
                    GameOver();
                }
                break;
            //����������
            case "RealMenya":
                mainScene.ChangeTotem(3);
                curtain.IsClosing = true;
                curtain.Video.GetComponent<VideoPlayer>().clip = Resources.Load<VideoClip>("Video/4");
                curtain.Video.GetComponent<VideoPlayer>().Play();
                IsKillBoss["Menya"] = true;
                menya.gameObject.SetActive(false);
                break;
            //��������
            case "Tigerya":
                mainScene.ChangeTotem(2);
                curtain.IsClosing = true;
                curtain.Video.GetComponent<VideoPlayer>().clip = Resources.Load<VideoClip>("Video/2");
                curtain.Video.GetComponent<VideoPlayer>().Play();
                IsKillBoss["Tigerya"] = true;
                tigerya.gameObject.SetActive(false);
                break;
            //��
            case "Sword":
                StartCoroutine(EventRemind("�������ǿ��и�����ָʾ"));
                mainScene.ConstellationImages[Chunk.randomTemp[0]].gameObject.SetActive(true);
                mainScene.ConstellationImages[Chunk.randomTemp[0]].CrossFadeAlpha(1, 0, true);
                swordArea.SetTile(Chunk.swordPos, null);
                swordArea.SetTile(Chunk.swordPos, swordIslandWithLight);
                break;
            //���
            case "CoiledArea":
                mainScene.ChangeTotem(0);
                if (IsPoison)
                {
                    curtain.IsClosing = true;
                    curtain.Video.GetComponent<VideoPlayer>().clip = Resources.Load<VideoClip>("Video/1");
                    curtain.Video.GetComponent<VideoPlayer>().Play();
                    IsKillBoss["Coiled"] = true;
                    coiled.SetTile(Chunk.coiledPos, null);
                }
                break;
            //��ά��ɣ��
            case "Leviathan":
                mainScene.ChangeTotem(1);
                curtain.IsClosing = true;
                curtain.Video.GetComponent<VideoPlayer>().clip = Resources.Load<VideoClip>("Video/3");
                curtain.Video.GetComponent<VideoPlayer>().Play();
                IsKillBoss["Leviathan"] = true;
                leviathan.SetTile(Chunk.leviathanPos, null);
                break;
            //������
            case "Octopus":
                Music.PlayingMusic(1, Resources.Load<AudioClip>("Audio/�����Ĵ�"));
                StartCoroutine(EventRemind("���ո��˴��֣�����ȴ��ս�����ܵ����˺�"));
                Vector3Int temp2 = new Vector3Int(-3, 9, 0) + new Vector3Int(rightClick, upClick, 0) + new Vector3Int(rightTempClick, upTempClick, 0);
                octan.SetTile(temp2, null);
                GameOver();
                FoodAdd(10);
                break;
            case "Pirate":

                break;
            case "Book":
                book.SetActive(true);
                bookArea.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void Fog()
    {
        int _killingNum = 0;
        foreach (bool _item in IsKillBoss.Values)
        {
            if (_item)
            {
                _killingNum++;
            }
        }
        if (_killingNum < 3)
        {
            StartCoroutine(EventRemind("�㲻���ʸ����п�Ѫ�������˽������Դ��ߵ�������"));
            GameOver();
        }
        else
        {
            door.SetTile(new Vector3Int(10, 9, 0), null);
            door.SetTile(new Vector3Int(10, 9, 0), yellowDoor);
        }
    }

    void PoisonGet()
    {
        IsPoison = true;
        mainCamera.orthographicSize = 0.75f;
        lastStepNum = 0;
        StartCoroutine(EventRemind("�����Ұ�������ˣ��ж�50�����ɱ��������"));
        cameraTrans = new Vector3(0, 0.2f, 0); 
    }
    /// <summary>
    /// �
    /// </summary>
    /// <param name="Index">�Ƿ�նɱ���</param>
    void PoisonFade()
    {
        if (IsKillBoss["Coiled"])
        {
            IsPoison = false;
            mainCamera.orthographicSize = 1.33f;
            lastStepNum = 0;
            cameraTrans = Vector3.zero;
        }
        else if (lastStepNum > 50)
        {
            IsPoison = false;
            mainCamera.orthographicSize = 1.33f;
            lastStepNum = 0;
            cameraTrans = Vector3.zero;
        }
        else
        {
            lastStepNum++;
        }
        
    }

    void GameOver()
    {
        CurrentHp--;
        if (CurrentHp <= 0)
        {
            keyRemind.SetActive(false);
            remind.SetActive(false);
            curtain.IsGameOver = true;
            Chunk.randomNum = new List<int>() { 0, 1, 2, 4, 5, 6, 7 };
            Chunk.randomTemp = new List<int>();
            if (dyingDay >= 7)
            {
                curtain.GameoverVideo.GetComponent<VideoPlayer>().clip = Resources.Load<VideoClip>("Video/deserve");
                curtain.GameoverVideo.GetComponent<VideoPlayer>().Play();
            }
            else
            {
                switch (CurrentArea.tag)
                {
                    case "DieFish":
                        curtain.GameoverVideo.GetComponent<VideoPlayer>().clip = Resources.Load<VideoClip>("Video/coiled");
                        curtain.GameoverVideo.GetComponent<VideoPlayer>().Play();
                        break;
                    case "Tigerya":
                        curtain.GameoverVideo.GetComponent<VideoPlayer>().clip = Resources.Load<VideoClip>("Video/tigerya");
                        curtain.GameoverVideo.GetComponent<VideoPlayer>().Play();
                        break;
                    case "RealMenya":
                        curtain.GameoverVideo.GetComponent<VideoPlayer>().clip = Resources.Load<VideoClip>("Video/menya");
                        curtain.GameoverVideo.GetComponent<VideoPlayer>().Play();
                        break;
                    case "FogArea":
                        curtain.GameoverVideo.GetComponent<VideoPlayer>().clip = Resources.Load<VideoClip>("Video/fog");
                        curtain.GameoverVideo.GetComponent<VideoPlayer>().Play();
                        break;
                    default:
                        break;
                }
                switch (PreArea.tag)
                {
                    case "Octopus":
                        curtain.GameoverVideo.GetComponent<VideoPlayer>().clip = Resources.Load<VideoClip>("Video/octopus");
                        curtain.GameoverVideo.GetComponent<VideoPlayer>().Play();
                        break;
                    case "Tao":
                        curtain.GameoverVideo.GetComponent<VideoPlayer>().clip = Resources.Load<VideoClip>("Video/tao");
                        curtain.GameoverVideo.GetComponent<VideoPlayer>().Play();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    IEnumerator EventRemind(string _text)
    {
        remind.SetActive(true);
        remind.GetComponent<Text>().text = _text;
        yield return new WaitForSeconds(2f);
        remind.SetActive(false);
    }
    IEnumerator KeyRemind(string _text)
    {
        keyRemind.SetActive(true);
        keyRemind.GetComponent<Text>().text = _text;
        yield return new WaitForSeconds(1f);
        keyRemind.SetActive(false);
    }

    /// <summary>
    /// �������ǰ������
    /// </summary>
    void PressedSpace()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            Music.PlayingMusic(1, Resources.Load<AudioClip>("Audio/�����ո�"));
            //����״̬Ϊ����
            isKeyPressed = true;
            canMove = false;
        }

        if (Input.GetKey(KeyCode.Space) && isKeyPressed)
        {
            keyPressedTime += Time.deltaTime;
            if (keyPressedTime > 1 & !isPressing)
            {
                isPressing = true;
                PressPreArea();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) && isKeyPressed)
        {
            keyPressedTime = 0;
            isKeyPressed = false;
            isPressing = false;
            canMove = true;
            Music.PlayingMusic(1, null);

        }
    }
    
    private void WaveUpgrade()
    {
        foreach (Vector3Int item in vector3s)
        {
            wave.SetTile(item, null);
        }

        vector3s = new List<Vector3Int>();

        for (int i = 0; i < 60; i++)
        {
            float q = UnityEngine.Random.value;
            Vector3Int temp = new Vector3Int(UnityEngine.Random.Range(-5, 24), UnityEngine.Random.Range(-5, 24), 0);
            if (q < 0.33f)
            {
                wave.SetTile(temp, lang1);
            }
            else if(q > 0.66f)
            {
                wave.SetTile(temp, lang2);
            }
            else
            {
                wave.SetTile(temp, lang3);
            }
            vector3s.Add(temp);
        }
    }
}