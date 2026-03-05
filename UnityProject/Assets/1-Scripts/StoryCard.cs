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
        if (manager == null)
        {
            Debug.LogError("StoryCard: No StoryStackManager found in scene!");
            return;
        }
        manager.OpenStory(this);
    }
}