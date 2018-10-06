NatiiveDialogs is iOS/Android plugin that can display native Alert dialogs:
- standart alert dialog with caption, message and buttons
- login/password dialog
- prompt dialog, so you can get some data from user
- secure prompt dialog, user input is masked by asterisk character
- progress dialog with spinning progress indicator

To start working with native dialogs, put NativeDialogs prefab to the scene. Then you can access plugin functionality
using NativeDialogs.Instance object.

There are several methods:

/**
 * Show alert dialog.
 * caption - alert title
 * message - alert message
 * buttons - list of buttons
 * 
 * string parameter of onClickAction will be set to text of button clicked
 */
public void ShowMessageBox(string caption, string message, string[] buttons, Action<string> onClickAction);


/**
 * Show login/password dialog.
 * caption - dialog title
 * message - dialog message
 * buttons - list of buttons
 * 
 * first string parameter of onClickAction will be set to login entered
 * second string parameter of onClickAction will be set to password entered
 * third string parameter of onClickAction will be set to text of button clicked
 */
public void ShowLoginPasswordMessageBox(string caption, string message, string[] buttons, Action<string, string, string> onClickAction);


/**
 * Show prompt dialog.
 * caption - dialog title
 * message - dialog message
 * buttons - list of buttons
 * 
 * first string parameter of onClickAction will be set to data entered
 * second string parameter of onClickAction will be set to text of button clicked
 */
public void ShowPromptMessageBox(string caption, string message, string[] buttons, Action<string, string> onClickAction);


/**
 * Show secure prompt dialog. User input is masked by asterisk character
 * caption - dialog title
 * message - dialog message
 * buttons - list of buttons
 * 
 * first string parameter of onClickAction will be set to data entered
 * second string parameter of onClickAction will be set to text of button clicked
 */
public void ShowSecurePromptMessageBox(string caption, string message, string[] buttons, Action<string, string> onClickAction);


/**
 * Show window with spinning progress indicator. It will dismiss previously shown progress window if needed.
 * caption - window title
 * message - window message
 */
public void ShowProgressDialog(string caption, string message);


/**
 * Hide window with spinning progress indicator
 */
public void HideProgressDialog();


You can fing example of usage of these methods in TestSceneScript.cs file.