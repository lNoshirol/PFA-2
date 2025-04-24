using System;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class DrawLeVrai : MonoBehaviour
{
    [SerializeField] Camera Cam;
    [SerializeField] LineRenderer trailPrefab;

    [SerializeField] private LineRenderer currentTrail;
    [SerializeField] private List<Vector3> points = new List<Vector3>();
    [SerializeField] private List<Vector2> points2 = new List<Vector2>();

    private Dictionary<string, List<Vector2>> templates = new Dictionary<string, List<Vector2>>();

    [SerializeField] private float distanceBetweenPoint;
    private float currentDistance;

    public TMP_Text resultText;
    public LineRenderer lineRenderer;
    public TextMeshProUGUI inputFieldText;

    private bool isDrawing = false;

    readonly float phi = 1 / 2 * (-1 + Mathf.Sqrt(5));

    float Theta = Mathf.Deg2Rad * 45f;
    float DeltaTheta = Mathf.Deg2Rad * 2f;

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
        // A (0.5, 0.866) → B (0, 0)
        new Vector3(0.5f, 0.866f, 0f),
        new Vector3(0.45f, 0.787f, 0f),
        new Vector3(0.4f, 0.709f, 0f),
        new Vector3(0.35f, 0.630f, 0f),
        new Vector3(0.3f, 0.551f, 0f),
        new Vector3(0.25f, 0.472f, 0f),
        new Vector3(0.2f, 0.394f, 0f),
        new Vector3(0.15f, 0.315f, 0f),
        new Vector3(0.1f, 0.236f, 0f),
        new Vector3(0.05f, 0.157f, 0f),
        new Vector3(0f, 0f, 0f),

        // B (0, 0) → C (1, 0)
        new Vector3(0.1f, 0f, 0f),
        new Vector3(0.2f, 0f, 0f),
        new Vector3(0.3f, 0f, 0f),
        new Vector3(0.4f, 0f, 0f),
        new Vector3(0.5f, 0f, 0f),
        new Vector3(0.6f, 0f, 0f),
        new Vector3(0.7f, 0f, 0f),
        new Vector3(0.8f, 0f, 0f),
        new Vector3(0.9f, 0f, 0f),
        new Vector3(1f, 0f, 0f),

        // C (1, 0) → A (0.5, 0.866)
        new Vector3(0.95f, 0.087f, 0f),
        new Vector3(0.9f, 0.173f, 0f),
        new Vector3(0.85f, 0.260f, 0f),
        new Vector3(0.8f, 0.346f, 0f),
        new Vector3(0.75f, 0.433f, 0f),
        new Vector3(0.7f, 0.520f, 0f),
        new Vector3(0.65f, 0.606f, 0f),
        new Vector3(0.6f, 0.693f, 0f),
        new Vector3(0.55f, 0.779f, 0f),
        new Vector3(0.5f, 0.866f, 0f),
        new Vector3(0.5f, 0.866f, 0f) // boucle
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
        templates.Add("Circle", ScaleTo(TranslateTo(ConvertVec3ToVec2(circlePoints), Vector2.zero), 250));
        templates.Add("Square", ScaleTo(TranslateTo(ConvertVec3ToVec2(square), Vector2.zero), 250) );
        templates.Add("Triangle", ScaleTo(TranslateTo(ConvertVec3ToVec2(triangle), Vector2.zero), 250) );
        templates.Add("Ellipse", ScaleTo(TranslateTo(ConvertVec3ToVec2(ellipse), Vector2.zero), 250) );
        templates.Add("Check", ScaleTo(TranslateTo(ConvertVec3ToVec2(check), Vector2.zero), 250) );
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
            
            points2 = Resemple(ConvertVec3ToVec2(points), 32);


            Tuple<String, float> RecognizeResult = Recognize(ScaleTo(TranslateTo(points2, Vector2.zero), 250), templates, 250);
            string rocognizedShape = RecognizeResult.Item1;
            resultText.text = "Reconnu : " + rocognizedShape;
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

    public float GetMaxDistance(List<Vector2> points)
    {
        float maxDistance = 0f;
        Vector2 centroid = GetDrawCentroid(points);

        foreach (Vector2 point in points)
        {
            float distance = Vector2.Distance(point, centroid);
            if (distance >= maxDistance)
            {
                maxDistance = distance;
            }
        }

        return maxDistance;
    }

    public List<Vector2> ConvertVec3ToVec2(List<Vector3> points)
    {
        List<Vector2> newPoints = new();

        foreach (Vector3 point in points)
        {
            Vector2 newPoint = new Vector2(point.x, point.y);
            newPoints.Add(newPoint);
        }

        return newPoints;
    }

    public Vector2 GetDrawCentroid(List<Vector2> points)
    {
        Vector2 sum = Vector2.zero;

        foreach (Vector2 point in points)
        {
            sum += point;
        }

        Vector2 centroid = sum / points.Count;

        return centroid;
    }

    public Tuple<float, float, float, float> BoundingBox(List<Vector2> points)
    {
        float minX = int.MinValue;
        float maxX = int.MinValue;

        float minY = int.MinValue;
        float maxY = int.MinValue;

        foreach (Vector2 point in points)
        {
            if (point.x < minX)
            {
                minX = point.x;
            }
            if (point.y < minY)
            {
                minY = point.y;
            }
            if (maxX > point.x)
            {
                maxX = point.x;
            }
            if (maxY > point.y)
            {
                maxY = point.y;
            }
        }

        return Tuple.Create(minX, maxX, minY, maxY);
    }

    //Step 1

    //Resample a points path into n evenly spaced points.We
    //use n=64. For gestures serving as templates, Steps 1-3 should be
    //carried out once on the raw input points.For candidates, Steps 1-4
    //should be used just after the candidate is articulated.

    public List<Vector2> Resemple(List<Vector2> Path, int pointNumber)
    {
        float I = PathLenght(Path) / (pointNumber - 1);
        float D = 0;
        List<Vector2> newPath = new()
        {
            Path[0]
        };

        for (int i = 1; i < pointNumber; i++)
        {
            float d = Vector2.Distance(Path[i - 1], Path[i]);

            if (D + d > I)
            {
                float Qx = Path[i - 1].x + ((I - D / d) * (Path[i].x - Path[i - 1].x));
                float Qy = Path[i - 1].y + ((I - D / d) * (Path[i].y - Path[i - 1].y));

                Vector2 q = new(Qx, Qy);

                newPath.Add(q);
                Path.Insert(i, q);
            }
            else
            {
                D += d;
            }
        }
        points2 = newPath;
        
        return newPath;
    }

    public float PathLenght(List<Vector2> draw)
    {
        float DrawLenght = 0;

        for (int i = 1; i < draw.Count; i++)
        {
            DrawLenght += Vector2.Distance(draw[i - 1], draw[i]);
        }

        return DrawLenght;
    }

    //Step 2

    //Find and save the indicative angle w from the points’
    //centroid to first point.Then rotate by –w to set this angle to 0°.

    public float IndicativeAngle(List<Vector2> points)
    {
        Vector2 c = GetDrawCentroid(points);
        return Mathf.Atan2(c.y - points[0].y, c.x - points[0].x);
    }

    public List<Vector2> RotateBy(List<Vector2> points, float w)
    {
        Vector2 c = GetDrawCentroid(points);
        List<Vector2> newPoints = new() { Vector2.zero };

        foreach (Vector2 p in points)
        {
            //float Qx = (p.x - c.x) * Mathf.Cos(w - (p.y - c.y)) * Mathf.Sin(w+c.x);

            float Qx = (p.x - c.x) * Mathf.Cos(w) - (p.y - c.y) * Mathf.Sin(w) + c.x;
            float Qy = (p.x - c.x) * Mathf.Sin(w) - (p.y - c.y) * Mathf.Cos(w) + c.x;
            newPoints.Add(new Vector2(Qx, Qy));
        }

        return newPoints;
    }

    //step 3

    public List<Vector2> ScaleTo(List<Vector2> points, float size)
    {
        Tuple<float, float, float, float> B = BoundingBox(points);

        Tuple<float, float> boxHeightWidth = new Tuple<float, float>(MathF.Abs(B.Item3) + Mathf.Abs(B.Item4), MathF.Abs(B.Item1) + Mathf.Abs(B.Item2));

        List<Vector2> newPoints = new();

        foreach (Vector2 p in points)
        {
            float Qx = p.x * size / boxHeightWidth.Item2;
            float Qy = p.y * size / boxHeightWidth.Item1;

            Vector2 newp = new Vector2(Qx, Qy);

            newPoints.Add(newp);
        }

        return newPoints;
    }

    public List<Vector2> TranslateTo(List<Vector2> points, Vector2 k)
    {
        Vector2 c = GetDrawCentroid(points);
        List<Vector2> newPoints = new();

        foreach (Vector2 p in points)
        {
            float Qx = p.x + k.x - c.x;
            float Qy = p.y + k.y - c.y;

            Vector2 newp = new Vector2(Qx, Qy);
            newPoints.Add(newp);
        }

        return newPoints;
    }

    /*public List<Vector2> ScaleAndRecenter(List<Vector2> points)
    {
        List<Vector2> newPoints = new();
        Vector2 c = GetDrawCentroid(points);
        float d = GetMaxDistance(points);

        foreach (Vector2 p in points)
        {
            Vector2 reCenter = p - c;
            Vector2 scaled = reCenter / d;
            newPoints.Add(scaled);
        }

        return newPoints;
    }*/

    //step 4 

    //Match points against a set of templates.The size variable
    //on line 7 of RECOGNIZE refers to the size passed to SCALE-TO in
    //Step 3. The symbol ϕ equals ½(-1 + √5). We use θ=±45° and
    //θ∆=2° on line 3 of RECOGNIZE.Due to using RESAMPLE, we can
    //assume that A and B in PATH-DISTANCE contain the same number
    //of points, i.e., |A|=|B|. 

    public Tuple<String, float> Recognize(List<Vector2> points, Dictionary<string, List<Vector2>> templates, float size)
    {
        float b = int.MaxValue;
        string bestTemplate = null;

        foreach (var template in templates)
        {
            float distance = DistanceAtBestAngle(points, template.Value, -Theta, Theta, DeltaTheta, template.Key);
            if (distance < b)
            {
                b = distance;
                bestTemplate = template.Key;
            }

            //Debug.Log($"distance : {distance}, score : {(float)(1 - distance / (0.5 * Mathf.Sqrt(size * size + size * size)))}, template name : {template.Key}");
        }

        float score = (float)(1 - b / (0.5 * Mathf.Sqrt(size*size+size*size)));

        return new Tuple<string, float>(bestTemplate, score);
    }

    public float DistanceAtBestAngle(List<Vector2> points, List<Vector2> template, float ThetaA, float ThetaB, float ThetaDelta, string DebugTemplateName)
    {
        float x1 = phi * ThetaA + (1 - phi) * ThetaB;
        float f1 = DistanceAtAngle(points, template, x1, DebugTemplateName);

        float x2 = (1 - phi) * ThetaA + (1 - phi) * ThetaB;
        float f2 = DistanceAtAngle(points, template, x2, DebugTemplateName);

        while (Mathf.Abs(ThetaB - ThetaA) > ThetaDelta)
        {
            if (f1 < f2)
            {
                ThetaB = x2;
                x2 = x1;
                f2 = f1;
                x1 = phi * ThetaA + (1-phi) * ThetaB;
                f1 = DistanceAtAngle(points, template, x1, DebugTemplateName);
            }
            else
            {
                ThetaA = x1;
                x1 = x2;
                f1 = f2;
                x2 = (1-phi)* ThetaA + phi * ThetaB;
                f2 = DistanceAtAngle(points, template, x2, DebugTemplateName);
            }

        }

        return Mathf.Min(f1, f2);
    }

    public float DistanceAtAngle(List<Vector2> points, List<Vector2> template, float Theta, string DebugTemplateName)
    {
        List<Vector2> newPoints = RotateBy(points, Theta);
        float d = PathDistance(newPoints, template, DebugTemplateName);

        return d;
    }

    public float PathDistance(List<Vector2> points, List<Vector2> template, string DebugTemplateName)
    {
        float d = 0;

        //Debug.Log($"Draw points count : {points.Count}, Template points count : {template.Count}, Template name : {DebugTemplateName}");

        for (int i = 0; i < points.Count; i++)
        {
            d += Vector2.Distance(points[i], template[i]);
        }

        return d/points.Count;
    }
}