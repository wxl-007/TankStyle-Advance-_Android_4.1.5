using UnityEngine;
using System.Collections;

public class ShopIniter : MonoBehaviour
{
    /// <summary>
    /// 坦克列表
    /// </summary>
    public GameObject _TankList;
    /// <summary>
    /// 能量武器列表
    /// </summary>
    public GameObject _EnergyList;
    /// <summary>
    /// 爆炸性武器列表
    /// </summary>
    public GameObject _BombList;

    /// <summary>
    /// 用户拥有的齿轮数量
    /// </summary>
    public UILabel _Gears;
    /// <summary>
    /// 齿轮数量的提示
    /// </summary>
    public GameObject _GearsTip;

    /// <summary>
    /// 确认对话框
    /// </summary>
    public UIPanel _ConfirmDialog;


    /// <summary>
    /// 装备类型
    /// </summary>
    private ShopItemIniter.WEAPON _Current_Weapon = ShopItemIniter.WEAPON.Tank;

    /// <summary>
    /// 商品的id
    /// </summary>
    public int _Current_Id;
    /// <summary>
    /// 商品的售价
    /// </summary>
    public int _Current_Price;

    /// <summary>
    /// Loading 脚本；
    /// </summary>
    public GameObject _LoadingScript;

    //public GameObject _FuckCenter;

    private GameObject _BgSound;

    public bool EnterCenter;

    public int CenterMoney;

    void Start()
    {
        this._Gears.text = GlobalManager.GetInstants()._PlayerEntity._Gear.ToString();
        this.Switch2Tank();
        _BgSound = GameObject.Find("BgSound");

        //this._GearsTip.SendMessage("DropDown");
    }


