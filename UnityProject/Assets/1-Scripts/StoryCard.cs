using UnityEngine;

public class StoryCard : MonoBehaviour
{
    public string storyTitle;
    [TextArea] public string storyBody;

    private StoryStackManager manager;

    void Awake()
    {
        manager = FindObjectOfType<StoryStackManager>();
    }

    public void Init(StoryStackManager mgr)
    {
        manager = mgr;
    }

    public void OnCardClicked()
    {
        PopupManager.instance.OpenStory(this);
    }
}