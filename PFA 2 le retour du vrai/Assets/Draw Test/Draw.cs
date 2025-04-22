using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class Draw : MonoBehaviour
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
        foreach ()
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
            string rocognizedShape = Recognize(points);
            resultText.text = "Reconnu : " + rocognizedShape;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Resetpoint();
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

    public List<Vector3> Vector2ToVector3(List<Vector3> pointsList)
    {
        List<Vector3> vector3Points = new List<Vector3>();

        foreach (Vector2 point in pointsList)
        {
            vector3Points.Add(new Vector3(point.x, 0, point.y));
        }

        return vector3Points;
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

    public void Resetpoint()
    {
        lineRenderer.positionCount = 0;
    }

    string Recognize(List<Vector3> inputPoints)
    {
        List<Vector3> normalizedPoints = NormalizePoints(inputPoints);
        float minDistance = float.MaxValue;
        string bestMatch = "Inconnu";

        if (templates.Count > 0)
        {
            foreach (var template in templates)
            {
                float distance = CalculatePathDistance(normalizedPoints, template.Value);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    bestMatch = template.Key;
                }
            }
        }
        else if (templates.Count == 0)
        {
            bestMatch = "aucun templates";
        }
        

        return bestMatch;
    }

    float CalculatePathDistance(List<Vector3> path1, List<Vector3> path2)
    {
        float distance = 0;
        for (int i = 0; i < path1.Count; i++)
        {
            distance += Vector2.Distance(path1[i], path2[i]);
            //Debug.Log($"Boucle {i}, distance : {distance}, Point dessin : {path1[i]}, Point template : {path2[i]}");
        }
        Debug.Log($"Distance: {distance/path1.Count}");
        return distance / path1.Count;
    }

    List<Vector3> NormalizePoints(List<Vector3> points)
    {
        //Debug.Log("<================ NORMALIZE ================>");

        // Redimensionner et recentrer
        Vector3 sum = Vector3.zero;
        //Debug.Log($"Somme : {sum}");

        foreach (Vector3 point in points)
        {
            sum += point;
        }

        Vector3 centroid = sum / points.Count;
        //Debug.Log($"Controid {centroid}");

        float maxDistance = 0;

        int debugIndex = 0;

        foreach (Vector3 point in points)
        {
            float distance = Vector3.Distance(point, centroid);
            if (distance > maxDistance)
            {
                maxDistance = distance;
            }
            //Debug.Log($"Boucle {debugIndex}, Point actuel : {point}, Distance : {distance}, MaxDistance : {maxDistance}");

            debugIndex++;
        }
        debugIndex = 0;
        // Créer une nouvelle liste de points normalisés
        List<Vector3> normalizedPoints = new List<Vector3>();

        foreach (Vector3 point in points)
        {
            Vector3 shifted = point - centroid;           // On recentre autour de (0,0)
            Vector3 scaled = shifted / maxDistance;       // On met à l’échelle pour que ça tienne dans un cercle de rayon 1
            normalizedPoints.Add(scaled);

            //Debug.Log($"Boucle {debugIndex}, Point actuel : {point}, Shift : {shifted}, Scaled : {scaled}");

            debugIndex++;
        }

        //Debug.Log("<============== NORMALIZE END ==============>");
        return normalizedPoints;
    }

    public void SaveTemplate()
    {
        List<Vector3> normalizedPoints = NormalizePoints(points);
        templates[inputFieldText.text] = normalizedPoints;
        Debug.Log($"Template '{inputFieldText.text}' enregistré avec {normalizedPoints.Count} points.");
    }

    public void Resemple(List<Vector3> Path, int pointNumber)
    {
        float I = PathLenght(Path) / pointNumber-1;
        float D = 0;
        List<Vector3> newPath = new List<Vector3>();
    }

    public float PathLenght(List<Vector3> draw)
    {
        float DrawLenght = 0;

        for (int i = 1; i < draw.Count; i++)
        {
            DrawLenght += Vector3.Distance(draw[i-1], draw[i]);
        }

        return DrawLenght;
    }

    public void TestEnd()
    {
        Debug.Log("AAAAAAAAAAAAH");
    }
}