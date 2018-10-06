﻿using UnityEngine;
using System.Collections;

public class PropBomb3_ParticleEffect : MonoBehaviour
{
    /// <summary>
    /// 进入影响范围的坦克；
    /// </summary>
    private ArrayList _SuckTanks = new ArrayList();
	// Use this for initialization
	void Start () {
        Invoke("CloseBlackHole", 5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary>
    ///移除效果。
    /// </summary>
    void CloseBlackHole() {
        gameObject.SetActiveRecursively(false);
    }

    /// <summary>
    ///延迟时间，爆炸消灭坦克 
    /// </summary>
    void ExplisionRange()
    {
        if (_SuckTanks != null)
        {
            foreach (GameObject tank in _SuckTanks)
            {
                tank.SendMessage("Kill", SendMessageOptions.DontRequireReceiver);
                GameCommander.Instance.ScoreUp(1);
                Debug.Log("Kill Tank");
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
            // Debug.Log("trigger on!!");
            if (other.tag.StartsWith("tank"))
            {
                //Vector3 suckDir = gameObject.transform.position - other.transform.position;

                _SuckTanks.Add(other.transform.gameObject);
                other.SendMessage("Contact", other.transform.gameObject, SendMessageOptions.DontRequireReceiver);
                Invoke("ExplisionRange", 1f);

                //_SuckTanks.Remove(other.transform.gameObject);

                Debug.Log("stuck trigger");
            }
        }
    }
}
