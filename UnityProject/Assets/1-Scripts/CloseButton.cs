using UnityEngine;
using UnityEngine.UI;
public class CloseButton : MonoBehaviour
{
    public Button closeButton;

    void Start()
    {
        closeButton.onClick.AddListener(HidePopup);
    }
    public void HidePopup()
    {
        PopupManager.instance.HideLocationPopup();
    }
}
