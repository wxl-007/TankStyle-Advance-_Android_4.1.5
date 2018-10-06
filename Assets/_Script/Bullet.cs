using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    /// <summary>
    /// 承载自己子弹的缓冲池
    /// </summary>
    private BreedPool _BulletPool;
    /// <summary>
    /// 子弹的移动速度
    /// </summary>
    public float _MoveSpeed = 1;
    /// <summary>
    /// 发射自己的坦克
    /// </summary>
    public GameObject _Father;
    /// <summary>
    /// 发射的粒子效果
    /// </summary>
    public GameObject _ShootExplos;


	// Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _MoveSpeed);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("board"))
        {
            this.Kill();
        }
        else if (other.gameObject.tag.StartsWith("tank") && _Father != other.gameObject)
        {
            this.Kill();
            //other.SendMessage("SpawnGears");
           // other.SendMessage("Kill");
            
            other.GetComponent<Tank>().SpawnGears();
            other.GetComponent<Tank>().Kill();
            GameCommander.Instance.ScoreUp(1);
            GameCommander.Instance.CountCombo(transform.position);
            
            GameObject.Find("GameCommander").SendMessage("SpawnProps", transform.position);
        }
    }

    void OnSpawn(BreedPool pmp)
    {
        // we will use this when unspawning, thats why it was passed - so we don't have to type PoolManager.GetPool
        this._BulletPool = pmp;
        Explosion();
    }

    void Explosion()
    {
        if (!_ShootExplos)
        {
            this._ShootExplos = transform.FindChild("ShootExplosion").gameObject;
        }
        GameObject tS = Instantiate(_ShootExplos, transform.position, Quaternion.identity) as GameObject;
        tS.GetComponent<ParticleEmitter>().emit = true;
    }

    /// <summary>
    /// 死！
    /// </summary>
    void Kill()
    {
        this._BulletPool.Unspawn(gameObject);
    }
}
