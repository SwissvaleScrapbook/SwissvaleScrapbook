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

    [Header("Scene Objects")]
    public GameObject location;
    public GameObject allLocations;
    public GameObject player;
    public GameObject recenterButton;
    public GameObject mapButton;

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
        location.SetActive(false);
        allLocations.SetActive(false);
        player.SetActive(false);
        recenterButton.SetActive(false);
        mapButton.SetActive(false);
    }

    public void AdvanceTutorial()
    {
        // check if there are any tutorial steps left
        if(tutorialSteps.Count > 1)
        {
            // Remove the first step from the list and disable it
            GameObject currentStep = tutorialSteps[0];
            tutorialSteps.RemoveAt(0);
            currentStep.SetActive(false);            
        }
        else
        {
            // If there are no steps left, disable the entire tutorial
            allLocations.SetActive(true);
            tutorialObj.SetActive(false);
            location.SetActive(false);
            player.SetActive(true);
            recenterButton.SetActive(true);
            mapButton.SetActive(true);
            
            // Print out to console that tutorial is finished
            UnityEngine.Debug.Log("TUTORIAL FINISHED");
        }

        // Set next step to active if there are steps left
        if(tutorialSteps.Count > 0)
        {
            tutorialSteps[0].SetActive(true);
        }
    }

    public void SkipTutorial()
    {
        // Disable TutorialManager
        tutorialObj.SetActive(false);

        // Enable all scene objects
        location.SetActive(true);
        allLocations.SetActive(true);
        player.SetActive(true);
        recenterButton.SetActive(true);
        mapButton.SetActive(true);
    }

    public void ShowLocationPopup()
    {
        if(location != null)
        {
            PopupManager.instance.ShowLocationPopup(location);
        }
    }

    public void ShowStoryPopup()
    {
        PopupManager.instance.OpenStory(location.GetComponent<LocationData>().storyList[0]);

    }


}
