using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SaveChat : MonoBehaviour {

    string str;
    public InputField inputField;
    public Text text;

   public SaveChat()
    {
        str = inputField.text;
        text.text = str;
        inputField.text = "";

    }
}
