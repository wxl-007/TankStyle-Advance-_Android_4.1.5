using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {


    public GameObject _LoadTopCube;
    public GameObject _LoadBotCube;

    private bool _Closed = true;
   // private bool _BeInvoked = false;

    public void LoadScene()
    {
        if (_Closed == false )
        {
            iTween.MoveTo(_LoadTopCube, iTween.Hash("x", 0, "y", -340, "z", 0, "time", 0.6f, "easetype", "easeInOutCubic", "islocal", true));
            iTween.MoveTo(_LoadBotCube, iTween.Hash("x", 0, "y", 180, "z", 0, "time", 0.6f, "easetype", "easeInOutCubic", "islocal", true));
            Debug.Log("Close"+ _Closed);
           
            _Closed = true;
        }
        else
        {
            iTween.MoveTo(_LoadTopCube, iTween.Hash("x", 0, "y", -900, "z", 0, "time", 0.6f, "easetype", "easeOutCirc", "islocal", true));
            iTween.MoveTo(_LoadBotCube, iTween.Hash("x", 0, "y", 900, "z", 0, "time", 0.6f, "easetype", "easeOutCirc", "islocal", true));
            //Debug.Log("Close" + _Closed);
            
            _Closed = false;
            
        }
    }

	// Use this for initialization
	void Start () {
        Invoke("LoadScene",0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
