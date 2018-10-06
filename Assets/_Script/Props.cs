using UnityEngine;
using System.Collections;

public class Props : MonoBehaviour {

    /// <summary>
    /// 旋转的速度
    /// </summary>
    public float _RotateSpeed = 1;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        transform.Rotate(Vector3.up, Time.fixedDeltaTime * _RotateSpeed, Space.World);
	}
}
