using UnityEngine;
using System.Collections;

public class LeaderboardController : MonoBehaviour
{
    public Light _Light;
    public float _Duration;

    public GameObject _LoadingScript;

    private GameObject _BgSound;

    public UILabel[] _LabelRank;
    public UILabel[] _LabelName;
    

    void Start()
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
            this.InitLeaderboard();
        
        
        _BgSound = GameObject.Find("BgSound");
     
    }
    void Update()
    {
        float tPhi = Time.time / _Duration * 2 * Mathf.PI;
        float tAmplitude = Mathf.Cos(tPhi) * 0.5F + 0.5F;
        _Light.intensity = tAmplitude;
    }

    IEnumerator ToMainMenu()
    {

        Time.timeScale = 1;
        _LoadingScript.GetComponent<Loading>().LoadScene();
        yield return new WaitForSeconds(1f);
        Destroy(_BgSound);
        AsyncOperation async = Application.LoadLevelAsync("MainMenu");
        yield return async;
    }


    public void InitLeaderboard() {
        string[] tRank;
        for (int i = 0; i < 5; i++)
        {
            tRank = PlayerPrefs.GetString(GlobalManager.GetInstants()._Leaderboard[i]).Split(',');
           // Debug.Log(tRank[0] + "===1===" + tRank[1] + "===2===" );
            _LabelRank[i].text = tRank[0];
            _LabelName[i].text = tRank[1];
            
        }
    }
}

