using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTimeScale : MonoBehaviour
{
    public GameObject myTimer;
    public void Start()
    {
        updTimeScale();
        //Adds a listener to the main slider and invokes a method when the value changes.
        GetComponent<InputField>().onValueChanged.AddListener(delegate { updTimeScale(); });
    }

    // Invoked when the value of the slider changes.
    public void updTimeScale()
    {
        InputField input = GetComponent<InputField>();
        input.text = input.text.Replace(',', '.');

        if (!string.IsNullOrEmpty(input.text) && input.text.Length > 1 && input.text.StartsWith("0") && !input.text.StartsWith("0."))
        {
            input.text = input.text.Substring(1);
        }

        float curTimeScale;
        if (float.TryParse(input.text, out curTimeScale))
        {
            myTimer.GetComponent<MyTimer>().timeScale = curTimeScale;
        }
        else if (string.IsNullOrEmpty(input.text))
        {
            input.text = "0";
        }
        else
        {
            input.text = myTimer.GetComponent<MyTimer>().timeScale.ToString();
        }
    }
}
