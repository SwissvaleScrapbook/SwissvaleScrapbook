using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerTrigger : MonoBehaviour
{
    
    void ShowPopup(GameObject mapMarker)
    {
        PopupManager.instance.ShowLocationPopup(mapMarker);
    }

    void HidePopup()
    {
        //PopupManager.instance.HideLocationPopup();
    }

    // IEnumerator FadeInSequence(Transform mapMarker)
    // {
    //     Transform imgCanvas = mapMarker.GetChild(4);
    //     Transform textCanvas = mapMarker.GetChild(5);
    //     Transform bgCanvas = mapMarker.GetChild(3);

    //     // Canvas activation
    //     bgCanvas.gameObject.SetActive(true);
    //     textCanvas.gameObject.SetActive(true);
    //     imgCanvas.gameObject.SetActive(true);
        
    //     // Get the text elements (LocationDiscovered and LocationName)
    //     Transform locationDiscovered = textCanvas.GetChild(0);
    //     Transform locationName = textCanvas.GetChild(1);
        
    //     // 1. Fade in "LOCATION DISCOVERED" text
    //     if (locationDiscovered != null)
    //     {
    //         yield return StartCoroutine(FadeInElement(locationDiscovered));
    //         yield return new WaitForSeconds(delayBetweenElements);
    //     }
        
    //     // 2. Fade in location name text
    //     if (locationName != null)
    //     {
    //         yield return StartCoroutine(FadeInElement(locationName));
    //         yield return new WaitForSeconds(delayBetweenElements);
    //     }
        
    //     // 3. Fade in images one by one
    //     foreach (Transform img in imgCanvas)
    //     {
    //         yield return StartCoroutine(FadeInElement(img));
    //         yield return new WaitForSeconds(delayBetweenElements);
    //     }
    // }

    // IEnumerator FadeInElement(Transform element)
    // {
    //     // Make sure the element has a CanvasGroup component
    //     CanvasGroup canvasGroup = element.GetComponent<CanvasGroup>();
    //     if (canvasGroup == null)
    //     {
    //         canvasGroup = element.gameObject.AddComponent<CanvasGroup>();
    //     }

    //     // Start with alpha at 0
    //     canvasGroup.alpha = 0f;
    //     element.gameObject.SetActive(true);

    //     // Fade in
    //     float elapsed = 0f;
    //     while (elapsed < fadeDuration)
    //     {
    //         elapsed += Time.deltaTime;
    //         canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
    //         yield return null;
    //     }
        
    //     canvasGroup.alpha = 1f; // Ensure it's fully visible
    // }

    // void ResetAlpha(Transform element)
    // {
    //     CanvasGroup cg = element.GetComponent<CanvasGroup>();
    //     if (cg != null)
    //     {
    //         cg.alpha = 0f;
    //     }
        
    //     // Also reset children
    //     foreach (Transform child in element)
    //     {
    //         CanvasGroup childCg = child.GetComponent<CanvasGroup>();
    //         if (childCg != null)
    //         {
    //             childCg.alpha = 0f;
    //         }
    //     }
    // }
}