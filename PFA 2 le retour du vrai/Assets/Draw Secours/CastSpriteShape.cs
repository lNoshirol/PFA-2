using NUnit.Framework;
using UnityEngine;
using UnityEngine.U2D;
using System.Collections.Generic;

public class CastSpriteShape : MonoBehaviour
{
    [Header("Sprite Shape")]
    [SerializeField] SpriteShapeController controller;
    [SerializeField] PolygonCollider2D polygonCollider;
    [SerializeField] float splineWidth;

    [Header("Line")]
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] private float distanceBetweenPoint;
    private float currentDistance;
    [SerializeField] private List<Vector3> points = new();
    bool isDrawing;

    [Header("Jsp")]
    public Camera Cam;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            points.Clear();
            lineRenderer.positionCount = 0;
        }

        if (Input.GetMouseButton(0))
        {
            AddPoint();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
            LineToSpriteShape();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Resetpoint();
        }
    }

    private void UpdateLinePoints()
    {
        if (lineRenderer != null && points.Count > 1)
        {
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }
    }

    private void AddPoint()
    {
        var Ray = Cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(Ray, out hit))
        {
            if (hit.collider.CompareTag("Writeable"))
            {
                if (points.Count == 0)
                {
                    // points.Add(new Vector3(hit.point.x, 0f, hit.point.z));
                    points.Add(hit.point);
                    //points.Add(hit.point);
                    UpdateLinePoints();
                    return;
                }
                else
                {
                    currentDistance = Vector3.Distance(points[points.Count - 1], hit.point);

                    if (currentDistance >= distanceBetweenPoint)
                    {
                        // points.Add(new Vector3(hit.point.x, 0f, hit.point.z));
                        points.Add(hit.point);
                        UpdateLinePoints();
                        return;
                    }
                }
            }
        }
    }

    public void Resetpoint()
    {
        lineRenderer.positionCount = 0;
    }

    public void LineToSpriteShape()
    {
        List<Vector3> worldPosToScreen = new List<Vector3>();
        
        controller.spline.Clear();

        foreach (Vector3 point in points)
        {
            controller.spline.InsertPointAt(controller.spline.GetPointCount(), point /*+ Vector3.one/10*/);
        }
        /*for(int i = controller.spline.GetPointCount()-1; i >= 0; i--)
        {
            controller.spline.InsertPointAt(controller.spline.GetPointCount(), points[i] *//*- Vector3.one*//*);
        }*/
    }
}
