using UnityEngine;

public class TriggerToile : MonoBehaviour
{
    private bool _isActive;
    [SerializeField] private GameObject toile;

    private void Start()
    {
        _isActive = toile.activeSelf;
    }

    public void OnClick()
    {
        if (_isActive == false)
        {
            _isActive = true;
            toile.SetActive(_isActive);
        }
        else
        {
            _isActive = false;
            toile.SetActive(_isActive);
        }
    }
}
