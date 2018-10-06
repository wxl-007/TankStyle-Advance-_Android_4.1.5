﻿using UnityEngine;
using System.Collections;

public class Atomicbomb : Props
{

    /// <summary>
    /// 核弹爆炸效果；
    /// </summary>
    public GameObject _BombExplosion;




    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 5) && hit.transform.name.StartsWith("PropBomb"))
                {
                    hit.transform.gameObject.SendMessage("BombExplosion", SendMessageOptions.DontRequireReceiver);
                }

            }
        }

        else
        {

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 5) && hit.transform.name.StartsWith("PropBomb"))
                {
                    hit.transform.gameObject.SendMessage("BombExplosion", SendMessageOptions.DontRequireReceiver);
                }

            }

        }
    }

    /// <summary>
    /// 爆炸性武器 爆破！ 逻辑同坦克爆炸一样。
    /// </summary>
    void BombExplosion()
    {
        if (!_BombExplosion)
        {
            this._BombExplosion = transform.FindChild("PropBomb1").gameObject;
        }
        GameObject BS = Instantiate(_BombExplosion, transform.position, Quaternion.identity) as GameObject;
        foreach (ParticleEmitter BP in BS.GetComponentsInChildren<ParticleEmitter>())
        {
            BP.emit = true;
        }
        this.BombKillTank();

        gameObject.SetActiveRecursively(false);

    }

    /// <summary>
    /// 爆破坦克
    /// </summary>
    void BombKillTank()
    {
        GameObject[] _CurrentTanks = GameObject.FindGameObjectsWithTag("tank");
        foreach (GameObject tTank in _CurrentTanks)
        {
            tTank.SendMessage("Kill", SendMessageOptions.DontRequireReceiver);
            tTank.SendMessage("SpawnGears");
         //   Debug.Log("kill it ");
            GameCommander.Instance.ScoreUp(1);
        }
    }
}
