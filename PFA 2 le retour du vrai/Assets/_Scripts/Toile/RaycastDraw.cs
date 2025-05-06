using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using NaughtyAttributes.Test;
using System.Drawing;

public class RaycastDraw : MonoBehaviour
{
    [Header("Settings")]
    public Camera mainCamera;
    public LayerMask raycastLayerMask;
    public List<Vector3> points2D = new List<Vector3>();
    public List<Vector3> points3D = new List<Vector3>();

    private Vector3[] vertices = new Vector3[4];
    //private int triangles = new int[6];

    [Header("Draw Data")]
    public List<Vector2> points2D = new();
    public List<Vector3> points3D = new();

    private GameObject meshObject;
    private Mesh mesh;

    [SerializeField] private CastSpriteShape _pen;
    [SerializeField] private MeshFilter _filter;

    //test
    Vector3[] pointsDebug;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
        mesh = new Mesh();
        mesh.name = "Procedural Shape";

        mesh = new Mesh { name = "Custom Mesh" };

        meshObject = new GameObject("MeshObject", typeof(MeshRenderer), typeof(MeshFilter));
        meshObject.GetComponent<MeshFilter>().mesh = mesh;
    }

    public void DrawRayCastInRealTime()
    {
        Vector2 screenPoint;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            screenPoint = Touchscreen.current.primaryTouch.position.ReadValue();
        meshObject.GetComponent<MeshFilter>().mesh = mesh;

        mesh.vertices = vertices;
        //mesh.triangles = triangles;
    }

    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 point2D = Mouse.current.position.ReadValue();
            points2D.Add(point2D);


            Vector3 point3D = ConvertToWorldSpaceWithRaycast(point2D);
            points3D.Add(point3D);
        }
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            screenPoint = Mouse.current.position.ReadValue();
        }
        else
        {
            return;
        }

        points2D.Add(screenPoint);

        Vector3 worldPoint = ConvertToWorldSpaceWithRaycast(screenPoint);
        if (worldPoint != Vector3.zero)
        {
            points3D.Add(worldPoint);
        }

        DebugRaycastLines();
    }

    private Vector3 ConvertToWorldSpaceWithRaycast(Vector2 screenPoint)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPoint);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, raycastLayerMask))
        {
            return hit.point;
        }

        return Vector3.zero;
        return ray.GetPoint(10f);
    }

    public void DebugRaycastLines()
    {
        if (points3D.Count < 2) return;

        for (int i = 0; i < points3D.Count - 1; i++)
        {
            Debug.DrawLine(points3D[i], points3D[i + 1], Color.red);
        if (points3D.Count > 0)
        {
            for (int i = 0; i < points3D.Count - 1; i++)
            {
                Debug.DrawLine(points3D[i], points3D[i + 1], UnityEngine.Color.red);
            }

            Debug.DrawLine(points3D[points3D.Count - 1], points3D[0], UnityEngine.Color.red);
        }
    }

    public void GenerateSpellShapeMesh()
    {
        mesh.Clear();
        _filter.mesh = mesh;

        // number of vertices
        Vector3[] vertices = new Vector3[points3D.Count+ 1];

        // Set vertices
        // The first is the center of the shape, the rest is all its other points
        vertices[0] = _pen.GetDrawData().worldCenter; 
        for (int i = 1; i < vertices.Length; i+=2)
        {
            vertices[i] = points3D[i - 1];
        }

        pointsDebug = vertices;
        mesh.vertices = vertices;
        mesh.RecalculateBounds();

        Vector2[] uvs = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        int[] triangles = new int[vertices.Length * 3];

        for (int i = 0; i < triangles.Length; i += 3)
        {
            // AKA le centre
            triangles[i] = 0;
            triangles[(i + 1) % triangles.Length] = (i + 1) % vertices.Length;
            triangles[(i + 2) % triangles.Length] = (i + 2) % vertices.Length;

            //print($"tri : {triangles[i]}, {triangles[(i + 1) % triangles.Length]}, {triangles[(i + 2) % triangles.Length]}");
        }

        print(triangles.Length);
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        //_filter.gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    private void OnDrawGizmos()
    {
        foreach (var point in points3D)
        {
            Gizmos.color = UnityEngine.Color.black;
            Gizmos.DrawSphere(point, 0.2f);
        }


        if (pointsDebug != null)
        {
            //print(_pen.GetDrawData().worldCenter);
            //Gizmos.color = UnityEngine.Color.red;
            //foreach(var point in pointsDebug)
            //{
            //    print("YO");
            //    Gizmos.DrawSphere(point, 0.2f);
            //}
        }
    }

    public void ClearRaycastLines()
    {
        points2D.Clear();
        points3D.Clear();
    }
}
