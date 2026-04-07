using System.Diagnostics;
using UnityEngine;

public class StoryCard : MonoBehaviour
{
    public string storyTitle;
    [TextArea] public string storyBody;

    public void OnCardClicked()
    {
        PopupManager.instance.OpenStory(this);
    }

    public void TutorialOnCardClicked()
    {
        // Check if tutorial manager exists (if not, the tutorial has ended or been completed)
        if(TutorialManager.instance == null)
        {
            UnityEngine.Debug.Log("TutorialManager instance not found. Proceeding with normal card click behavior.");
            return;
        }
        else
        {
            UnityEngine.Debug.Log("TutorialManager instance found. Checking tutorial state.");
        }

        if(TutorialManager.instance.storyClicked)
        {
            UnityEngine.Debug.Log("Story card already clicked. Ignoring click.");
            return;
        }

        UnityEngine.Debug.Log("IN STORY CARD -- advancing!");
        
        TutorialManager.instance.AdvanceTutorial();
        TutorialManager.instance.storyClicked = true;
        
    }
}