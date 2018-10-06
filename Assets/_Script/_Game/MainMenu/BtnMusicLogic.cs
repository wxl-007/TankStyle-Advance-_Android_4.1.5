﻿using UnityEngine;
using System.Collections;

public class BtnMusicLogic : MonoBehaviour
{
    public GameObject _BtnMusicTexture;

    void Start()
    {
        if (GlobalManager.GetInstants()._CanMusic == true)
        {
            _BtnMusicTexture.GetComponent<UISlicedSprite>().spriteName = "MusicOn";  
        }
        else
        {
            _BtnMusicTexture.GetComponent<UISlicedSprite>().spriteName = "MusicOff";
        }
    }
}