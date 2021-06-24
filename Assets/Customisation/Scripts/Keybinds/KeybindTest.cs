using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeybindTest : MonoBehaviour
{
    public KeyBinds keybinds;

    public Text text;

    public void ChangeKeybinds(string keyBind)
    {
        
        StartCoroutine(listenForKeyChange(keyBind));
    }

    IEnumerator listenForKeyChange(string keyBind)
    {
        KeyCode keyCode;
        do
        {
            keyCode = keybinds.GetKeyPressed();
            yield return 0;
        } while (keyCode == KeyCode.None);

        keybinds.ChangeKeyBind(keyBind, keyCode);

        text.text = keyCode.ToString();
    }

}
