using UnityEngine;
using System.Collections;

public class Introduce : MonoBehaviour {

    public GameObject _LoadingScript;
    private GameObject _BGSound;
    
    private bool _BtnPress = false;

    
    void Start() {
        _BGSound = GameObject.Find("BgSound"); ;
    }
    IEnumerator ToGameSence() {
        if (_BtnPress == false)
        {
            _BtnPress = true;
            DestroyObject(_BGSound);
            _LoadingScript.GetComponent<Loading>().LoadScene();
            yield return new WaitForSeconds(1f);
           // PlayerPrefs.SetString(Consts.Introduce, "1");
           // PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetString(Consts.Introduce, "1"));
            AsyncOperation async = Application.LoadLevelAsync("Game");
            yield return async;
            _BtnPress = false;
        }
    }
}
