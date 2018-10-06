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
    /// ��Чͼ
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
    /// �Ƿ��ű�������
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
    /// �Ƿ�����Ч
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
    /// �򿪱�������
    /// </summary>
    void Open_BG_Sound()
    {
        PlayerPrefs.SetInt(Consts.Sound.BACKGROUND_SOUND_KEY, Consts.Sound.SOUND_ON);
        GetComponent<AudioSource>().Play();
    }
    /// <summary>
    /// �رձ�������
    /// </summary>
    void Mute_BG_Sound()
    {
        PlayerPrefs.SetInt(Consts.Sound.BACKGROUND_SOUND_KEY, Consts.Sound.SOUND_OFF);
        GetComponent<AudioSource>().Pause();
        //��ͼ
    }
    /// <summary>
    /// ����Ч
    /// </summary>
    void Open_EF_Sound()
    {
        PlayerPrefs.SetInt(Consts.Sound.EFFECT_SOUND_KEY, Consts.Sound.SOUND_ON);
        NGUITools.soundVolume = 1f;
    }
    /// <summary>
    /// �ر���Ч
    /// </summary>
    void Mute_EF_Sound()
    {
        PlayerPrefs.SetInt(Consts.Sound.EFFECT_SOUND_KEY, Consts.Sound.SOUND_OFF);
        NGUITools.soundVolume = 0f;
    }

    void PlaySource(AudioSource ads)
    {
        Debug.Log("�ҽ�����");
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
