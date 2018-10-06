using UnityEngine;
using System.Collections;

public class EnergyWeapen2 : Props {


    /// <summary>
    /// 爆炸效果；
    /// </summary>
    public GameObject _BombExplosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void BombExplosion()
    {
        if (!_BombExplosion)
        {
            this._BombExplosion = transform.FindChild("EnergyBomb_2").gameObject;
        }
        GameObject BS = Instantiate(_BombExplosion, transform.position, Quaternion.identity) as GameObject;
        foreach (ParticleEmitter BP in BS.GetComponentsInChildren<ParticleEmitter>())
        {
            BP.emit = true;
        }
        // this.BombKillTank();
        gameObject.SetActiveRecursively(false);
    }

}
