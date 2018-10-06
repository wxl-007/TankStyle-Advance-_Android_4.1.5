using UnityEngine;
using System.Collections;

public class TestBtn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 90, 100, 30), " MOGO ADS "))
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.tysci.unitygames.adsmogo.tankstyle.MogoUtil");
            //AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"); 
            jc.Call("ShowMogoAds");
            Debug.Log("***********************MOGO ADS SETUP!!!!!!!!!!!!!!!!!!!!!");
        }

        if (GUI.Button(new Rect(0, 20, 100, 30), " MOGO ADS CLOSE "))
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.tysci.unitygames.adsmogo.tankstyle.MogoUtil");
            //AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"); 
            jc.Call("CloseMogoAds");

            Debug.Log("***********************MOGO ADS CLOSE=====================++++++++++++");
        }

      
    }

    IEnumerator AdsTimeCtrl() {
        yield return new WaitForSeconds(3);
        //AndroidJavaClass jc = new AndroidJavaClass("com.tysci.unitygames.adsmogo.tankstyle.MogoUtil");
        //AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"); 
        //jc.Call("CloseMogoAds");
        //Debug.Log("+++++++++++++++++++++ MainActivity.closeAd ++++++++++++++++++++");
    
    }


}
