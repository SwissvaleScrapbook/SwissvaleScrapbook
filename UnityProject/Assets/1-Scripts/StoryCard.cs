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
        Debug.Log("HIIIIIIIIIIIIIIIIIIIIIII!");

        // if(TutorialManager.instance.storyClicked)
        // {
        //     UnityEngine.Debug.Log("Story card already clicked. Ignoring click.");
        //     return;
        // }

        // Debug.Log("IN STORY CARD -- advancing!");
        
        // PopupManager.instance.OpenStory(this);
        // TutorialManager.instance.AdvanceTutorial();
        // TutorialManager.instance.storyClicked = true;
        
    }
}