using UnityEngine;
using System.Collections;

public class PropBomb_2 : Props {
    
    /// <summary>
    ///导弹爆炸效果 
    /// </summary>
    public GameObject _MissileExplosion;

    ///<summary>
    ///距离的平方！平方！这张地图的最大的距离的平方大概128！
    /// </summary>
    public float _Distance;
	// Use this for initialization
	void Start () {
        StartCoroutine(ControllExplosion());
	}

    
    /// <summary>
    /// 导弹轰炸
    /// </summary>
    void MissileExplosion() { 
        if (!_MissileExplosion)
        {
            this._MissileExplosion = transform.FindChild("PropBomb2").gameObject;
        }
        GameObject BS = Instantiate(_MissileExplosion, transform.position, Quaternion.identity) as GameObject;
        foreach (ParticleEmitter BP in BS.GetComponentsInChildren<ParticleEmitter>())
        {
            BP.emit = true;
           // Debug.Log("bob~!");
        }
        this.ExplosionRange();

        gameObject.SetActiveRecursively(false);
    }

    /// <summary>
    /// 点击会随机出现地图爆炸
    /// </summary>
    /// <returns></returns>
    IEnumerator ControllExplosion() {
        yield return new WaitForSeconds(2.5f);
        this.MissileExplosion();
    }
 
    ///<summary>
    ///爆炸范围
    /// </summary>
    void ExplosionRange() {
        GameObject[] curTanks = GameObject.FindGameObjectsWithTag("tank");
        foreach(GameObject tank in curTanks){
            //距离的平方，因为运行速度比较快。
            float dis = Vector3.SqrMagnitude(tank.transform.position - gameObject.transform.position);
            if (dis <= _Distance)
            {
                tank.SendMessage("Kill", SendMessageOptions.DontRequireReceiver);
                GameCommander.Instance.ScoreUp(1);
                //Debug.Log("kill the near tank !");
            }
        }
    } 

}
