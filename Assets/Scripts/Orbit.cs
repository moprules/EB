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

    List<Vector3> myPositions = new List<Vector3>();
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
        DrawCurrentTrajectory();
    }

    void MyMove(float seconds)
    {
        float orbitAngle = (startAngle * Mathf.PI / 180f + omega * seconds) % (2 * Mathf.PI);
        transform.position = MyPosition(orbitAngle);
    }

    void DrawCurrentTrajectory()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        if (myPositions.Count == 0 || !transform.position.Equals(myPositions[myPositions.Count - 1]))
        {
            myPositions.Add(transform.position);
            lineRenderer.positionCount = myPositions.Count;
            lineRenderer.SetPosition(myPositions.Count - 1, transform.position);
        }
    }

    public Vector3 MyPosition(float orbitAngle)
    {
        float inclinationAngle = (inclination * Mathf.PI / 180f) % (2 * Mathf.PI);
        float x1 = A * Mathf.Cos(orbitAngle);
        float y1 = B * Mathf.Sin(orbitAngle);
        // Учитываем наклонение орбиты
        float z1 = x1 * Mathf.Sin(inclinationAngle);
        x1 *= Mathf.Cos(inclinationAngle);

        return transform.parent.transform.position + new Vector3(x1, y1, z1);
    }

    private void OnValidate()
    {
        // myCenter.transform.position = center;
        MyMove(0);
    }

    private void OnDrawGizmosSelected()
    {
        // Показываем линию только при выборе объекта
        // Рисуем линию до цели
        Gizmos.color = Color.green;
        Vector3 before = MyPosition(0);
        for (int a = 1; a <= 360; a++)
        {
            Vector3 newPos = MyPosition(a);
            Gizmos.DrawLine(before, newPos);
            before = newPos;
        }
    }
}
