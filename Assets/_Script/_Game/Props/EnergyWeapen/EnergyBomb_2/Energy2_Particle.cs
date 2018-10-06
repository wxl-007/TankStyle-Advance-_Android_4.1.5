﻿using UnityEngine;
using System.Collections;

public class Energy2_Particle : MonoBehaviour {

    /// <summary>
    /// 进入范围的坦克；
    /// </summary>
     private ArrayList _SuckTanks = new ArrayList();

    ///<summary>
    ///燃烧的效果；
    /// </summary>
     public GameObject _BurnEffect;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// 延迟消灭。爆炸消灭tank 并且加分。
    /// </summary>
    void ExplisionRange()
    {
        if (_SuckTanks != null)
        {
            foreach (GameObject tank in _SuckTanks)
            {
                tank.SendMessage("Kill", SendMessageOptions.DontRequireReceiver);
                Debug.Log("Kill Tank");
                GameCommander.Instance.ScoreUp(1);
            }
            _SuckTanks = null;
        }
    }

    /// <summary>
    /// 爆炸的范围触发器
    /// </summary>
    /// <param name="other"></param>
    
    void OnTriggerEnter(Collider other)
    {
        if (gameObject.collider.isTrigger == true)
        {
            
            if (other.tag.StartsWith("tank"))
            {
                _SuckTanks.Add(other.transform.gameObject);
                GameObject fire = (GameObject)Instantiate(_BurnEffect,other.transform.position,Quaternion.identity);
                fire.transform.parent = other.transform;
                
                 Debug.Log("burn on!!");
                Invoke("ExplisionRange", 1.5f);
            }
        }
    }
}
