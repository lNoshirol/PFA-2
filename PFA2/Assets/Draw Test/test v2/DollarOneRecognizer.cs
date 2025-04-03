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

    private List<Vector2> points = new List<Vector2>();
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
        Vector2 centroid = points.Aggregate(Vector2.zero, (acc, p) => acc + p) / points.Count;
        float maxDistance = points.Max(p => Vector2.Distance(p, centroid));

        return points.Select(p => (p - centroid) / maxDistance).ToList();
    }

    public void SaveTemplate()
    {
        List<Vector2> normalizedPoints = NormalizePoints(points);
        templates[inputFieldText.text] = normalizedPoints;
        Debug.Log($"Template '{inputFieldText.text}' enregistré avec {normalizedPoints.Count} points.");
    }
}