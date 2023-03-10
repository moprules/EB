using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sputnik : MonoBehaviour
{
    public GameObject myTimer;
    // параметры орбиты
    public float A = 1;
    public float B = 1;
    public float C = 1;
    public float angle = 0;
    public float omega = 4 * Mathf.PI / 180 / 60f;
    // Start is called before the first frame update
    void Start()
    {
        MyMove(0);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float seconds = myTimer.GetComponent<MyTimer>().curTime;
        MyMove(seconds);
    }

    void MyMove(float seconds)
    {
        float curAngle = angle + omega * seconds;
        float x = A * Mathf.Sin(curAngle);
        float z = C * Mathf.Cos(curAngle);
        transform.position = new Vector3(x, 0, z);
    }
}
