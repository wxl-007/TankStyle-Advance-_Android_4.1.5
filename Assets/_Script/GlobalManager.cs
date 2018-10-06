using UnityEngine;
using System.Collections;

public class GlobalManager
{
    /// <summary>
    /// 
    /// </summary>
    public PlayerEntity _PlayerEntity;

    /// <summary>
    /// 
    /// </summary>
    public bool _CanVibrate = true;
    /// <summary>
    /// 
    /// </summary>
    public bool _CanMusic = true;

    public bool _ShowIntroduce = true;

    public string[] _Leaderboard = new string[] { "1st", "2nd", "3rd", "4th", "5th" };


    
    /// <summary>
    /// 
    /// </summary>
    private static GlobalManager _GlobalManager;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static GlobalManager GetInstants()
    {
        if (_GlobalManager == null)
            _GlobalManager = new GlobalManager();
        return _GlobalManager;
    }


    /// <summary>
    /// 更新玩家的齿轮数量
    /// </summary>
    /// <param name="num"></param>
    public void UpdatePlayerGear(int num)
    {
        int tC = _PlayerEntity._Gear + num;
        if (tC <= 0)
            tC = 0;
        PlayerPrefs.SetInt(Consts.PREF_GEAR, tC);
        this._PlayerEntity._Gear = tC;
    }

    /// <summary>
    /// 同步用户数据
    /// </summary>
    public void SyncPlayerData()
    {
        this._PlayerEntity = new PlayerEntity();



        this._PlayerEntity._Gear = PlayerPrefs.GetInt(Consts.PREF_GEAR, 200);
        this._PlayerEntity._Energy_Weapons = PlayerPrefs.GetInt(Consts.PREF_ENERGY_WEAPON, 1);
        this._PlayerEntity._Bombing_Weapons = PlayerPrefs.GetInt(Consts.PREF_BOMBING_WEAPON, 1);
        this._PlayerEntity._Tank_Id = PlayerPrefs.GetInt(Consts.PREF_TANK, 1);

        string tOwnTanks = PlayerPrefs.GetString(Consts.PREF_OWN_TANK, "1");
        string[] tOwnTank_Array = tOwnTanks.Split(new char[1] { ',' });

        foreach (string tT in tOwnTank_Array)
        {
            if (!tT.Equals(string.Empty))
            {
                this._PlayerEntity._Own_Tanks.Add(int.Parse(tT));
            }
        }




        string tOwnEnergys = PlayerPrefs.GetString(Consts.PREF_OWN_ENERGY, "1");
        string[] tOwnEnergys_Array = tOwnEnergys.Split(new char[1] { ',' });

        foreach (string tE in tOwnEnergys_Array)
        {
            this._PlayerEntity._Own_Energy.Add(int.Parse(tE));
        }




        string tOwnBombs = PlayerPrefs.GetString(Consts.PREF_OWN_BOMB, "1");
        string[] tOwnBombs_Array = tOwnBombs.Split(new char[1] { ',' });

        foreach (string tB in tOwnBombs_Array)
        {
            if (!_PlayerEntity._Own_Bomb.Contains(int.Parse(tB)))
            {
                this._PlayerEntity._Own_Bomb.Add(int.Parse(tB));
            }

        }
    }
}
