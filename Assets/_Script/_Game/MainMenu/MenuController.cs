using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
   // public GameObject _Mobclick;
    /// <summary>
    /// 3D root下的主面板
    /// </summary>
    public GameObject _3DPanelMain;
    /// <summary>
    /// 震动按钮的贴图
    /// </summary>
    public GameObject _BtnVibrateTexture;

    public GameObject _BtnMusicTexture;

    public GameObject _BtnIntroduceTexture;
    /// <summary>
    /// 所有按钮的贴图
    /// </summary>
    public GameObject[] _BtnTextures;

    /// <summary>
    /// 是否可以触摸屏幕
    /// </summary>
    private bool _CanTouch;

    /// <summary>
    /// 
    /// </summary>
    public GameObject _LoadingScript;

    public int _TestGear;

    private GameObject _BgSound;

    private bool _BtnPress = false;

    private string _GuideOff = "off";
    //private bool _Guide = false;


    void Awake()
    {

        if (string.IsNullOrEmpty(PlayerPrefs.GetString("Init")))
        {
            for (int i = 0; i < 5; i++)
            {
                PlayerPrefs.SetString(GlobalManager.GetInstants()._Leaderboard[i], (5 - i) * 40
                    + ",Top" + (i + 1));

            }
            PlayerPrefs.SetString("Init", "1");
            PlayerPrefs.Save();
        }
    }
    void Start()
    {
        if (PlayerPrefs.GetString(Consts.Introduce, "0") == "0")
        {
            _GuideOff = "On";
            PlayerPrefs.SetString(Consts.Introduce, "0");
        }
        else {
            _GuideOff = "Off";
            PlayerPrefs.SetString(Consts.Introduce, "1");
        
        }
        

        //_Mobclick.SendMessage("Start");
        _BgSound = GameObject.Find("BgSound");
       // MusicInit
        if (PlayerPrefs.GetInt(Consts.Sound.BACKGROUND_SOUND_KEY) == Consts.Sound.SOUND_ON)
        {
            GlobalManager.GetInstants()._CanMusic = true;
            Debug.Log("MusicOn");
            _BtnMusicTexture.GetComponent<UISlicedSprite>().spriteName = "MusicOn";
            AudioListener.volume = 1f;
        }
        else if (PlayerPrefs.GetInt(Consts.Sound.BACKGROUND_SOUND_KEY) == Consts.Sound.SOUND_OFF)
        {
            GlobalManager.GetInstants()._CanMusic = false;
            _BtnMusicTexture.GetComponent<UISlicedSprite>().spriteName = "MusicOff";
            AudioListener.volume = 0f;
            _BtnMusicTexture.GetComponent<UISlicedSprite>().enabled = true;
        }

        //introduce init
        if(PlayerPrefs.GetString(Consts.Introduce)== "1")
        {     
            GlobalManager.GetInstants()._ShowIntroduce = false;
            _BtnIntroduceTexture.GetComponent<UISlicedSprite>().spriteName = "CloseGuide";
        }
        else
        {
            GlobalManager.GetInstants()._ShowIntroduce = true;
            _BtnIntroduceTexture.GetComponent<UISlicedSprite>().spriteName = "Guide";       
        }
    }

    void Update()
    {
        //返回按钮的点击事件
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
               NativeDialogs.Instance.ShowMessageBox("提示", "真的要退出吗?", new string[] { "退出", "取消" }, (string button) => { QuitHint(button); });
            }

        }
        //根据PlayMaker判定何时可以触摸按钮
        _CanTouch = _3DPanelMain.gameObject.transform.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmBool("CanTouch").Value;
    }

    /// <summary>
    /// 跳转About场景的方法
    /// </summary>
    IEnumerator ToAboutScene()
    {
        //如果可以触摸则跳转到About场景
        if (_CanTouch == true && _BtnPress == false)
        {
            _BtnPress = true;
            DontDestroyOnLoad(_BgSound);
            //Application.LoadLevel("About");
            _LoadingScript.GetComponent<Loading>().LoadScene();
            yield return new WaitForSeconds(1f);

            AsyncOperation async = Application.LoadLevelAsync("About");
            yield return async;
            _BtnPress = false;
        }
    }
    ///<summary>
    /// 跳转到排行榜
    /// </summary>
    IEnumerator ToLeaderboardScene() {
        if (_CanTouch == true && _BtnPress == false) {
            _BtnPress = true;
            DontDestroyOnLoad(_BgSound);
            _LoadingScript.GetComponent<Loading>().LoadScene();
            yield return new WaitForSeconds(1f);
            AsyncOperation async = Application.LoadLevelAsync("LeaderBorad");
            yield return async;
            _BtnPress = false;
        }
        
    }

    

    /// <summary>
    /// 跳转Game场景的方法
    /// </summary>
    IEnumerator ToGameScene()
    {
        //如果可以触摸则跳转到Game场景
        if (_CanTouch == true  && _BtnPress == false)
        {
            _BtnPress = true;
            if (PlayerPrefs.GetString(Consts.Introduce, "0") == "0")
            {
                DontDestroyOnLoad(_BgSound);
                _LoadingScript.GetComponent<Loading>().LoadScene();
                yield return new WaitForSeconds(1f);

                AsyncOperation async = Application.LoadLevelAsync("Introduce");
                yield return async;
                _BtnPress = false;
            }
            else
            {
                DestroyObject(_BgSound);

                _LoadingScript.GetComponent<Loading>().LoadScene();
                yield return new WaitForSeconds(1f);

                AsyncOperation async = Application.LoadLevelAsync("Game");
                yield return async;
                _BtnPress = false;
            }
            
        }
        
    }

    void ToQuit()
    {
        if (_CanTouch == true)
        {
            NativeDialogs.Instance.ShowMessageBox(
                "提示",
                "真的要退出吗?",
                new string[] { "退出", "取消" }, (string button) => { QuitHint(button); });
        }
    }


    void QuitHint(string code)
    {
        if (code.Equals("退出"))
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// 跳转Shop场景的方法
    /// </summary>
    IEnumerator ToShopScene()
    {
        //如果可以触摸则跳转到Shop场景
        if (_CanTouch == true && _BtnPress == false)
        {
            _BtnPress = true;
            DontDestroyOnLoad(_BgSound);
            _LoadingScript.GetComponent<Loading>().LoadScene();
            yield return new WaitForSeconds(1f);

            AsyncOperation async = Application.LoadLevelAsync("Shop");
            yield return async;
            _BtnPress = false;
        }
    }

  
    /// <summary>
    /// 音乐按钮开关的方法
    /// </summary>
    void MusicToggle(){
       
        //如果当前为音乐开则点击后为音乐关 反之反之
        GlobalManager.GetInstants()._CanMusic = GlobalManager.GetInstants()._CanMusic == true ? false : true;
        //如果音乐开则打开声音并更换贴图 反之反之
        this.MusicPic();
     }
    void MusicPic() {
        //if (PlayerPrefs.GetString(Consts.Sound.BACKGROUND_SOUND_KEY) == "1")
        if (GlobalManager.GetInstants()._CanMusic == true)
        {
            Debug.Log("MusicOn");
            PlayerPrefs.SetInt(Consts.Sound.BACKGROUND_SOUND_KEY,Consts.Sound.SOUND_ON);
         //   Consts.Sound.BACKGROUND_SOUND_KEY
            _BtnMusicTexture.GetComponent<UISlicedSprite>().spriteName = "MusicOn";
            AudioListener.volume = 1f;
        }
        else
        {
            _BtnMusicTexture.GetComponent<UISlicedSprite>().spriteName = "MusicOff";
            PlayerPrefs.SetInt(Consts.Sound.BACKGROUND_SOUND_KEY, Consts.Sound.SOUND_OFF);
            PlayerPrefs.Save();
            Debug.Log("MusicOff");
            AudioListener.volume = 0f;
            _BtnMusicTexture.GetComponent<UISlicedSprite>().enabled = true;
        }
    
    }


    void IntroduceToggle() {
        if (_CanTouch == true)
        {
            GlobalManager.GetInstants()._ShowIntroduce = !GlobalManager.GetInstants()._ShowIntroduce;
            if (GlobalManager.GetInstants()._ShowIntroduce == false)
            {
                _GuideOff = "Off";
                PlayerPrefs.SetString(Consts.Introduce, "1");
                _BtnIntroduceTexture.GetComponent<UISlicedSprite>().spriteName = "CloseGuide";
                Debug.Log(GlobalManager.GetInstants()._ShowIntroduce + "==========" + PlayerPrefs.GetString(Consts.Introduce, "0"));

            }
            else
            {
                _GuideOff = "On";
                PlayerPrefs.SetString(Consts.Introduce, "0");
                _BtnIntroduceTexture.GetComponent<UISlicedSprite>().spriteName = "Guide";
                Debug.Log(GlobalManager.GetInstants()._ShowIntroduce + "==========" + PlayerPrefs.GetString(Consts.Introduce, "0"));
            }
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// 震动按钮开关的方法
    /// </summary>
    void VibrateToggle()
    {
        //如果可以触摸屏幕
        if (_CanTouch == true)
        {
            //如果当前为震动则点击后为取消震动 反之反之
            GlobalManager.GetInstants()._CanVibrate = GlobalManager.GetInstants()._CanVibrate == true ? false : true;
            //如果震动则替换为震动的贴图
            if (GlobalManager.GetInstants()._CanVibrate == true)
            {
                Debug.Log("VibrateOn");
                _BtnVibrateTexture.GetComponent<UISlicedSprite>().spriteName = "VibrateOn";
            }
            //否则替换为不震动的贴图
            else
            {
                _BtnVibrateTexture.GetComponent<UISlicedSprite>().spriteName = "VibrateOff";
                Debug.Log("VibrateOff");
            }
        }
    }


    /*  
      void OnGUI()
      {

          if(GUI.Button(new Rect(0, 0, 100, 30)," Guide" + _GuideOff )){
              _Guide = !_Guide;
              if (_Guide == false)
              {
                  _GuideOff = "Off";
                  PlayerPrefs.SetString(Consts.Introduce, "1");
                  Debug.Log(_Guide + "==========" + PlayerPrefs.GetString(Consts.Introduce, "0"));

              }else{
                  _GuideOff = "On";
                  PlayerPrefs.SetString(Consts.Introduce, "0");                
                  Debug.Log(_Guide + "==========" + PlayerPrefs.GetString(Consts.Introduce, "0"));
              }
              PlayerPrefs.Save();
          }

        


           if (GUI.Button(new Rect(0, 0, 100, 30), "AddGear:" + _TestGear))
           {
               GlobalManager.GetInstants().UpdatePlayerGear(_TestGear);
               GlobalManager.GetInstants().SyncPlayerData();
           }

           if (GUI.Button(new Rect(0, 30, 100, 30), "ResetGear"))
           {
               GlobalManager.GetInstants().UpdatePlayerGear(-99999999);
               GlobalManager.GetInstants().UpdatePlayerGear(200);
               GlobalManager.GetInstants().SyncPlayerData();
           }
           if (GUI.Button(new Rect(0, 60, 100, 30), "91 Login Center"))
           {
               AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); ;
               AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"); ;
               //PlayerPrefs.DeleteAll();
               jo.Call("showSocial");
           }

           if (GUI.Button(new Rect(0, 90, 100, 30), " show local "))
           {
               AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); ;
               AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"); ;
               //PlayerPrefs.DeleteAll();
               jo.Call("showSocial");
           }
           if (GUI.Button(new Rect(0, 120, 100, 30), "Charge")) {
               AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); ;
               AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"); ;

               jo.Call("bean91Recharge");
           }
           if (GUI.Button(new Rect(0, 150, 100, 30), " Pay")) {

               AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); ;
               AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"); ;

               jo.Call("pay");
           }
      } */

}