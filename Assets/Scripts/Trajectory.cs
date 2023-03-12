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
        if (myPositions.Count == 0 || !transform.position.Equals(myPositions[myPositions.Count - 1]))
        {
            myPositions.Add(transform.parent.transform.position);
        }

        lineRenderer.positionCount = myPositions.Count;
        lineRenderer.SetPositions(myPositions.ToArray());
    }


}
