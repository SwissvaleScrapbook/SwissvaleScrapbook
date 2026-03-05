using UnityEngine;
using TMPro;

public class DebugStory : MonoBehaviour
{
    public TextMeshProUGUI titleText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("TESTING: Setting title text now");
            titleText.text = "HELLO WORLD TEST";
            Debug.Log("TESTING: Text set to: " + titleText.text);
            Debug.Log("TESTING: TitleText active: " + titleText.gameObject.activeInHierarchy);
            Debug.Log("TESTING: TitleText color: " + titleText.color);
            Debug.Log("TESTING: TitleText position: " + titleText.rectTransform.position);
        }
    }
}
