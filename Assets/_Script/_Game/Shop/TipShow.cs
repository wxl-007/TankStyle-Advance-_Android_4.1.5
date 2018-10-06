using UnityEngine;
using System.Collections;

public class TipShow : MonoBehaviour {

    
	// Use this for initialization
	void Start () {

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
