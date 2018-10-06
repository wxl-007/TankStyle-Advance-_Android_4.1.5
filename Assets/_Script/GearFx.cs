using UnityEngine;
using System.Collections;

public class GearFx : MonoBehaviour
{
    /// <summary>
    /// 销毁齿轮的时间
    /// </summary>
    public int _DestroyTime = 5;
    /// <summary>
    /// 生成出齿轮到齿轮开始向终点移动的时间
    /// </summary>
    public int _MoveStartTime = 1;
    /// <summary>
    /// 齿轮移动的终点
    /// </summary>
    private Vector3 _EndPositon;

    private Camera _Camera;

    void Start()
    {
        //if (!_Camera)
        //{
        //    _Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //}
        _EndPositon = Camera.mainCamera.ScreenToWorldPoint(new Vector3(30, Screen.height - 10));
        Debug.Log(_EndPositon);
        iTween.RotateBy(this.gameObject, iTween.Hash("x", Random.Range(1, 5), "y", Random.Range(1, 5), "time", 30f));

        Invoke("Destroy", _DestroyTime);
        Invoke("Go", _MoveStartTime);
    }

    /// <summary>
    /// 齿轮开始移动
    /// </summary>
    void Go()
    {
        iTween.RotateTo(this.gameObject, _EndPositon, 3f);
        iTween.MoveTo(this.gameObject, _EndPositon, 3f);
    }

    /// <summary>
    /// 销毁齿轮
    /// </summary>
    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
