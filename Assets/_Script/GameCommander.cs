using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 游戏控制器
/// </summary>
public class GameCommander : MonoBehaviour
{
    public GameObject[] _TimeCommmbo;
    /// <summary>
    /// 坦克的预制件
    /// </summary>
    public GameObject[] _Prefab_Tank;
    /// <summary>
    /// 子弹的预制件
    /// </summary>
    public GameObject[] _Prefab_Bullets;
    /// <summary>
    /// 道具预制件
    /// </summary>
    public GameObject[] _Prefab_Props;




    /// <summary>
    /// 
    /// </summary>
    public List<Transform> _SpawnSet = new List<Transform>(3);
    /// <summary>
    /// 坦克的移动目标点
    /// </summary>
    public Transform[] _TargetDirectPosition;
    /// <summary>
    /// 坦克出生时的特效
    /// </summary>
    public GameObject _SpanwnEffect;






    /// <summary>
    /// 坦克的缓冲池
    /// </summary>
    private BreedPool _Tank_Pool;
    /// <summary>
    /// 道具的缓冲池
    /// </summary>
    private BreedPool _Prop_Pool;
    /// <summary>
    /// 当前子弹的缓冲池
    /// </summary>
    [HideInInspector]
    public BreedPool _BulletPool;

    /// <summary>
    /// 时间特效10,5s
    /// </summary>
    public GameObject _AddTime5s;
    public GameObject _AddTime10s;


    /// <summary>
    /// 当前控制的tank
    /// </summary>
    [HideInInspector]
    public GameObject _CurTank;
    /// <summary>
    /// 坦克的出现间隔
    /// </summary>
    //[HideInInspector]
    public float _TimeStep;
    /// <summary>
    /// 统计销毁的坦克
    /// </summary>
    public int _Score = 0;
    /// <summary>
    /// 游戏是否结束
    /// </summary>
    private bool _IsGameOver = false;



    /// <summary>
    /// 获取战场时间；
    /// </summary>
    private int _CurrentTime;
    /// <summary>
    /// 坦克死时候爆炸出的道具概率0.01~1
    /// </summary>
    private float _ChanceNum = 5;
    /// <summary>
    ///最大炸弹数；
    /// </summary>
    //   private float _MaxBombNum;

    /// <summary>
    /// Loading 脚本；
    /// </summary>
    public GameObject _LoadingScript;

    /// <summary>
    /// 上次被击杀时间（combo） 
    /// </summary>
    private float _LastTime = 0;

    /// <summary>
    /// 连击时间间隔；
    /// </summary>
    public float _ComboInterval = 0.5f;

    ///<summary>
    ///combo 数
    /// </summary>
    private int _ComboNum;

    /// <summary>
    /// 时间的特效物体
    /// </summary>
    /// 
    public GameObject _TimeObj;

    ///
    /// <summary>
    /// combo的特效物体
    /// </summary>
    /// 
    //public GameObject _ComboObj;
    /// <summary>
    /// 
    /// </summary>
    private static GameCommander instance;

    /// <summary>
    /// 地板图片
    /// </summary>
    public Material[] PlaneMaterial;


    public GameObject Plane;

    int SuvivalTime = 0;

    public UIInput _Input;
    /// <summary>
    /// 排行榜
    /// </summary>
    /// 
   // private string[] _Leaderboard = new string[3] {"1st","2nd","3rd"};
        



