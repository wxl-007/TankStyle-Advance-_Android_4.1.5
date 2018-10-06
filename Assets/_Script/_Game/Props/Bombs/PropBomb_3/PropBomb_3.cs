using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PropBomb_3 : Props
{
    ///<summary>
    ///黑洞效果
    /// </summary>
    public GameObject _BlackholeExplosion;


    //private ArrayList _SuckTanks = new ArrayList();
	// Use this for initialization
	void Start () {
	
	}

    void OpenBlackHole()
    {
        if (!_BlackholeExplosion)
        {
            this._BlackholeExplosion = transform.FindChild("BlackholeEffect").gameObject;
        }
        GameObject BS = Instantiate(_BlackholeExplosion, transform.position, Quaternion.identity) as GameObject;
        foreach (ParticleEmitter BP in BS.GetComponentsInChildren<ParticleEmitter>())
        {
            BP.emit = true;
        }

        gameObject.SetActiveRecursively(false);
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    ///<summary>
    ///爆炸范围
    /// </summary>
    /* IEnumerator ExplosionRange()
     {
         GameObject[] curTanks = GameObject.FindGameObjectsWithTag("tank");
         foreach (GameObject tank in curTanks)
         {
             Vector3 suckForce = tank.transform.position - gameObject.transform.position;
             //距离的平方，因为运行速度比较快。
             float dis = Vector3.SqrMagnitude(suckForce);

             if (dis <= _Distance)
             {
                 //先卡住，缓慢往里吸；
                 tank.SendMessage("Stuck",SendMessageOptions.DontRequireReceiver);
                // tank.rigidbody.AddForceAtPosition(suckForce,tank.transform.position);
                 //yield return new WaitForSeconds(1.5f);

                // tank.SendMessage("Kill", SendMessageOptions.DontRequireReceiver);
                 GameCommander.Instance.ScoreUp(1);
                 Debug.Log("contract the near tank !");
             }
         }
     } 

    
     void ExplisionRange() {
         if (_SuckTanks != null) {
            foreach(GameObject tank in _SuckTanks){
                tank.SendMessage("Kill",SendMessageOptions.DontRequireReceiver);
                Debug.Log("Kill Tank");
            }
            _SuckTanks = null;
         }
     }

     void OnTriggerEnter(Collider other)
     {
         if (gameObject.collider.isTrigger == true)
         {
            // Debug.Log("trigger on!!");
             if (other.tag.StartsWith("tank"))
             {
                 Vector3 suckDir = gameObject.transform.position - other.transform.position;
                
                 _SuckTanks.Add(other.transform.gameObject);
                 other.SendMessage("Contact",other.transform.gameObject,SendMessageOptions.DontRequireReceiver);
                 Invoke("ExplisionRange",1f);

                 //_SuckTanks.Remove(other.transform.gameObject);

                 Debug.Log("stuck trigger");
             }
         }
     }
     */
}
