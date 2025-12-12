using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class PopupManager : MonoBehaviour
{
    // This is a singleton instance that can be referenced globally
    public static PopupManager instance;
    private Transform transform;
    private GameObject canvas;

    [SerializeField] private GameObject locationName;
    [SerializeField] private GameObject closeButton;


    [Header("Background Settings")]
    [SerializeField] private GameObject background;
    [SerializeField] private float fadeDuration = 0.5f; // Duration of fade animation

    private Image backgroundImage;
    private CanvasGroup backgroundCanvasGroup;


    [Header("Story Objects")]
    [SerializeField] private GameObject story1;
    [SerializeField] private GameObject story2;
    [SerializeField] private GameObject story3;
    [SerializeField] private GameObject story4;
    [SerializeField] private GameObject story5;
    [SerializeField] private GameObject story6;
    [SerializeField] private GameObject story7;

    [Header("Image Objects")]
    [SerializeField] private GameObject image1;
    [SerializeField] private GameObject image2;
    [SerializeField] private GameObject image3;

    private Coroutine backgroundFadeCoroutine;
    private Coroutine storyFadeCoroutine;
    private Coroutine imageFadeCoroutine;

    void Awake()
    {
        instance = this;
        transform = GetComponent<Transform>();
        canvas = transform.GetChild(0).gameObject;

        backgroundImage = background.GetComponent<Image>();
    }
    
    public void ShowLocationPopup(GameObject mapMarker)
    {
        Debug.Log("Showing popup for: " + mapMarker.GetComponent<LocationMarker>().locationData.locationName);

        // Enable the canvas (PopupManager's first child)
        canvas.SetActive(true);

        // Set location name
        SetLocationName(mapMarker);

        // Set story titles based on map marker data
        SetStoryTitles(mapMarker);

        // Set images based on map marker data
        SetImages(mapMarker);
        
        // Fade in the background, close button, and title together
        FadeBackgroundIn();

        // Fade in the stories
        FadeInStories();

        // Fade in the images one by one
        FadeInImages();
    }

    public void HideLocationPopup()
    {
        Debug.Log("Hiding location popup");

        // Fade out the background, then disable canvas
        StartCoroutine(HideWithFade());
    }

    private IEnumerator HideWithFade()
    {
        // Start all fade outs simultaneously
        Coroutine backgroundFade = StartCoroutine(FadeBackgroundCoroutine(1f, 0f));
        Coroutine storiesFade = StartCoroutine(FadeOutAllStories());
        Coroutine imagesFade = StartCoroutine(FadeOutAllImages());

        // Wait for all fades to complete
        yield return backgroundFade;
        yield return storiesFade;
        yield return imagesFade;

        canvas.SetActive(false);
    }

    private IEnumerator FadeOutAllStories()
    {
        GameObject[] allStories = { story1, story2, story3, story4, story5, story6, story7 };
        
        // Start all story fades simultaneously
        Coroutine[] fadeCoroutines = new Coroutine[allStories.Length];
        
        for (int i = 0; i < allStories.Length; i++)
        {
            if (allStories[i] != null)
            {
                fadeCoroutines[i] = StartCoroutine(FadeStoryCoroutine(allStories[i], 1f, 0f));
            }
        }
        
        // Wait for all to finish
        foreach (Coroutine coroutine in fadeCoroutines)
        {
            if (coroutine != null)
            {
                yield return coroutine;
            }
        }
    }

    private IEnumerator FadeOutAllImages()
    {
        GameObject[] allImages = { image1, image2, image3 };
        
        // Start all image fades simultaneously
        Coroutine[] fadeCoroutines = new Coroutine[allImages.Length];
        
        for (int i = 0; i < allImages.Length; i++)
        {
            if (allImages[i] != null)
            {
                fadeCoroutines[i] = StartCoroutine(FadeImageCoroutine(allImages[i], 1f, 0f));
            }
        }
        
        // Wait for all to finish
        foreach (Coroutine coroutine in fadeCoroutines)
        {
            if (coroutine != null)
            {
                yield return coroutine;
            }
        }
    }

    // Fade background in function
    private void FadeBackgroundIn()
    {
        if (backgroundFadeCoroutine != null)
        {
            StopCoroutine(backgroundFadeCoroutine);
        }
        backgroundFadeCoroutine = StartCoroutine(FadeBackgroundCoroutine(0f, 1f));
    }

    // Fade background out function
    private IEnumerator FadeBackgroundOut()
    {
        if (backgroundFadeCoroutine != null)
        {
            StopCoroutine(backgroundFadeCoroutine);
        }
        yield return StartCoroutine(FadeBackgroundCoroutine(1f, 0f));
    }

    private IEnumerator FadeBackgroundCoroutine(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        // Set initial alpha for close button and location name
        SetUIElementAlpha(closeButton, startAlpha);
        SetUIElementAlpha(locationName, startAlpha);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);

            // Fade background
            if (backgroundImage != null)
            {
                Color color = backgroundImage.color;
                color.a = alpha;
                backgroundImage.color = color;
            }
            else if (backgroundCanvasGroup != null)
            {
                backgroundCanvasGroup.alpha = alpha;
            }

            // Fade close button and location name together
            SetUIElementAlpha(closeButton, alpha);
            SetUIElementAlpha(locationName, alpha);

            yield return null;
        }

        // Ensure final alpha is set
        if (backgroundImage != null)
        {
            Color color = backgroundImage.color;
            color.a = endAlpha;
            backgroundImage.color = color;
        }
        else if (backgroundCanvasGroup != null)
        {
            backgroundCanvasGroup.alpha = endAlpha;
        }

        SetUIElementAlpha(closeButton, endAlpha);
        SetUIElementAlpha(locationName, endAlpha);
    }

    private void SetUIElementAlpha(GameObject element, float alpha)
    {
        if (element == null) return;

        CanvasGroup canvasGroup = element.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = alpha;
            return;
        }

        Image[] images = element.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            Color color = img.color;
            color.a = alpha;
            img.color = color;
        }

        TextMeshProUGUI[] texts = element.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in texts)
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }

    // Fade in stories, starting with story 1, then 2-7 all happen at the same time
    private void FadeInStories()
    {
        if (storyFadeCoroutine != null)
        {
            StopCoroutine(storyFadeCoroutine);
        }
        storyFadeCoroutine = StartCoroutine(FadeInStoriesCoroutine());
    }

    private IEnumerator FadeInStoriesCoroutine()
    {
        // Create array of all stories for easier management
        GameObject[] allStories = { story1, story2, story3, story4, story5, story6, story7 };
        
        // Set all stories to alpha 0 initially
        foreach (GameObject story in allStories)
        {
            if (story != null)
            {
                SetStoryAlpha(story, 0f);
                story.SetActive(true); // Make sure they're active but invisible
            }
        }
        
        // Fade in story 1 first
        if (story1 != null)
        {
            yield return StartCoroutine(FadeStoryCoroutine(story1, 0f, 1f));
        }
        
        // Then fade in stories 2-7 simultaneously
        Coroutine[] fadeCoroutines = new Coroutine[6];
        GameObject[] subStories = { story2, story3, story4, story5, story6, story7 };
        
        for (int i = 0; i < subStories.Length; i++)
        {
            if (subStories[i] != null)
            {
                fadeCoroutines[i] = StartCoroutine(FadeStoryCoroutine(subStories[i], 0f, 1f));
            }
        }
        
        // Wait for all sub-stories to finish fading (they run in parallel)
        foreach (Coroutine coroutine in fadeCoroutines)
        {
            if (coroutine != null)
            {
                yield return coroutine;
            }
        }
    }

    private IEnumerator FadeStoryCoroutine(GameObject story, float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            SetStoryAlpha(story, alpha);
            yield return null;
        }

        // Ensure final alpha is set
        SetStoryAlpha(story, endAlpha);
    }

    private void SetStoryAlpha(GameObject story, float alpha)
    {
        if (story == null) return;

        // Try CanvasGroup first (recommended approach)
        CanvasGroup canvasGroup = story.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = alpha;
            return;
        }

        // Fallback to Image if no CanvasGroup
        Image image = story.GetComponent<Image>();
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }

        // Also fade any child images/text
        Image[] childImages = story.GetComponentsInChildren<Image>();
        foreach (Image img in childImages)
        {
            Color color = img.color;
            color.a = alpha;
            img.color = color;
        }

        TextMeshProUGUI[] childTexts = story.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in childTexts)
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }

    // Fade in images one by one
    private void FadeInImages()
    {
        if (imageFadeCoroutine != null)
        {
            StopCoroutine(imageFadeCoroutine);
        }
        imageFadeCoroutine = StartCoroutine(FadeInImagesCoroutine());
    }

    private IEnumerator FadeInImagesCoroutine()
    {
        GameObject[] allImages = { image1, image2, image3 };

        // Set all images to alpha 0 initially
        foreach (GameObject img in allImages)
        {
            if (img != null)
            {
                SetImageAlpha(img, 0f);
                img.SetActive(true);
            }
        }

        // Fade in each image one by one
        foreach (GameObject img in allImages)
        {
            if (img != null)
            {
                yield return StartCoroutine(FadeImageCoroutine(img, 0f, 1f));
            }
        }
    }

    private IEnumerator FadeImageCoroutine(GameObject imageObj, float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            SetImageAlpha(imageObj, alpha);
            yield return null;
        }

        SetImageAlpha(imageObj, endAlpha);
    }

    private void SetImageAlpha(GameObject imageObj, float alpha)
    {
        if (imageObj == null) return;

        CanvasGroup canvasGroup = imageObj.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = alpha;
            return;
        }

        Image image = imageObj.GetComponent<Image>();
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }

        Image[] childImages = imageObj.GetComponentsInChildren<Image>();
        foreach (Image img in childImages)
        {
            Color color = img.color;
            color.a = alpha;
            img.color = color;
        }
    }

    private void SetLocationName(GameObject mapMarker)
    {
        string name = mapMarker.GetComponent<LocationMarker>().locationData.locationName;
        locationName.GetComponent<TextMeshProUGUI>().text = name;
    }

    private void SetStoryTitles(GameObject mapMarker)
    {
        GameObject[] allStories = { story1, story2, story3, story4, story5, story6, story7 };
        
        for (int i = 0; i < allStories.Length; i++)
        {
            if (allStories[i] != null && i < mapMarker.GetComponent<LocationMarker>().locationData.storyList.Count)
            {
                string storyName = mapMarker.GetComponent<LocationMarker>().locationData.storyList[i].title;
                allStories[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = storyName;
            }
        }
    }

    private void SetImages(GameObject mapMarker)
    {
        GameObject[] allImages = { image1, image2, image3 };
        
        for (int i = 0; i < allImages.Length; i++)
        {
            if (allImages[i] != null && i < mapMarker.GetComponent<LocationMarker>().locationData.imgList.Count)
            {
                Sprite imageSprite = mapMarker.GetComponent<LocationMarker>().locationData.imgList[i].image;
                Image imgComponent = allImages[i].GetComponent<Image>();
                
                if (imgComponent != null && imageSprite != null)
                {
                    imgComponent.sprite = imageSprite;
                }
            }
        }
    }
}