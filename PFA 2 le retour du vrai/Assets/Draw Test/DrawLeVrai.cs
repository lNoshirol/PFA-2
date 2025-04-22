using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawLeVrai : MonoBehaviour
{
    [SerializeField] Camera Cam;
    [SerializeField] LineRenderer trailPrefab;

    [SerializeField] private LineRenderer currentTrail;
    [SerializeField] private List<Vector3> points = new List<Vector3>();

    private Dictionary<string, List<Vector3>> templates = new Dictionary<string, List<Vector3>>();

    [SerializeField] private float distanceBetweenPoint;
    private float currentDistance;

    public TMP_Text resultText;
    public LineRenderer lineRenderer;
    public TextMeshProUGUI inputFieldText;

    private bool isDrawing = false;

    #region Base SHape

    List<Vector3> circlePoints = new List<Vector3>
    {
        new Vector3(1.000f, 0.000f, 0f),
        new Vector3(0.981f, 0.195f, 0f),
        new Vector3(0.924f, 0.383f, 0f),
        new Vector3(0.831f, 0.556f, 0f),
        new Vector3(0.707f, 0.707f, 0f),
        new Vector3(0.556f, 0.831f, 0f),
        new Vector3(0.383f, 0.924f, 0f),
        new Vector3(0.195f, 0.981f, 0f),
        new Vector3(0.000f, 1.000f, 0f),
        new Vector3(-0.195f, 0.981f, 0f),
        new Vector3(-0.383f, 0.924f, 0f),
        new Vector3(-0.556f, 0.831f, 0f),
        new Vector3(-0.707f, 0.707f, 0f),
        new Vector3(-0.831f, 0.556f, 0f),
        new Vector3(-0.924f, 0.383f, 0f),
        new Vector3(-0.981f, 0.195f, 0f),
        new Vector3(-1.000f, 0.000f, 0f),
        new Vector3(-0.981f, -0.195f, 0f),
        new Vector3(-0.924f, -0.383f, 0f),
        new Vector3(-0.831f, -0.556f, 0f),
        new Vector3(-0.707f, -0.707f, 0f),
        new Vector3(-0.556f, -0.831f, 0f),
        new Vector3(-0.383f, -0.924f, 0f),
        new Vector3(-0.195f, -0.981f, 0f),
        new Vector3(0.000f, -1.000f, 0f),
        new Vector3(0.195f, -0.981f, 0f),
        new Vector3(0.383f, -0.924f, 0f),
        new Vector3(0.556f, -0.831f, 0f),
        new Vector3(0.707f, -0.707f, 0f),
        new Vector3(0.831f, -0.556f, 0f),
        new Vector3(0.924f, -0.383f, 0f),
        new Vector3(0.981f, -0.195f, 0f),
    };
    List<Vector3> square = new List<Vector3>
    {
        new Vector3(-1f, -1f, 0f),
        new Vector3(-0.75f, -1f, 0f),
        new Vector3(-0.5f, -1f, 0f),
        new Vector3(-0.25f, -1f, 0f),
        new Vector3(0f, -1f, 0f),
        new Vector3(0.25f, -1f, 0f),
        new Vector3(0.5f, -1f, 0f),
        new Vector3(0.75f, -1f, 0f),

        new Vector3(1f, -1f, 0f),
        new Vector3(1f, -0.75f, 0f),
        new Vector3(1f, -0.5f, 0f),
        new Vector3(1f, -0.25f, 0f),
        new Vector3(1f, 0f, 0f),
        new Vector3(1f, 0.25f, 0f),
        new Vector3(1f, 0.5f, 0f),
        new Vector3(1f, 0.75f, 0f),

        new Vector3(1f, 1f, 0f),
        new Vector3(0.75f, 1f, 0f),
        new Vector3(0.5f, 1f, 0f),
        new Vector3(0.25f, 1f, 0f),
        new Vector3(0f, 1f, 0f),
        new Vector3(-0.25f, 1f, 0f),
        new Vector3(-0.5f, 1f, 0f),
        new Vector3(-0.75f, 1f, 0f),

        new Vector3(-1f, 1f, 0f),
        new Vector3(-1f, 0.75f, 0f),
        new Vector3(-1f, 0.5f, 0f),
        new Vector3(-1f, 0.25f, 0f),
        new Vector3(-1f, 0f, 0f),
        new Vector3(-1f, -0.25f, 0f),
        new Vector3(-1f, -0.5f, 0f),
        new Vector3(-1f, -0.75f, 0f),
    };
    List<Vector3> triangle = new List<Vector3>
    {
        // Base gauche vers droite
        new Vector3(-0.866f, -0.5f, 0f),
        new Vector3(-0.673f, -0.5f, 0f),
        new Vector3(-0.480f, -0.5f, 0f),
        new Vector3(-0.287f, -0.5f, 0f),
        new Vector3(-0.094f, -0.5f, 0f),
        new Vector3(0.099f, -0.5f, 0f),
        new Vector3(0.292f, -0.5f, 0f),
        new Vector3(0.485f, -0.5f, 0f),
        new Vector3(0.678f, -0.5f, 0f),
        new Vector3(0.866f, -0.5f, 0f),

        // Vers le sommet
        new Vector3(0.722f, -0.295f, 0f),
        new Vector3(0.578f, -0.09f, 0f),
        new Vector3(0.433f, 0.115f, 0f),
        new Vector3(0.289f, 0.32f, 0f),
        new Vector3(0.144f, 0.525f, 0f),
        new Vector3(0.000f, 0.73f, 0f),
        new Vector3(-0.144f, 0.525f, 0f),
        new Vector3(-0.289f, 0.32f, 0f),
        new Vector3(-0.433f, 0.115f, 0f),
        new Vector3(-0.578f, -0.09f, 0f),
        new Vector3(-0.722f, -0.295f, 0f),

        // Fermeture base
        new Vector3(-0.866f, -0.5f, 0f), // Reprise
    };
    List<Vector3> ellipse = new List<Vector3>
    {
        new Vector3(1.000f, 0.000f, 0f),
        new Vector3(0.981f, 0.098f, 0f),
        new Vector3(0.924f, 0.191f, 0f),
        new Vector3(0.831f, 0.278f, 0f),
        new Vector3(0.707f, 0.354f, 0f),
        new Vector3(0.556f, 0.415f, 0f),
        new Vector3(0.383f, 0.462f, 0f),
        new Vector3(0.195f, 0.490f, 0f),
        new Vector3(0.000f, 0.500f, 0f),
        new Vector3(-0.195f, 0.490f, 0f),
        new Vector3(-0.383f, 0.462f, 0f),
        new Vector3(-0.556f, 0.415f, 0f),
        new Vector3(-0.707f, 0.354f, 0f),
        new Vector3(-0.831f, 0.278f, 0f),
        new Vector3(-0.924f, 0.191f, 0f),
        new Vector3(-0.981f, 0.098f, 0f),
        new Vector3(-1.000f, 0.000f, 0f),
        new Vector3(-0.981f, -0.098f, 0f),
        new Vector3(-0.924f, -0.191f, 0f),
        new Vector3(-0.831f, -0.278f, 0f),
        new Vector3(-0.707f, -0.354f, 0f),
        new Vector3(-0.556f, -0.415f, 0f),
        new Vector3(-0.383f, -0.462f, 0f),
        new Vector3(-0.195f, -0.490f, 0f),
        new Vector3(0.000f, -0.500f, 0f),
        new Vector3(0.195f, -0.490f, 0f),
        new Vector3(0.383f, -0.462f, 0f),
        new Vector3(0.556f, -0.415f, 0f),
        new Vector3(0.707f, -0.354f, 0f),
        new Vector3(0.831f, -0.278f, 0f),
        new Vector3(0.924f, -0.191f, 0f),
        new Vector3(0.981f, -0.098f, 0f),
    };
    List<Vector3> check = new List<Vector3>
    {
        new Vector3(-0.8f, 0.5f, 0f),
        new Vector3(-0.7f, 0.3f, 0f),
        new Vector3(-0.6f, 0.1f, 0f),
        new Vector3(-0.5f, -0.1f, 0f),
        new Vector3(-0.4f, -0.3f, 0f),
        new Vector3(-0.3f, -0.45f, 0f),
        new Vector3(-0.2f, -0.55f, 0f),
        new Vector3(-0.1f, -0.6f, 0f),

        new Vector3(0f, -0.6f, 0f),
        new Vector3(0.1f, -0.55f, 0f),
        new Vector3(0.2f, -0.45f, 0f),
        new Vector3(0.3f, -0.3f, 0f),
        new Vector3(0.4f, -0.1f, 0f),
        new Vector3(0.5f, 0.1f, 0f),
        new Vector3(0.6f, 0.3f, 0f),
        new Vector3(0.7f, 0.45f, 0f),

        new Vector3(0.75f, 0.5f, 0f),
        new Vector3(0.72f, 0.48f, 0f),
        new Vector3(0.69f, 0.46f, 0f),
        new Vector3(0.66f, 0.44f, 0f),
        new Vector3(0.63f, 0.42f, 0f),
        new Vector3(0.6f, 0.4f, 0f),
        new Vector3(0.57f, 0.38f, 0f),
        new Vector3(0.54f, 0.36f, 0f),
        new Vector3(0.51f, 0.34f, 0f),
        new Vector3(0.48f, 0.32f, 0f),
        new Vector3(0.45f, 0.3f, 0f),
        new Vector3(0.42f, 0.28f, 0f),
        new Vector3(0.39f, 0.26f, 0f),
        new Vector3(0.36f, 0.24f, 0f),
        new Vector3(0.33f, 0.22f, 0f),
        new Vector3(0.3f, 0.2f, 0f),
    };

    #endregion


    void Start()
    {
        if (!Cam)
        {
            Cam = Camera.main;
        }

        SetUpDicBaseShape();

        for (int i = 1; i < 4; i++)
        {

        }
    }

    void SetUpDicBaseShape()
    {
        templates.Add("Circle", circlePoints);
        templates.Add("Square", square);
        templates.Add("Triangle", triangle);
        templates.Add("Ellipse", ellipse);
        templates.Add("Check", check);
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
            /*string rocognizedShape = Recognize(points);
            resultText.text = "Reconnu : " + rocognizedShape;*/
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Resetpoint();
        }
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

    //Step 1

    //Resample a points path into n evenly spaced points.We
    //use n=64. For gestures serving as templates, Steps 1-3 should be
    //carried out once on the raw input points.For candidates, Steps 1-4
    //should be used just after the candidate is articulated.

    public void Resemple(List<Vector3> Path, int pointNumber)
    {
        float I = PathLenght(Path) / pointNumber - 1;
        float D = 0;
        List<Vector3> newPath = new List<Vector3>();
        newPath.Add(Path[0]);

        for (int i = 1; i < PathLenght(Path); i++)
        {
            float d = Vector3.Distance(Path[i - 1], Path[i]);

            if (D + d > I)
            {
                float Qx = Path[i - 1].x + ((I - D / d) * (Path[i].x - Path[i - 1].x));
                float Qy = Path[i - 1].y + ((I - D / d) * (Path[i].y - Path[i - 1].y));

                Vector3 q = new Vector3(Qx, Qy, 0);

                newPath.Add(q);
                Path.Insert(i, q);
            }
            else
            {
                D += d;
            }
        }
    }

    public float PathLenght(List<Vector3> draw)
    {
        float DrawLenght = 0;

        for (int i = 1; i < draw.Count; i++)
        {
            DrawLenght += Vector3.Distance(draw[i - 1], draw[i]);
        }

        return DrawLenght;
    }

    //Step 2

    //Find and save the indicative angle w from the points’
    //centroid to first point.Then rotate by –w to set this angle to 0°.

    public float IndicativeAngle(List<Vector3> points)
    {
        Vector3 c = GetDrawCentroid(points);
        return Mathf.Atan2(c.y - points[0].y, c.x - points[0].x);
    }

    public List<Vector3> RotateBy(List<Vector3> points, float w)
    {
        Vector3 c = GetDrawCentroid(points);
        List<Vector3> newPoints = new() { Vector3.zero };

        foreach (Vector3 p in points)
        {
            //float Qx = (p.x - c.x) * Mathf.Cos(w - (p.y - c.y)) * Mathf.Sin(w+c.x);

            float Qx = (p.x - c.x) * Mathf.Cos(w) - (p.y - c.y) * Mathf.Sin(w) + c.x;
            float Qy = (p.x - c.x) * Mathf.Sin(w) - (p.y - c.y) * Mathf.Cos(w) + c.x;
            newPoints.Add(new Vector3(Qx, Qy));
        }

        return newPoints;
    }

    //step 3

    //Scale points so that the resulting bounding box will be of
    //size2
    //size.We use size=250. Then translate points to the origin
    //k=(0,0). BOUNDING-BOX returns a rectangle defined by(minx,
    //miny), (maxx, maxy).

    public Vector3 ScaleTo(Vector3 points)
    {
        return Vector3.zero;
    }
}