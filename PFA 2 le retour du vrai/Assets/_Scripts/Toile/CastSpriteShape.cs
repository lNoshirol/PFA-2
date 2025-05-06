using UnityEngine;
using UnityEngine.U2D;
using System.Collections.Generic;
using PDollarGestureRecognizer;
using System.IO;
using System;
using UnityEngine.InputSystem;

public class CastSpriteShape : MonoBehaviour
{
    [Header("Line")]
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] private float distanceBetweenPoint;
    private float currentDistance;
    [SerializeField] private List<Vector3> points = new();
    [SerializeField] float _drawOffset;
    private DrawData _drawData;

    public bool touchingScreen = false;

    public List<Gesture> trainingSet = new List<Gesture>();

    bool isDrawing;

    [Header("Jsp (on va dire debug tkt)")]
    public Camera Cam;
    public GameObject CubeCentroid;
    public GameObject CubeCentre;
    public GameObject CubeTest;
    public LayerMask IgnoreMeUwU;
    public Vector3 vecTest;
    public Vector3 vecTest2;

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
        if (touchingScreen)
        {
            AddPoint();
        }

        DebugRay();
    }

    public void OnTouchScreen(InputAction.CallbackContext callbackContext)
    {
        Debug.Log($"CastSpriteShape L92/ AAAAAAAAAAAAH {gameObject.transform.parent.gameObject.activeSelf}");

        if (callbackContext.started)
        {
            ToileMain.Instance.RaycastDraw.ClearRaycastLines();
            touchingScreen = true;
            isDrawing = true;
            points.Clear();
            lineRenderer.positionCount = 0;
            if (!ToileMain.Instance.gestureIsStarted && gameObject.transform.parent.gameObject.activeSelf)
                ToileMain.Instance.timerCo = StartCoroutine(ToileMain.Instance.ToileTimer());
        }

        if (callbackContext.canceled)
        {
            if (points.Count > 10)
            {
                isDrawing = false;

                List<Point> drawReady = Vec3ToPoints(RecenterAndRotate());
                
                GetSpellTargetPointFromCentroid(points);
                GetSpellTargetPointFromCenter(points);

                Gesture candidate = new Gesture(drawReady.ToArray());
                Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

                TryMakeAdaptativeCollider(GetDrawCenter(points), gestureResult);

                _drawData = new DrawData(points, GetDrawDim(points), gestureResult, GetSpellTargetPointFromCenter(points));

                Debug.Log(gestureResult.GestureClass + " " + gestureResult.Score);
            }

            touchingScreen = false;
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
        Ray Ray;

        if (Mouse.current != null)
        {
            Ray = Cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        }
        else if (Touchscreen.current != null)
        {
            Ray = Cam.ScreenPointToRay(Touchscreen.current.position.ReadValue());
        }
        else
        {
            Debug.Log("Dommage");
            Ray = new Ray();
        }

        RaycastHit hit;

        if (Physics.Raycast(Ray, out hit))
        {
            if (hit.collider.CompareTag("Writeable"))
            {
                if (points.Count == 0)
                {
                    points.Add(hit.point + new Vector3(0, _drawOffset, -_drawOffset));

                    UpdateLinePoints();
                    return;
                }
                else
                {
                    currentDistance = Vector3.Distance(points[points.Count - 1], hit.point);

                    if (currentDistance >= distanceBetweenPoint)
                    {
                        points.Add(hit.point + new Vector3(0, _drawOffset, -_drawOffset));

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

    public Vector2 GetDrawDim(List<Vector3> points)
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

        Debug.Log($"MinX : {minX}, MaxX : {maxX}, minY : {minY}, maxY : {maxY}");
        Debug.Log($"distance X : {maxX - minX}, distance Y : {maxY - minY}");

        float X = minX >= 0 & maxX >= 0 ? minX : maxX;

        return new(maxX - minX, maxY - minY);
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
                CubeCentroid.transform.position = hit.point;
            }
        }
    }

    public Vector3 GetSpellTargetPointFromCenter(List<Vector3> points)
    {
        Vector3 center = GetDrawCenter(points);

        Ray Ray = Cam.ScreenPointToRay(Cam.WorldToScreenPoint(center));
        RaycastHit hit;

        if (Physics.Raycast(Ray, out hit, 200f, ~IgnoreMeUwU))
        {
            Debug.Log(hit.collider.gameObject.name);

            if (hit.collider.CompareTag("Ground"))
            {
                Debug.Log($"Spell cast location from center : {hit.point}");
                CubeCentre.transform.position = hit.point;
                return hit.point;
            }

            Debug.LogError("No Ground Hit");
            return Vector3.zero;
        }

        Debug.LogError("No Object Hit");
        return Vector3.zero;
    }

    public void TryMakeAdaptativeCollider(Vector3 center, Result result)
    {
        GameObject collider = new();
        collider.transform.position = GetSpellTargetPointFromCenter(points);

        switch (result.GestureClass)
        {
            case "Circle":
                collider.AddComponent<SphereCollider>();
                SphereCollider sphereColliderComponent;
                collider.TryGetComponent(out sphereColliderComponent);
                sphereColliderComponent.isTrigger = true;

                Vector2 drawDim = GetDrawDim(points);

                sphereColliderComponent.radius = (drawDim.x >= drawDim.y ? drawDim.x : drawDim.y)*1.5f;
                break;
            case "Square":
                collider.AddComponent<BoxCollider>();
                BoxCollider boxColliderComponent;
                collider.TryGetComponent(out boxColliderComponent);

                Vector3 cameraForward = Cam.transform.forward;
                Vector3 cameraRight = Cam.transform.right;

                Vector3 toTarget = (collider.transform.position - Cam.transform.position).normalized;
                
                float signedAngle = Vector3.SignedAngle(cameraForward, toTarget, Vector3.up);
                float signedAngleUp = Vector3.SignedAngle(cameraForward, toTarget, -Vector3.right);
                
                collider.transform.rotation = Quaternion.Euler(new Vector3(signedAngleUp, 0, signedAngle));

                boxColliderComponent.isTrigger = true;

                Vector2 dim = GetDrawDim(points);

                Vector3 size = new(dim.x, Mathf.Abs(center.y), dim.y);

                Debug.Log($"Centre : {center}, Size : {size}");

                boxColliderComponent.size = size;


                break;
        }
    }

    public DrawData GetDrawData()
    {
        return _drawData;
    }

    public void DebugRay()
    {
        if (points.Count > 0)
        {
            Vector3 centroid = GetDrawCentroid(points);

            Ray ray = Cam.ScreenPointToRay(Cam.WorldToScreenPoint(centroid));
            Debug.DrawRay(centroid, vecTest, Color.red);
        }

        Debug.DrawRay(Cam.ScreenToWorldPoint(Vector3.zero), vecTest, Color.red);
    }
}
