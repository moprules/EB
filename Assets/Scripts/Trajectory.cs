using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    LineRenderer lineRenderer;
    List<Vector3> myPositions = new List<Vector3>();
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    void FixedUpdate()
    {
        DrawCurrentTrajectory();
    }

    void DrawCurrentTrajectory()
    {
        Vector3 curPos = transform.parent.transform.position;
        if (myPositions.Count == 0 || !curPos.Equals(myPositions[myPositions.Count - 1]))
        {
            myPositions.Add(curPos);
            lineRenderer.positionCount = myPositions.Count;
            lineRenderer.SetPosition(myPositions.Count - 1, curPos);
        }
    }
}
