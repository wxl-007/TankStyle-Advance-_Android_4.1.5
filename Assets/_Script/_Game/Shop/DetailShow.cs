﻿using UnityEngine;
using System.Collections;

public class DetailShow : MonoBehaviour
{

    public GameObject _UpLight;

    public GameObject _DownLight;

    public GameObject[] _LightLine;

    private string _Introduction1;
    private string _Introduction2;
    private string _Introduction3;


    public void DisplayDetail(string tRadiatingTime, string tFireRate, string tSpeed, string tWeapon)
    {
        switch (tWeapon)
        {
            case "Tank":
                _Introduction1 = "散热时间：";
                _Introduction2 = "发射速率：";
                _Introduction3 = "移动速率：";
                break;
            case "Bomb":
                _Introduction1 = "";
                _Introduction2 = "介绍：";
                _Introduction3 = "范围：";
                break;
            case "Energy":
                _Introduction1 = "";
                _Introduction2 = "介绍：";
                _Introduction3 = "范围：";
                break;

            default:
                break;
        }

        this.transform.FindChild("Detail_Child2").transform.FindChild("Lbl_RadiatingTime").GetComponent<UILabel>().text = _Introduction1 + tRadiatingTime;
        this.transform.FindChild("Detail_Child2").transform.FindChild("Lbl_FireRate").GetComponent<UILabel>().text = _Introduction2 + tFireRate;
        this.transform.FindChild("Detail_Child2").transform.FindChild("Lbl_Speed").GetComponent<UILabel>().text = _Introduction3 + tSpeed;
    }

    void StartShowing()
    {
        iTween.MoveTo(_UpLight, iTween.Hash("y", 154.1128f, "islocal", true, "time", 0.5f));
        iTween.MoveTo(_DownLight, iTween.Hash("y", -102.1905f, "islocal", true, "time", 0.5f));


        //iTween.FadeTo(_LightLine, iTween.Hash("alpha", 0, "time", 0f));
        foreach (GameObject tG in _LightLine)
        {
            tG.SetActiveRecursively(true);
            if (tG.GetComponent<UILabel>() && !tG.GetComponent<TypewriterEffect>())
            {
                tG.AddComponent<TypewriterEffect>();
            }
            //iTween.FadeTo(tG, iTween.Hash("alpha", 1, "time", 1f));
        }
    }

    void GoneShow()
    {
        iTween.MoveTo(_UpLight, iTween.Hash("y", 900f, "islocal", true, "time", 0.3f));
        iTween.MoveTo(_DownLight, iTween.Hash("y", -900f, "islocal", true, "time", 0.3f));
        foreach (GameObject tG in _LightLine)
        {
            //iTween.FadeTo(tG, iTween.Hash("alpha", 0, "time", 1));
            tG.SetActiveRecursively(false);
        }

    }
}
