using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryStackManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject storyStackPanel;
    public GameObject storyDetailCanvas;

    [Header("Detail View")]
    public TextMeshProUGUI detailTitle;
    public TextMeshProUGUI detailBody;
    public TextMeshProUGUI counterText;

    [Header("Cards")]
    public List<StoryCard> cards;

    public StoryCard currentCard;


    void Start()
{
    if (storyDetailCanvas != null)
        storyDetailCanvas.SetActive(false);
    ArrangeStack();
}

    public void RefreshStack()
{
    cards = new List<StoryCard>(storyStackPanel.GetComponentsInChildren<StoryCard>());
    ArrangeStack();
}

   void ArrangeStack()
{
    if (cards == null || cards.Count == 0) return;
    
    cards.RemoveAll(card => card == null); // ADD THIS - removes null entries
    
    if (cards.Count == 0) return;

    for (int i = 0; i < cards.Count; i++)
    {
        RectTransform rt = cards[i].GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(0, -i * 12f);
        rt.localScale = Vector3.one * (1f - i * 0.03f);
        cards[i].transform.SetSiblingIndex(cards.Count - 1 - i);
    }
    UpdateCounter();
}

    public void OpenStory(StoryCard card)
    {
        Debug.Log("OpenStory called! Title: " + card.storyTitle); // TEST
        currentCard = card;
        detailTitle.text = card.storyTitle;
        detailBody.text = card.storyBody;
        storyDetailCanvas.SetActive(true);
        storyStackPanel.SetActive(false);
    }

    public void OnBackPressed()
    {
        storyDetailCanvas.SetActive(false);
        storyStackPanel.SetActive(true);
        StartCoroutine(SendToBackAnimation(currentCard));
    }

    IEnumerator SendToBackAnimation(StoryCard card)
    {
        RectTransform rt = card.GetComponent<RectTransform>();
        Vector2 startPos = rt.anchoredPosition;
        Vector2 offscreen = startPos + new Vector2(400f, 0);

        // Slide out to the right
        float t = 0f;
        float duration = 0.5f;
        while (t < duration)
        {
            t += Time.deltaTime;
            rt.anchoredPosition = Vector2.Lerp(startPos, offscreen, t / duration);
            yield return null;
        }

        // Move card to back of list
        cards.Remove(card);
        cards.Add(card);

        // Rearrange and update
        ArrangeStack();
    }

    public void SendCurrentCardToBack()
{
    if (currentCard == null) return;
    StartCoroutine(SendToBackAnimation(currentCard));
}

    void UpdateCounter()
{
    if (counterText != null && cards.Count > 0)
        counterText.text = $"1 / {cards.Count}";
}
}