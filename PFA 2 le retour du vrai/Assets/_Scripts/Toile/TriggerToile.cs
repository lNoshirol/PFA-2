using UnityEngine;
using UnityEngine.UI;

public class TriggerToile : MonoBehaviour
{
    private bool _isActive;
    [SerializeField] private GameObject toile;
    [SerializeField] private Button toileButton;
    
    private Coroutine timerCo;

    private void Start()
    {
        _isActive = toile.activeSelf;
        toileButton.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        if (_isActive == false)
        {
            _isActive = true;
            toile.SetActive(_isActive);
            PlayerMain.Instance.playerInput.DeactivateInput();
            timerCo = StartCoroutine(ToileMain.Instance.ToileTimer()); 
        }
        else
        {
            _isActive = false;
            toile.SetActive(_isActive);
            PlayerMain.Instance.playerInput.ActivateInput();
            StopCoroutine(timerCo);
            Debug.Log("AJUGHeiushgbuorlj");
        }
    }

    public void EnableToileButton()
    {
        toileButton.gameObject.SetActive(true);
    }
}
