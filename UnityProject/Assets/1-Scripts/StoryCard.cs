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
        if(TutorialManager.instance.storyClicked)
        {
            Debug.LogWarning("Story card already clicked. Ignoring click.");
            return;
        }
        
        PopupManager.instance.OpenStory(this);
        TutorialManager.instance.AdvanceTutorial();
        TutorialManager.instance.storyClicked = true;
    }
}