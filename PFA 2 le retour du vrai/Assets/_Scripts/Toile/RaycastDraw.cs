using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using NaughtyAttributes.Test;
using System.Drawing;

public class RaycastDraw : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask raycastLayerMask;  
    public List<Vector3> points2D = new List<Vector3>();  
    public List<Vector3> points3D = new List<Vector3>();  

    private Vector3[] vertices = new Vector3[4];
    //private int triangles = new int[6];

    private GameObject meshObject;
    private Mesh mesh;

    [SerializeField] private CastSpriteShape _pen;
    [SerializeField] private MeshFilter _filter;

    private void Start()
    {
        mesh = new Mesh();
        mesh.name = "Custom mesh";

        meshObject = new GameObject("Mesh object", typeof(MeshRenderer), typeof(MeshFilter));

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
        DebugRaycastLines();
    }

    Vector3 ConvertToWorldSpaceWithRaycast(Vector3 screenPoint)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPoint);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastLayerMask))
        {
            return hit.point;
        }

        return ray.GetPoint(10f);  
    }

    void DebugRaycastLines()
    {
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
        Mesh mesh = new Mesh();
        mesh.name = "Procedural Shape";
        _filter.mesh = mesh;

        // number of vertices
        Vector3[] vertices = new Vector3[points3D.Count + 1];

        Vector2[] uvs = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        int[] triangles = new int[vertices.Length * 6];

        foreach(int tri in triangles)
        {
            if (tri % 3  == 0)
            {

            }
        }

    }

    private void OnDrawGizmos()
    {
        foreach (var point in points3D)
        {
            Gizmos.color = UnityEngine.Color.black;
            Gizmos.DrawSphere(point, 0.2f);
        }


        if (Application.isPlaying)
        {
            //print(_pen.GetDrawData().worldCenter);
            Gizmos.color = UnityEngine.Color.red;
            //Gizmos.DrawSphere(_pen.GetDrawData().worldCenter, 0.5f);
        }
    }

    public void ClearRaycastLines()
    {
        points3D.Clear();  
    }
}
