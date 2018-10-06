using UnityEngine;
using System.Collections;

public class AddTimeFx : MonoBehaviour
{
    /// <summary>
    /// �ƶ���ָ��λ��ʱ��
    /// </summary>
    public float _MoveStartTime = 0.5f;
    private Vector3 _EndPositon;
    /// <summary>
    /// ����Ч��
    /// </summary>
    public GameObject _ParticleEffect;
    /// <summary>
    /// ����ʱ��־��
    /// </summary>
    private GameObject _TimeLabel;
    /// <summary>
    /// ʱ��ͼƬ
    /// </summary>
    public GameObject _TimeSprite;

    /// <summary>
    /// �����յ�ʱ����Ч
    /// </summary>
    public GameObject _EndParticle;

    void Start()
    {
        _EndPositon = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height - 10));
        Invoke("ParticleMove", _MoveStartTime);
        Invoke("Destroy", 2.5f);
    }

    /// <summary>
    /// ʱ���ƶ�
    /// </summary>
    void ParticleMove()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", _EndPositon, "time", 1f, "easetype", "linear", "oncomplete", "EndParticle"));
        this.SetParticle();
       
    }
    

    /// <summary>
    /// ����Ч������
    /// </summary>
    void SetParticle() {
        //GameObject tParticle = Instantiate(_ParticleEffect, transform.position, Quaternion.identity) as GameObject;
        //tParticle.transform.parent = gameObject.transform;
        //foreach (ParticleEmitter BP in tParticle.GetComponentsInChildren<ParticleEmitter>())
        //{
        //    BP.emit = true;
        //}
        //Debug.Log("Particle  true");
        _ParticleEffect.GetComponent<ParticleEmitter>().emit = true;
    }

    void EndParticle() {
        _EndParticle.GetComponentInChildren<ParticleEmitter>().emit = true;
       // Debug.Log("bomb!");
    }
    /// <summary>
    /// ʱ����ʧ
    /// </summary>
    void Destroy()
    {
        _TimeLabel = GameObject.Find("Lbl_Time").gameObject;
        _TimeLabel.SendMessage("HeardTip", SendMessageOptions.DontRequireReceiver);
        Destroy(this.gameObject);
    }
}
