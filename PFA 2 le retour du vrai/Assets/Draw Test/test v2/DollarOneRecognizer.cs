using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class DollarOneRecognizer : MonoBehaviour
{
    public TMP_Text resultText;
    public LineRenderer lineRenderer;
    public TextMeshProUGUI inputFieldText;

    [SerializeField] private List<Vector2> points = new List<Vector2>();
    private Dictionary<string, List<Vector2>> templates = new Dictionary<string, List<Vector2>>();
    private bool isDrawing = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            points.Clear();
            lineRenderer.positionCount = 0;
        }

        if (isDrawing && Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            points.Add(mousePos);
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPosition(points.Count - 1, mousePos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
            string recognizedShape = Recognize(points);
            resultText.text = "Reconnu : " + recognizedShape;
        }
    }

    string Recognize(List<Vector2> inputPoints)
    {
        List<Vector2> normalizedPoints = NormalizePoints(inputPoints);
        float minDistance = float.MaxValue;
        string bestMatch = "Inconnu";

        foreach (var template in templates)
        {
            float distance = CalculatePathDistance(normalizedPoints, template.Value);
            if (distance < minDistance)
            {
                minDistance = distance;
                bestMatch = template.Key;
            }
        }

        return bestMatch;
    }

    float CalculatePathDistance(List<Vector2> path1, List<Vector2> path2)
    {
        float distance = 0;
        for (int i = 0; i < path1.Count; i++)
        {
            distance += Vector2.Distance(path1[i], path2[i]);
        }
        return distance / path1.Count;
    }

    List<Vector2> NormalizePoints(List<Vector2> points)
    {
        // Redimensionner et recentrer
        Vector2 sum = Vector2.zero;

        foreach (Vector2 point in points)
        {
            sum += point;
        }

        Vector2 centroid = sum / points.Count;

        float maxDistance = 0;

        foreach (Vector2 point in points)
        {
            float distance = Vector2.Distance(point, centroid);
            if (distance < maxDistance)
            {
                maxDistance = distance;
            }
        }

        // Créer une nouvelle liste de points normalisés
        List<Vector2> normalizedPoints = new List<Vector2>();

        foreach (Vector2 point in points)
        {
            Vector2 shifted = point - centroid;           // On recentre autour de (0,0)
            Vector2 scaled = shifted / maxDistance;       // On met à l’échelle pour que ça tienne dans un cercle de rayon 1
            normalizedPoints.Add(scaled);
        }

        return normalizedPoints;
    }

    public void SaveTemplate()
    {
        List<Vector2> normalizedPoints = NormalizePoints(points);
        templates[inputFieldText.text] = normalizedPoints;
        Debug.Log($"Template '{inputFieldText.text}' enregistré avec {normalizedPoints.Count} points.");
    }
}