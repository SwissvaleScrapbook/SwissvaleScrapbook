using UnityEngine;
using UnityEngine.UI;

public class TutorialBlurb : MonoBehaviour
{
    public bool isEnabled = true;

    public Button nextButton;
    public Button skipButton;

    [Header("Scene Objects")]
    public bool enableLocation;
    public bool enablePlayer;
    public bool enableRecenterButton;
    public bool enableMapButton;

    [Header("Triggers")]
    public bool triggerLocationPopup;
    public bool triggerStoryPopup;
    public bool recenterPlayer;

    void Start()
    {
        if(nextButton != null)
        {
            nextButton.onClick.AddListener(SignalAdvance);
        }

        if(skipButton != null)
        {
            skipButton.onClick.AddListener(SignalSkip);
        }

        // Enable scene objects based on settings
        if(enableLocation)
        {
            TutorialManager.instance.location.SetActive(true);
        }
        if(enablePlayer)
        {
            TutorialManager.instance.player.SetActive(true);
        }
        if(enableRecenterButton)
        {
            TutorialManager.instance.recenterButton.SetActive(true);
        }
        if(enableMapButton)
        {
            TutorialManager.instance.mapButton.SetActive(true);
        }
        if(triggerLocationPopup)
        {
            // Trigger location popup
            TutorialManager.instance.ShowLocationPopup();
        }
        if(triggerStoryPopup)
        {
            // Trigger story popup of the first StoryCard in the location's story list
            TutorialManager.instance.ShowStoryPopup();
        }
        if(recenterPlayer)
        {
            // Recenter player
            TutorialManager.instance.RecenterPlayer();
        }
    }

    private void SignalAdvance()
    {
        TutorialManager.instance.AdvanceTutorial();

        //Print to console
        Debug.Log("Tutorial advanced");
    }

    private void SignalSkip()
    {
        TutorialManager.instance.SkipTutorial();
        
        //Print to console
        Debug.Log("Tutorial skipped");
    }
}