    void Update()
    {
        //返回按钮的点击事件
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(BackToMenu());
            }
        }
    }

    /// <summary>
    /// 切换到坦克列表
    /// </summary>
    void Switch2Tank()
    {
        this._TankList.SetActiveRecursively(true);
        //this._TankList.transform.FindChild("Grid").gameObject.GetComponent<UICenterOnChild>().Recenter(_FuckCenter);
        foreach (ShopItemIniter tS in _TankList.GetComponentsInChildren<ShopItemIniter>())
        {
            tS.SendMessage("Init");
        }
        this._BombList.SetActiveRecursively(false);
        this._EnergyList.SetActiveRecursively(false);
        GetComponentInChildren<ExtentDragPanel>()._DetailShow.SendMessage("GoneShow");
    }

    /// <summary>
    /// 切换到能量武器列表
    /// </summary>
    void Switch2Energy()
    {
        this._TankList.SetActiveRecursively(false);
        this._BombList.SetActiveRecursively(false);
        this._EnergyList.SetActiveRecursively(true);
        foreach (ShopItemIniter tS in _EnergyList.GetComponentsInChildren<ShopItemIniter>())
        {
            tS.SendMessage("Init");
        }
        GetComponentInChildren<ExtentDragPanel>()._DetailShow.SendMessage("GoneShow");
    }

    /// <summary>
    /// 切换到轰炸武器列表
    /// </summary>
    void Switch2Bomb()
    {
        this._TankList.SetActiveRecursively(false);
        this._BombList.SetActiveRecursively(true);
        foreach (ShopItemIniter tS in _BombList.GetComponentsInChildren<ShopItemIniter>())
        {
            tS.SendMessage("Init");
        }
        this._EnergyList.SetActiveRecursively(false);
        GetComponentInChildren<ExtentDragPanel>()._DetailShow.SendMessage("GoneShow");
    }

    /// <summary>
    /// 打开确认对话框
    /// </summary>
    public void ShowConfirmDialog(ShopItemIniter.WEAPON weapon, int id, int price)
    {
        this._Current_Weapon = weapon;
        this._Current_Id = id;
        this._Current_Price = price;

        this._ConfirmDialog.gameObject.SetActiveRecursively(true);
        this._ConfirmDialog.transform.localPosition = new Vector3(0, 0, _ConfirmDialog.transform.localPosition.z);
        this._ConfirmDialog.GetComponentInChildren<UILabel>().text = "你想花" + price + "个齿轮买这件商品吗?";
    }

    /// <summary>
    /// 展示对应信息的对话框
    /// </summary>
    /// <param name="message"></param>
    public void ShowConfirmDialog(string message)
    {
        this._ConfirmDialog.gameObject.SetActiveRecursively(true);
        this._ConfirmDialog.transform.localPosition = new Vector3(0, 0, _ConfirmDialog.transform.localPosition.z);
        this._ConfirmDialog.GetComponentInChildren<UILabel>().text = message;
    }


    /// <summary>
    /// 关闭确认对话框
    /// </summary>
    void DismissConfirmDialog()
    {
        this._ConfirmDialog.gameObject.SetActiveRecursively(false);
        this._ConfirmDialog.transform.localPosition = new Vector3(100000, 0, _ConfirmDialog.transform.localPosition.z);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pay"></param>
    void PaySMS(string pay)
    {
        GlobalManager.GetInstants().UpdatePlayerGear(1000);
        this._Gears.text = GlobalManager.GetInstants()._PlayerEntity._Gear.ToString();
    }

    /// <summary>
    /// 回调的方法 充值成功 则发送齿轮
    /// </summary>
    /// <param name="BeanNum"></param>
    void PayCallBack(int BeanNum) {
        GlobalManager.GetInstants().UpdatePlayerGear(BeanNum);
        this._Gears.text = GlobalManager.GetInstants()._PlayerEntity._Gear.ToString();
    
    }
    void TestFailToPay() {
        GlobalManager.GetInstants().UpdatePlayerGear(10);
        this._Gears.text = GlobalManager.GetInstants()._PlayerEntity._Gear.ToString();
    }
   

    /// <summary>
    /// 确认购买
    /// </summary>
    void Buy()
    {
        Debug.Log("Enter 91Recharge" + "=============" + EnterCenter + " ===========" + CenterMoney);
        if (EnterCenter ==true)
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            ///0.01  91豆 = 10齿轮   四舍五入计算
            
             int tBeanNum = Mathf.FloorToInt(CenterMoney / 100);
            jo.Call("ReCharge", tBeanNum);
           
        }
        else
        {

            Debug.Log(" Yes To Buy ~~~~~~~");
            switch (_Current_Weapon)
            {
                case ShopItemIniter.WEAPON.Tank:

                    string tTanks = "";
                    foreach (int tI in GlobalManager.GetInstants()._PlayerEntity._Own_Tanks)
                    {
                        tTanks += (tI.ToString() + ",");
                    }
                    tTanks += _Current_Id.ToString();
                    PlayerPrefs.SetString(Consts.PREF_OWN_TANK, tTanks);
                    break;

                case ShopItemIniter.WEAPON.Energy:

                    string tEnergy = "";
                    foreach (int tI in GlobalManager.GetInstants()._PlayerEntity._Own_Energy)
                    {
                        tEnergy += (tI.ToString() + ",");
                    }
                    tEnergy += _Current_Id.ToString();
                    PlayerPrefs.SetString(Consts.PREF_OWN_ENERGY, tEnergy);
                    break;

                case ShopItemIniter.WEAPON.Bomb:

                    string tBomb = "";
                    foreach (int tI in GlobalManager.GetInstants()._PlayerEntity._Own_Bomb)
                    {
                        tBomb += (tI.ToString() + ",");
                    }
                    tBomb += _Current_Id.ToString();
                    PlayerPrefs.SetString(Consts.PREF_OWN_BOMB, tBomb);
                    break;
            }

            GlobalManager.GetInstants().UpdatePlayerGear(-_Current_Price);
            this._Gears.text = GlobalManager.GetInstants()._PlayerEntity._Gear.ToString();
            GlobalManager.GetInstants().SyncPlayerData();


            foreach (ShopItemIniter tS in GetComponentsInChildren<ShopItemIniter>())
            {
                tS.SendMessage("Init");
            }
        }
        DismissConfirmDialog();
    }

    /// <summary>
    /// 返回主菜单
    /// </summary>
    IEnumerator BackToMenu()
    {
        //Application.LoadLevel("MainMenu");
        Time.timeScale = 1;
        _LoadingScript.GetComponent<Loading>().LoadScene();
        yield return new WaitForSeconds(1f);
        Destroy(_BgSound);
        AsyncOperation async = Application.LoadLevelAsync("MainMenu");
        yield return async;
    }
    /*
    void OnGUI() {
        GUILayout.BeginArea(new Rect(0,0,300,500));
        if (GUILayout.Button("1个 91 豆 == 1000 齿轮")) {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            jo.Call("ReCharge", 1);
        }
        if (GUILayout.Button("2个 91 豆 == 2000 齿轮")) {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            jo.Call("ReCharge", 2);
        }
        GUILayout.EndArea();
    }
     * */
}
