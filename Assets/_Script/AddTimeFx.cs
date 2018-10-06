using UnityEngine;
using System.Collections;

public class AddTimeFx : MonoBehaviour
{
    /// <summary>
    /// 移动到指定位置时间
    /// </summary>
    public float _MoveStartTime = 0.5f;
    private Vector3 _EndPositon;
    /// <summary>
    /// 粒子效果
    /// </summary>
    public GameObject _ParticleEffect;
    /// <summary>
    /// 倒计时标志。
    /// </summary>
    private GameObject _TimeLabel;
    /// <summary>
    /// 时间图片
    /// </summary>
    public GameObject _TimeSprite;

    /// <summary>
    /// 到达终点时的特效
    /// </summary>
    public GameObject _EndParticle;

    void Start()
    {
        _EndPositon = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height - 10));
        Invoke("ParticleMove", _MoveStartTime);
        Invoke("Destroy", 2.5f);
    }

    /// <summary>
    /// 时间移动
    /// </summary>
    void ParticleMove()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", _EndPositon, "time", 1f, "easetype", "linear", "oncomplete", "EndParticle"));
        this.SetParticle();
       
    }
    

    /// <summary>
    /// 粒子效果启动
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
    /// 时间消失
    /// </summary>
    void Destroy()
    {
        _TimeLabel = GameObject.Find("Lbl_Time").gameObject;
        _TimeLabel.SendMessage("HeardTip", SendMessageOptions.DontRequireReceiver);
        Destroy(this.gameObject);
    }
}
