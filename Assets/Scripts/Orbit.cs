using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Orbit : MonoBehaviour
{
    [SerializeField] GameObject myTimer;
    // параметры орбиты
    public float aphelion = 1;
    public float perihelion = 1;
    public float T = 5;
    public float startTetaAngle = 0;
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

        // Учитываем начально смещение в эквивалентных секундах
        seconds += T * (180 + startTetaAngle / 360f);
        // Вычисляем наше положение относительно времени витка
        float curTetaAngle = (360f * (seconds % T) / T) % 360f;
        transform.position = MyPosition(curTetaAngle);
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

    public Vector3 MyPosition(float curTetaAngle)
    {
        curTetaAngle *= Mathf.PI / 180;
        float e = (aphelion - perihelion) / (aphelion + perihelion);
        float r = aphelion * (1 - e) / (1 - e * Mathf.Cos(curTetaAngle));
        float x1 = -aphelion + perihelion + r * Mathf.Cos(curTetaAngle);
        float y1 = r * Mathf.Sin(curTetaAngle);

        Vector3 resPos = transform.TransformDirection(new Vector3(x1, y1, 0));
        // Vector3 resPos = new Vector3(x1, y1, 0);

        // Если у объекта есть родителский объект
        if (transform.parent != null)
        {
            // то позиция строится относительно этого родителя
            // Инача относительно начала координат
            resPos += transform.parent.transform.position;
        }

        return resPos;
    }

    private void OnValidate()
    {
        MyMove(0);
    }

    private void OnDrawGizmosSelected()
    {
        if (!EditorApplication.isPlaying)
        {
            MyMove(0);
        }
        Gizmos.color = Color.green;
        Vector3 before = MyPosition(0);
        for (int a = 1; a <= 360; a++)
        {
            Vector3 newPos = MyPosition(a);
            Gizmos.DrawLine(before, newPos);
            before = newPos;
        }

        Vector3 centerPos = new Vector3(0, 0, 0);
        // Если у объекта есть родителский объект
        if (transform.parent != null)
        {
            // то позиция строится относительно этого родителя
            // Инача относительно начала координат
            centerPos += transform.parent.transform.position;
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(centerPos, 0.1f);
    }
}
