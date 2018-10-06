using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour
{


    /// <summary>
    /// 坦克的状态枚举
    /// </summary>
    public enum TankStatu
    {
        MOVING,
        STOPED
    }
    /// <summary>
    /// 碰撞的坦克列表
    /// </summary>
    private ArrayList _ContactTanks = new ArrayList();
    /// <summary>
    /// 坦克的移动速率
    /// </summary>
    public float _MoveSpeed = 1.6f;
    /// <summary>
    /// 发射子弹的间隔时间
    /// </summary>
    public float _ATKRate = 0.5f;

    /// <summary>
    /// 坦克的当前状态
    /// </summary>
    public TankStatu _Statu;

    /// <summary>
    /// 坦克的炮台
    /// </summary>
    private GameObject _TankBarrel;
    /// <summary>
    /// 承载自己的坦克池
    /// </summary>
    private BreedPool _TankPool;
    /// <summary>
    /// 坦克的爆破粒子
    /// </summary>
    private GameObject _TankExplosion;
    /// <summary>
    /// 子弹的生成位置
    /// </summary>
    private GameObject _BulletSpawn_Position;
    /// <summary>
    /// 子弹瞄准的方向
    /// </summary>
    private GameObject _Lookat_Position;
    /// <summary>
    /// 记录是否被用户操纵
    /// </summary>
    private bool _IsContrlled;
    /// <summary>
    /// 记录被用户操纵时间
    /// </summary>
    public int _ContrlledTime = 10;
    private int _MaxContrlledTime = 10;
    /// <summary>
    /// 锁定的目标位置
    /// </summary>
    private Vector3 _Target;
    /// <summary>
    /// 记录是否已经开始攻击
    /// </summary>
    private bool _IsAttacking;
    /// <summary>
    /// 记录是否和别的坦克发生接触
    /// </summary>
    public bool _IsContact;
    /// <summary>
    /// 击杀坦克后的齿轮；
    /// </summary>
    public GameObject _PreGear;


    private AudioSource _AudioFire;
   // private AudioSource _AudioBomb;

    private ParticleEmitter _Smoke;

    /// <summary>
    /// 增加的时间；
    /// </summary>
    public GameObject _TimeNumObj;






    void Start()
    {
        this._AudioFire = transform.FindChild("AudioFire").gameObject.GetComponent<AudioSource>();
        //this._AudioBomb = transform.FindChild("AudioBomb").gameObject.GetComponent<AudioSource>();
        this._TankBarrel = transform.FindChild("TankBarrel").gameObject;
        this._Lookat_Position = _TankBarrel.transform.FindChild("Lookat_Position").gameObject;
        this._BulletSpawn_Position = _TankBarrel.transform.FindChild("BulletSpawn_Position").gameObject;
        this._Smoke = transform.FindChild("Smoke").gameObject.GetComponent<ParticleEmitter>();
    }
    public IEnumerator StartContrlled()
    {
        while (_ContrlledTime>0)
        {
            _ContrlledTime--;
            yield return new WaitForSeconds(0.1f);
        }
    }
    void FixedUpdate()
    {
        foreach (GameObject tG in _ContactTanks)
        {
            if (tG.GetComponent<Tank>()._IsContact)
            {
                this.Contact(tG);
                return;
            }
        }

        if (IsInvoking("BeginCount"))
        {
            CancelInvoke("BeginCount");
        }

        if (_Smoke.emit)
        {
            _Smoke.emit = false;

            this.gameObject.renderer.material.SetColor("_OutlineColor", Color.black);
        }
        this.Move();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("board"))
        {
            this.Kill();
            return;
        }
        if (other.tag.StartsWith("tank"))
        {
            this.Contact(other.gameObject);
            //开始计时三秒 结束游戏。
            if (_IsContact)
            {
                Invoke("BeginCount", 3);
            }
        }
    }

    void OnSpawn(BreedPool pmp)
    {
        // we will use this when unspawning, thats why it was passed - so we don't have to type PoolManager.GetPool
        this._TankPool = pmp;
    }

    /// <summary>
    /// 移动
    /// </summary>
    void Move()
    {
        if (_ContactTanks.Count > 0)
        {
            _ContactTanks.Clear();
        }
        this._Statu = TankStatu.MOVING;
        transform.Translate(Vector3.forward * Time.deltaTime * _MoveSpeed);
    }

    /// <summary>
    /// 停止，一般就是卡住了
    /// </summary>
    void Contact(GameObject tank)
    {
        this._Statu = TankStatu.STOPED;
        this._IsContact = true;
        if (!IsInvoking("SmokeUp") && !_Smoke.emit)
            this.Invoke("SmokeUp", 1);

        if (!_ContactTanks.Contains(tank))
        {
            this._ContactTanks.Add(tank);
        }
    }

    /// <summary>
    /// 冒烟吧~警告下
    /// </summary>
    void SmokeUp()
    {
        if (_IsContact && !_Smoke.emit)
        {
            this._Smoke.GetComponent<ParticleEmitter>().emit = true;

            this.gameObject.renderer.material.SetColor("_OutlineColor",Color.red);
        }
    }

    /// <summary>
    /// 攻击
    /// </summary>
    /// <param name="target"></param>
    void Attack(Vector3 target)
    {
        this._TankBarrel.transform.LookAt(
            new Vector3(
                target.x,
                _TankBarrel.transform.position.y,
                target.z));
        this._Target = target;

        if (!_IsAttacking)
        {
            this._IsAttacking = true;
            StartCoroutine("SpawnBullet", target);
        }
    }

    /// <summary>
    /// 被操纵
    /// </summary>
    void Controlled()
    {
        transform.FindChild("Circle_Red").GetComponent<ParticleEmitter>().emit = true;
        _ContrlledTime = _MaxContrlledTime;
        this._IsContrlled = true;
        
        Attack(this.transform.forward);
        StartCoroutine(StartContrlled());
    }

    /// <summary>
    /// 丢失操纵
    /// </summary>
    void LoseControl()
    {
        StopAllCoroutines();
        this._IsContrlled = _IsAttacking = false;
        transform.FindChild("Circle_Red").GetComponent<ParticleEmitter>().emit = false;
    }

    /// <summary>
    /// 生成炮弹~
    /// </summary>
    IEnumerator SpawnBullet(Vector3 target)
    {
        while (_IsContrlled)
        {
            CreadBullet();
            yield return new WaitForSeconds(_ATKRate);
            CreadBullet();
        }
        
    }

    void CreadBullet()
    {
        GameObject tBullet = GameObject.Find("GameCommander").GetComponent<GameCommander>()._BulletPool.Spawn(_BulletSpawn_Position.transform.position, Quaternion.identity);

        tBullet.transform.LookAt(_Lookat_Position.transform);
        tBullet.GetComponent<Bullet>()._Father = gameObject;
        this._AudioFire.Play();
    }

    /// <summary>
    /// 死！
    /// </summary>
    public void Kill()
    {
      
      
        //this.COPYReadyUnSpawn();
        iTween.ShakePosition(GameObject.Find("PlaneCamera"), new Vector3(0.2f, 0.1f, 0.2f), 0.2f);

        if (!_TankExplosion)
        {
            this._TankExplosion = transform.FindChild("TankExplosion").gameObject;
        }
        GameObject tS = Instantiate(_TankExplosion, transform.position, Quaternion.identity) as GameObject;
        tS.GetComponent<ParticleEmitter>().emit = true;
        foreach (ParticleEmitter tP in tS.GetComponentsInChildren<ParticleEmitter>())
        {
            tP.emit = true;
        }

        StartCoroutine(ReadyUnSpawn());
        this.LoseControl();
        this._ContactTanks.Clear();
        this._IsContact = false;
       // Invoke("COPYReadyUnSpawn",2f);
        
        gameObject.SetActiveRecursively(false);
        transform.FindChild("AudioBomb").gameObject.SetActiveRecursively(true);

        this.PhoneShake();
    }

   public IEnumerator ReadyUnSpawn()
    {
        yield return new WaitForSeconds(1.5f);
        if (gameObject.activeSelf == false)
            this._TankPool.Unspawn(gameObject);
         
    }


    void  COPYReadyUnSpawn()
    {
        //yield return new WaitForSeconds(1.5f);
        this._TankPool.Unspawn(gameObject);
    }
    ///<summary>
    ///震动
    ///<summary>
    void PhoneShake()
    {
        if (GlobalManager.GetInstants()._CanVibrate == true)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
                Handheld.Vibrate();
        }

    }
    ///<summary>
    ///卡住 开始计算死亡时间
    /// </summary>

    void BeginCount()
    {
        if (_IsContact)
        {
            GameCommander.Instance.GameOver(false);
        }
    }

    ///<summary>
    ///产生齿轮 
    /// </summary>
    public void SpawnGears()
    {
        Instantiate(_PreGear, transform.position, Quaternion.identity);
    }
    ///<summary>
    ///时间特效。
    /// </summary>
    public void AddTimeEffect()
    {
        Instantiate(_TimeNumObj, transform.position, Quaternion.identity);
    }
}
