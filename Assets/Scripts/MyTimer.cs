using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyTimer : MonoBehaviour
{
    public float curTime = 0f;
    public float timeScale = 1;
    private IEnumerator StartTimer()
    {
        while (true)
        {
            curTime += Time.deltaTime * timeScale;
            UpdateTimeText();
            yield return null;
        }
    }

    private void Start()
    {
        StartCoroutine(StartTimer());
    }

    private void UpdateTimeText()
    {
        float minutes = Mathf.FloorToInt(curTime / 60);
        float seconds = Mathf.FloorToInt(curTime % 60);
        GetComponent<Text>().text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

}
