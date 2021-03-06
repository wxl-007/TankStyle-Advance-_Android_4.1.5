﻿using UnityEngine;
using System.Collections;

public class ComboEffect : MonoBehaviour {
    /// <summary>
    /// combo是否出现；
    /// </summary>
    public bool _Appear;
	// Use this for initialization
	void Start () {
	
	}
    public void ComboAppear() {
        if (_Appear == true) {
            this.HeardTip();
            return;
        }
        iTween.ScaleTo(gameObject, iTween.Hash("x", 250.4216f, "y", 91.90279,"time",0.5f));
        _Appear = true;
    }

    public void ComboOver() {
        iTween.ScaleTo(gameObject, iTween.Hash("x", 0, "y", 0, "time", 0.5f));
        _Appear = false;
    }


	// Update is called once per frame
	void Update () {
	
	}

    public void HeardTip()
    {
        iTween.PunchScale(gameObject, new Vector3(50, 50, 1), 0.7f);
    }

    void DropDown()
    {
        DropDown(-96.32321f);
    }

    void DropDown(float y)
    {
        iTween.MoveTo(gameObject, iTween.Hash("y", y, "islocal", true, "time", 1));
    }

    void RiseUp(float y)
    {
        iTween.MoveTo(gameObject, iTween.Hash("y", y, "islocal", true, "time", 1));
    }
}
