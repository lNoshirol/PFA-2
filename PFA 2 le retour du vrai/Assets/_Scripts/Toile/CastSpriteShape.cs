using UnityEngine;
using UnityEngine.U2D;
using System.Collections.Generic;
using PDollarGestureRecognizer;
using System.IO;

public class CastSpriteShape : MonoBehaviour
{
    [Header("Line")]
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] private float distanceBetweenPoint;
    private float currentDistance;
    [SerializeField] private List<Vector3> points = new();

    public List<Gesture> trainingSet = new List<Gesture>();

    bool isDrawing;

    [Header("Jsp")]
    public Camera Cam;
    public GameObject UnCaca;
    public GameObject UnCaca2;
    public LayerMask IgnoreMeUwU;
    public Vector3 vecTest;


    

    void Start()
    {
        //Load pre-made gestures
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/LostColors/");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        Cam = Camera.main;

        Debug.Log("CastSpriteShape.cs l45/ " + trainingSet.Count);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ToileMain.Instance.TriggerToile._isActive)
        {
            isDrawing = true;
            points.Clear();
            lineRenderer.positionCount = 0;
            if(!ToileMain.Instance.gestureIsStarted)
            ToileMain.Instance.timerCo = StartCoroutine(ToileMain.Instance.ToileTimer());
        }

        if (Input.GetMouseButton(0))
        {
            AddPoint();
        }

        if (Input.GetMouseButtonUp(0) && lineRenderer.positionCount > 10)
        {
            isDrawing = false;

            List<Point> drawReady = Vec3ToPoints(RecenterAndRotate());

            GetSpellTargetPointFromCentroid(points);
            GetSpellTargetPointFromCenter(points);

            Gesture candidate = new Gesture(drawReady.ToArray());
            Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

            Debug.Log(gestureResult.GestureClass + " " + gestureResult.Score);

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Resetpoint();
        }

        DebugRay();
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
        Ray Ray = Cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(Ray, out hit))
        {
            if (hit.collider.CompareTag("Writeable"))
            {
                if (points.Count == 0)
                {
                    points.Add(hit.point + new Vector3(0, 0.5f, -0.5f));

                    UpdateLinePoints();
                    return;
                }
                else
                {
                    currentDistance = Vector3.Distance(points[points.Count - 1], hit.point);

                    if (currentDistance >= distanceBetweenPoint)
                    {
                        points.Add(hit.point + new Vector3(0, 0.5f, -0.5f));

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

    public Vector3 GetDrawCenter(List<Vector3> points)
    {
        float minX = points[0].x;
        float maxX = points[0].x;

        float minY = points[0].y;
        float maxY = points[0].y;

        float minZ = points[0].z;
        float maxZ = points[0].z;

        foreach (Vector3 point in points)
        {
            minX = point.x < minX ? point.x : minX;
            maxX = point.x > maxX ? point.x : maxX;

            minY = point.y < minY ? point.y : minY;
            maxY = point.y > maxY ? point.y : maxY;

            minZ = point.z < minZ ? point.z : minZ;
            maxZ = point.z > maxZ ? point.z : maxZ;
        }

        float x = (maxX + minX)/2;
        float y = (maxY + minY)/2;
        float z = (maxZ + minZ)/2;

        return new Vector3(x, y, z);
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

    public void GetSpellTargetPointFromCentroid(List<Vector3> points)
    {
        Vector3 centroid = GetDrawCentroid(points);

        Ray Ray = Cam.ScreenPointToRay(Cam.WorldToScreenPoint(centroid));
        RaycastHit hit;

        if (Physics.Raycast(Ray, out hit, 20000f, ~IgnoreMeUwU) )
        {
            Debug.Log(hit.collider.gameObject.name);

            if (hit.collider.CompareTag("Ground"))
            {
                Debug.Log($"Spell cast location from centroid : {hit.point}");
                UnCaca.transform.position = hit.point;
            }
        }
    }

    public void GetSpellTargetPointFromCenter(List<Vector3> points)
    {
        Vector3 center = GetDrawCenter(points);

        Ray Ray = Cam.ScreenPointToRay(Cam.WorldToScreenPoint(center));
        RaycastHit hit;

        if (Physics.Raycast(Ray, out hit, 20000f, ~IgnoreMeUwU))
        {
            Debug.Log(hit.collider.gameObject.name);

            if (hit.collider.CompareTag("Ground"))
            {
                Debug.Log($"Spell cast location from center : {hit.point}");
                UnCaca2.transform.position = hit.point;
            }
        }
    }

    public void DebugRay()
    {
        if (points.Count > 0)
        {
            Vector3 centroid = GetDrawCentroid(points);

            Ray ray = Cam.ScreenPointToRay(Cam.WorldToScreenPoint(centroid));
            Debug.DrawRay(centroid, vecTest, Color.red);
        }
    }
}
