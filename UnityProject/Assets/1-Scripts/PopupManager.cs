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
    [SerializeField] private GameObject storyDetailPanel;
    [SerializeField] private GameObject storyCardPrefab; 

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
        //detailTitle.text = card.storyTitle;
        ///detailBody.text = card.storyBody;
        storyDetailPanel.SetActive(true);
        storyStackPanel.SetActive(false);
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
        List<StoryCard> markerStoryList = mapMarker.GetComponent<LocationMarker>().locationData.storyList;
        PopupManager.instance.stories = new List<StoryCard>(markerStoryList);
    }

    private void DisplayStories()
    {
        // create instance of a sotrycard child and make it a child of the story stack panel 
        // whenever you hit the close button and hide popup is called, make sure all the children r deleted 
    
    // Clear any existing cards from a previous location
    foreach (Transform child in storyStackPanel.transform)
    {
        Destroy(child.gameObject);
    }

    // Instantiate a card for each story in the list
    foreach (StoryCard story in stories)
    {
        GameObject newCard = Instantiate(storyCardPrefab, storyStackPanel.transform);
        StoryCard newStoryCard = newCard.GetComponent<StoryCard>();
        newStoryCard.storyTitle = story.storyTitle;
        newStoryCard.storyBody = story.storyBody;
    }

    // Tell StoryStackManager to re-arrange the stack with the new cards
    storyStackPanel.GetComponent<StoryStackManager>().RefreshStack();
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
