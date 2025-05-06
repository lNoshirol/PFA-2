using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using NaughtyAttributes.Test;

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
                Debug.DrawLine(points3D[i], points3D[i + 1], Color.red);  
            }

            Debug.DrawLine(points3D[points3D.Count - 1], points3D[0], Color.red);
        }
    }

    public void ClearRaycastLines()
    {
        points3D.Clear();  
    }
}
