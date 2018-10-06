using UnityEngine;
using System.Collections;

public class Consts
{

    public class Sound
    {
        public const string BACKGROUND_SOUND_KEY = "bs";

        public const string EFFECT_SOUND_KEY = "es";

        public const int SOUND_ON = 0;

        public const int SOUND_OFF = 1;
    }

    /******PREF代表内部存储用数据key*********/
    /// <summary>
    /// 齿轮key
    /// </summary>
    public const string PREF_GEAR = "gear";
    /// <summary>
    /// 能量武器key
    /// </summary>
    public const string PREF_ENERGY_WEAPON = "energy_weapon";
    /// <summary>
    /// 轰炸武器key
    /// </summary>
    public const string PREF_BOMBING_WEAPON = "bombing_weapon";
    /// <summary>
    /// 坦克key
    /// </summary>
    public const string PREF_TANK = "tank";

    /// <summary>
    /// 拥有的坦克
    /// </summary>
    public const string PREF_OWN_TANK = "own_tank";
    /// <summary>
    /// 拥有的能量武器
    /// </summary>
    public const string PREF_OWN_ENERGY = "own_energy";
    /// <summary>
    /// 拥有的轰炸武器
    /// </summary>
    public const string PREF_OWN_BOMB = "own_bomb";

    public const int MAX_TIME = 60;

    public const int MAX_BOMB = 3;

    public const string Introduce = "FirstBlood";

    public const string RankBoard = "Rank";

   
    
}
