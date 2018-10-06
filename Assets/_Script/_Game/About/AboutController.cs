using UnityEngine;
using System.Collections;

public class AboutController : MonoBehaviour
{
    public Light _Light;
    public float _Duration;

    public GameObject _LoadingScript;

    private GameObject _BgSound;

    void Start()
    {
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

}
