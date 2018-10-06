using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour {

	/// <summary>
	/// �ж�̹���Ƿ� ���ֹ�tank��
	/// </summary>
    public  bool _IsExit = true;

    private List<GameObject> _Stay = new List<GameObject>();
    private GameObject _InTank;
	void Start () {
       
	}
	
	// Update is called once per frame

	void Update () {
        
	}
    

    /// <summary>
    /// �ж������Ƿ���̹��
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other ) {
        if (other.tag.StartsWith("tank"))
        {
            _IsExit = false;
            _InTank = other.transform.gameObject ;
            _Stay.Add(_InTank);
            StartCoroutine(StayTank());
        }   
    }
    void OnTriggerExit(Collider other)
    {
        _IsExit = true;
        if (other.tag.StartsWith("tank") && _InTank == other.transform.gameObject)
        {
            _IsExit = true;
            _Stay.Remove(_InTank);
           
        }
    }

    IEnumerator StayTank() {
        yield return new WaitForSeconds(5f);
        if (_Stay.Contains(_InTank)) { 
            _Stay.Remove(_InTank);
            this._IsExit = true;
        }
    }

    
    /// <summary>
    /// ������ŷ�Χ��
    /// </summary>
    void OnDrawGizmos() {
        Gizmos.color = Color.yellow ;
        Gizmos.DrawWireCube(gameObject.transform.position, new Vector3(1.2f, 1f, 1.8f));
    }
}
