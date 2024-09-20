using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;

public class Keyboard : MonoBehaviour
{
    public Button sendButton;
    public TMP_InputField inputField;
    public GameObject PopUp;
    public GameObject PopUpError;
    public int charLimit = 6;


    public string GetInput(){
        return inputField.text;
    }

    public void BoardKey(string key)
    {
        if (inputField.text.Length < charLimit) 
        {
            inputField.text += key;
        }
        else
        {
            return;
        }
    }

    public void BackSpace()
    {
        if (inputField.text.Length == 0)
        {
            return;
        }
        inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
    }


    private void OnEnable()
    {
        inputField.text = "";
        PopUp.SetActive(false);
        PopUpError.SetActive(false);
    }
    
}
