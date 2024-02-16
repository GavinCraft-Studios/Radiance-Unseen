using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderer_Controller : MonoBehaviour
{
    public LineRenderer lr;
    public Transform[] points;

    void Awake()
    {
        lr.positionCount = 2;
    }

    void Update()
    {
        lr.SetPosition(0, points[0].position);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lr.SetPosition(1, worldPosition);
    }
}
