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
    [Header("Story Stack")]

    // adding references to storystack and story detail panels so we can enable/disable them as needed
    [SerializeField] private GameObject storyStackPanel;
    // [SerializeField] private GameObject storyDetailPanel;
    [SerializeField] private GameObject storyCardPrefab; 
    [SerializeField] private GameObject storyDetailCanvas;

    // adding ref to title and body
    [Header("Story Detail")]
[SerializeField] private TextMeshProUGUI detailTitle;
[SerializeField] private TextMeshProUGUI detailBody;

    private Image backgroundImage;
    private CanvasGroup backgroundCanvasGroup;

    private List<StoryCard> stories;

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
    
   public void OpenStory(StoryCard card)
    {
        Debug.Log("OpenStory called! Title: " + card.storyTitle); // TEST
        //currentCard = card;
        detailTitle.text = card.storyTitle;
        detailBody.text = card.storyBody;

        // add to tell storystackmanager which card is opened
        storyStackPanel.GetComponent<StoryStackManager>().currentCard = card;

        // Hide main popup elements
    background.SetActive(false);
    locationName.SetActive(false);
    closeButton.SetActive(false);
    storyStackPanel.SetActive(false);

    // Show detail panel
    storyDetailCanvas.SetActive(true);

     Debug.Log("StoryDetailPanel active: " + storyDetailCanvas.activeSelf);
    Debug.Log("Background active: " + storyDetailCanvas.transform.Find("Background").gameObject.activeSelf);
    Debug.Log("TitleText: " + detailTitle.text);
    }

    public void CloseStory()
{
    background.SetActive(true);
    locationName.SetActive(true);
    closeButton.SetActive(true);
    storyStackPanel.SetActive(true);
    storyDetailCanvas.SetActive(false);

    storyStackPanel.GetComponent<StoryStackManager>().SendCurrentCardToBack();
}



    public void ShowLocationPopup(GameObject mapMarker)
    {
        locationName.GetComponent<TextMeshProUGUI>().text = mapMarker.GetComponent<LocationMarker>().locationData.locationName;
        SetStories(mapMarker);
        DisplayStories(); 
        canvas.SetActive(true);
    }


    private void SetLocationName(GameObject mapMarker)
    {
        string name = mapMarker.GetComponent<LocationMarker>().locationData.locationName;
        locationName.GetComponent<TextMeshProUGUI>().text = name;
    }

    private void SetStories(GameObject mapMarker)
    {
        LocationData data = mapMarker.GetComponent<LocationMarker>().locationData;
    Debug.Log("Stories count: " + data.stories.Count); // ADD THIS
    
    stories = new List<StoryCard>();

    foreach (StoryObject s in data.stories)
    {
        GameObject temp = new GameObject("TempStoryCard");
        StoryCard card = temp.AddComponent<StoryCard>();
        card.storyTitle = s.title;
        card.storyBody = s.body;
        stories.Add(card);
    }
    }

    private void DisplayStories()
{
    // Clear any existing cards from a previous location
    foreach (Transform child in storyStackPanel.transform)
    {
        Destroy(child.gameObject);
    }

    foreach (StoryCard story in stories)
{
    if (story == null)
    {
        Debug.LogError("Null story in list, skipping!");
        continue;
    }

    GameObject newCard = Instantiate(storyCardPrefab, storyStackPanel.transform);
    StoryCard newStoryCard = newCard.GetComponent<StoryCard>();

    if (newStoryCard == null)
    {
        Debug.LogError("StoryCard component not found on prefab!");
        continue;
    }

    newStoryCard.storyTitle = story.storyTitle;
    newStoryCard.storyBody = story.storyBody;

    TextMeshProUGUI label = newCard.GetComponentInChildren<TextMeshProUGUI>();
    if (label != null)
        label.text = story.storyTitle;
}

    // Instantiate a card for each story in the list
    /*foreach (StoryCard story in stories)
    {
        GameObject newCard = Instantiate(storyCardPrefab, storyStackPanel.transform);
        StoryCard newStoryCard = newCard.GetComponent<StoryCard>();

        if (newStoryCard == null)
        {
            Debug.LogError("StoryCard component not found on prefab!");
            continue;
        }

        newStoryCard.storyTitle = story.storyTitle;
        newStoryCard.storyBody = story.storyBody;

        // Update the visible title label on the card
        TextMeshProUGUI label = newCard.GetComponentInChildren<TextMeshProUGUI>();
        if (label != null)
            label.text = story.storyTitle;
    }
    */

    // Tell StoryStackManager to re-arrange the stack with the new cards
    StartCoroutine(RefreshStackNextFrame());
}

private IEnumerator RefreshStackNextFrame()
{
    yield return null;
    storyStackPanel.GetComponent<StoryStackManager>().RefreshStack();
}

// for close button to work

    public void HideLocationPopup() {
    Debug.Log("HideLocationPopup called!"); 
    Debug.Log("HideLocationPopup called from: " + System.Environment.StackTrace);
{
    foreach (Transform child in storyStackPanel.transform)
    {
        Destroy(child.gameObject);
    }

    background.SetActive(true);
    locationName.SetActive(true);
    closeButton.SetActive(true);
    storyDetailCanvas.SetActive(false);
    storyStackPanel.SetActive(true);

    canvas.SetActive(false);
}
    }


    /*private void SetImages(GameObject mapMarker)
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
    */
}
