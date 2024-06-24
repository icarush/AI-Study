using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class tempScript : MonoBehaviour
{
    public ImageSenderSocket so;
    // Start is called before the first frame update

    private void Start()
    {
        string t = "66";
        int r = int.Parse(t);
    }
    public void inputLenght(TMPro.TMP_InputField go)
    {
        string t = go.text.Trim();
        so.inputLen = int.Parse(t);
        //if (int.TryParse(t, out length))
        //{
        //    Debug.LogError($"Failed to parse input '{t}' as an integer.");
        //    return;
        //}
    }

}
