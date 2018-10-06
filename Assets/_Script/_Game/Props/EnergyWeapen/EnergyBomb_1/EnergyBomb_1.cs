﻿using UnityEngine;
using System.Collections;

public class EnergyBomb_1 : MonoBehaviour {

    /// <summary>
    /// 爆炸效果；
    /// </summary>
    public GameObject _BombExplosion;

    ///<summary>
    ///爆炸范围；同样是范围的平方最大128；
    /// </summary>
    public float _Distance;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other) {
        if (other.collider.tag.StartsWith("tank")) {
            this.BombExplosion();
        }
    }

    void BombExplosion()
    {
        //if (!_BombExplosion)
        //{
        //    this._BombExplosion = transform.FindChild("Bomb3").gameObject;
        //}
        GameObject BS = Instantiate(_BombExplosion, transform.position, Quaternion.identity) as GameObject;
        foreach (ParticleEmitter BP in BS.GetComponentsInChildren<ParticleEmitter>())
        {
            BP.emit = true;
   
        }
        this.ExplosionRange();
        gameObject.SetActiveRecursively(false);
    }

    void ExplosionRange()
    {
        GameObject[] curTanks = GameObject.FindGameObjectsWithTag("tank");
        foreach (GameObject tank in curTanks)
        {
            //距离的平方，因为运行速度比较快。
            float dis = Vector3.SqrMagnitude(tank.transform.position - gameObject.transform.position);
            if (dis <= _Distance)
            {
                tank.SendMessage("Kill", SendMessageOptions.DontRequireReceiver);
                GameCommander.Instance.ScoreUp(1);
                
            }
        }
    } 
}