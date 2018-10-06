using UnityEngine;
using System.Collections;

public class ShopItemIniter : MonoBehaviour
{

    public enum WEAPON
    {
        Tank,
        Energy,
        Bomb
    }
    /// <summary>
    /// 装备类型
    /// </summary>
    public WEAPON _Weapon /*= WEAPON.Tank*/;



    /// <summary>
    /// 商品的id
    /// </summary>
    public int _Id;
    /// <summary>z
    /// 商品的售价
    /// </summary>
    public int _Price;




    /// <summary>
    /// 购买按钮
    /// </summary>
    public GameObject _Btn_Buy;
    /// <summary>
    /// 选择按钮
    /// </summary>
    public GameObject _Btn_Select;



    /// <summary>
    /// 价格载体
    /// </summary>
    public UILabel _Lbl_Price;

    public GameObject _SelectedEffect;


    /// <summary>
    /// 散热时间
    /// </summary>
    public string _RadiatingTime;

    /// <summary>
    /// 子弹的速度
    /// </summary>
    public string _FireRate;

    /// <summary>
    /// 坦克的移动速度
    /// </summary>
    public string _Speed;


    public int _NeedPay;

    /// <summary>
    /// 购买
    /// </summary>
    void Buy()
    {
        if (GlobalManager.GetInstants()._PlayerEntity._Gear >= _Price)
        {
            NGUITools.FindInParents<ShopIniter>(gameObject).ShowConfirmDialog(_Weapon, _Id, _Price);
            NGUITools.FindInParents<ShopIniter>(gameObject).EnterCenter = false;
            
        }
        else
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                NGUITools.FindInParents<ShopIniter>(gameObject).EnterCenter = true;
               
                NGUITools.FindInParents<ShopIniter>(gameObject).ShowConfirmDialog("你还差" +(_Price - GlobalManager.GetInstants()._PlayerEntity._Gear).ToString()+ "齿轮，可以通过91豆购买");
                //Debug.Log(_Enter91);
                NGUITools.FindInParents<ShopIniter>(gameObject).CenterMoney = _Price - GlobalManager.GetInstants()._PlayerEntity._Gear; 

            }
            else
            {
                NGUITools.FindInParents<ShopIniter>(gameObject).ShowConfirmDialog("你的齿轮数量不足，加油玩游戏赚取齿轮吧");
            }
        }
    }


   

    /// <summary>
    /// 选择
    /// </summary>
    void Select()
    {
        switch (_Weapon)
        {
            case WEAPON.Tank:
                PlayerPrefs.SetInt(Consts.PREF_TANK, _Id);
                break;

            case WEAPON.Energy:
                PlayerPrefs.SetInt(Consts.PREF_ENERGY_WEAPON, _Id);
                break;

            case WEAPON.Bomb:
                PlayerPrefs.SetInt(Consts.PREF_BOMBING_WEAPON, _Id);
                break;
        }
        GlobalManager.GetInstants().SyncPlayerData();
        foreach (ShopItemIniter tS in  NGUITools.FindInParents<ShopIniter>(gameObject).GetComponentsInChildren<ShopItemIniter>())
        {
            tS.SendMessage("Init", SendMessageOptions.DontRequireReceiver);
        }

    }

    /// <summary>
    /// 初始化
    /// </summary>
    void Init()
    {
        this._SelectedEffect.SetActiveRecursively(false);
        this._Lbl_Price.text = _Price.ToString();

        switch (_Weapon)
        {
            case WEAPON.Tank:
                if (GlobalManager.GetInstants()._PlayerEntity._Own_Tanks.Contains(_Id))
                {
                    this._Btn_Buy.SetActiveRecursively(false);
                    this._Btn_Select.SetActiveRecursively(true);
                    if (GlobalManager.GetInstants()._PlayerEntity._Tank_Id == _Id)
                    {
                        this._SelectedEffect.SetActiveRecursively(true);
                    }
                    else
                    {
                        this._SelectedEffect.SetActiveRecursively(false);
                    }
                }
                else
                {
                    this._Btn_Buy.SetActiveRecursively(true);
                    this._Btn_Select.SetActiveRecursively(false);
                }
                break;

            case WEAPON.Energy:
                if (GlobalManager.GetInstants()._PlayerEntity._Own_Energy.Contains(_Id))
                {
                    this._Btn_Buy.SetActiveRecursively(false);
                    this._Btn_Select.SetActiveRecursively(true);
                    if (GlobalManager.GetInstants()._PlayerEntity._Energy_Weapons == _Id)
                    {
                        this._SelectedEffect.SetActiveRecursively(true);
                    }
                    else
                    {
                        this._SelectedEffect.SetActiveRecursively(false);
                    }
                }
                else
                {
                    this._Btn_Buy.SetActiveRecursively(true);
                    this._Btn_Select.SetActiveRecursively(false);
                }
                break;

            case WEAPON.Bomb:
                if (GlobalManager.GetInstants()._PlayerEntity._Own_Bomb.Contains(_Id))
                {
                    this._Btn_Buy.SetActiveRecursively(false);
                    this._Btn_Select.SetActiveRecursively(true);
                    if (GlobalManager.GetInstants()._PlayerEntity._Bombing_Weapons == _Id)
                    {
                        this._SelectedEffect.SetActiveRecursively(true);
                    }
                    else
                    {
                        this._SelectedEffect.SetActiveRecursively(false);
                    }
                }
                else
                {
                    this._Btn_Buy.SetActiveRecursively(true);
                    this._Btn_Select.SetActiveRecursively(false);
                }
                break;
        }
    }




    void Start()
    {
        this.Init();
    }
}
