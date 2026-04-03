using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

public class TutorialManager : MonoBehaviour
{
    // This is a singleton instance that can be referenced globally
    public static TutorialManager instance;
    public GameObject tutorialObj;

    // List of child objects that represent each step of the tutorial
    public List<GameObject> tutorialSteps;
    private int currentStepIndex = 0;

    [Header("Scene Objects")]
    public GameObject tutorialLocation;
    public GameObject allLocations;
    public GameObject player;
    public GameObject recenterButton;
    public GameObject mapButton;

    [Header("Flags")]
    public bool storyClicked = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Fill in tutorial steps with children of tutorialObj that have isEnabled == true
        foreach(Transform child in tutorialObj.transform)
        {
            if(child.gameObject.GetComponent<TutorialBlurb>() != null && child.gameObject.GetComponent<TutorialBlurb>().isEnabled)
            {
                tutorialSteps.Add(child.gameObject);
            }
        }
        
        // Set first step to active
        if(tutorialSteps.Count > 0)
        {
            tutorialSteps[0].SetActive(true);
        }

        // Disable all scene objects
        tutorialLocation.SetActive(false);
        allLocations.SetActive(false);
        player.SetActive(false);
        recenterButton.SetActive(false);
        mapButton.SetActive(false);
    }

    public void AdvanceTutorial()
    {
        if(currentStepIndex >= tutorialSteps.Count - 1)
        {
            UnityEngine.Debug.LogWarning("No more tutorial steps to advance to.");

            tutorialSteps[currentStepIndex].SetActive(false);


            // If there are no steps left, disable the entire tutorial
            allLocations.SetActive(true);
            tutorialObj.SetActive(false);
            tutorialLocation.SetActive(false);
            player.SetActive(true);
            recenterButton.SetActive(true);
            mapButton.SetActive(true);

            return;
        }
        else
        {
            UnityEngine.Debug.Log("ADVANCED TUTORIAL! Step: " + currentStepIndex);

            // Disable current step
            tutorialSteps[currentStepIndex].SetActive(false);

            // Increment step index
            currentStepIndex++;

            // Set next step to active
            tutorialSteps[currentStepIndex].SetActive(true);
            
        }
    }

    public void SkipTutorial()
    {
        // Disable TutorialManager
        tutorialObj.SetActive(false);
        tutorialLocation.SetActive(false);

        // Enable all scene objects
        allLocations.SetActive(true);
        player.SetActive(true);
        recenterButton.SetActive(true);
        mapButton.SetActive(true);
    }

    public void ShowLocationPopup()
    {
        if(tutorialLocation != null)
        {
            PopupManager.instance.ShowLocationPopup(tutorialLocation);
        }
    }
}
