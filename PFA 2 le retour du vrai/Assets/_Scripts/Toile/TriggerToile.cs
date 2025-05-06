using UnityEngine;
using UnityEngine.InputSystem;
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
            PlayerMain.Instance.Move.canMove = false;
            PlayerMain.Instance.playerInput.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current, Mouse.current);
            //StopCoroutine(ToileMain.Instance.timerCo);
            Debug.Log("Open");

        }
        else
        {
            _isActive = false;
            toile.SetActive(_isActive);
            PlayerMain.Instance.UI.HidePlayerControls();
            PlayerMain.Instance.playerInput.ActivateInput();
            PlayerMain.Instance.Move.canMove = true;
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
