using UnityEngine;
using System.Collections;

public class TestSceneScript : MonoBehaviour {
	
	float _timeToShowProgress;
	bool _progressVisible;
	
	// Use this for initialization
	void Start() {
				
	}
	
	// Update is called once per frame
	void Update() {
		if (_progressVisible) {
			_timeToShowProgress -= Time.deltaTime;
			
			if (_timeToShowProgress < 0) {
				NativeDialogs.Instance.HideProgressDialog();
				_progressVisible = false;
			}
		}
	}
	
	void OnGUI() {
		float width = Screen.width/2;
		float height = Screen.width / 5;
		Rect drawRect = new Rect((Screen.width - width)/2, height, width, height);
		
		if (GUI.Button(drawRect, "Alert dialog") ) {
			NativeDialogs.Instance.ShowMessageBox("Dialog title", "Press button", new string[] {"Button1", "Button2"}, (string button) => {
				string message = "Pressed button is " + button;
				NativeDialogs.Instance.ShowMessageBox("Alert dialog", message, new string[]{"Ok"}, (string b) => {});
			});
		}
		
		drawRect.y += height;
		if (GUI.Button(drawRect, "Login/password") ) {
			NativeDialogs.Instance.ShowLoginPasswordMessageBox("Authorization", "", new string[] {"Ok", "Cancel"}, (string login, string password, string button) => {
				string message = string.Format("Login:{0}\nPassword:{1}\nButton pressed:{2}", login, password, button);
				NativeDialogs.Instance.ShowMessageBox("Login/password", message, new string[]{"Ok"}, (string b) => {});
			});
		}
		
		drawRect.y += height;
		if (GUI.Button(drawRect, "Prompt") ) {
			NativeDialogs.Instance.ShowPromptMessageBox("Prompt", "", new string[] {"Ok", "Cancel"}, (string prompt, string button) => {
				string message = string.Format("Value:{0}\nButton pressed:{1}", prompt, button);
				NativeDialogs.Instance.ShowMessageBox("Prompt", message, new string[]{"Ok"}, (string b) => {});
			});
		}
		
		drawRect.y += height;
		if (GUI.Button(drawRect, "Secure prompt") ) {
			NativeDialogs.Instance.ShowSecurePromptMessageBox("Secure prompt", "", new string[] {"Ok", "Cancel"}, (string prompt, string button) => {
				string message = string.Format("Value:{0}\nButton pressed:{1}", prompt, button);
				NativeDialogs.Instance.ShowMessageBox("Secure prompt", message, new string[]{"Ok"}, (string b) => {});
			});
		}
		
		drawRect.y += height;
		if (GUI.Button(drawRect, "Progress dialog")) {
			NativeDialogs.Instance.ShowProgressDialog("Progress title", "Progress message");
			_timeToShowProgress = 3;
			_progressVisible = true;
		}
	}
}
