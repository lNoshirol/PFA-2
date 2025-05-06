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
        toileButton.interactable = false;
    }

    public void OpenAndCloseToileMagique()
    {
        ToileMain.Instance.CastSpriteShape.Resetpoint();
        if (_isActive == false)
        {
            ToileMain.Instance.ToileUI.UpdateToileUI(ToileMain.Instance.toileTime);
            _isActive = true;
            toile.SetActive(_isActive);
            PlayerMain.Instance.UI.HidePlayerControls();
            //PlayerMain.Instance.playerInput.DeactivateInput();
            //StopCoroutine(ToileMain.Instance.timerCo);
            Debug.Log("Open");

        }
        else
        {
            _isActive = false;
            toile.SetActive(_isActive);
            PlayerMain.Instance.UI.HidePlayerControls();
            PlayerMain.Instance.playerInput.ActivateInput();
            ToileMain.Instance.gestureIsStarted = false;
            //StopCoroutine(ToileMain.Instance.timerCo);
            Debug.Log("Close");
        }
    }

    public void EnableToileButton()
    {
        toileButton.interactable = true;
    }
}
