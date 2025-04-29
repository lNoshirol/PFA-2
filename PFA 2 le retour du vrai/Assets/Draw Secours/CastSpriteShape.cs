using UnityEngine;
using UnityEngine.U2D;
using System.Collections.Generic;
using PDollarGestureRecognizer;
using System.IO;

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

    public List<Gesture> trainingSet = new List<Gesture>();

    bool isDrawing;

    [Header("Jsp")]
    public Camera Cam;

    [Header("Debug")]
    public GameObject centroidVisu;
    [SerializeField] LineRenderer lineRendererRecenter;
    [SerializeField] LineRenderer lineRendererRotate;
    public List<Vector3> testTkt;

    void Start()
    {
        //Load pre-made gestures
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/LostColors/");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        //Load user custom gestures
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (string filePath in filePaths)
            trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));

        Debug.Log("CastSpriteShape.cs l45/ " + trainingSet.Count);
    }

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

            //LineToSpriteShape();

            List<Point> drawReady = Vec3ToPoints(RecenterAndRotate());

            Gesture candidate = new Gesture(drawReady.ToArray());
            Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

            Debug.Log(gestureResult.GestureClass + " " + gestureResult.Score);

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Resetpoint();
        }
    }

    public List<Point> Vec3ToPoints(List<Vector3> list)
    {
        List<Point> listPoint = new List<Point>();

        foreach (Vector3 point in list)
        {
            Point newPoint = new Point(point.x, point.y, 1);
            listPoint.Add(newPoint);
        }

        return listPoint;
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

    public Vector3 GetDrawCentroid(List<Vector3> points)
    {
        Vector3 sum = Vector3.zero;

        foreach (Vector3 point in points)
        {
            sum += point;
        }

        Vector3 centroid = sum / points.Count;

        return centroid;
    }

    public List<Vector3> RecenterAndRotate()
    {
        Vector3 centroid = GetDrawCentroid(points);

        List<Vector3> recenterDraw = new List<Vector3>();

        foreach (Vector3 point in points)
        {
            Vector3 newPoint = point - centroid;

            newPoint = Quaternion.Euler(-45, 0, 0) * newPoint;

            newPoint = Quaternion.Euler(0, 0, 180) * newPoint;

            recenterDraw.Add(newPoint);
        }

        return recenterDraw;
    }

    public void LineToSpriteShape()
    {
        Vector3 centroid = GetDrawCentroid(points);

        centroidVisu.transform.position = centroid;


        List<Vector3> recenterDraw = new List<Vector3>();

        foreach (Vector3 point in points)
        {
            Vector3 newPoint = Quaternion.Euler(-45, 0, 0) * (point - centroid);

            recenterDraw.Add(newPoint);
        }

        lineRendererRecenter.positionCount = recenterDraw.Count;
        lineRendererRecenter.SetPositions(recenterDraw.ToArray());

        List<Vector3> rotateDraw = new();

        foreach (Vector3 point in recenterDraw)
        {
            Vector3 newPoint = Quaternion.Euler(-45, 0, 0) * point;
            rotateDraw.Add(newPoint);
        }

        lineRendererRotate.positionCount = rotateDraw.Count;
        lineRendererRotate.SetPositions(rotateDraw.ToArray());

        List<Vector3> tkt = new();

        foreach (Vector3 point in recenterDraw)
        {
            Vector3 newPoint = point + centroid;
            tkt.Add(newPoint);
        }


        foreach (Vector3 point in tkt)
        {
            controller.spline.InsertPointAt(controller.spline.GetPointCount(), point /*+ Vector3.one/10*/);
        }
        
        /*for(int i = controller.spline.GetPointCount()-1; i >= 0; i--)
        {
            controller.spline.InsertPointAt(controller.spline.GetPointCount(), points[i] *//*- Vector3.one*//*);
        }*/
    }
}
