using UnityEngine;

public class TriggerToile : MonoBehaviour
{
    private bool _isActive;
    [SerializeField] private GameObject toile;
    private Coroutine timerCo;

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
            timerCo = StartCoroutine(ToileMain.Instance.ToileTimer()); 
        }
        else
        {
            _isActive = false;
            toile.SetActive(_isActive);
            StopCoroutine(timerCo);
            Debug.Log("AJUGHeiushgbuorlj");
        }
    }
}
