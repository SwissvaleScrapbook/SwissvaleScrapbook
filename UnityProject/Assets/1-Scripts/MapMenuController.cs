using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Map;

public class MapMenuController : MonoBehaviour
{
    [Header("References")]
    public GameObject mapOptionsCanvas;
    public CanvasGroup popupCanvasGroup;
    public Button mapMenuButton;
    public AbstractMap map;

    private void Start()
    {
        mapMenuButton = GameObject.Find("MapMenuButton").GetComponent<Button>();
        mapOptionsCanvas.SetActive(false);
        mapMenuButton.onClick.AddListener(() => StartCoroutine(ShowPopup()));
    }

    // MAP STYLE METHODS
    public void SetMapStreets()
    {
        map.ImageLayer.SetLayerSource(ImagerySourceType.MapboxStreets);
        StartCoroutine(HidePopup());
    }

    public void SetMapSatellite()
    {
        map.ImageLayer.SetLayerSource(ImagerySourceType.MapboxSatellite);
        StartCoroutine(HidePopup());
    }

    public void SetMapLight()
    {
        map.ImageLayer.SetLayerSource(ImagerySourceType.MapboxLight);
        StartCoroutine(HidePopup());
    }

    // POPUP METHODS
    public void OnOverlayClicked()
    {
        StartCoroutine(HidePopup());
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
}