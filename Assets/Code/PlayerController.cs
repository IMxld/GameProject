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
    //判断是否可以移动
    public bool canMove = true;
    //判断是否正在移动
    private bool isMoving = false;
    //移动目标点
    private Vector3 targetPos;
    //主摄像机
    public Camera mainCamera;
    //移动方向列表
    private List<bool> moveBools = new List<bool>() {false, false, false, false};
    //提示点
    public GameObject point;
    //隔壁要一步一动的元素
    public Tile tigeryaShadow;
    //需要一步一动的地图层
    public Tilemap tigerya;
    //判断boss能不能移动
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
    /// 按键是否被按下
    /// </summary>
    private bool isKeyPressed = false;
    /// <summary>
    /// 按键按下
    /// </summary>
    private float keyPressedTime;
    /// <summary>
    private bool isPressing;

    /// 天数
    /// </summary>
    public int DayTime;
    /// <summary>
    /// 最大血量
    /// </summary>
    public int MaxHp = 5;
    /// <summary>
    /// 当前血量
    /// </summary>
    public int CurrentHp = 5;
    /// <summary>
    /// 饿的要死
    /// </summary>
    public bool IsDying = false;
    //濒死倒计时
    public int dyingDay;
    /// <summary>
    /// 最大食物数量
    /// </summary>
    public int MaxFood = 40;
    /// <summary>
    /// 当前粮食数
    /// </summary>
    public int CurrentFood = 40;
    /// <summary>
    /// 粮食减少数
    /// </summary>
    public int DecreaseFood;
    /// <summary>
    /// 喂鱼消耗
    /// </summary>
    int FeedingComsume = 5;
    /// <summary>
    /// 钓鱼收获
    /// </summary>
    int FishingFood = 18;

    /// <summary>
    /// 中毒状态
    /// </summary>
    /// 
    bool IsPoison;
    public Dictionary<string, bool> IsKillBoss;//boss击杀数
    private int lastStepNum;//毒还剩下的天数


    /// <summary>
    /// 当前区域Tag
    /// </summary>
    public string CurrentAreaTag;
    /// <summary>
    /// 即将区域Tag
    /// </summary>
    public string PreAreaTag;
    /// <summary>
    /// 当前区域物体
    /// </summary>
    public GameObject CurrentArea;
    /// <summary>
    /// 即将区域物体
    /// </summary>
    public GameObject PreArea;
    int StepNum;
    //喂鱼数
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
        //如果正在移动
        if (isMoving)
        {

            //不管
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


            //插值移动
            transform.position = Vector3.Lerp(transform.position, targetPos, 15 * Time.deltaTime);
            //如果物体现在距离到目的地距离小于0.01m
            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                //可以进行下一次移动
                canMove = true;
                //没有正在移动
                isMoving = false;
                //矫正物体坐标
                transform.position = targetPos;

                Save.walked.Add(targetPos);

                //走完之后
                Collider2D collider2D = Physics2D.OverlapCircle(transform.position + new Vector3(0, -0.31f, 1), 0.1f);
                CurrentArea = collider2D.gameObject;
                CurrentAreaTag = CurrentArea.tag;

                CheckCurrentArea();

                FoodComsume(1);

                //濒死判定
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
        //主摄像机跟随船
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
                    //正在运动状态
                    isMoving = true;
                    canMove = false;
                    //目的地坐标
                    targetPos = transform.position + new Vector3(0.5f, 0.288675f, 0);//0.288675即√3/6，是正交比例的1/2
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

            //无论有没有走成都转到特定方向
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
    ///// 计算物体在玩家的东还是西
    ///// </summary>
    ///// <param name="_ObjectPos"></param>
    ///// <returns></returns>
    //string CaculateHorizontalPos(Vector2 _ObjectPos)
    //{
    //    if (transform.position.x - _ObjectPos.x > 0)
    //    {
    //        return "东";
    //    }
    //    else if (transform.position.x - _ObjectPos.x < 0)
    //    {
    //        return "西";
    //    }
    //    else
    //    {
    //        return "";
    //    }
    //}
    ///// <summary>
    ///// 计算物体在玩家的南还是北
    ///// </summary>
    ///// <param name="_ObjectPos"></param>
    ///// <returns></returns>
    //string CaculateVerticalPos(Vector2 _ObjectPos)
    //{
    //    if (transform.position.y - _ObjectPos.y > 0)
    //    {
    //        return "北";
    //    }
    //    else if (transform.position.y - _ObjectPos.y < 0)
    //    {
    //        return "南";
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
                return "北";
            }
            else if(vector.x > 0 && vector.y < 0)
            {
                return "东";
            }
            else if(vector.x < 0 && vector.y > 0)
            {
                return "西";
            }
            else if(vector.x < 0 && vector.y < 0)
            {
                return "南";
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
                return "西北";
            }
            else if (vector.y < 0)
            {
                return "东南";
            }
            else
            {
                return " ";
            }
        }
    }

    /// <summary>
    /// 饕餮提示
    /// </summary>
    void TaoAreaRemind()
    {
        if (DecreaseFood - 10 >= 0 && !IsKillBoss["Tao"])
        {
            DecreaseFood -= 10;
            //EventRemind("从" + CaculateHorizontalPos(GameObject.FindGameObjectWithTag("Tao").transform.position) + CaculateVerticalPos(GameObject.FindGameObjectWithTag("Tao").transform.position) + "方向飘来了食物的香气……");
            StartCoroutine(EventRemind("从" + PlayerToTaoPos(Chunk.taoPos) + "方向飘来了食物的香气……"));
        }
    }

    /// <summary>
    /// 检测当前区域
    /// </summary>
    void CheckCurrentArea()
    {
        switch (CurrentAreaTag)
        {
            //钓鱼区
            case "FishingArea":
                if(CurrentFood >= 5 && fedNum < 3)
                {
                    fedNum++;
                    FoodComsume(FeedingComsume);
                    StartCoroutine(KeyRemind("鱼群抢走了你的食物！"));
                    if (fedNum >= 3)
                    {
                        vortex.gameObject.SetActive(false);
                        StartCoroutine(EventRemind("不知为何，翻涌澎湃的波涛渐渐平复下来..."));
                    }
                }
                break;
            //门雾区
            case "FogArea":
                Fog();
                break;
            //毒雾区
            case "PoisonArea":
                PoisonGet();
                break;
            //提迦利亚
            case "Tigerya":
                StartCoroutine(EventRemind("提迦利亚在电光火石之间把你连人带船一起撞碎了，你甚至没有感觉到任何痛苦。"));
                GameOver();
                break;
            //七蟠领地
            case "DieFish":
                if (!IsPoison && !IsKillBoss["Coiled"])
                {
                    Music.PlayingMusic(1,Resources.Load<AudioClip>("Audio/非致盲状态走到死鱼群块"));
                    GameOver();
                }
                break;
            //美尼亚真身
            case "RealMenya":
                Music.PlayingMusic(1, Resources.Load<AudioClip>("Audio/走上美妮娅的地砖"));
                StartCoroutine(EventRemind("你撞进了一个透明实体的温暖怀抱，世界随着这怀抱的逐渐收紧而慢慢变暗。"));
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
            //飞升门
            case "RisingDoor":
                StartCoroutine(KeyRemind("长按空格以打开飞升之门"));
                break;
            //钓鱼区
            case "FishingArea":
                StartCoroutine(KeyRemind("长按空格以捕鱼"));
                break;
            //饕餮区
            case "Tao":
                StartCoroutine(KeyRemind("长按空格以斩杀饕餮"));
                break;
            //真美妮雅区
            case "RealMenya":
                StartCoroutine(KeyRemind("长按空格以斩杀美妮娅"));
                break;
            //提迦利亚
            case "Tigerya":
                StartCoroutine(KeyRemind("长按空格以斩杀提迦利亚"));
                break;
            //剑
            case "Sword":
                StartCoroutine(KeyRemind("长按空格以点亮"));
                break;
            //七蟠
            case "CoiledArea":
                if (IsPoison)
                {
                    StartCoroutine(KeyRemind("长按空格以斩杀七蟠"));
                }
                break;
            //里维雅桑区
            case "Leviathan":
                StartCoroutine(KeyRemind("长按空格以斩杀利维亚桑"));
                break;
            //触手妈
            case "Octopus":
                StartCoroutine(KeyRemind("长按空格以斩断触手"));
                break;
            case "Pirate":
                StartCoroutine(KeyRemind("打败海盗"));
                break;
            case "Vortex":
                StartCoroutine(KeyRemind("漩涡湍急，不可进入。"));
                break;
            case "Book":
                StartCoroutine(KeyRemind("长按空格以拾取史书"));
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 长按空格触发即将区域事件
    /// </summary>
    void PressPreArea()
    {
        switch (PreArea.tag)
        {
            //飞升门
            case "RisingDoor":
                //这之前要Save
                SceneManager.LoadScene("MyGod");
                break;
            //钓鱼区
            case "FishingArea":
                FoodAdd(18);
                Music.PlayingMusic(1, Resources.Load<AudioClip>("Audio/捕鱼"));
                Vector3Int temp = new Vector3Int(-3, 9, 0) + new Vector3Int(rightClick, upClick, 0) + new Vector3Int(rightTempClick, upTempClick, 0);
                fish.SetTile(temp, null);
                if (fedNum > 0)
                {
                    fedNum--;
                }
                break;
            //饕餮区
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
                    StartCoroutine(EventRemind("饕嘲笑了你心中脆弱的欲望，随后向你展示了一位真正食客的盛宴，而你是今晚的主菜。"));
                    GameOver();
                }
                break;
            //真美妮雅区
            case "RealMenya":
                mainScene.ChangeTotem(3);
                curtain.IsClosing = true;
                curtain.Video.GetComponent<VideoPlayer>().clip = Resources.Load<VideoClip>("Video/4");
                curtain.Video.GetComponent<VideoPlayer>().Play();
                IsKillBoss["Menya"] = true;
                menya.gameObject.SetActive(false);
                break;
            //提迦利亚
            case "Tigerya":
                mainScene.ChangeTotem(2);
                curtain.IsClosing = true;
                curtain.Video.GetComponent<VideoPlayer>().clip = Resources.Load<VideoClip>("Video/2");
                curtain.Video.GetComponent<VideoPlayer>().Play();
                IsKillBoss["Tigerya"] = true;
                tigerya.gameObject.SetActive(false);
                break;
            //剑
            case "Sword":
                StartCoroutine(EventRemind("将军在星空中给予了指示"));
                mainScene.ConstellationImages[Chunk.randomTemp[0]].gameObject.SetActive(true);
                mainScene.ConstellationImages[Chunk.randomTemp[0]].CrossFadeAlpha(1, 0, true);
                swordArea.SetTile(Chunk.swordPos, null);
                swordArea.SetTile(Chunk.swordPos, swordIslandWithLight);
                break;
            //七蟠
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
            //里维雅桑区
            case "Leviathan":
                mainScene.ChangeTotem(1);
                curtain.IsClosing = true;
                curtain.Video.GetComponent<VideoPlayer>().clip = Resources.Load<VideoClip>("Video/3");
                curtain.Video.GetComponent<VideoPlayer>().Play();
                IsKillBoss["Leviathan"] = true;
                leviathan.SetTile(Chunk.leviathanPos, null);
                break;
            //触手妈
            case "Octopus":
                Music.PlayingMusic(1, Resources.Load<AudioClip>("Audio/触手拍打"));
                StartCoroutine(EventRemind("你收割了触手，但是却在战斗中受到了伤害"));
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
            StartCoroutine(EventRemind("你不够资格，雾中渴血的守门人将夺走自大者的生命。"));
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
        StartCoroutine(EventRemind("你的视野被限制了，行动50步或击杀七蟠后解除！"));
        cameraTrans = new Vector3(0, 0.2f, 0); 
    }
    /// <summary>
    /// 祛毒
    /// </summary>
    /// <param name="Index">是否斩杀七蟠</param>
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
    /// 长按检测前方格子
    /// </summary>
    void PressedSpace()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            Music.PlayingMusic(1, Resources.Load<AudioClip>("Audio/长按空格"));
            //按键状态为按下
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