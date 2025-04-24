using System.Collections.Generic;
using UnityEngine;

public class DrawMatrice : MonoBehaviour
{
    public static DrawMatrice Instance;

    public GameObject CellPrefab;

    public List<GameObject> CellsList { get; private set; } = new List<GameObject>();

    [Header("Colors")]
    public Color BlankCellColor;
    public Color HighlightedCellColor;
    public Color MovementPreviewColor;
    public Color EnnemyOnCaseRangePreview;
    public Color AllyOnCaseRangePreview;
    public Color CaseInSkillRange;
    public Color CaseInDamageZone;

    // System
    public Dictionary<Vector2, GameObject> _map = new();

    public List<Vector2> _newOffsets = new();
    [SerializeField] private List<Vector2> _oldOffsets = new();

    private bool _isShown = true;

    private Vector2 _lastPointedPos;
    public GameObject CurrentPointedCell;
    private GameObject _lastPointedCell;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int x = -12; x <= 12; x++)
        {

            for (int y = -12; y <= 12; y++)
            {
                GameObject newCell = Instantiate(CellPrefab, new Vector3(x, 0, y), Quaternion.identity, this.transform);
                CellsList.Add(newCell);

                //newCell.AddComponent<tkt>();

                newCell.transform.Rotate(Vector3.right * 90);
                _map.Add(new Vector2(x, y), newCell);
                if (x == -12 | x == 12 | y == -12 | y == 12)
                {
                    newCell.TryGetComponent(out SpriteRenderer spriteRenderer);
                    spriteRenderer.enabled = false;
                }
            }
        }
        CurrentPointedCell = _map[Vector2.zero];
        _lastPointedCell = CurrentPointedCell;
    }
}