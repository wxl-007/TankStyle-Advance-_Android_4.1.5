using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    public enum AXIS
    {
        X,
        Y,
        Z
    }

    /// <summary>
    /// 旋转围绕轴
    /// </summary>
    public AXIS _Axis = AXIS.Y;

    /// <summary>
    /// 相对参照物
    /// </summary>
    public Space _Space = Space.Self;
    /// <summary>
    /// 旋转速度
    /// </summary>
    public float _Rotate_Speed = 10;


    /// <summary>
    /// 类变量-旋转轴
    /// </summary>
    private Vector3 Axis;

	// Use this for initialization
	void Start () 
    {
        switch (_Axis)
        {
            case AXIS.X:
                Axis = Vector3.right;
                break;

            case AXIS.Y:
                Axis = Vector3.up;
                break;

            case AXIS.Z:
                Axis = Vector3.forward;
                break;
        }
	}
	
	void Update () 
    {
        transform.Rotate(Axis, Time.deltaTime * _Rotate_Speed, _Space);
	}
}
