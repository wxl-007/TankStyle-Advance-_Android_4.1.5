using UnityEngine;
using System.Collections;

public class Shop_Light : MonoBehaviour {
    /// <summary>
    /// 商店的灯
    /// </summary>
    public Light[] _PointLight;
    /// <summary>
    /// sin值变化
    /// </summary>
    private float _Angle = 0;
    /// <summary>
    /// 查数
    /// </summary>
    private int _Count = 0;
    /// <summary>
    /// 亮度
    /// </summary>
    private float _LightIntisity;
    
    ///<summary>
    ///调节亮度增加频率
    /// <summary>
    public int _Frequency;
    /// <summary>
    /// 每次增加的角度，单位长。
    /// </summary>
    public float _Unit;
    ///<summary>
    ///增加的最大亮度
    /// </summary>
    public int _MaxIntisty;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame

	void Update () {
        _Count++;
        if (_Count % _Frequency == 0) { 
            _Angle += _Unit;
            //根据正弦值只在—1到1之间浮动，需要取一次绝对值。
             _LightIntisity = Mathf.Abs(Mathf.Sin(_Angle));
             float smoothTime = 10 * Time.smoothDeltaTime;
            for (int i = 0; i < 3; i++) {
                _PointLight[i].intensity = _LightIntisity * _MaxIntisty  * smoothTime;
            }
        }
	}
}
