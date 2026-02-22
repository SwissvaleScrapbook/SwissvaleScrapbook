using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MapMenuController : MonoBehaviour
{
    [Header("References")]
    public GameObject mapOptionsCanvas;   // drag MapMenu > OptionsCanvas here
    public CanvasGroup popupCanvasGroup;  // drag PopupPanel here (once you make it)
    public Button mapMenuButton;          // drag MapMenuButton here

    private void Start()
    {
        mapOptionsCanvas.SetActive(false);
        mapMenuButton.onClick.AddListener(() => StartCoroutine(ShowPopup()));
    }

    public IEnumerator ShowPopup()
    {
        mapOptionsCanvas.SetActive(true);
        popupCanvasGroup.alpha = 0f;
        popupCanvasGroup.interactable = false;
        popupCanvasGroup.blocksRaycasts = false;

        float duration = 0.2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            popupCanvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        popupCanvasGroup.alpha = 1f;
        popupCanvasGroup.interactable = true;
        popupCanvasGroup.blocksRaycasts = true;
    }

    public IEnumerator HidePopup()
    {
        popupCanvasGroup.interactable = false;
        popupCanvasGroup.blocksRaycasts = false;

        float duration = 0.15f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            popupCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            yield return null;
        }

        popupCanvasGroup.alpha = 0f;
        mapOptionsCanvas.SetActive(false);
    }

    public void OnOverlayClicked()
{
    StartCoroutine(HidePopup());
}
}