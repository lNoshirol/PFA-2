using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class RaycastDraw : MonoBehaviour
{
    [Header("Settings")]
    public Camera mainCamera;
    public LayerMask raycastLayerMask;

    [Header("Draw Data")]
    public List<Vector2> points2D = new();
    public List<Vector3> points3D = new();

    private GameObject meshObject;
    private Mesh mesh;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

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
    }

    public void DebugRaycastLines()
    {
        if (points3D.Count < 2) return;

        for (int i = 0; i < points3D.Count - 1; i++)
        {
            Debug.DrawLine(points3D[i], points3D[i + 1], Color.red);
        }
    }

    public void ClearRaycastLines()
    {
        points2D.Clear();
        points3D.Clear();
    }
}
