using UnityEngine;
using System.Collections;

public class GearFx : MonoBehaviour
{
    /// <summary>
    /// ���ٳ��ֵ�ʱ��
    /// </summary>
    public int _DestroyTime = 5;
    /// <summary>
    /// ���ɳ����ֵ����ֿ�ʼ���յ��ƶ���ʱ��
    /// </summary>
    public int _MoveStartTime = 1;
    /// <summary>
    /// �����ƶ����յ�
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
    /// ���ֿ�ʼ�ƶ�
    /// </summary>
    void Go()
    {
        iTween.RotateTo(this.gameObject, _EndPositon, 3f);
        iTween.MoveTo(this.gameObject, _EndPositon, 3f);
    }

    /// <summary>
    /// ���ٳ���
    /// </summary>
    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
