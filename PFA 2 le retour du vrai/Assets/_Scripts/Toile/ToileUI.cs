using TMPro;
using UnityEngine;

public class ToileUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerUGUI;
    [SerializeField] TextMeshProUGUI paintAmountUGUI;

    private void Start()
    {
        
    }

    public void UpdateToileUI(int timerText)
    {
        timerUGUI.text = "timeAmountLeft : " + timerText;
        paintAmountUGUI.text = "paintAmount : " + PlayerMain.Instance.Inventory.paintAmount;
    }
}
