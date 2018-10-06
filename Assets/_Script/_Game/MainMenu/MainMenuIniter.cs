using UnityEngine;
using System.Collections;

public class MainMenuIniter : MonoBehaviour
{
    /// <summary>
    /// 得到要展示的所有tank
    /// </summary>
    public GameObject[] _Tanks;

    /// <summary>
    /// 当前的坦克
    /// </summary>
    private GameObject _CurTank;

    void Awake()
    {
        //同步玩家数据
        GlobalManager.GetInstants().SyncPlayerData();
        //根据玩家数据得到玩家当前的tank
        this._CurTank = _Tanks[GlobalManager.GetInstants()._PlayerEntity._Tank_Id - 1];
        //主菜单中展示玩家当前的tank
        this.gameObject.transform.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmGameObject("CurrentTank").Value = _CurTank;
    }
}
