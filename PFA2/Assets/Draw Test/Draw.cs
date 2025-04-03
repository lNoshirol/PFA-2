using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Draw : MonoBehaviour
{
    [SerializeField] Camera Cam;
    [SerializeField] LineRenderer trailPrefab;

    [SerializeField] private LineRenderer currentTrail;
    [SerializeField] private List<Vector3> points = new List<Vector3>();

    [SerializeField] private float distanceBetweenPoint;
    private float currentDistance;

    void Start()
    {
        if (!Cam)
        {
            Cam = Camera.main;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateNewLine();
        }

        if (Input.GetMouseButton(0))
        {
            AddPoint();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            DeleteLines();
        }
    }

    private void CreateNewLine()
    {
        currentTrail = Instantiate(trailPrefab);
        currentTrail.transform.SetParent(transform, true);
        points.Clear();
    }

    private void UpdateLinePoints()
    {
        if (currentTrail != null && points.Count > 1)
        {
            currentTrail.positionCount = points.Count;
            currentTrail.SetPositions(points.ToArray());
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
                    points.Add(hit.point);
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

    public void DeleteLines()
    {
        if (transform.childCount != 0)
        {
            foreach (Transform R in transform)
            {
                Destroy(R.gameObject);
            }
        }
    }
}
