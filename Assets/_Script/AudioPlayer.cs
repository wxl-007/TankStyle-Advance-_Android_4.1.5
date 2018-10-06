using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour
{
/*
    public AudioSource _AudioFire;
    public AudioSource _AudioBomb;

    public AudioClip _Fire;

    public AudioClip _Bomb;
*/
    /// <summary>
    /// 音效图
    /// </summary>
    //public GameObject _BtnMusicTexture;


    void Start()
    {
        if(Is_BG_Sound())
        {
            Open_BG_Sound();
        }
        else
        {
            Mute_BG_Sound();
        }

        if (Is_EF_Sound())
        {
            Open_EF_Sound();
        }
        else
        {
            Mute_EF_Sound();
        }
    }

    /// <summary>
    /// 是否开着背景音乐
    /// </summary>
    /// <returns></returns>
    public bool Is_BG_Sound()
    {
        if (PlayerPrefs.GetInt(Consts.Sound.BACKGROUND_SOUND_KEY, Consts.Sound.SOUND_ON) == Consts.Sound.SOUND_ON)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 是否开着音效
    /// </summary>
    /// <returns></returns>
    public bool Is_EF_Sound()
    {
        if (PlayerPrefs.GetInt(Consts.Sound.EFFECT_SOUND_KEY, Consts.Sound.SOUND_ON) == Consts.Sound.SOUND_ON)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

   public  void Toggle_BG_Sound()
    {
        if(Is_BG_Sound())
        {
            Mute_BG_Sound();
        }
        else
        {
            Open_BG_Sound();
        }
    }

    void Toggle_EF_Sound()
    {
        if (Is_EF_Sound())
        {
            Mute_EF_Sound();
        }
        else
        {
            Open_EF_Sound();
        }
    }

    /// <summary>
    /// 打开背景音乐
    /// </summary>
    void Open_BG_Sound()
    {
        PlayerPrefs.SetInt(Consts.Sound.BACKGROUND_SOUND_KEY, Consts.Sound.SOUND_ON);
        GetComponent<AudioSource>().Play();
    }
    /// <summary>
    /// 关闭背景音乐
    /// </summary>
    void Mute_BG_Sound()
    {
        PlayerPrefs.SetInt(Consts.Sound.BACKGROUND_SOUND_KEY, Consts.Sound.SOUND_OFF);
        GetComponent<AudioSource>().Pause();
        //切图
    }
    /// <summary>
    /// 打开音效
    /// </summary>
    void Open_EF_Sound()
    {
        PlayerPrefs.SetInt(Consts.Sound.EFFECT_SOUND_KEY, Consts.Sound.SOUND_ON);
        NGUITools.soundVolume = 1f;
    }
    /// <summary>
    /// 关闭音效
    /// </summary>
    void Mute_EF_Sound()
    {
        PlayerPrefs.SetInt(Consts.Sound.EFFECT_SOUND_KEY, Consts.Sound.SOUND_OFF);
        NGUITools.soundVolume = 0f;
    }

    void PlaySource(AudioSource ads)
    {
        Debug.Log("我进来了");
        if (ads)
        {
            Debug.Log("yes");
        }
        else
        {
            Debug.Log("no");
        }
        AudioSource tAds = ads;
        tAds.Play();
    }
}