    public static GameCommander Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (GameCommander)FindObjectOfType(typeof(GameCommander));
            }
            if (!instance)
            {
                Debug.LogError("GameCommander could not find himself!");
            }
            return instance;
        }
    }




    void Awake()
    {

        //this.InitRank();
        Time.timeScale = 1;
        this.PrepareTank();
        this.PrepareBullet(_Prefab_Bullets[0]);
        this.PrepareProp();

        this._IsGameOver = false;
        this._Score = 0;
        //  this._MaxBombNum = Consts.MAX_BOMB;
        this._CurrentTime = Consts.MAX_TIME;
        this.PreparePlane();

        Application.runInBackground = true;      
        Debug.Log("First :" + PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[0]) + "============2nd :" + PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[1]) + "============3rd :" + PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[2]) + "======Suvival Time" + PlayerPrefs.GetInt(SuvivalStr));       
    }


    /// <summary>
    /// 初始化 排名
    /// </summary>
    void InitRank() {
        if (string.IsNullOrEmpty(PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[0])))
        {
            Debug.Log("Rank is null");
            for (int i = 0; i < 3; i++)
            {
                PlayerPrefs.SetString(GlobalManager.GetInstants()._Leaderboard[i], "0,0");
            }
        }
    }
    void PreparePlane()
    {
        switch (GlobalManager.GetInstants()._PlayerEntity._Tank_Id)
        {
            case 1:
                Plane.renderer.material = PlaneMaterial[0];
                break;
            case 2:
                Plane.renderer.material = PlaneMaterial[1];
                break;
            case 3:
                Plane.renderer.material = PlaneMaterial[2];
                break;
            case 4:
                Plane.renderer.material = PlaneMaterial[3];
                break;
            case 5:
                Plane.renderer.material = PlaneMaterial[4];
                break;
        }
    }

    void Start()
    {
        // Invoke(_LoadingScript.GetComponent<Loading>().LoadScene();
        InvokeRepeating("Counter", 1, 1);
        StartCoroutine(SpawnTank());

        //Invoke("TestAddTime", 3);
    }




    void Update()
    {
        //返回按钮的点击事件
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                this.PauseGame();
                return;
            }
        }

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 5) && hit.transform.tag.StartsWith("tank"))
                {
                    if (_CurTank && _CurTank != hit.transform.gameObject)
                    {
                        this._CurTank.SendMessage("Attack", hit.point, SendMessageOptions.DontRequireReceiver);
                        this._CurTank.SendMessage("CreadBullet", SendMessageOptions.DontRequireReceiver);
                        this._CurTank.SendMessage("LoseControl", SendMessageOptions.DontRequireReceiver);
                    }
                    this._CurTank = hit.transform.gameObject;
                    this._CurTank.SendMessage("Controlled");
                }

            }

            //有选中的坦克，就跟着鼠标转
            if (_CurTank && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (this._CurTank.GetComponent<Tank>()._ContrlledTime <= 0)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 5))
                    {
                        this._CurTank.SendMessage("Attack", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }

        else
        {

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 10) && hit.transform.tag.StartsWith("tank"))
                {
                    if (_CurTank && _CurTank != hit.transform.gameObject)
                    {
                        this._CurTank.SendMessage("Attack", Camera.mainCamera.ScreenToWorldPoint(Input.mousePosition), SendMessageOptions.DontRequireReceiver);
                        this._CurTank.SendMessage("CreadBullet", SendMessageOptions.DontRequireReceiver);
                        this._CurTank.SendMessage("LoseControl", SendMessageOptions.DontRequireReceiver);
                    }
                    this._CurTank = hit.transform.gameObject;
                    this._CurTank.SendMessage("Controlled");
                }
            }

            if (_CurTank && _CurTank.GetComponent<Tank>()._ContrlledTime <= 0)
            {
                this._CurTank.SendMessage("Attack", Camera.mainCamera.ScreenToWorldPoint(Input.mousePosition), SendMessageOptions.DontRequireReceiver);
            }
        }
    }


    /// <summary>
    /// 为坦克准备缓冲池
    /// </summary>
    void PrepareTank()
    {
        //Breed.Instance().Create("tank", _Prefab_Tank[GlobalManager.GetInstants()._PlayerEntity._Tank_Id - 1]);
        Breed.Instance().Create("tank", _Prefab_Tank[GlobalManager.GetInstants()._PlayerEntity._Tank_Id - 1]);
        this._Tank_Pool = Breed.Instance().Get("tank");
        this._Tank_Pool.Preload(20);
    }

    /// <summary>
    /// 为炸弹准备缓冲池
    /// </summary>
    void PrepareProp()
    {
        Breed.Instance().Create("prop", _Prefab_Props[GlobalManager.GetInstants()._PlayerEntity._Bombing_Weapons - 1]);
        this._Prop_Pool = Breed.Instance().Get("prop");
        this._Prop_Pool.Preload(5);
    }

    /// <summary>
    /// 为子弹准备缓冲池
    /// </summary>
    void PrepareBullet(GameObject bullet)
    {
        Breed.Instance().Create("bullet", bullet);
        this._BulletPool = Breed.Instance().Get("bullet");
        this._BulletPool.Preload(10);
    }


    /// <summary>
    /// 添加坦克 
    /// </summary>
    IEnumerator SpawnTank()
    {
        while (!_IsGameOver)
        {
            int tIndex = Random.Range(0, _SpawnSet.Count);
            if (!_SpawnSet[tIndex].GetComponent<SpawnController>()._IsExit)
            {
                yield return new WaitForSeconds(0.1f);
                continue;
            }
            Vector3 tSpawnPosition = _SpawnSet[tIndex].position;
            Instantiate(_SpanwnEffect, tSpawnPosition, Quaternion.identity);

            GameObject tTank = _Tank_Pool.Spawn(tSpawnPosition, Quaternion.identity);
            tTank.transform.LookAt(_TargetDirectPosition[Random.Range(0, _TargetDirectPosition.Length)]);
            yield return new WaitForSeconds(_TimeStep);
            //yield return new WaitForSeconds(5f);
        }
    }


    /// <summary>
    /// 产生道具+（有一定几率）wxl
    /// </summary>
    /// <param name="target"></param>
    void SpawnProps(Vector3 target)
    {
        int randomNum = Random.Range(0, 100);
        switch (GlobalManager.GetInstants()._PlayerEntity._Bombing_Weapons - 1)
        {
            case 0:
                if (randomNum <= _ChanceNum)
                {
                    _Prop_Pool.Spawn(target, Quaternion.identity);
                }
                break;
            case 1:
                if (randomNum <= (_ChanceNum + 3f))
                {
                    _Prop_Pool.Spawn(target, Quaternion.identity);
                }
                break;
            case 2:
                if (randomNum <= (_ChanceNum + 8f))
                {
                    _Prop_Pool.Spawn(target, Quaternion.identity);
                }
                break;
        }


        //if (randomNum <= _ChanceNum * 100)
        //{
        //    //生成道具 在这里 ！！
        //    GameObject tTank = _Prop_Pool.Spawn(target, Quaternion.identity);
        //}
    }


    /// <summary>
    /// 计算分数，击杀数++
    /// </summary>
    /// <param name="score"></param>
    public void ScoreUp(int score)
    {
        this._Score += score;
        GameGUIManager.Instance.StartCoroutine("UpdateKill", _Score);
    }


    /// <summary>
    /// 计时器
    /// </summary>
    void Counter()
    {
        if (_CurrentTime > 0)
        {
            _CurrentTime--;
            this.ReduceTimeStep();
            System.GC.Collect();
            SuvivalTime++;
        }

        GameGUIManager.Instance.StartCoroutine("UpdateTime", _CurrentTime);
        if (_CurrentTime <= 0 && !_IsGameOver)
        {
            this.GameOver(true);
        }
        this.TimeEffect();

    }


    /// <summary>
    /// 每隔10秒减少出坦克间隔。
    /// </summary>
    private int _Step = 1;
    void ReduceTimeStep()
    {
        _Step++;
        if (_Step % 10 == 0)
        {
            _TimeStep -= 0.14f;
            //  System.GC.Collect();
        }

        if (_Step % 15 == 0)
        {
            _ChanceNum += 1f;
        }
    }

    /// <summary>
    /// 战斗加时间的方法。
    /// </summary>
    /// <param name="delta"></param>
    IEnumerator UpdateTime(int delta)
    {
        yield return new WaitForSeconds(2.5f);
        Mathf.Clamp(_CurrentTime += delta, 0, 60);

    }




    #region 弹窗部分
    /// <summary>
    /// 暂停游戏
    /// </summary>
    void PauseGame()
    {
        Time.timeScale = 0;
        GameGUIManager.Instance.Pause();
    }

    ///</summary>
    ///消失对话框方法
    /// </summary>
    void ResumeGame()
    {
        Time.timeScale = 1;
        GameGUIManager.Instance.Resume();
    }

    ///</summary>
    ///到商店的方法
    ///</summary>
    IEnumerator GoShopPanel()
    {
        Time.timeScale = 1;
        _LoadingScript.GetComponent<Loading>().LoadScene();
        yield return new WaitForSeconds(1f);
        AsyncOperation async = Application.LoadLevelAsync("Shop");
        yield return async;

    }

    ///</summary>
    ///回到菜单方法
    ///</summary>
    IEnumerator BackMenu()
    {

        Time.timeScale = 1;
        _LoadingScript.GetComponent<Loading>().LoadScene();
        yield return new WaitForSeconds(1f);
        AsyncOperation async = Application.LoadLevelAsync("MainMenu");
        yield return async;
    }


    /// <summary>
    /// 清空缓冲坦克
    /// </summary>
    void OnDestroy()
    {
        this._BulletPool.Clear();
        this._Tank_Pool.Clear();
        this._Prop_Pool.Clear();
        Breed.Instance().RemoveAll();
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    public void GameOver(bool tFuck)
    {
        if (_IsGameOver)
            return;

        //Debug.Log("我执行了：" + Time.time);
        this._IsGameOver = true;
        Time.timeScale = 0;

        CancelInvoke("Counter");


        //加入齿轮数
        GlobalManager.GetInstants()._PlayerEntity._Gear += _Score;
        PlayerPrefs.SetInt(Consts.PREF_GEAR, GlobalManager.GetInstants()._PlayerEntity._Gear);
        //this.Ranking(_Score);
        
        //GlobalManager.LocatedRank(_Score);
        GameGUIManager.Instance.GameOver(_Score, tFuck);

       // Debug.Log("First :" + PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[0]) + "============2nd :" + PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[1]) + "============3rd :" + PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[2]) + "======Suvival Time" + PlayerPrefs.GetInt(SuvivalStr)); 
    }

    /// <summary>
    /// 重新开始游戏
    /// </summary>
    IEnumerator Restart()
    {
        
        Time.timeScale = 1;
        _LoadingScript.GetComponent<Loading>().LoadScene();
        yield return new WaitForSeconds(1f);
        AsyncOperation async = Application.LoadLevelAsync("Game");
        yield return async;

    }

    #endregion


    ///<summary>
    ///combo计算
    /// </summary> 
    public void CountCombo(Vector3 transF)
    {
        if (_LastTime == 0)
        {
            _LastTime = Time.time;
        }
        else
        {
            if (Time.time - _LastTime <= _ComboInterval)
            {
                //  StopCoroutine("NewComb");
                _LastTime = Time.time;
                _ComboNum++;

            }
            else
            {
                switch (_ComboNum)
                {
                    case 1:
                        _LastTime = 0;
                        _ComboNum = 0;
                        break;
                    case 2:
                        StartCoroutine(UpdateTime(5));
                        //UpdateTime(5);
                        TestAddTime5(transF);
                        this.ToggleCombo(1);
                        break;
                    case 3:
                        StartCoroutine(UpdateTime(5));
                        TestAddTime5(transF);
                        this.ToggleCombo(2);
                        break;
                    case 4:
                        StartCoroutine(UpdateTime(10));
                        TestAddTime10(transF);
                        this.ToggleCombo(3);
                        break;
                    case 5:
                        StartCoroutine(UpdateTime(10));
                        TestAddTime10(transF);
                        this.ToggleCombo(4);
                        break;
                }
                _LastTime = 0;
                _ComboNum = 0;
            }
        }
    }

    /// <summary>
    /// 生成增加时间的特效
    /// </summary>
    void TestAddTime5(Vector3 transF)
    {
        Instantiate(_AddTime5s, transF, Quaternion.identity);
    }

    void TestAddTime10(Vector3 transF)
    {
        Instantiate(_AddTime10s, transF, Quaternion.identity);
    }

    void ToggleCombo(int tTime)
    {
        switch (tTime)
        {
            case 1:
                _TimeCommmbo[0].gameObject.SetActiveRecursively(true);
                StartCoroutine(GuanBCombo(_TimeCommmbo[0]));
                break;             
            case 2:
                _TimeCommmbo[1].gameObject.SetActiveRecursively(true);
                StartCoroutine(GuanBCombo(_TimeCommmbo[1]));
                break;
            case 3:
                _TimeCommmbo[2].gameObject.SetActiveRecursively(true);
                StartCoroutine(GuanBCombo(_TimeCommmbo[2]));
                break;
            case 4:
                _TimeCommmbo[3].gameObject.SetActiveRecursively(true);
                StartCoroutine(GuanBCombo(_TimeCommmbo[3]));
                break;
            default:

                break;
        }
    }

    IEnumerator GuanBCombo(GameObject tT)
    {
        yield return new WaitForSeconds(2F);
        tT.gameObject.SetActiveRecursively(false);
    }
    ///<summary>
    ///时间的UI特效
    /// </summary>
    void TimeEffect()
    {
        if (_CurrentTime <= 10)
        {
            _TimeObj.GetComponent<TipShow>().HeardTip();
            this._TimeObj.GetComponent<UILabel>().color = new Color(255, 0, 0, 255);
        }
        else
        {
            this._TimeObj.GetComponent<UILabel>().color = Color.white;
        }
    }



    /// <summary>
    /// 排名 score + Name
    /// </summary>
    
    string SuvivalStr = "Time";  
    void Ranking(int PScore)
    {   //排名的范围
        int tLength=4;

        if (SuvivalTime > PlayerPrefs.GetInt(SuvivalStr))
        {
            PlayerPrefs.SetInt(SuvivalStr, SuvivalTime);
        }
        
            string[] tScore = PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[tLength]).Split(',');
           // Debug.Log(int.Parse(tScore[0]));

        while (PScore >= int.Parse(tScore[0]) && tLength >= 0)
        {
            
            if (tLength != 4)
            {
                PlayerPrefs.SetString(GlobalManager.GetInstants()._Leaderboard[tLength + 1], PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[tLength]));
                PlayerPrefs.SetString(GlobalManager.GetInstants()._Leaderboard[tLength], PScore + "," + _Input.text.Replace(",", ""));
            }
            else {                
                PlayerPrefs.SetString(GlobalManager.GetInstants()._Leaderboard[tLength], PScore + "," + _Input.text.Replace(",", ""));
                Debug.Log(PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[tLength]));
            }
            if (tLength > 0)
            {
                tLength--;
                tScore = PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[tLength]).Split(',');
            }
            else break;
        }
        
        PlayerPrefs.Save();
        Debug.Log("First :" + PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[0]) + "============2nd :" + PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[1]) + "============3rd :" + PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[2]) + "======Suvival Time" + PlayerPrefs.GetInt(SuvivalStr)); 
    }



    ///</summary>
    ///回到菜单方法
    ///</summary>
    IEnumerator RecordBackMenu()
    {
        this.Ranking(_Score);
        Time.timeScale = 1;
        _LoadingScript.GetComponent<Loading>().LoadScene();
        yield return new WaitForSeconds(1f);
        AsyncOperation async = Application.LoadLevelAsync("MainMenu");
        yield return async;
    }

    IEnumerator RecordRestart()
    {
        this.Ranking(_Score);
        Time.timeScale = 1;
        _LoadingScript.GetComponent<Loading>().LoadScene();
        yield return new WaitForSeconds(1f);
        AsyncOperation async = Application.LoadLevelAsync("Game");
        yield return async;

    }
/*
    void OnGUI() {
        if (GUI.Button(new Rect(0, 0, 100, 50), "Delete all"))
            PlayerPrefs.DeleteAll();
  
    }
 * */
}
