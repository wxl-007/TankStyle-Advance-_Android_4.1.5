using UnityEngine;
using System.Collections;

/// <summary>
/// 用户实体类
/// </summary>
public class PlayerEntity {

    /// <summary>
    /// 用户拥有的齿轮数量
    /// </summary>
    public int _Gear;
    /// <summary>
    /// 用户拥有的能量武器的id
    /// </summary>
    public int _Energy_Weapons;
    /// <summary>
    /// 用户拥有的轰炸武器的id
    /// </summary>
    public int _Bombing_Weapons;
    /// <summary>
    /// 用户拥有的坦克的id
    /// </summary>
    public int _Tank_Id;

    /// <summary>
    /// 
    /// </summary>
    public ArrayList _Own_Tanks = new ArrayList();
    /// <summary>
    /// 
    /// </summary>
    public ArrayList _Own_Energy = new ArrayList();
    /// <summary>
    /// 
    /// </summary>
    public ArrayList _Own_Bomb = new ArrayList();

    //public ArrayList _Own_Rank = new ArrayList();
    
}
