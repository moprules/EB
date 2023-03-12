using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] GameObject myTimer;
    // параметры орбиты
    public float A = 1;
    public float B = 1;
    public float inclination = 0;
    public float startAngle = 0;
    public float omega = 4 * Mathf.PI / 180 / 60f;
    // Start is called before the first frame update
    void Start()
    {
        MyMove(0);
    }
    // Update is called once per frame
    void Update()
    {
        float seconds = myTimer.GetComponent<MyTimer>().curTime;
        MyMove(seconds);
    }

    void MyMove(float seconds)
    {
        float orbitAngle = (startAngle * Mathf.PI / 180f + omega * seconds) % (2 * Mathf.PI);
        float inclinationAngle = (inclination * Mathf.PI / 180f) % (2 * Mathf.PI);
        float x1 = A * Mathf.Cos(orbitAngle);
        float y1 = B * Mathf.Sin(orbitAngle);
        // Учитываем наклонение орбиты
        float z1 = x1 * Mathf.Sin(inclinationAngle);
        x1 *= Mathf.Cos(inclinationAngle);

        transform.position = transform.parent.transform.position + new Vector3(x1, y1, z1);
    }

    private void OnValidate()
    {
        // myCenter.transform.position = center;
        MyMove(0);
    }
}
