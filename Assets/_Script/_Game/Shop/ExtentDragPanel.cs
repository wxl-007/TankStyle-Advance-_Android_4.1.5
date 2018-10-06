﻿using UnityEngine;
using System.Collections;

public class ExtentDragPanel : MonoBehaviour
{

    public GameObject _DetailShow;

    void OnDrag(Vector2 delta)
    {
        if (enabled)
        {
            this._DetailShow.SendMessage("GoneShow");
        }
    }

    /// <summary>
    /// 当商品被居中放置时触发的事件
    /// </summary>
    void Recenter(GameObject center)
    {
        Debug.Log("促发2");
        string tRadiatingTime = center.gameObject.transform.FindChild("ShopItem").GetComponent<ShopItemIniter>()._RadiatingTime;
        string tFireRate = center.gameObject.transform.FindChild("ShopItem").GetComponent<ShopItemIniter>()._FireRate;
        string tSpeed = center.gameObject.transform.FindChild("ShopItem").GetComponent<ShopItemIniter>()._Speed;
        string tWeapon = center.gameObject.transform.FindChild("ShopItem").GetComponent<ShopItemIniter>()._Weapon.ToString();

        this._DetailShow.SendMessage("StartShowing");
        this._DetailShow.GetComponent<DetailShow>().DisplayDetail(tRadiatingTime, tFireRate, tSpeed, tWeapon);
    }
}
