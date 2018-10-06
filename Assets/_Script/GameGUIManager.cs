﻿using UnityEngine;
using System.Collections;

public class GameGUIManager : MonoBehaviour
{

    /// <summary>
    /// 剩余时间
    /// </summary>
    public UILabel _Lbl_Time;
    /// <summary>
    /// 杀死数量
    /// </summary>
    public UILabel _Lbl_Kill;
    /// <summary>
    /// 轰炸武器剩余数量                       
    /// </summary>
    public UILabel _Lbl_BombNumber;

    /// <summary>
    /// 游戏前台ui
    /// </summary>
    public Transform _Panel_UI;

    /// <summary>
    /// 暂停的对话框
    /// </summary>
    public Transform _Panel_Pause;

    /// <summary>
    /// 游戏结束对话框
    /// </summary>
    public Transform _Panel_GameOver;

    /// <summary>
    /// 游戏结束获得齿轮数
    /// </summary>
    public UILabel _Lbl_TotalGear;
    /// <summary>
    /// 游戏结束时的总齿轮数
    /// </summary>
    public UILabel _Lbl_ReceiveGear;


    public UILabel _LblWhat;

    public Transform _Panel_NewRecord;

    public UILabel _Lbl_NewRecordGear;


    void Start()
    {
        this.InitGUI();
    }




    /// <summary>
    /// 外部调用
    /// </summary>
    private static GameGUIManager instance;
    public static GameGUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (GameGUIManager)FindObjectOfType(typeof(GameGUIManager));
            }
            if (!instance)
            {
                Debug.LogError("GameGUIManager could not find himself!");
            }
            return instance;
        }
    }


    /// <summary>
    /// 初始化GUI显示内容
    /// </summary>
    void InitGUI()
    {
        StartCoroutine(UpdateTime(60));
        StartCoroutine(UpdateKill(0));
        StartCoroutine(UpdateBomb(3));
    }

    /// <summary>
    /// 更新时间显示
    /// </summary>
    /// <param name="time"></param>
    public IEnumerator UpdateTime(int time)
    {
        this._Lbl_Time.text = TimeTrans(time);
        yield return null;
    }

    /// <summary>
    /// 更新杀死显示
    /// </summary>
    /// <param name="num"></param>
    public IEnumerator UpdateKill(int num)
    {
        this._Lbl_Kill.text = num.ToString();
        yield return null;

    }

    /// <summary>
    /// 更新轰炸武器显示
    /// </summary>
    /// <param name="num"></param>
    IEnumerator UpdateBomb(int num)
    {
        this._Lbl_BombNumber.text = num.ToString();
        yield return null;
    }

    /// <summary>
    /// 暂停，展示对话框
    /// </summary>
    public void Pause()
    {
        this._Panel_Pause.transform.localPosition = new Vector3(0, 0, -3825.942f);
    }

    ///<summary>
    ///游戏结束
    /// </summary>
    public void GameOver(int gears, bool tFuc)
    {

        if (tFuc)
        {

            string[] tScore = PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[2]).Split(',');
            Debug.Log(PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[2]));
            if (gears >= int.Parse(tScore[0]))
            {
                this._Panel_NewRecord.transform.localPosition = new Vector3(0, 0, -3586f);
                this._Lbl_NewRecordGear.text = gears.ToString();

            }
            else
            {
                _LblWhat.text = "再接再厉，冲击排名";
                this._Panel_GameOver.transform.localPosition = new Vector3(0, 0, -3586f);
                this._Lbl_ReceiveGear.text = gears.ToString();
                this._Lbl_TotalGear.text = GlobalManager.GetInstants()._PlayerEntity._Gear.ToString();
            }
        }
        else
        {
            string[] tScore = PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[2]).Split(',');
            Debug.Log(PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[2]));
            if (gears >= int.Parse(tScore[0]))
            {
                this._Panel_NewRecord.transform.localPosition = new Vector3(0, 0, -3586f);
                this._Lbl_NewRecordGear.text = gears.ToString();

            }
            else
            {
                _LblWhat.text = "继续努力，不必气馁";
                this._Panel_GameOver.transform.localPosition = new Vector3(0, 0, -3586f);
                this._Lbl_ReceiveGear.text = gears.ToString();
                this._Lbl_TotalGear.text = GlobalManager.GetInstants()._PlayerEntity._Gear.ToString();
            }
        }
        
    }


    /// <summary>
    /// 返回游戏
    /// </summary>
    public void Resume()
    {
        this._Panel_Pause.transform.Translate(new Vector3(-10000, 0, 0));
    }

    ///<summary>
    ///重新开始游戏
    /// </summary>
    public void RestartGame()
    {
        Resume();
        this._Panel_GameOver.transform.Translate(new Vector3(-10000, 0, 0));

    }

    /// <summary>
    /// 时间转换
    /// </summary>
    string TimeTrans(int sec)
    {
        string tMin = (sec / 60).ToString();
        if (tMin.Length < 2)
            tMin += "0";
        string tSec = (sec % 60).ToString();
        if (tSec.Length < 2)
            tSec = "0" + tSec;
        return tMin + ":" + tSec;
    }


}
