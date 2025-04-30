using UnityEngine;
using UnityEngine.UI;

public class TriggerToile : MonoBehaviour
{
    public bool _isActive;
    [SerializeField] private GameObject toile;
    [SerializeField] private Button toileButton;
    
    private void Start()
    {
        _isActive = toile.activeSelf;
        toileButton.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        if (_isActive == false)
        {
            ToileMain.Instance.ToileUI.UpdateToileUI(ToileMain.Instance.toileTime);
            _isActive = true;
            toile.SetActive(_isActive);
            PlayerMain.Instance.playerInput.DeactivateInput();
            
        }
        else
        {
            _isActive = false;
            toile.SetActive(_isActive);
            PlayerMain.Instance.playerInput.ActivateInput();
            StopCoroutine(ToileMain.Instance.timerCo);
        }
    }

    public void EnableToileButton()
    {
        toileButton.gameObject.SetActive(true);
    }
}
