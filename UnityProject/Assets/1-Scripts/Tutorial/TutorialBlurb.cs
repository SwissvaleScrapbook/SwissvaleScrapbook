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
    public bool enableLocationInteraction;
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
            TutorialManager.instance.tutorialLocation.SetActive(true);
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
        if(enableLocationInteraction)
        {
            TutorialManager.instance.tutorialLocation.GetComponent<LocationMarker>().isInteractable = true;
        }
    }

    private void SignalAdvance()
    {
        TutorialManager.instance.AdvanceTutorial();
    }

    private void SignalSkip()
    {
        TutorialManager.instance.SkipTutorial();
        
        //Print to console
        Debug.Log("Tutorial skipped");
    }
}
